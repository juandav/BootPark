using ReaderB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace RFID.CLASE
{
    class RFIDCL
    {
        private string tag;
        private string antena;
        private bool fIsInventoryScan = false;
        byte readerAdress = 0xFF; //comAdress
        private int fCmdRet = 30; //所有执行指令的返回值
        int fOpenComIndex;
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        string[] ListaTag = new string[3];


        public RFIDCL(string ip, string puerto, string tipo)
        {
            if (tipo == "RFID") 
            {
                int existeConexionRFID = StaticClassReaderB.OpenNetPort(Convert.ToInt32(puerto), ip, ref readerAdress, ref fOpenComIndex);
                StaticClassReaderB.SetBeepNotification(ref readerAdress, 0, fOpenComIndex);
                if (existeConexionRFID!=0) //si ocurrio un error en la conexion TCP/IP..  cierro puerto de  conexion.
                {
                     StaticClassReaderB.CloseNetPort(fOpenComIndex);
                }
            }
        }


        public void consultartag()
        {
            try
            {
                while (fIsInventoryScan == false) // Si  fIsInventoryScan es true se ha detectado tag.. paro de consultar los  tag
                {
                    Inventory();
                    Thread.Sleep(100); // tiempo con que consulto tag.
                }
            }
            catch (Exception e)
            {
               
            }
           
        }

        private void Inventory()
        {

            int CardNum = 0;
            int Totallen = 0;
            int EPClen, m;
            byte[] EPC = new byte[5000];
            int CardIndex;
            string temps;
            string sEPC;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            byte MaskFlag = 0;
            byte Ant = 0;
            string antstr = "";
            string lastepc = "";
            byte AdrTID = 0;
            byte LenTID = 0;
            byte TIDFlag = 0;

            fIsInventoryScan = true;
            MaskMem = 1; //  MASCARA POR DEFECTO EPC
            // MaskMem =2; MASCARA TID
            // MaskMem =3; MASCARA USER
            string maskadr = "0000";
            string maskLeng = "00";
            string maskData = "00";
            MaskAdr = HexStringToByteArray(maskadr); // direccion de la mascara por default
            MaskLen = Convert.ToByte(maskLeng); // tamaño mascara default
            MaskData = HexStringToByteArray(maskData); //array byte default
            fCmdRet = StaticClassReaderB.Inventory_G2(ref readerAdress, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, EPC, ref Ant, ref Totallen, ref CardNum, fOpenComIndex);
            if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))//代表已查找结束，
            {
                byte[] daw = new byte[Totallen];   // cuando  no detecta tag retorna 0
                Array.Copy(EPC, daw, Totallen);
                temps = ByteArrayToHexString(daw);
                fInventory_EPC_List = temps;            //存贮记录
                m = 0;
                if (CardNum == 0)
                {
                    fIsInventoryScan = false;
                    return;
                }
                antstr = Convert.ToString(Ant, 2);
                for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                {
                    EPClen = daw[m];
                    sEPC = temps.Substring(m * 2 + 2, EPClen * 2);
                    m = m + EPClen + 1;
                    if (sEPC.Length != EPClen * 2)
                        return;
                    if (sEPC != "") lastepc = sEPC;
                    this.Tag = lastepc;
                    this.Antena = antstr;
                    fIsInventoryScan = true;
                }
            }

        }


        public void CloseConnection()
        {
            StaticClassReaderB.CloseNetPort(fOpenComIndex);
        }

        #region Metodos de Conversion de Bytes

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

        #endregion

        #region  Variables(Entidades) de Retorno

        public string Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        public string Antena
        {
            get
            {
                return antena;
            }
            set
            {
                antena = value;
            }
        }
        # endregion
    }
}