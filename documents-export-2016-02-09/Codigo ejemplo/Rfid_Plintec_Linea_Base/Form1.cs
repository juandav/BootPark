using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Reflection;
using ReaderB;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace RFid_Plintec
{
    public partial class Form1 : Form
    {
        private bool fAppClosed; 
        private byte fComAdr=0xff; 
        private int ferrorcode;
        private byte fBaud;
        private double fdminfre;
        private double fdmaxfre;
        private int fCmdRet=30; 
        private int fOpenComIndex; 
        private bool fIsInventoryScan;
        private bool fisinventoryscan_6B;
        private byte[] fOperEPC=new byte[100];
        private byte[] fPassWord=new byte[4];
        private byte[] fOperID_6B=new byte[10];
        private int CardNum1 = 0;
        ArrayList list = new ArrayList();
        private bool fTimer_6B_ReadWrite;
        private string fInventory_EPC_List; 
        private int frmcomportindex;
        private bool ComOpen=false;
        private bool breakflag = false;
        private bool SeriaATflag = false;
        private double x_z;
        private double y_f;
        public DeviceClass SelectedDevice;
        private static List<DeviceClass> DevList;
        private static SearchCallBack searchCallBack = new SearchCallBack(searchCB);


        private static void searchCB(IntPtr dev, IntPtr data)
        {
            uint ipAddr = 0;
            StringBuilder devname = new StringBuilder(100);
            StringBuilder macAdd = new StringBuilder(100);

            DevControl.tagErrorCode eCode = DevControl.DM_GetDeviceInfo(dev, ref ipAddr, macAdd, devname);
            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                DeviceClass device = new DeviceClass(dev, ipAddr, macAdd.ToString(), devname.ToString());
                DevList.Add(device);
            }
            else
            {
                string errMsg = ErrorHandling.GetErrorMsg(eCode);
                Log.WriteError(errMsg);
            }
        }
        private static IPAddress getIPAddress(uint interIP)
        {
            return new IPAddress((uint)IPAddress.HostToNetworkOrder((int)interIP));
        }
        public Form1()
        {
            InitializeComponent();
            DevList = new List<DeviceClass>();

            DevControl.tagErrorCode eCode = DevControl.DM_Init(searchCallBack, IntPtr.Zero);
            if (eCode != DevControl.tagErrorCode.DM_ERR_OK)
            {
                string errMsg = ErrorHandling.HandleError(eCode);
                throw new Exception(errMsg);
            }
        }
        private void RefreshStatus()
        {
            if (!(ComboBox_AlreadyOpenCOM.Items.Count != 0))
                StatusBar1.Panels[1].Text = "COM Closed";
            else
                StatusBar1.Panels[1].Text = " COM" + Convert.ToString(frmcomportindex);
              StatusBar1.Panels[0].Text ="";
              StatusBar1.Panels[2].Text ="";
        }
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "successfully";
                case 0x01:
                    return "Return before Inventory finished";
                case 0x02:
                    return "the Inventory-scan-time overflow";
                case 0x03:
                    return "More Data";
                case 0x04:
                    return "Reader module MCU is Full";
                case 0x05:
                    return "Access Password Error";
                case 0x09:
                    return "Destroy Password Error";
                case 0x0a:
                    return "Destroy Password Error Cannot be Zero";
                case 0x0b:
                    return "Tag Not Support the command";
                case 0x0c:
                    return "Use the commmand,Access Password Cannot be Zero";
                case 0x0d:
                    return "Tag is protected,cannot set it again";
                case 0x0e:
                    return "Tag is unprotected,no need to reset it";
                case 0x10:
                    return "There is some locked bytes,write fail";
                case 0x11:
                    return "can not lock it";
                case 0x12:
                    return "is locked,cannot lock it again";
                case 0x13:
                    return "Parameter Save Fail,Can Use Before Power";
                case 0x14:
                    return "Cannot adjust";
                case 0x15:
                    return "Return before Inventory finished";
                case 0x16:
                    return "Inventory-Scan-Time overflow";
                case 0x17:
                    return "More Data";
                case 0x18:
                    return "Reader module MCU is full";
                case 0x19:
                    return "'Not Support Command Or AccessPassword Cannot be Zero";
                case 0xFA:
                    return "Get Tag,Poor Communication,Inoperable";
                case 0xFB:
                    return "No Tag Operable";
                case 0xFC:
                    return "Tag Return ErrorCode";
                case 0xFD:
                    return "Command length wrong";
                case 0xFE:
                    return "Illegal command";
                case 0xFF:
                    return "Parameter Error";
                case 0x30:
                    return "Communication error";
                case 0x31:
                    return "CRC checksummat error";
                case 0x32:
                    return "Return data length error";
                case 0x33:
                    return "Communication busy";
                case 0x34:
                    return "Busy,command is being executed";
                case 0x35:
                    return "ComPort Opened";
                case 0x36:
                    return "ComPort Closed";
                case 0x37:
                    return "Invalid Handle";
                case 0x38:
                    return "Invalid Port";
                case 0xEE:
                    return "Return Command Error";
                default:
                    return "";
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Other error";
                case 0x03:
                    return "Memory out or pc not support";
                case 0x04:
                    return "Memory Locked and unwritable";
                case 0x0b:
                    return "No Power,memory write operation cannot be executed";
                case 0x0f:
                    return "Not Special Error,tag not support special errorcode";
                default:
                    return "";
            }
        }
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }
        private void AddCmdLog(string CMD, string cmdStr, int cmdRet)
        {
            try
            {
                StatusBar1.Panels[0].Text = "";
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " " +
                                            cmdStr + ": " +
                                            GetReturnCodeDesc(cmdRet);
            }
            finally
            {
                ;
            }
        }
        private void AddCmdLog(string CMD, string cmdStr, int cmdRet,int errocode)
        {
            try
            {
                StatusBar1.Panels[0].Text = "";
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " " +
                                            cmdStr + ": " +
                                            GetReturnCodeDesc(cmdRet)+" "+"0x"+Convert.ToString(errocode,16).PadLeft(2,'0');
            }
            finally
            {
                ;
            }
        }
        private void ClearLastInfo()
        { 
            ComboBox_AlreadyOpenCOM.Refresh();
              RefreshStatus();
              Edit_Type.Text = "";
              Edit_Version.Text = "";
              ISO180006B.Checked=false;
              EPCC1G2.Checked=false;
              Edit_ComAdr.Text = "";
              Edit_powerdBm.Text = "";
              Edit_scantime.Text = "";
              Edit_dminfre.Text = "";
              Edit_dmaxfre.Text = "";
        }
        private void InitComList()
        {
            int i = 0;
            ComboBox_COM.Items.Clear();
              ComboBox_COM.Items.Add(" AUTO");
              for (i = 1; i < 13;i++ )
                  ComboBox_COM.Items.Add(" COM" + Convert.ToString(i));
              ComboBox_COM.SelectedIndex = 0;
              RefreshStatus();
        }
        private void InitReaderList()
        {
            int i=0;
            ComboBox_PowerDbm.SelectedIndex = 30;
            ComboBox_baud.SelectedIndex =3;
             for (i=0 ;i< 63;i++)
             {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6+i*0.4)+" MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
             }
              ComboBox_dmaxfre.SelectedIndex = 62;
              ComboBox_dminfre.SelectedIndex = 0;
              for (i=0x03;i<=0xff;i++)
                  ComboBox_scantime.Items.Add(Convert.ToString(i) + "*100ms");
              ComboBox_scantime.SelectedIndex = 7;
              i=10;
              while (i<=300)
              {
                  ComboBox_IntervalTime.Items.Add(Convert.ToString(i) + "ms");
              i=i+10;
              }
              ComboBox_IntervalTime.SelectedIndex = 0;
              for (i=0;i<7;i++)
                  ComboBox_BlockNum.Items.Add(Convert.ToString(i * 2) + " and " + Convert.ToString(i * 2 + 1));
              ComboBox_BlockNum.SelectedIndex = 0;
              i=40;

              for (i=0;i<256;i++)
              ComboBox_RelayTime.Items.Add(Convert.ToString(i)) ;
              ComboBox_RelayTime.SelectedIndex=0;

              for (i = 1; i < 256; i++)
                  comboBox6.Items.Add(Convert.ToString(i));
              comboBox6.SelectedIndex = 29;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
              fOpenComIndex = -1;
              fComAdr = 0;
              ferrorcode= -1;
              fBaud =5;
              InitComList();
              InitReaderList();
              NoAlarm_G2.Checked  =true;
             
              P_EPC.Checked=true;
              C_EPC.Checked=true;
              DestroyCode.Checked=true;
              NoProect.Checked=true;
              NoProect2.Checked=true;
              fAppClosed = false;
              fIsInventoryScan = false;
              fisinventoryscan_6B = false;
              fTimer_6B_ReadWrite=false ;
              Label_Alarm.Visible=false;
              Timer_Test_.Enabled = false;
              Timer_G2_Read.Enabled = false;
              Timer_G2_Alarm.Enabled = false;
              timer1.Enabled = false;

              Button3.Enabled = false;
              Button5.Enabled = false;
              Button1.Enabled = false;
              button2.Enabled = false;
              Button_DestroyCard.Enabled = false;
              Button_WriteEPC_G2.Enabled = false;
              Button_SetReadProtect_G2.Enabled = false;
              Button_SetMultiReadProtect_G2.Enabled = false;
              Button_RemoveReadProtect_G2.Enabled = false;
              Button_CheckReadProtected_G2.Enabled = false;
              Button_SetEASAlarm_G2.Enabled = false;
              button4.Enabled = false;
              Button_LockUserBlock_G2.Enabled = false;
              SpeedButton_Read_G2.Enabled = false;
              Button_DataWrite.Enabled = false;
              BlockWrite.Enabled = false;
              Button_BlockErase.Enabled = false;
              Button_SetProtectState.Enabled = false;

              DestroyCode.Enabled = false;
              AccessCode.Enabled = false;
              NoProect.Enabled = false;
              Proect.Enabled = false;
              Always.Enabled = false;
              AlwaysNot.Enabled = false;
              NoProect2.Enabled = false;
              Proect2.Enabled = false;
              Always2.Enabled = false;
              AlwaysNot2.Enabled = false;
              P_Reserve.Enabled = false;
              P_EPC.Enabled = false;
              P_TID.Enabled = false;
              P_User.Enabled = false;

               radioButton_band1.Checked = true;
       
             ComboBox_baud2.SelectedIndex = 3;

             GetClock.Checked = true;
             Radio_beepEn.Checked = true;
             R_EPC.Checked = true;
             Button_SetGPIO.Enabled = false;
             Button_GetGPIO.Enabled = false;
             Button_Ant.Enabled = false;
             Button_RelayTime.Enabled = false;
             ClockCMD.Enabled = false;
             Button_OutputRep.Enabled = false;
             Button_Beep.Enabled = false;
             radioButton1.Checked = true;

             button26.Enabled = false;
             button27.Enabled = false;
             button28.Enabled = false;
             button29.Enabled = false;
             button31.Enabled = false;
             button32.Enabled = false;
             button33.Enabled = false;
             button34.Enabled = false;
             button35.Enabled = false;
             button36.Enabled = false;
             button25.Enabled = false;
             button37.Enabled = false;

             protocolCB.SelectedIndex = 0;
        }

        private void OpenPort_Click(object sender, EventArgs e)
        {
            int port=0;
            int openresult,i;
            openresult = 30;
            string temp;
            Cursor = Cursors.WaitCursor;
              if  (Edit_CmdComAddr.Text=="")
              Edit_CmdComAddr.Text="FF";
              fComAdr = Convert.ToByte(Edit_CmdComAddr.Text,16); // $FF;
              try
              {
                  if (ComboBox_COM.SelectedIndex == 0)//Auto
                  {
                      fBaud=Convert.ToByte(ComboBox_baud2.SelectedIndex);
                      if (fBaud>2)
                          fBaud =Convert.ToByte(fBaud + 2);
                    openresult =StaticClassReaderB.AutoOpenComPort(ref port,ref fComAdr,fBaud,ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    if (openresult == 0 )
                    {
                        ComOpen = true;
                        if (fBaud > 3)
                        {
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                        }
                        else
                        {
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                        }
                        Button3_Click(sender, e); 
                      if ((fCmdRet==0x35) ||(fCmdRet==0x30))
                        {
                            MessageBox.Show("Communication error", "Information");
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                            ComOpen = false;
                        }
                    }          
                  }
                  else
                  {
                    temp = ComboBox_COM.SelectedItem.ToString();
                    temp = temp.Trim();
                    port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                    for (i = 6; i >= 0; i--)
                    {
                        fBaud = Convert.ToByte(i);
                        if (fBaud == 3)
                            continue;
                        openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                        fOpenComIndex = frmcomportindex;
                        if (openresult == 0x35)
                        {
                            MessageBox.Show("Com opened", "Information");
                            return;
                        }
                        if (openresult == 0)
                        {
                            ComOpen = true;
                            Button3_Click(sender, e); 
                            if (fBaud > 3)
                            {
                                ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                            }
                            else
                            {
                                ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                            }
                            if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                            {
                                ComOpen = false;
                                MessageBox.Show("Serial Communication Error", "Information");
                                StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                                return;
                            }
                            RefreshStatus();
                            break;
                        }
                    }
                  }
              }
              finally
              {
                  Cursor = Cursors.Default;
              }

              if ((fOpenComIndex != -1) &&(openresult != 0X35)  &&(openresult != 0X30))
              {
                ComboBox_AlreadyOpenCOM.Items.Add("COM"+Convert.ToString(fOpenComIndex)) ;
                ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
                Button3.Enabled = true ;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                Button_SetGPIO.Enabled = true;
                Button_GetGPIO.Enabled = true;
                Button_Ant.Enabled = true;
                Button_RelayTime.Enabled = true;
                ClockCMD.Enabled = true;
                Button_OutputRep.Enabled = true;
                Button_Beep.Enabled = true;

                button26.Enabled = true;
                button27.Enabled = true;
                button28.Enabled = true;
                button29.Enabled = true;
                button31.Enabled = true;
                button32.Enabled = true;
                button33.Enabled = true;
                button34.Enabled = true;
                button35.Enabled = true;
                button36.Enabled = true;
                button25.Enabled = true;
                button37.Enabled = true;
                ComOpen = true;
              }
              if ((fOpenComIndex == -1) &&(openresult == 0x30))
                  MessageBox.Show("Serial Communication Error", "Information");

            if ((ComboBox_AlreadyOpenCOM.Items.Count != 0)&&(fOpenComIndex != -1) && (openresult != 0x35) && (openresult != 0x30)&&(fCmdRet==0)) 
              {
                fComAdr = Convert.ToByte(Edit_ComAdr.Text,16);
                temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
              }
              RefreshStatus();
          }

        private void ClosePort_Click(object sender, EventArgs e)
        {
            int port;
            //string SelectCom ;
            string temp;
            ClearLastInfo();
              try
              {
                if (ComboBox_AlreadyOpenCOM.SelectedIndex  < 0 )
                {
                    MessageBox.Show("Please Choose COM Port to close", "Information");
                }
                else
                {
                    temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                    port = Convert.ToInt32(temp.Substring(3,temp.Length - 3));
                    fCmdRet = StaticClassReaderB.CloseSpecComPort(port);
                    if (fCmdRet == 0)
                    {
                        ComboBox_AlreadyOpenCOM.Items.RemoveAt(0);
                        if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
                        {
                            temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                            port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                            StaticClassReaderB.CloseSpecComPort(port);
                            fComAdr = 0xFF;
                            StaticClassReaderB.OpenComPort(port,ref fComAdr, fBaud,ref frmcomportindex);
                            fOpenComIndex = frmcomportindex;
                            RefreshStatus();
                            Button3_Click(sender,e); 
                        }
                    }               
                    else
                        MessageBox.Show("Serial Communication Error", "Information");
                  }
              }
              finally
              {
                  ;
              }
              if(ComboBox_AlreadyOpenCOM.Items.Count != 0)
                  ComboBox_AlreadyOpenCOM.SelectedIndex = 0;
              else
              {
                  fOpenComIndex = -1;
                  ComboBox_AlreadyOpenCOM.Items.Clear();
                  ComboBox_AlreadyOpenCOM.Refresh();
                  RefreshStatus();
                  Button3.Enabled = false;
                  Button5.Enabled = false;
                  Button1.Enabled = false;
                  button2.Enabled = false;
                  Button_DestroyCard.Enabled = false;
                  Button_WriteEPC_G2.Enabled = false;
                  Button_SetReadProtect_G2.Enabled = false;
                  Button_SetMultiReadProtect_G2.Enabled = false;
                  Button_RemoveReadProtect_G2.Enabled = false;
                  Button_CheckReadProtected_G2.Enabled = false;
                  Button_SetEASAlarm_G2.Enabled = false;
                  button4.Enabled = false;
                  Button_LockUserBlock_G2.Enabled = false;
                  SpeedButton_Read_G2.Enabled = false;
                  Button_DataWrite.Enabled = false;
                  BlockWrite.Enabled = false;
                  Button_BlockErase.Enabled = false;
                  Button_SetProtectState.Enabled = false;

                  DestroyCode.Enabled = false;
                  AccessCode.Enabled = false;
                  NoProect.Enabled = false;
                  Proect.Enabled = false;
                  Always.Enabled = false;
                  AlwaysNot.Enabled = false;
                  NoProect2.Enabled = false;
                  Proect2.Enabled = false;
                  Always2.Enabled = false;
                  AlwaysNot2.Enabled = false;

                  P_Reserve.Enabled = false;
                  P_EPC.Enabled = false;
                  P_TID.Enabled = false;
                  P_User.Enabled = false;
                  Alarm_G2.Enabled = false;
                  NoAlarm_G2.Enabled = false;

                  DestroyCode.Enabled = false;
                  AccessCode.Enabled = false;
                  NoProect.Enabled = false;
                  Proect.Enabled = false;
                  Always.Enabled = false;
                  AlwaysNot.Enabled = false;
                  NoProect2.Enabled = false;
                  Proect2.Enabled = false;
                  Always2.Enabled = false;
                  AlwaysNot2.Enabled = false;
                  P_Reserve.Enabled = false;
                  P_EPC.Enabled = false;
                  P_TID.Enabled = false;
                  P_User.Enabled = false;
                  Button_WriteEPC_G2.Enabled = false;
                  Button_SetMultiReadProtect_G2.Enabled = false;
                  Button_RemoveReadProtect_G2.Enabled = false;
                  Button_CheckReadProtected_G2.Enabled = false;
                  button4.Enabled = false;

                  Button_DestroyCard.Enabled = false;
                  Button_SetReadProtect_G2.Enabled = false;
                  Button_SetEASAlarm_G2.Enabled = false;
                  Alarm_G2.Enabled = false;
                  NoAlarm_G2.Enabled = false;
                  Button_LockUserBlock_G2.Enabled = false;
                  SpeedButton_Read_G2.Enabled = false;
                  Button_DataWrite.Enabled = false;
                  BlockWrite.Enabled = false;
                  Button_BlockErase.Enabled = false;
                  Button_SetProtectState.Enabled = false;
                  ListView1_EPC.Items.Clear();
                  ComboBox_EPC1.Items.Clear();
                  ComboBox_EPC2.Items.Clear();
                  ComboBox_EPC3.Items.Clear();
                  ComboBox_EPC4.Items.Clear();
                  ComboBox_EPC5.Items.Clear();
                  ComboBox_EPC6.Items.Clear();
                  button2.Text = "Query Tag";
                 
                  ComOpen = false;

                  timer1.Enabled = false;
                  Button_SetGPIO.Enabled = false;
                  Button_GetGPIO.Enabled = false;
                  Button_Ant.Enabled = false;
                  Button_RelayTime.Enabled = false;
                  ClockCMD.Enabled = false;
                  Button_OutputRep.Enabled = false;
                  Button_Beep.Enabled = false;

                  button26.Enabled = false;
                  button27.Enabled = false;
                  button28.Enabled = false;
                  button29.Enabled = false;
                  button31.Enabled = false;
                  button32.Enabled = false;
                  button33.Enabled = false;
                  button34.Enabled = false;
                  button35.Enabled = false;
                  button36.Enabled = false;
                  button25.Enabled = false;
                  button37.Enabled = false;
              }
         }
        private void Button3_Click(object sender, EventArgs e)
        {
              byte TrType=0;
              byte[] VersionInfo=new byte[2];
              byte ReaderType=0;
              byte ScanTime=0;
              byte dmaxfre=0;
              byte dminfre = 0;
              byte powerdBm=0;
              byte FreBand = 0;
              byte Ant=0;
			  byte BeepEn=0;
              byte OutputRep = 0;
              Edit_Version.Text = "";
              Edit_ComAdr.Text = "";
              Edit_scantime.Text = "";
              Edit_Type.Text = "";
              ISO180006B.Checked=false;
              EPCC1G2.Checked=false;
              Edit_powerdBm.Text = "";
              Edit_dminfre.Text = "";
              Edit_dmaxfre.Text = "";
              
              fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, ref TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime,ref Ant,ref BeepEn,ref OutputRep, frmcomportindex);
              if (fCmdRet == 0)
              {
                  Edit_Version.Text = Convert.ToString(VersionInfo[0], 10).PadLeft(2, '0') + "." + Convert.ToString(VersionInfo[1], 10).PadLeft(2, '0');
                  ComboBox_PowerDbm.SelectedIndex = Convert.ToInt32(powerdBm);   
                  Edit_ComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                  Edit_NewComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                  Edit_scantime.Text = Convert.ToString(ScanTime, 10).PadLeft(2, '0') + "*100ms";
                  ComboBox_scantime.SelectedIndex = ScanTime - 3;
                  Edit_powerdBm.Text = Convert.ToString(powerdBm, 10).PadLeft(2, '0');

                  FreBand= Convert.ToByte(((dmaxfre & 0xc0)>> 4)|(dminfre >> 6)) ;
                  switch (FreBand)
                  {
                      case 0:
                          {
                              radioButton_band1.Checked = true;
                              fdminfre = 902.6 + (dminfre & 0x3F) * 0.4;
                              fdmaxfre = 902.6 + (dmaxfre & 0x3F) * 0.4;
                          }
                          break;
                      case 1: 
                          {
                              radioButton_band2.Checked = true;
                              fdminfre = 920.125 + (dminfre & 0x3F) * 0.25;
                              fdmaxfre = 920.125 + (dmaxfre & 0x3F) * 0.25;
                          }
                          break;
                      case 2:
                          {
                              radioButton_band3.Checked = true;
                              fdminfre = 902.75 + (dminfre & 0x3F) * 0.5;
                              fdmaxfre = 902.75 + (dmaxfre & 0x3F) * 0.5;
                          }
                          break;
                      case 3:
                          {
                              radioButton_band4.Checked = true;
                              fdminfre = 917.1 + (dminfre & 0x3F) * 0.2;
                              fdmaxfre = 917.1 + (dmaxfre & 0x3F) * 0.2;
                          }
                          break;
                      case 4:
                          {
                              radioButton_band5.Checked = true;
                              fdminfre = 865.1 + (dminfre & 0x3F) * 0.2;
                              fdmaxfre = 865.1 + (dmaxfre & 0x3F) * 0.2;
                          }
                          break;
                  }
                  Edit_dminfre.Text = Convert.ToString(fdminfre) + "MHz";
                  Edit_dmaxfre.Text = Convert.ToString(fdmaxfre) + "MHz";
                  if (fdmaxfre != fdminfre)
                      CheckBox_SameFre.Checked = false;
                  ComboBox_dminfre.SelectedIndex = dminfre & 0x3F;
                  ComboBox_dmaxfre.SelectedIndex = dmaxfre & 0x3F;
                  if (ReaderType == 0x0A)
                      Edit_Type.Text = "UHFReader28";
                  if ((TrType & 0x02) == 0x02) 
                  {
                      ISO180006B.Checked = true;
                      EPCC1G2.Checked = true;
                  }
                  else
                  {
                      ISO180006B.Checked = false;
                      EPCC1G2.Checked = false;
                  }
                   switch (BeepEn)
                   {
                       case 1: 
                           Radio_beepEn.Checked=true;
                           break;
                       case 0: 
                           Radio_beepDis.Checked=true;
                           break;
                   }
                  if((Ant & 0x01)==1)
                   checkBox10.Checked=true;
                  else
                   checkBox10.Checked=false;

                 if((Ant & 0x02)==2)
                   checkBox11.Checked=true;
                  else
                   checkBox11.Checked=false;

                 if((Ant & 0x04)==4)
                   checkBox12.Checked=true;
                  else
                   checkBox12.Checked=false;

                 if((Ant & 0x08)==8)
                   checkBox13.Checked=true;
                  else
                   checkBox13.Checked=false;

                  if((OutputRep & 0x01)==1)
                   checkBox17.Checked=true;
                  else
                   checkBox17.Checked=false;

                  if((OutputRep & 0x02)==2)
                   checkBox16.Checked=true;
                  else
                   checkBox16.Checked=false;

               if ((OutputRep & 0x04) == 4)
                   checkBox15.Checked = true;
               else
                   checkBox15.Checked = false;

               if ((OutputRep & 0x08) == 8)
                   checkBox14.Checked = true;
               else
                   checkBox14.Checked = false;
              }
              AddCmdLog("GetReaderInformation", "GetReaderInformation", fCmdRet);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
              byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime, band=0;
              string returninfo="";
              string returninfoDlg="";
              string setinfo;
              if (radioButton_band1.Checked)
                  band = 0;
              if (radioButton_band2.Checked)
                  band = 1;
              if (radioButton_band3.Checked)
                  band = 2;
              if (radioButton_band4.Checked)
                  band = 3;
              if (radioButton_band5.Checked)
                  band = 4;
              if (Edit_NewComAdr.Text == "")
                  return;
              progressBar1.Visible = true;
              progressBar1.Minimum = 0;
              dminfre = Convert.ToByte(((band & 3) << 6) | (ComboBox_dminfre.SelectedIndex & 0x3F));
              dmaxfre = Convert.ToByte(((band & 0x0c) << 4) | (ComboBox_dmaxfre.SelectedIndex & 0x3F));
                  aNewComAdr = Convert.ToByte(Edit_NewComAdr.Text);
                  powerDbm = Convert.ToByte(ComboBox_PowerDbm.SelectedIndex);
                  fBaud = Convert.ToByte(ComboBox_baud.SelectedIndex);
                  if (fBaud > 2)
                      fBaud = Convert.ToByte(fBaud + 2);
                  scantime = Convert.ToByte(ComboBox_scantime.SelectedIndex + 3);
                  setinfo = "Write";
              progressBar1.Value =10;     
              fCmdRet = StaticClassReaderB.SetAddress(ref fComAdr,aNewComAdr,frmcomportindex);
              if (fCmdRet==0x13)
              fComAdr = aNewComAdr;
              if (fCmdRet == 0)
              {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "Address Successfully";
              }
              else if (fCmdRet==0xEE )
                  returninfo = returninfo + setinfo + "Address Response Command Error";
              else
              {
                  returninfo = returninfo + setinfo + "Address Fail";
                  returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }
              progressBar1.Value =25; 
              fCmdRet = StaticClassReaderB.SetRfPower(ref fComAdr,powerDbm,frmcomportindex);
              if (fCmdRet == 0)
                  returninfo = returninfo + ",Power Success";
              else if (fCmdRet==0xEE )
                  returninfo = returninfo + ",Power Response Command Error";
              else
              {
                  returninfo = returninfo + ",Power Fail";
                  returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response =0x"
                       +Convert.ToString(fCmdRet)+"("+GetReturnCodeDesc(fCmdRet)+")";
              }
              
              progressBar1.Value =40; 
              fCmdRet = StaticClassReaderB.SetRegion(ref fComAdr,dmaxfre,dminfre,frmcomportindex);
              if (fCmdRet == 0 )
                  returninfo = returninfo + ",Region Success";
              else if (fCmdRet==0xEE)
                  returninfo = returninfo + ",Region Response Command Error";
              else
              {
                  returninfo = returninfo + ",Region Fail";
                  returninfoDlg = returninfoDlg + " " + setinfo + "Region Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

              progressBar1.Value =55; 
              fCmdRet = StaticClassReaderB.SetBaudRate(ref fComAdr,fBaud,frmcomportindex);
              if (fCmdRet == 0)
                  returninfo = returninfo + ",BaudRate Success";
              else if (fCmdRet==0xEE)
                  returninfo = returninfo + ",BaudRate Response Command Error";
              else
              {
                  returninfo = returninfo + ",BaudRate Fail";
                  returninfoDlg = returninfoDlg + " " + setinfo + "BaudRate Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

              progressBar1.Value =70; 
              fCmdRet = StaticClassReaderB.SetInventoryScanTime(ref fComAdr,scantime,frmcomportindex);
              if (fCmdRet == 0 )
                  returninfo = returninfo + ",InventoryScanTime Success";
              else if (fCmdRet==0xEE)
                  returninfo = returninfo + ",InventoryScanTime Response Command Error";
              else
              {
                  returninfo = returninfo + ",InventoryScanTime Fail";
                  returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
             }
              progressBar1.Value =100; 
              Button3_Click(sender,e);
              progressBar1.Visible=false;
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
              if  (returninfoDlg!="")
                  MessageBox.Show(returninfoDlg, "Information");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime;
            string returninfo = "";
            string returninfoDlg = "";
            string setinfo;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            dminfre = 0;
            dmaxfre = 62;
            aNewComAdr =0x00;
            powerDbm = 30;
            fBaud=5;
            scantime=10;
            setinfo = " Restore ";
            ComboBox_baud.SelectedIndex = 3;
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.SetAddress(ref fComAdr, aNewComAdr, frmcomportindex);
            if (fCmdRet == 0x13)
                fComAdr = aNewComAdr;
            if (fCmdRet == 0)
            {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "Address Successfully";
            }
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + setinfo + "Address Response Command Error";
            else
            {
                returninfo = returninfo + setinfo + "Address Fail";
                returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 25;
            fCmdRet = StaticClassReaderB.SetRfPower(ref fComAdr, powerDbm, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Power Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Power Response Command Error";
            else
            {
                returninfo = returninfo + ",Power Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 40;
            fCmdRet = StaticClassReaderB.SetRegion(ref fComAdr, dmaxfre, dminfre, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Region Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Region Response Command Error";
            else
            {
                returninfo = returninfo + ",Region Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Region Fail Command Response=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 55;
            fCmdRet = StaticClassReaderB.SetBaudRate(ref fComAdr, fBaud, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",BaudRate Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",BaudRate Response Command Error";
            else
            {
                returninfo = returninfo + ",BaudRate Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "BaudRate Fail Command Response=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 70;
            fCmdRet = StaticClassReaderB.SetInventoryScanTime(ref fComAdr, scantime, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",InventoryScanTime Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",InventoryScanTime Response Command Error";
            else
            {
                returninfo = returninfo + ",InventoryScanTime Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 100;
            Button3_Click(sender, e);
            progressBar1.Visible = false;
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
            if (returninfoDlg != "")
                MessageBox.Show(returninfoDlg, "Information");
            
        }

        private void CheckBox_SameFre_CheckedChanged(object sender, EventArgs e)
        {
             if (CheckBox_SameFre.Checked)
              ComboBox_dmaxfre.SelectedIndex = ComboBox_dminfre.SelectedIndex;
        }


        private void ComboBox_dfreSelect(object sender, EventArgs e)
        {
             if (CheckBox_SameFre.Checked )
             {
                 ComboBox_dminfre.SelectedIndex =ComboBox_dmaxfre.SelectedIndex;
             }
              else if  (ComboBox_dminfre.SelectedIndex> ComboBox_dmaxfre.SelectedIndex )
             {
                 ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
                 MessageBox.Show("Min.Frequency is equal or lesser than Max.Frequency", "Error Information");
             }
        }
        public void ChangeSubItem(ListViewItem ListItem, int subItemIndex, string ItemText)
        {
            if (subItemIndex == 1)
            {
                if (ItemText=="")
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    if (ListItem.SubItems[subItemIndex + 3].Text == "")
                    {
                        ListItem.SubItems[subItemIndex + 3].Text = "1";
                    }
                    else
                    {
                        ListItem.SubItems[subItemIndex + 3].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex + 3].Text) + 1);
                    }
                }
                else 
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    ListItem.SubItems[subItemIndex+3].Text = "1";
                }
                else
                {
                    ListItem.SubItems[subItemIndex + 3].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex + 3].Text) + 1);
                    if( (Convert.ToUInt32(ListItem.SubItems[subItemIndex + 3].Text)>9999))
                        ListItem.SubItems[subItemIndex + 3].Text="1";
                }

            }
            if (subItemIndex == 2)
            {
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                }
            }
            if (subItemIndex == 3)
            {
                if (ListItem.SubItems[subItemIndex].Text =="")
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                }
                else
                {
                    ListItem.SubItems[subItemIndex].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex].Text, 2) | Convert.ToInt32(ItemText, 2), 2);
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckBox_TID.Checked)
            {
                if ((textBox12.Text.Length) != 2 || ((textBox13.Text.Length) != 2))
                {
                    StatusBar1.Panels[0].Text = "TID Parameter Error！";
                    return;
                }
            }
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error!", "Error information");
                    return;
                }
            }
           
            Timer_Test_.Enabled = !Timer_Test_.Enabled;
            if (!Timer_Test_.Enabled)
            {
                textBox12.Enabled = true;
                textBox13.Enabled = true;
                CheckBox_TID.Enabled = true;
                if (ListView1_EPC.Items.Count != 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = true;
                    Proect2.Enabled = true;
                    Always2.Enabled = true;
                    AlwaysNot2.Enabled = true;
                    P_Reserve.Enabled = true;
                    P_EPC.Enabled = true;
                    P_TID.Enabled = true;
                    P_User.Enabled = true;
                    Button_DestroyCard.Enabled = true;
                    Button_SetReadProtect_G2.Enabled = true;
                    Button_SetEASAlarm_G2.Enabled = true;
                    Alarm_G2.Enabled = true;
                    NoAlarm_G2.Enabled = true;
                    Button_LockUserBlock_G2.Enabled = true;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button4.Enabled = true;
                    SpeedButton_Read_G2.Enabled = true;
                    Button_SetProtectState.Enabled = true;
                    Button_DataWrite.Enabled = true;
                    BlockWrite.Enabled = true;
                    Button_BlockErase.Enabled = true;
                }
                if (ListView1_EPC.Items.Count == 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = false ;
                    Proect2.Enabled = false ;
                    Always2.Enabled = false ;
                    AlwaysNot2.Enabled = false ;
                    P_Reserve.Enabled = false;
                    P_EPC.Enabled = false;
                    P_TID.Enabled = false;
                    P_User.Enabled = false;
                    Button_DestroyCard.Enabled = false;
                    Button_SetReadProtect_G2.Enabled = false;
                    Button_SetEASAlarm_G2.Enabled = false;
                    Alarm_G2.Enabled = false;
                    NoAlarm_G2.Enabled = false;
                    Button_LockUserBlock_G2.Enabled = false;
                    SpeedButton_Read_G2.Enabled = false;
                    Button_DataWrite.Enabled = false;
                    BlockWrite.Enabled = false;
                    Button_BlockErase.Enabled = false;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true; 
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button4.Enabled = true;
                    Button_SetProtectState.Enabled = false;

                }
                AddCmdLog("Inventory", "Exit inventory", 0);
                button2.Text = "Query tag";
            }
            else
            {
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                CheckBox_TID.Enabled = false;
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_WriteEPC_G2.Enabled = false ;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                button4.Enabled = false;

                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                ListView1_EPC.Items.Clear();
                ComboBox_EPC1.Items.Clear();
                ComboBox_EPC2.Items.Clear();
                ComboBox_EPC3.Items.Clear();
                ComboBox_EPC4.Items.Clear();
                ComboBox_EPC5.Items.Clear();
                ComboBox_EPC6.Items.Clear();
                button2.Text = "Stop";
                textBox18.Text = "";
                textBox17.Text = "";
            }
        }
        private void Inventory()
        {
              int i;
              int CardNum=0;
              int Totallen = 0;
              int EPClen,m;
              byte[] EPC=new byte[5000];
              int CardIndex;
              string temps;
              string s, sEPC;
              bool isonlistview;
              byte MaskMem=0;
              byte[] MaskAdr=new byte[2];
              byte MaskLen=0;
              byte[] MaskData=new byte[100];
              byte MaskFlag=0;
              byte Ant=0;
              string antstr="";
              string lastepc = "";
              byte AdrTID = 0;
              byte LenTID = 0;
              byte TIDFlag = 0;
              if (CheckBox_TID.Checked)
              {
                  AdrTID = Convert.ToByte(textBox12.Text, 16);
                  LenTID = Convert.ToByte(textBox13.Text, 16);
                  TIDFlag = 1;
              }
              else
              {
                  AdrTID = 0;
                  LenTID = 0;
                  TIDFlag = 0;
              }
              fIsInventoryScan = true;
              if(checkBox1.Checked)
                  MaskFlag=1;
              else
                  MaskFlag=0;
              if (R_EPC.Checked) MaskMem = 1;
              if (R_TID.Checked) MaskMem = 2;
              if (R_User.Checked) MaskMem = 3;
              MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
              MaskLen = Convert.ToByte(maskLen_textBox.Text);
              MaskData = HexStringToByteArray(maskData_textBox.Text);
              ListViewItem aListItem = new ListViewItem();
              fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, EPC, ref Ant, ref Totallen, ref CardNum, frmcomportindex);      
             if ( (fCmdRet == 1)| (fCmdRet == 2)| (fCmdRet == 3)| (fCmdRet == 4)|(fCmdRet == 0xFB) )
             {
                 byte[] daw = new byte[Totallen];
                 Array.Copy(EPC, daw, Totallen);               
                 temps = ByteArrayToHexString(daw);
                 fInventory_EPC_List = temps;          
                 m=0;
                 if (CardNum==0)
                 {
                     fIsInventoryScan = false;
                     return;
                 }
                 antstr = Convert.ToString(Ant, 2);
                 for (CardIndex = 0;CardIndex<CardNum;CardIndex++)
                 {
                     EPClen = daw[m];
                     sEPC = temps.Substring(m * 2 + 2, EPClen * 2);
                     m = m + EPClen + 1;
                     if (sEPC.Length != EPClen*2 )
                     return;
                     isonlistview = false;
                   
                      for (i=0; i< ListView1_EPC.Items.Count;i++)    
                      {
                            if (sEPC==ListView1_EPC.Items[i].SubItems[1].Text)
                            {
                                aListItem = ListView1_EPC.Items[i];
                                ChangeSubItem(aListItem, 1, sEPC);
                                ChangeSubItem(aListItem, 3, antstr);                    
                                isonlistview=true;
                            }
                      }
                      if (!isonlistview)
                      {
                          aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString());
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          s = sEPC;
                          ChangeSubItem(aListItem, 1, s);
                          s = (sEPC.Length / 2).ToString().PadLeft(2, '0');
                          ChangeSubItem(aListItem, 2, s);
                          ChangeSubItem(aListItem, 3, antstr);
                          ListView1_EPC.EnsureVisible(ListView1_EPC.Items.Count-1);
                          if (!CheckBox_TID.Checked)
                          {
                              if (ComboBox_EPC1.Items.IndexOf(sEPC) == -1)
                              {
                                  ComboBox_EPC1.Items.Add(sEPC);
                                  ComboBox_EPC2.Items.Add(sEPC);
                                  ComboBox_EPC3.Items.Add(sEPC);
                                  ComboBox_EPC4.Items.Add(sEPC);
                                  ComboBox_EPC5.Items.Add(sEPC);
                                  ComboBox_EPC6.Items.Add(sEPC);
                              }
                          }
                         
                      }
                      if (sEPC != "") lastepc = sEPC;
                     if(ListView1_EPC.Items.Count>0)
                     {
                          textBox18.Text=ListView1_EPC.Items.Count.ToString();
                          textBox17.Text= lastepc;
                          ListView1_EPC.TopItem = ListView1_EPC.Items[ListView1_EPC.Items.Count - 1];
                     }
           

                 }            
            }
            if (!CheckBox_TID.Checked)
            {
                if ((ComboBox_EPC1.Items.Count != 0))
                {
                    ComboBox_EPC1.SelectedIndex = 0;
                    ComboBox_EPC2.SelectedIndex = 0;
                    ComboBox_EPC3.SelectedIndex = 0;
                    ComboBox_EPC4.SelectedIndex = 0;
                    ComboBox_EPC5.SelectedIndex = 0;
                    ComboBox_EPC6.SelectedIndex = 0;
                }
            }
            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }
        private void Timer_Test__Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;           
            Inventory();
        }

        private void SpeedButton_Read_G2_Click(object sender, EventArgs e)
        {
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("Start address is empty!Please input!", "Information");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("length is empty!", "Information");
                return;
            }
            if (Edit_AccessCode2.Text == "")
            {
                MessageBox.Show("Password is empty!", "Information");
                return;
            }
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error!", "Information");
                    return;
                }
            }
  
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
               Timer_G2_Read.Enabled =!Timer_G2_Read.Enabled;
               if (Timer_G2_Read.Enabled)
               {
                   DestroyCode.Enabled = false;
                   AccessCode.Enabled = false;
                   NoProect.Enabled = false;
                   Proect.Enabled = false;
                   Always.Enabled = false;
                   AlwaysNot.Enabled = false;
                   NoProect2.Enabled = false;
                   Proect2.Enabled = false;
                   Always2.Enabled = false;
                   AlwaysNot2.Enabled = false;
                   P_Reserve.Enabled = false;
                   P_EPC.Enabled = false;
                   P_TID.Enabled = false;
                   P_User.Enabled = false;
                   Button_WriteEPC_G2.Enabled = false;
                   Button_SetMultiReadProtect_G2.Enabled = false;
                   Button_RemoveReadProtect_G2.Enabled = false;
                   Button_CheckReadProtected_G2.Enabled = false;
                   button4.Enabled = false;

                   Button_DestroyCard.Enabled = false;
                   Button_SetReadProtect_G2.Enabled = false;
                   Button_SetEASAlarm_G2.Enabled = false;
                   Alarm_G2.Enabled = false;
                   NoAlarm_G2.Enabled = false;
                   Button_LockUserBlock_G2.Enabled = false;
                   button2.Enabled = false;
                   Button_DataWrite.Enabled = false;
                   BlockWrite.Enabled = false;
                   Button_BlockErase.Enabled = false;
                   Button_SetProtectState.Enabled = false;
                   SpeedButton_Read_G2.Text = "Stop";
               }
               else
               {
                   if (ListView1_EPC.Items.Count != 0)
                   {
                       DestroyCode.Enabled = false;
                       AccessCode.Enabled = false;
                       NoProect.Enabled = false;
                       Proect.Enabled = false;
                       Always.Enabled = false;
                       AlwaysNot.Enabled = false;
                       NoProect2.Enabled = true;
                       Proect2.Enabled = true;
                       Always2.Enabled = true;
                       AlwaysNot2.Enabled = true;
                       P_Reserve.Enabled = true;
                       P_EPC.Enabled = true;
                       P_TID.Enabled = true;
                       P_User.Enabled = true;
                       Button_DestroyCard.Enabled = true;
                       Button_SetReadProtect_G2.Enabled = true;
                       Button_SetEASAlarm_G2.Enabled = true;
                       Alarm_G2.Enabled = true;
                       NoAlarm_G2.Enabled = true;
                       Button_LockUserBlock_G2.Enabled = true;
                       Button_WriteEPC_G2.Enabled = true;
                       Button_SetMultiReadProtect_G2.Enabled = true;
                       Button_RemoveReadProtect_G2.Enabled = true;
                       Button_CheckReadProtected_G2.Enabled = true;
                       button4.Enabled = true;
                       button2.Enabled = true;
                       Button_SetProtectState.Enabled = true;
                       
                       Button_DataWrite.Enabled = true;
                       BlockWrite.Enabled = true;
                       Button_BlockErase.Enabled = true;
                   }
                   if (ListView1_EPC.Items.Count == 0)
                   {
                       DestroyCode.Enabled = false;
                       AccessCode.Enabled = false;
                       NoProect.Enabled = false;
                       Proect.Enabled = false;
                       Always.Enabled = false;
                       AlwaysNot.Enabled = false;
                       NoProect2.Enabled = false;
                       Proect2.Enabled = false;
                       Always2.Enabled = false;
                       AlwaysNot2.Enabled = false;
                       P_Reserve.Enabled = false;
                       P_EPC.Enabled = false;
                       P_TID.Enabled = false;
                       P_User.Enabled = false;
                       Button_DestroyCard.Enabled = false;
                       Button_SetReadProtect_G2.Enabled = false;
                       Button_SetEASAlarm_G2.Enabled = false;
                       Alarm_G2.Enabled = false;
                       NoAlarm_G2.Enabled = false;
                       Button_LockUserBlock_G2.Enabled = false;
                       Button_SetProtectState.Enabled = false;
                       button2.Enabled = true;
                       Button_DataWrite.Enabled = false;
                       BlockWrite.Enabled = false;
                       Button_BlockErase.Enabled = false;
                       Button_WriteEPC_G2.Enabled = true;
                       Button_SetMultiReadProtect_G2.Enabled = true;
                       Button_RemoveReadProtect_G2.Enabled = true;
                       Button_CheckReadProtected_G2.Enabled = true;
                       button4.Enabled = true;
                   }
                   SpeedButton_Read_G2.Text = "Read";
               }
        }

        private void Timer_G2_Read_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            fIsInventoryScan = true;
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            string str;
            byte[] CardData=new  byte[320];
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if ((maskadr_textbox.Text=="")||(maskLen_textBox.Text==""))            
              {
                  fIsInventoryScan = false;
                  return;
              }
             
              if (textBox1.Text == "")
              {
                  fIsInventoryScan = false;
                  return;
              }
                if (ComboBox_EPC2.Items.Count == 0)
                {
                    fIsInventoryScan = false;
                    return;
                }
                if (ComboBox_EPC2.SelectedItem == null)
                {
                    fIsInventoryScan = false;
                    return;
                }
                str = ComboBox_EPC2.SelectedItem.ToString();
                if (checkBox1.Checked)
                {
                    ENum = 255;
                    if (R_EPC.Checked) MaskMem = 1;
                    if (R_TID.Checked) MaskMem = 2;
                    if (R_User.Checked) MaskMem = 3;
                    MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                    MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                    MaskData = HexStringToByteArray(maskData_textBox.Text);
                }
                else
                {
                    ENum = Convert.ToByte(str.Length / 4);
                }
                byte[] EPC = new byte[ENum*2];
                EPC = HexStringToByteArray(str);
                if (C_Reserve.Checked)
                    Mem = 0;
                if (C_EPC.Checked)
                    Mem = 1;
                if (C_TID.Checked)
                    Mem = 2;
                if (C_User.Checked)
                    Mem = 3;
                if (Edit_AccessCode2.Text == "")
                {
                    fIsInventoryScan = false;
                    return;
                }
                if (Edit_WordPtr.Text == "")
                {
                    fIsInventoryScan = false;
                    return;
                }
                WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
                Num = Convert.ToByte(textBox1.Text);
                if (Edit_AccessCode2.Text.Length != 8)
                {
                    fIsInventoryScan = false;
                    return;
                }
                fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
                fCmdRet = StaticClassReaderB.ReadData_G2(ref fComAdr, EPC, ENum, Mem, WordPtr, Num, fPassWord, MaskMem,MaskAdr, MaskLen, MaskData, CardData, ref ferrorcode, frmcomportindex);
                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[Num*2];
                    Array.Copy(CardData, daw, Num * 2);
                    listBox1.Items.Add(ByteArrayToHexString(daw));
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    AddCmdLog("ReadData_G2", "Read", fCmdRet);
                }
                if (ferrorcode != -1)
                {
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +
                   " 'Read' Response error=0x" + Convert.ToString(ferrorcode, 2) +
                   "(" + GetErrorCodeDesc(ferrorcode) + ")";
                    ferrorcode=-1;
                }
                fIsInventoryScan = false;
                  if (fAppClosed)
                    Close();
        }

        private void Button_DataWrite_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Mem = 0;
            byte WNum = 0;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error!", "error information");
                    return;
                }
            }
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            if (C_Reserve.Checked)
                Mem = 0;
            if (C_EPC.Checked)
                Mem = 1;
            if (C_TID.Checked)
                Mem = 2;
            if (C_User.Checked)
                Mem = 3;
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("start address is empty!", "information");
                return;
            }
            
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
            {
                return;
            }
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            if (Edit_WriteData.Text == "")
                return;
            s2 = Edit_WriteData.Text;
            if (s2.Length % 4 != 0)
            {
                MessageBox.Show("Please input Data in words in hexadecimal form!'+#13+#10+'For example: 1234、12345678.", "Write");
                return;
            }

            WNum = Convert.ToByte(s2.Length / 4);
            byte[] Writedata = new byte[WNum * 2 + 1];
            Writedata = HexStringToByteArray(s2);
            if ((checkBox_pc.Checked) && (C_EPC.Checked))
            {
                WordPtr = 1;
                WNum = Convert.ToByte(s2.Length / 4 + 1);
                Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            }
            fCmdRet = StaticClassReaderB.WriteData_G2(ref fComAdr, EPC,WNum,ENum, Mem, WordPtr, Writedata, fPassWord,MaskMem,MaskAdr,MaskLen,MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("Write data", "write", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'write'command Response=0x00" +
                  "(Write successfully!)";
            }    
        }

        private void Button_BlockErase_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            string str;
            byte[] CardData = new byte[320];
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error!", "Error information");
                    return;
                }
            }
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (str == "")
                return;
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            if (C_Reserve.Checked)
                Mem = 0;
            if (C_EPC.Checked)
                Mem = 1;
            if (C_TID.Checked)
                Mem = 2;
            if (C_User.Checked)
                Mem = 3;
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("Start address is empty","information");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Length of Block erase is empty", "information");
                return;
            }
            if (Convert.ToInt32(Edit_WordPtr.Text) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
                return;
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            if ((Mem == 1) & (WordPtr < 2))
            {
                MessageBox.Show("the length of start Address of erasing EPC area is equal or greater than 0x01", "information");
                return;
            }
            Num = Convert.ToByte(textBox1.Text);
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            fCmdRet = StaticClassReaderB.BlockErase_G2(ref fComAdr, EPC, ENum, Mem, WordPtr, Num, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("EraseCard", "Block Erase", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'BlockErase'command Response=0x00" +
                     "(Block Erase successfully!)";
            }       
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void Button_SetProtectState_Click(object sender, EventArgs e)
        {
              byte select=0;
              byte setprotect=0;
              string str;
              byte ENum;
              byte[] CardData = new byte[320];
              byte MaskMem = 0;
              byte[] MaskAdr = new byte[2];
              byte MaskLen = 0;
              byte[] MaskData = new byte[100];
              if (checkBox1.Checked)
              {
                  if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                  {
                      MessageBox.Show("Mask data error!", "information");
                      return;
                  }
              }
              if (ComboBox_EPC1.Items.Count == 0)
                  return;
              if (ComboBox_EPC1.SelectedItem == null)
                  return;
              str = ComboBox_EPC1.SelectedItem.ToString();
              if (str == "")
                  return;
              if (checkBox1.Checked)
              {
                  ENum = 255;
                  if (R_EPC.Checked) MaskMem = 1;
                  if (R_TID.Checked) MaskMem = 2;
                  if (R_User.Checked) MaskMem = 3;
                  MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                  MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                  MaskData = HexStringToByteArray(maskData_textBox.Text);
              }
              else
              {
                  ENum = Convert.ToByte(str.Length / 4);
              }
              byte[] EPC = new byte[ENum*2];
              EPC = HexStringToByteArray(str);
              if (textBox2.Text.Length != 8)
              {
                  MessageBox.Show("Access Password Less Than 8 digit!", "information");
                  return;
              }
              fPassWord = HexStringToByteArray(textBox2.Text);
              if ((P_Reserve.Checked) & (DestroyCode.Checked))
                  select = 0x00;
              else if ((P_Reserve.Checked) & (AccessCode.Checked))
                  select = 0x01;
              else if (P_EPC.Checked)
                  select = 0x02;
              else if (P_TID.Checked)
                  select = 0x03;
              else if (P_User.Checked)
                  select = 0x04;
              if (P_Reserve.Checked)
              {
                  if (NoProect.Checked )
                   setprotect=0x00;
                  else if (Proect.Checked)
                   setprotect=0x02;
                  else if (Always.Checked )
                  {
                   setprotect=0x01;
                   if (MessageBox.Show(this, "Set permanently readable and writeable Confirmed?", "information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
                  else if (AlwaysNot.Checked )
                  {
                   setprotect=0x03;
                   if (MessageBox.Show(this, "Set never readable and writeable Confirmed", "information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
              }
              else
              {
                  if (NoProect2.Checked)
                   setprotect=0x00;
                  else if (Proect2.Checked)
                   setprotect=0x02;
                  else if (Always2.Checked)
                  {
                   setprotect=0x01;
                   if (MessageBox.Show(this, "Set permanently writeable Confirmed", "information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
                  else if (AlwaysNot2.Checked )
                  {
                   setprotect=0x03;
                   if (MessageBox.Show(this, "Set never writeable Confirmed?", "information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
              }
              fCmdRet = StaticClassReaderB.Lock_G2(ref fComAdr, EPC, ENum, select, setprotect, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex); ;
              AddCmdLog("Lock_G2", "Lock", fCmdRet);
        }

        private void Button_DestroyCard_Click(object sender, EventArgs e)
        {
            string str;
            byte ENum;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error!", "information");
                    return;
                }
            }
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "";
            if (MessageBox.Show(this, "Kill the Tag  Confirmed?", "information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            if (Edit_DestroyCode.Text.Length != 8)
            {
                MessageBox.Show("Kill Password Less Than 8 digit!Please input again!", "information");
                return;
            }
            if (ComboBox_EPC3.Items.Count == 0)
                return;
            if (ComboBox_EPC3.SelectedItem == null)
                return;
            str = ComboBox_EPC3.SelectedItem.ToString();
            if (str == "")
                return;
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            fPassWord = HexStringToByteArray(Edit_DestroyCode.Text);
            fCmdRet = StaticClassReaderB.KillTag_G2(ref fComAdr, EPC, ENum,fPassWord,MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("KillTag_G2", "Kill Tag", fCmdRet);
            if (fCmdRet == 0)
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Kill Tag'Response return=0x00" +
                          "(Kill successfully)";
        }

        private void Button_WriteEPC_G2_Click(object sender, EventArgs e)
        {
              byte[] WriteEPC =new byte[100];
              byte WriteEPClen;
              byte ENum;
              if (Edit_AccessCode3.Text.Length < 8)
              {
                  MessageBox.Show("Access Password Less Than 8 digit!Please input again!!", "information");
                  return;
              }
             if ((Edit_WriteEPC.Text.Length%4) !=0) 
            {
                MessageBox.Show("Please input Data in words in hexadecimal form!'+#13+#10+'For example: 1234、12345678", "information");
                    return;
            }
            WriteEPClen=Convert.ToByte(Edit_WriteEPC.Text.Length/ 2) ;
            ENum = Convert.ToByte(Edit_WriteEPC.Text.Length / 4);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(Edit_WriteEPC.Text);
            fPassWord = HexStringToByteArray(Edit_AccessCode3.Text);
            fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, ENum, ref ferrorcode, frmcomportindex);
            AddCmdLog("WriteEPC_G2", "Write EPC", fCmdRet);
              if (fCmdRet == 0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'Write EPC'command Response=0x00" +
                            "(Write EPC successfully!)";
        }

        private void Button_SetReadProtect_G2_Click(object sender, EventArgs e)
        {
            byte ENum;
            string str;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error!", "information");
                    return;
                }
            }
             if (Edit_AccessCode4.Text.Length < 8)
              {
                  MessageBox.Show("Access Password Less Than 8 digit!", "information");
                  return;
              }
              if (ComboBox_EPC4.Items.Count == 0)
                  return;
              if (ComboBox_EPC4.SelectedItem == null)
                  return;
              str = ComboBox_EPC4.SelectedItem.ToString();
              if (str == "")
                  return;
              if (checkBox1.Checked)
              {
                  ENum = 255;
                  if (R_EPC.Checked) MaskMem = 1;
                  if (R_TID.Checked) MaskMem = 2;
                  if (R_User.Checked) MaskMem = 3;
                  MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                  MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                  MaskData = HexStringToByteArray(maskData_textBox.Text);
              }
              else
              {
                  ENum = Convert.ToByte(str.Length / 4);
              }
              byte[] EPC = new byte[ENum*2];
              EPC = HexStringToByteArray(str);
              fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
              fCmdRet = StaticClassReaderB.SetPrivacyByEPC_G2(ref fComAdr, EPC, ENum, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
              AddCmdLog("SetPrivacyByEPC_G2", "Set Privacy By EPC", fCmdRet);
              if (fCmdRet==0)
              {
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Set Privacy By EPC'command Response=0x00" +
                        "Set Privacy By EPC successfully!";
              }
        }

        private void Button_SetMultiReadProtect_G2_Click(object sender, EventArgs e)
        {
            if (Edit_AccessCode4.Text.Length < 8)
            {
                MessageBox.Show("Access Password Less Than 8 digit!", "information");
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
             fCmdRet=StaticClassReaderB.SetPrivacyWithoutEPC_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
             AddCmdLog("SetPrivacyWithoutEPC_G2", "Set Privacy Without EPC", fCmdRet);
              if (fCmdRet==0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Set Privacy Without EPC'command Response=0x00" +
                        "(Set Privacy Without EPC successfully)";
        }

        private void Button_RemoveReadProtect_G2_Click(object sender, EventArgs e)
        {
            if (Edit_AccessCode4.Text.Length < 8)
            {
                MessageBox.Show("Access Password Less Than 8 digit!", "information");
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
             fCmdRet=StaticClassReaderB.ResetPrivacy_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
             AddCmdLog("ResetPrivacy_G2", "Reset Privacy", fCmdRet);
              if (fCmdRet==0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Reset Privacy'command Response=0x00" +
                        "(Reset Privacy)";
        }

        private void Button_CheckReadProtected_G2_Click(object sender, EventArgs e)
        {
              byte readpro=2;
              fCmdRet=StaticClassReaderB.CheckPrivacy_G2(ref fComAdr,ref readpro,ref ferrorcode,frmcomportindex);
              AddCmdLog("CheckPrivacy_G2", "Check Privacy", fCmdRet);
              if (fCmdRet==0)
              {
               if (readpro==0)
                   StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Check Privacy'command Response=0x00" +
                        "(Single Tag is unprotected)";
               if (readpro==1)
                   StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Check Privacy'command Response=0x01" +
                        "(Single Tag is protected)";
              }
        }

        private void Button_SetEASAlarm_G2_Click(object sender, EventArgs e)
        {
            byte  EAS=0;
            byte ENum;
            string str;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error", "information");
                    return;
                }
            }
            if (Edit_AccessCode5.Text.Length < 8)
            {
                MessageBox.Show("Access Password Less Than 8 digit!", "information");
                return;
            }
            if (ComboBox_EPC5.Items.Count == 0)
                return;
            if (ComboBox_EPC5.SelectedItem == null)
                return;
            str = ComboBox_EPC5.SelectedItem.ToString();
            if (str == "")
                return;
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            fPassWord = HexStringToByteArray(Edit_AccessCode5.Text);
             if (Alarm_G2.Checked) 
             EAS= 1;
             else 
             EAS=0;
            fCmdRet = StaticClassReaderB.EASConfigure_G2(ref fComAdr, EPC, ENum, fPassWord, EAS, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("EASConfigure_G2", "EAS Configure", fCmdRet);     
              if (fCmdRet==0)
              {
                  if (Alarm_G2.Checked)                                
                      StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'EAS Configure'command Response=0x00" +
                                "Set EAS Alarm successfully)";
                  else
                      StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'EAS Configure'command Response=0x00" +
                                "(Clear EAS Alarm successfully)";
              }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Timer_G2_Alarm.Enabled = !Timer_G2_Alarm.Enabled;
            if (Timer_G2_Alarm.Enabled)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_WriteEPC_G2.Enabled = false;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                button2.Enabled = false;

                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                button4.Text = "Stop";
            }
            else
            {
                if (ListView1_EPC.Items.Count != 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = true;
                    Proect2.Enabled = true;
                    Always2.Enabled = true;
                    AlwaysNot2.Enabled = true;
                    P_Reserve.Enabled = true;
                    P_EPC.Enabled = true;
                    P_TID.Enabled = true;
                    P_User.Enabled = true;
                    Button_DestroyCard.Enabled = true;
                    Button_SetReadProtect_G2.Enabled = true;
                    Button_SetEASAlarm_G2.Enabled = true;
                    Alarm_G2.Enabled = true;
                    NoAlarm_G2.Enabled = true;
                    Button_LockUserBlock_G2.Enabled = true;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button2.Enabled = true;
                    Button_SetProtectState.Enabled = true;
                    SpeedButton_Read_G2.Enabled = true;
                
                        Button_DataWrite.Enabled = true;
                        BlockWrite.Enabled = true;
                    Button_BlockErase.Enabled = true;
                }
                if (ListView1_EPC.Items.Count == 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = false;
                    Proect2.Enabled = false;
                    Always2.Enabled = false;
                    AlwaysNot2.Enabled = false;
                    P_Reserve.Enabled = false;
                    P_EPC.Enabled = false;
                    P_TID.Enabled = false;
                    P_User.Enabled = false;
                    Button_DestroyCard.Enabled = false;
                    Button_SetReadProtect_G2.Enabled = false;
                    Button_SetEASAlarm_G2.Enabled = false;
                    Alarm_G2.Enabled = false;
                    NoAlarm_G2.Enabled = false;
                    Button_LockUserBlock_G2.Enabled = false;
                    SpeedButton_Read_G2.Enabled = false;
                    Button_DataWrite.Enabled = false;
                    BlockWrite.Enabled = false;
                    Button_BlockErase.Enabled = false;
                    Button_SetProtectState.Enabled = false;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button2.Enabled = true;

                }
                button4.Text = "Check Alarm";
                Label_Alarm.Visible = false;                       
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "  'Check EAS Alarm'over";
            }
        }

        private void Timer_G2_Alarm_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            fIsInventoryScan = true;
             fCmdRet=StaticClassReaderB.EASAlarm_G2(ref fComAdr,ref ferrorcode,frmcomportindex);
            if (fCmdRet==0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'EAS Alarm'command Response=0x00" +
                          "(EAS alarm detected)";
                 Label_Alarm.Visible=true;                       
            }
            else
            {
              Label_Alarm.Visible=false;                       
              AddCmdLog("EASAlarm_G2", "EAS Alarm", fCmdRet);
            }
            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }

        private void Button_LockUserBlock_G2_Click(object sender, EventArgs e)
        {
             byte BlockNum = 0;
             byte ENum;
             string str;
             byte MaskMem = 0;
             byte[] MaskAdr = new byte[2];
             byte MaskLen = 0;
             byte[] MaskData = new byte[100];
             if (checkBox1.Checked)
             {
                 if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                 {
                     MessageBox.Show("Mask data error", "information");
                     return;
                 }
             }
             if (Edit_AccessCode6.Text.Length < 8)
             {
                 MessageBox.Show("Access Password Less Than 8 digit!", "Information");
                 return;
             }
             if (ComboBox_EPC6.Items.Count == 0)
                 return;
             if (ComboBox_EPC6.SelectedItem == null)
                 return;
             str = ComboBox_EPC6.SelectedItem.ToString();
             if (str == "")
                 return;
             if (checkBox1.Checked)
             {
                 ENum = 255;
                 if (R_EPC.Checked) MaskMem = 1;
                 if (R_TID.Checked) MaskMem = 2;
                 if (R_User.Checked) MaskMem = 3;
                 MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                 MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                 MaskData = HexStringToByteArray(maskData_textBox.Text);
             }
             else
             {
                 ENum = Convert.ToByte(str.Length / 4);
             }
             byte[] EPC = new byte[ENum*2];
             EPC = HexStringToByteArray(str);
             fPassWord = HexStringToByteArray(Edit_AccessCode6.Text);
             BlockNum=Convert.ToByte(ComboBox_BlockNum.SelectedIndex*2);
             fCmdRet=StaticClassReaderB.BlockLock_G2(ref fComAdr,EPC,ENum,fPassWord,BlockNum,MaskMem, MaskAdr, MaskLen, MaskData,ref ferrorcode,frmcomportindex);
             AddCmdLog("BlockLock_G2", "Block Lock", fCmdRet);
              if (fCmdRet==0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Block Lock'command Response=0x00" +
                        "(Block Lock successfully)";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_Test_.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            fAppClosed = true;
            StaticClassReaderB.CloseComPort();
            if (frmcomportindex>1023)
            StaticClassReaderB.CloseNetPort(frmcomportindex);
        }

        private void ComboBox_IntervalTime_SelectedIndexChanged(object sender, EventArgs e)
        {
                Timer_Test_.Interval =(ComboBox_IntervalTime.SelectedIndex+1)*10;
        }

        public void ChangeSubItem1(ListViewItem ListItem, int subItemIndex, string ItemText)
        {
            if (subItemIndex == 1)
            {
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    ListItem.SubItems[subItemIndex + 1].Text = "1";
                }
                else
                {
                    ListItem.SubItems[subItemIndex + 1].Text = Convert.ToString(Convert.ToUInt32(ListItem.SubItems[subItemIndex + 1].Text) + 1);
                    if ((Convert.ToUInt32(ListItem.SubItems[subItemIndex + 1].Text) > 9999))
                        ListItem.SubItems[subItemIndex + 1].Text = "1";
                }

             }
        }
        private void Inventory_6B()
        {
             byte[] ID_6B=new byte[2000];
             byte[] ID2_6B=new byte[5000] ;
             ListViewItem aListItem = new ListViewItem();
             byte[] ConditionContent =new byte[300];

             if (fAppClosed)
                 Close();
        }
        private void Timer_Test_6B_Tick(object sender, EventArgs e)
        {
            if (fisinventoryscan_6B)
                return;
            fisinventoryscan_6B = true;
            Inventory_6B();
            fisinventoryscan_6B = false;
        }

        private void Read_6B()
        {
            string temp, temps;
            byte[] CardData = new byte[320];
            byte[] ID_6B = new byte[8];
            byte  Num, StartAddress;
            temp = ""; //1234567890123456789012345678901234567890123456789012345678901234567890
            if (temp == "")
                return;
            ID_6B = HexStringToByteArray(temp);
            StartAddress = 1; //123456789012345678901234567890123456789012345678901234567890
            Num = 1; //1234567890123456789012345678901234567890123456789012345678901234567890
            fCmdRet = StaticClassReaderB.ReadData_6B(ref fComAdr, ID_6B, StartAddress, Num, CardData, ref ferrorcode, frmcomportindex);
            if (fCmdRet == 0)
            {
                byte[] data = new byte[Num];
                Array.Copy(CardData, data, Num);
                temps = ByteArrayToHexString(data);
            }
            if(fAppClosed )
                Close();
        }

        private void Timer_6B_Read_Tick(object sender, EventArgs e)
        {
            if (fTimer_6B_ReadWrite)
                return;
            fTimer_6B_ReadWrite = true;
            Read_6B();
            fTimer_6B_ReadWrite = false;
        }

        private void Write_6B()
        {
            string temp;
            byte[] CardData = new byte[320];
            byte[] ID_6B = new byte[8];
            byte  StartAddress;       
            byte Writedatalen;
            int writtenbyte=0;
            temp = ""; //123456789012345678901234567890123456789012345678901234567890
            if (temp == "")
                return;
            ID_6B = HexStringToByteArray(temp);
            StartAddress = 1; //12345678901234567890123456789012345678901234567890
            Writedatalen =Convert.ToByte(1); //12345678901234567890123456789012345678901234567890
            byte[] Writedata = new byte[Writedatalen];
            Writedata = HexStringToByteArray(""); //12345678901234567890123456789012345678901234567890
            fCmdRet=StaticClassReaderB.WriteData_6B(ref fComAdr,ID_6B,StartAddress,Writedata,Writedatalen,ref writtenbyte,ref ferrorcode,frmcomportindex);
            AddCmdLog("WriteData_6B", "Write", fCmdRet);
              if (fAppClosed)
                  Close();
        }

        private void Timer_6B_Write_Tick(object sender, EventArgs e)
        {
            if (fTimer_6B_ReadWrite)
                return;
            fTimer_6B_ReadWrite = true;
            Write_6B();
            fTimer_6B_ReadWrite = false;
        }

        private void P_Reserve_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count != 0)
            {
                DestroyCode.Enabled = true;
                AccessCode.Enabled = true;
                NoProect.Enabled = true;
                Proect.Enabled = true;
                Always.Enabled = true;
                AlwaysNot.Enabled = true;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
            }
        }

        private void P_EPC_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count != 0)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
            }
        }

        private void P_TID_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count != 0)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
            }
        }

        private void P_User_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count!=0)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
            }
        }

        private void C_EPC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                Edit_WordPtr.Text = "02";
                Edit_WordPtr.ReadOnly = true;
            }
            else
            {
                Edit_WordPtr.ReadOnly = false;
            }
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
            }
        }

        private void C_TID_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
                if (ListView1_EPC.Items.Count != 0)
                    Button_DataWrite.Enabled = true;
            }
            Edit_WordPtr.ReadOnly = false;
        }

        private void C_User_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) & (!Timer_G2_Read.Enabled))
            {
                if (ListView1_EPC.Items.Count != 0)
                    Button_DataWrite.Enabled = true;
            }
            Edit_WordPtr.ReadOnly = false;
        }

        private void C_Reserve_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
                if (ListView1_EPC.Items.Count != 0)
                    Button_DataWrite.Enabled = true;
            }
            Edit_WordPtr.ReadOnly = false;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            timer1.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_Test_.Enabled = false;
            SpeedButton_Read_G2.Text = "Read";
            button2.Text = "Query tag";
            button4.Text = "Check Alarm";
            if ((ListView1_EPC.Items.Count != 0) && (ComOpen))
            {
                button2.Enabled = true;
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
                P_Reserve.Enabled = true;
                P_EPC.Enabled = true;
                P_TID.Enabled = true;
                P_User.Enabled = true;
                Button_DestroyCard.Enabled = true;
                Button_SetReadProtect_G2.Enabled = true;
                Button_SetEASAlarm_G2.Enabled = true;
                Alarm_G2.Enabled = true;
                NoAlarm_G2.Enabled = true;
                Button_LockUserBlock_G2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                SpeedButton_Read_G2.Enabled = true;
                Button_SetProtectState.Enabled = true;
                Button_DataWrite.Enabled = true;
                BlockWrite.Enabled = true;
                Button_BlockErase.Enabled = true;
            }
            if ((ListView1_EPC.Items.Count == 0) && (ComOpen))
            {
                button2.Enabled = true;
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                Button_SetProtectState.Enabled = false;
            }

            Timer_Test_6B.Enabled = false;
            Timer_6B_Read.Enabled = false;
            Timer_6B_Write.Enabled = false;

            breakflag = true;

            if ((tabControl1.SelectedIndex == 1) && (ComOpen == true))
            {
                button27_Click(sender, e);
                Thread.Sleep(200);
                if (SeriaATflag)
                {
                    SeriaATflag = false;
                    return;
                }
                //    Thread.Sleep(1000);
                button34_Click(sender, e);
                Thread.Sleep(100);
                if (SeriaATflag)
                {
                    SeriaATflag = false;
                    return;
                }
                button35_Click(sender, e);
                Thread.Sleep(100);
                if (SeriaATflag)
                {
                    SeriaATflag = false;
                    return;
                }
                button36_Click(sender, e);
            }        
        }

        private void GetData()
        {
              byte[] ScanModeData = new byte[40960];
              int ValidDatalength,i;
              string temp, temps;
              ValidDatalength = 0;
              fCmdRet = StaticClassReaderB.ReadActiveModeData(ScanModeData, ref ValidDatalength, frmcomportindex);
              if (fCmdRet == 0)
              { 
                temp="";
                temps=ByteArrayToHexString(ScanModeData);
                for(i=0;i<ValidDatalength;i++)
                {
                    temp = temp + temps.Substring(i * 2, 2) + " ";
                }
              }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                fIsInventoryScan = true;
            GetData();
            if (fAppClosed)
                Close();
            fIsInventoryScan = false;
        }


        private void radioButton_band1_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 63; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 62;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band2_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 20; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 19;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band3_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 50; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 49;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band4_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 32; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 31;
            ComboBox_dminfre.SelectedIndex = 0;
        } 

        private void ComboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
              ComboBox_baud2.Items.Clear();
              if(ComboBox_COM.SelectedIndex==0)
              {
                  ComboBox_baud2.Items.Add("9600bps");
                  ComboBox_baud2.Items.Add("19200bps");
                  ComboBox_baud2.Items.Add("38400bps");
                  ComboBox_baud2.Items.Add("57600bps");
                  ComboBox_baud2.Items.Add("115200bps");
                  ComboBox_baud2.SelectedIndex=3;
              }
              else
              {
                  ComboBox_baud2.Items.Add("Auto");
                  ComboBox_baud2.SelectedIndex=0;
              }
        }

        private void BlockWrite_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Mem = 0;
            byte WNum = 0;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("Mask data error", "information");
                    return;
                }
            }
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum * 2];
            EPC = HexStringToByteArray(str);
            if (C_Reserve.Checked)
                Mem = 0;
            if (C_EPC.Checked)
                Mem = 1;
            if (C_TID.Checked)
                Mem = 2;
            if (C_User.Checked)
                Mem = 3;
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("start address is empty", "information");
                return;
            }

            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
            {
                return;
            }
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            if (Edit_WriteData.Text == "")
                return;
            s2 = Edit_WriteData.Text;
            if (s2.Length % 4 != 0)
            {
                MessageBox.Show("Please input Data in words in hexadecimal form.", "information");
                return;
            }
            WNum = Convert.ToByte(s2.Length / 4);
            byte[] Writedata = new byte[WNum * 2+1];
            Writedata = HexStringToByteArray(s2);
            if ((checkBox_pc.Checked) && (C_EPC.Checked))
            {
                WordPtr = 1;
                WNum = Convert.ToByte(s2.Length / 4 + 1);
                Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            }
            fCmdRet = StaticClassReaderB.BlockWrite_G2(ref fComAdr, EPC, WNum, ENum, Mem, WordPtr, Writedata, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("Write data", "Block write", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'Block write'command Response=0x00" +
                     "(Block write successfully)";
            }    
        }
  
        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
              int i;
              ComboBox_dminfre.Items.Clear();
              ComboBox_dmaxfre.Items.Clear();
             for (i=0;i<15;i++)
             {
                 ComboBox_dminfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
                 ComboBox_dmaxfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
             }
             ComboBox_dmaxfre.SelectedIndex = 14;
             ComboBox_dminfre.SelectedIndex=0;
        }
        
        private void Button_SetGPIO_Click(object sender, EventArgs e)
        {
              byte OutputPin=0;
              if(checkBox2.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x01);
              if(checkBox3.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x02);
              if(checkBox4.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x04);
              if(checkBox5.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x08);
              if (checkBox6.Checked)
              OutputPin = Convert.ToByte(OutputPin | 0x10);
              if(checkBox7.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x20);
              if(checkBox8.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x40);
              if(checkBox9.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x80);
              fCmdRet = StaticClassReaderB.SetGPIO(ref fComAdr, OutputPin, frmcomportindex);
              AddCmdLog("SetGPIO", "Set", fCmdRet);
        }

        private void Button_GetGPIO_Click(object sender, EventArgs e)
        {
              byte OutputPin=0;
              fCmdRet=StaticClassReaderB.GetGPIOStatus(ref fComAdr,ref OutputPin,frmcomportindex);
              if(fCmdRet==0)
              {
                   if((OutputPin & 0x01)==1)
                   checkBox2.Checked=true;
                   else
                   checkBox2.Checked=false;

                   if((OutputPin & 0x02)==2)
                   checkBox3.Checked=true;
                   else
                   checkBox3.Checked=false;
                   
                   if((OutputPin & 0x04)==4)
                   checkBox4.Checked=true;
                   else
                   checkBox4.Checked=false;

                   if((OutputPin & 0x08)==8)
                   checkBox5.Checked=true;
                   else
                   checkBox5.Checked=false;
                   
                   if((OutputPin & 0x10)==0x10)
                   checkBox6.Checked=true;  
                   else
                   checkBox6.Checked=false;

                   if((OutputPin & 0x20)==0x20)
                   checkBox7.Checked=true;
                   else
                   checkBox7.Checked=false;
               
                   if ((OutputPin & 0x40) == 0x40)
                   checkBox8.Checked = true;
                   else
                   checkBox8.Checked = false;

                   if ((OutputPin & 0x80) == 0x80)
                       checkBox9.Checked = true;
                   else
                       checkBox9.Checked = false;
               }
               AddCmdLog("GetGPIOStatus", "Get", fCmdRet);
        }

        private void Button_Ant_Click(object sender, EventArgs e)
        {
            byte  ANT=0;
            if(checkBox10.Checked) ANT=Convert.ToByte(ANT | 1);
            if(checkBox11.Checked) ANT=Convert.ToByte(ANT | 2);
            if(checkBox12.Checked) ANT=Convert.ToByte(ANT | 4);
            if(checkBox13.Checked) ANT=Convert.ToByte(ANT | 8);
            fCmdRet=StaticClassReaderB.SetAntennaMultiplexing(ref fComAdr,ANT,frmcomportindex);
            AddCmdLog("SetAntenna", "Set", fCmdRet);
        }

        private void Button_RelayTime_Click(object sender, EventArgs e)
        {   
              byte RelayTime=0;
              RelayTime=Convert.ToByte(ComboBox_RelayTime.SelectedIndex);
              fCmdRet = StaticClassReaderB.SetRelay(ref fComAdr, RelayTime, frmcomportindex);
              AddCmdLog("SetRelay", "Set", fCmdRet);
        }

        private void ClockCMD_Click(object sender, EventArgs e)
        {
             byte[] SetTime=new byte[6] ;
             byte[]  CurrentTime=new byte[6];
             if (SetClock.Checked)
             {
                   if((Text_year.Text == "") || (Text_month.Text == "")||(Text_day.Text == "")||(Text_hour.Text =="")||(Text_min.Text =="")||(Text_sec.Text ==""))
                   {
                        MessageBox.Show("Please input right data", "information");
                        return;
                   }
                      SetTime[0] = Convert.ToByte(Text_year.Text) ;      
                      SetTime[1] = Convert.ToByte(Text_month.Text) ; 
                      SetTime[2] = Convert.ToByte(Text_day.Text) ; 
                      SetTime[3] = Convert.ToByte(Text_hour.Text) ; 
                      SetTime[4] = Convert.ToByte(Text_min.Text) ; 
                      SetTime[5] = Convert.ToByte(Text_sec.Text) ; 

                    if ((Convert.ToByte(Text_year.Text) < 0)||(Convert.ToByte(Text_year.Text) > 0x63))
                    {
                        MessageBox.Show("Please input data from 00-99", "information");
                      return;
                    }
                    fCmdRet = StaticClassReaderB.SetReal_timeClock(ref fComAdr, SetTime,frmcomportindex);
                    AddCmdLog("SetClock", "Set Clock", fCmdRet);
              }
              else
              {
                fCmdRet = StaticClassReaderB.GetTime(ref fComAdr, CurrentTime,frmcomportindex);
                if (fCmdRet==0)
                {
                    Text_year.Text= Convert.ToString(CurrentTime[0]).PadLeft(2,'0');
                    Text_month.Text = Convert.ToString(CurrentTime[1]).PadLeft(2, '0');
                    Text_day.Text = Convert.ToString(CurrentTime[2]).PadLeft(2, '0');
                    Text_hour.Text = Convert.ToString(CurrentTime[3]).PadLeft(2, '0');
                    Text_min.Text = Convert.ToString(CurrentTime[4]).PadLeft(2, '0');
                    Text_sec.Text = Convert.ToString(CurrentTime[5]).PadLeft(2, '0');                     
                }
                AddCmdLog("GetClock", "Query Clock", fCmdRet);
              }
        }

        private void Button_OutputRep_Click(object sender, EventArgs e)
        {
              byte OutputRep=0;
              if(checkBox17.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x01);
              if(checkBox16.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x02);
              if(checkBox15.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x04);
              if(checkBox14.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x08);
              fCmdRet=StaticClassReaderB.SetNotificationPulseOutput(ref fComAdr,OutputRep,frmcomportindex);
              AddCmdLog("SetNotificationPulseOutput", "Set", fCmdRet);
        }

        private void Button_Beep_Click(object sender, EventArgs e)
        {
             byte BeepEn=0;
             if(Radio_beepEn.Checked)
              BeepEn=1;
             else
              BeepEn=0;
             fCmdRet=StaticClassReaderB.SetBeepNotification(ref fComAdr,BeepEn,frmcomportindex);
             AddCmdLog("SetBeepNotification", "Set", fCmdRet);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskadr_textbox.Enabled = true;
                maskLen_textBox.Enabled = true;
                maskData_textBox.Enabled = true;
                R_EPC.Enabled = true;
                R_TID.Enabled = true;
                R_User.Enabled = true;
            }
            else
            {
                maskadr_textbox.Enabled = false;
                maskLen_textBox.Enabled = false;
                maskData_textBox.Enabled = false;
                R_EPC.Enabled = false;
                R_TID.Enabled = false;
                R_User.Enabled = false;
            }
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DevControl.tagErrorCode eCode = DevControl.DM_DeInit();
            if (eCode != DevControl.tagErrorCode.DM_ERR_OK)
            {
                ErrorHandling.HandleError(eCode);
            }
        }


        private void ComboBox_COM_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox_baud2.Items.Clear();
            if (ComboBox_COM.SelectedIndex == 0)
            {
              ComboBox_baud2.Items.Add("9600bps");
              ComboBox_baud2.Items.Add("19200bps");
              ComboBox_baud2.Items.Add("38400bps");
              ComboBox_baud2.Items.Add("57600bps");
              ComboBox_baud2.Items.Add("115200bps");
              ComboBox_baud2.SelectedIndex =3;
            }
            else
            {
              ComboBox_baud2.Items.Add("Auto");
              ComboBox_baud2.SelectedIndex=0;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            OpenPort.Enabled=true;
            ClosePort.Enabled = true;
            OpenNetPort.Enabled=false;
            CloseNetPort.Enabled=false;
            CloseNetPort_Click(sender, e);
        }

        private void CloseNetPort_Click(object sender, EventArgs e)
        {
            ClearLastInfo();
            fCmdRet = StaticClassReaderB.CloseNetPort(frmcomportindex);
            if (fCmdRet == 0)
            {
                fOpenComIndex = -1;
                RefreshStatus();
                Button3.Enabled = false;
                Button5.Enabled = false;
                Button1.Enabled = false;
                button2.Enabled = false;
                Button_DestroyCard.Enabled = false;
                Button_WriteEPC_G2.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                button4.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;

                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;

                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;

                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_WriteEPC_G2.Enabled = false;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                button4.Enabled = false;

                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                ListView1_EPC.Items.Clear();
                ComboBox_EPC1.Items.Clear();
                ComboBox_EPC2.Items.Clear();
                ComboBox_EPC3.Items.Clear();
                ComboBox_EPC4.Items.Clear();
                ComboBox_EPC5.Items.Clear();
                ComboBox_EPC6.Items.Clear();
                button2.Text = "Query Tag";

                ComOpen = false;

                timer1.Enabled = false;
                Button_SetGPIO.Enabled = false;
                Button_GetGPIO.Enabled = false;
                Button_Ant.Enabled = false;
                Button_RelayTime.Enabled = false;
                ClockCMD.Enabled = false;
                Button_OutputRep.Enabled = false;
                Button_Beep.Enabled = false;

                button26.Enabled = false;
                button27.Enabled = false;
                button28.Enabled = false;
                button29.Enabled = false;
                button31.Enabled = false;
                button32.Enabled = false;
                button33.Enabled = false;
                button34.Enabled = false;
                button35.Enabled = false;
                button36.Enabled = false;
                button25.Enabled = false;
                button37.Enabled = false;
            }
        }

        private void OpenNetPort_Click(object sender, EventArgs e)
        {
            int port, openresult = 0;
            string IPAddr;
            if (textBox9.Text == "")
                Edit_CmdComAddr.Text = "FF";
            fComAdr = Convert.ToByte(textBox9.Text, 16); // $FF;
            if ((textBox7.Text == "") || (textBox8.Text == ""))
                MessageBox.Show("Config error!", "information");
            port = Convert.ToInt32(textBox7.Text);
            IPAddr = textBox8.Text;
            openresult = StaticClassReaderB.OpenNetPort(port, IPAddr, ref fComAdr, ref frmcomportindex);
            fOpenComIndex = frmcomportindex;
            if (openresult == 0)
            {
                ComOpen = true;
                Button3_Click(sender, e); 
            }
            if ((openresult == 0x35) || (openresult == 0x30))
            {
                MessageBox.Show("TCPIP error", "Information");
                StaticClassReaderB.CloseNetPort(frmcomportindex);
                ComOpen = false;
                return;
            }
            if ((fOpenComIndex != -1) && (openresult != 0X35) && (openresult != 0X30))
            {
                Button3.Enabled = true;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                Button_SetGPIO.Enabled = true;
                Button_GetGPIO.Enabled = true;
                Button_Ant.Enabled = true;
                Button_RelayTime.Enabled = true;
                ClockCMD.Enabled = true;
                Button_OutputRep.Enabled = true;
                Button_Beep.Enabled = true;

                button26.Enabled = true;
                button27.Enabled = true;
                button28.Enabled = true;
                button29.Enabled = true;
                button31.Enabled = true;
                button32.Enabled = true;
                button33.Enabled = true;
                button34.Enabled = true;
                button35.Enabled = true;
                button36.Enabled = true;
                button25.Enabled = true;
                button37.Enabled = true;
                ComOpen = true;
            }
            if ((fOpenComIndex == -1) && (openresult == 0x30))
                MessageBox.Show("TCPIP Communication Error", "Information");
            RefreshStatus();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(ComboBox_AlreadyOpenCOM.Items.Count>0)
            ClosePort_Click( sender,  e);
            OpenPort.Enabled=false;
            ClosePort.Enabled=false;
            OpenNetPort.Enabled=true;
            CloseNetPort.Enabled = true;
        }

        private void Edit_WriteData_TextChanged(object sender, EventArgs e)
        {
            int m, n;
            n = Edit_WriteData.Text.Length;
            if ((checkBox_pc.Checked) && (n % 4 == 0) && (C_EPC.Checked))
            {
                m = n / 4;
                m = (m & 0x3F) << 3;
                textBox_pc.Text = Convert.ToString(m, 16).PadLeft(2, '0') + "00";
            }
        }

        private void checkBox_pc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                Edit_WordPtr.Text = "02";
                Edit_WordPtr.ReadOnly = true;
                int m, n;
                n = Edit_WriteData.Text.Length;
                if ((checkBox_pc.Checked) && (n % 4 == 0) && (C_EPC.Checked))
                {
                    m = n / 4;
                    m = (m & 0x3F) << 3;
                    textBox_pc.Text = Convert.ToString(m, 16).PadLeft(2, '0') + "00";
                }
            }
            else
            {
                Edit_WordPtr.ReadOnly = false;
            }
        }

        private void ListView1_EPC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            byte[] SeriaNo = new byte[4];
            textBox14.Text = "";
            fCmdRet = StaticClassReaderB.GetSeriaNo(ref fComAdr, SeriaNo, frmcomportindex);
            if (fCmdRet == 0)
            {
                textBox14.Text = ByteArrayToHexString(SeriaNo);
            }
            AddCmdLog("GetSeriaNo", "Get", fCmdRet);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!SP?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                SeriaATflag = false;
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("Get failed!", "Information");
                        SeriaATflag = true;
                        return;
                    }
                    int m = 0;
                    int n = 0;
                    string code = "";
                    m = recvs.IndexOf(":");
                    recvs = recvs.Substring(m + 2);
                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    baudrateCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    databitCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    stopbitCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    parityCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    flowCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf("\r\n");
                    code = recvs.Substring(0, n);
                    fifoCB.SelectedIndex = Convert.ToInt32(code);
                }
                AddCmdLog("TransparentCMD", "Get", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!SP=0,1,";
                cmd = cmd + baudrateCB.SelectedIndex.ToString() + "," + databitCB.SelectedIndex.ToString()
                    + "," + stopbitCB.SelectedIndex.ToString() + "," + parityCB.SelectedIndex.ToString()
                    + "," + flowCB.SelectedIndex.ToString() + "," + fifoCB.SelectedIndex.ToString();
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("Set failed!", "Information");
                        return;
                    }
                   
                }
                AddCmdLog("TransparentCMD", "Set", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!TC?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                SeriaATflag = false;
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("Get failed!", "Information");
                        SeriaATflag = true;
                        return;
                    }
                    int m = 0;
                    int n = 0;
                    string code = "";
                    m = recvs.IndexOf(",");
                    recvs = recvs.Substring(m + 1);
                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    workasCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    tcpActiveCB.SelectedIndex = Convert.ToInt32(code) - 1;

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    tcpLocalPortNUD.Text = code;

                    if (recvs.IndexOf("1800")>0)
                    {
                        recvs=recvs.Substring(1);
                         n = recvs.IndexOf("\"");
                        code = recvs.Substring(0, n);
                        recvs = recvs.Substring(n + 2);
                        tcpRomteHostTB.Text = code;

                        n = recvs.IndexOf(",");
                        code = recvs.Substring(0, n);
                        tcpRemotePortNUD.Text = code;
                    }
                    else
                    {
                         n = recvs.IndexOf("\"");
                        code = recvs.Substring(0, n - 1);
                        recvs = recvs.Substring(n + 1);
                        tcpRemotePortNUD.Text = code;

                        n = recvs.IndexOf(",");
                        code = recvs.Substring(0, n - 1);
                        recvs = recvs.Substring(n + 1);
                        tcpRomteHostTB.Text = code;
                    }
                     
                }
                AddCmdLog("TransparentCMD", "Get", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!TC=0,";
                cmd = cmd + workasCB.SelectedIndex.ToString() + "," + Convert.ToString(tcpActiveCB.SelectedIndex + 1)
                    + "," + tcpLocalPortNUD.Text + ",\"" + tcpRomteHostTB.Text
                    + "\"," + tcpRemotePortNUD.Text + "," +  "," ;
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        cmd = "AT!TC=0,"; 
                        cmd = cmd + workasCB.SelectedIndex.ToString() + "," + Convert.ToString(tcpActiveCB.SelectedIndex + 1)
                            + "," + tcpLocalPortNUD.Text + ",\"" + tcpRemotePortNUD.Text
                            + "\"," + tcpRomteHostTB.Text + "," + ",";
                        data = Encoding.ASCII.GetBytes(cmd);
                        cmdlen = Convert.ToByte(cmd.Length);
                        Array.Copy(data, cmddata, cmdlen);
                        timeout = 30;
                        cmddata[cmdlen] = 0x0d;
                        cmddata[cmdlen + 1] = 0x0a;
                        cmdlen = Convert.ToByte(cmdlen + 2);
                        fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                        if (fCmdRet == 0)
                        {
                            recvs = Encoding.ASCII.GetString(recvdata);
                            if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                            {
                                MessageBox.Show("Set failed!", "Information");
                                return;
                            }

                        }
                    }
                    
                }
                AddCmdLog("TransparentCMD", "Set", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!IC?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                SeriaATflag = false;
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("Get failed!", "Information");
                        SeriaATflag = true;
                        return;
                    }
                    int m = 0;
                    int n = 0;
                    string code = "";
                    m = recvs.IndexOf("\"");
                    recvs = recvs.Substring(m + 1);
                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    ipTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    subnetTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    gatewayTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    pDNSTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    altDNSTB.Text = code;
                    AddCmdLog("TransparentCMD", "Get", fCmdRet);
                }


                cmd = "AT!EC?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("Get failed!", "Information");
                        SeriaATflag = true;
                        return;
                    }
                    int m = 0;
                    int n = 0;
                    string code = "";
                    m = recvs.IndexOf("\"");
                    recvs = recvs.Substring(m + 1);
                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    macTB.Text = code;
                   
                }
                AddCmdLog("TransparentCMD", "Get", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!IC=0,\"";
                cmd = cmd + ipTB.Text + "\",\"" + subnetTB.Text
                    + "\",\"" + gatewayTB.Text + "\",\"" + pDNSTB.Text
                    + "\",\"" + altDNSTB.Text + "\"";
              
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        cmd = "AT!IC=0,\"";
                        cmd = cmd + ipTB.Text + "\","
                            + ",\"" + gatewayTB.Text + "\",\"" + pDNSTB.Text
                            + "\",\"" + altDNSTB.Text + "\"";
                        data = Encoding.ASCII.GetBytes(cmd);
                        cmdlen = Convert.ToByte(cmd.Length);
                        Array.Copy(data, cmddata, cmdlen);
                        timeout = 60;
                        cmddata[cmdlen] = 0x0d;
                        cmddata[cmdlen + 1] = 0x0a;
                        cmdlen = Convert.ToByte(cmdlen + 2);
                        fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                        if (fCmdRet == 0)
                        {
                            recvs = Encoding.ASCII.GetString(recvdata);
                            if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                            {
                                MessageBox.Show("Set failed!", "Information");
                                return;
                            }

                        }
                    }
                    
                }
                AddCmdLog("TransparentCMD", "Set", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            byte ATMode = 1;
            SeriaATflag = false;
            fCmdRet = StaticClassReaderB.ChangeATMode(ref fComAdr, ATMode, frmcomportindex);
            if (fCmdRet!=0)
            {
                SeriaATflag = true;
            }
            AddCmdLog("ChangeATMode", "GOTO", fCmdRet);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            byte ATMode = 0;
            fCmdRet = StaticClassReaderB.ChangeATMode(ref fComAdr, ATMode, frmcomportindex);
            AddCmdLog("ChangeATMode", "EXIT", fCmdRet);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox16.Text == "")
                {
                    MessageBox.Show("Command is null!");
                    return;
                }
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                data = Encoding.ASCII.GetBytes(textBox16.Text);
                cmdlen = Convert.ToByte(textBox16.Text.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = Convert.ToByte(comboBox6.SelectedIndex + 1);
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    textBox15.Text = Encoding.ASCII.GetString(recvdata);
                }
                AddCmdLog("ChangeATMode", "Send", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            textBox15.Text = "";
            textBox16.Text = "";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!S";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    cmd = "AT!R";
                    data = Encoding.ASCII.GetBytes(cmd);
                    cmdlen = Convert.ToByte(cmd.Length);
                    Array.Copy(data, cmddata, cmdlen);
                    timeout = 30;
                    cmddata[cmdlen] = 0x0d;
                    cmddata[cmdlen + 1] = 0x0a;
                    cmdlen = Convert.ToByte(cmdlen + 2);
                    fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);

                }
                AddCmdLog("TransparentCMD", "Set", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!LD";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("Set failed!", "Information");
                        return;
                    }
                }
                AddCmdLog("TransparentCMD", "Load default value", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }     
    }
}