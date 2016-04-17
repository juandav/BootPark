using System;
using ReaderB;
using System.Threading;
using System.Text;

namespace TolBarad
{
    public class TolBarad
    {
        #region Variables
        private string _IP_READER = "";
        private string _PORT_READER = "";
        /////////////////////////////////
        private byte _IP_HANDLE = 0xFF;
        private int _PORT_HANDLE = 0;
        #endregion

        #region Objetos
        private Evaluate _eval = new Evaluate(); 
        #endregion


        /// <summary>
        ///     Estancia de un lector RFID
        /// </summary>
        /// <param name="ip">default: 192.168.1.250</param>
        /// <param name="port">default: 27011</param>
        public TolBarad(string ip = "192.168.1.250", string port = "27011")
        {
            this._IP_READER = ip;
            this._PORT_READER = port;
        }

        /// <summary>
        ///     Conecta el lector RFID, retorna true si se crea la conexión y false si es lo contrario.
        /// </summary>
        /// <returns>true or false</returns>
        private bool RFIDConnect()
        {
            int connect = StaticClassReaderB.OpenNetPort(Convert.ToInt32(_PORT_READER), _IP_READER, ref _IP_HANDLE, ref _PORT_HANDLE);
            if (connect != 0)
            {
                StaticClassReaderB.CloseNetPort(_PORT_HANDLE);
                return false;
            }
            else
            {
                StaticClassReaderB.SetBeepNotification(ref _IP_HANDLE, 0, _PORT_HANDLE);
                return true;
            }
        }

        /// <summary>
        ///     Detecta el tag cerca de la Antena conectada a un Lector RFID.
        ///     Retornando el "#tag,#antena"
        /// </summary>
        /// <returns>Tag, Antena</returns>
        public string RunRFID() {
            return StartDetection();
        }

        private string StartDetection() {

            if (RFIDConnect()) {
                try
                {
                    bool detect = false;
                    while (!detect) {
                        string response = DetectLabel();
                        string[] data = _eval.Parser(response);
                        detect = Convert.ToBoolean(data[0]);
                        if (detect) {
                            return data[1] + "," + data[2];
                        }
                        Thread.Sleep(100);
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    StaticClassReaderB.CloseNetPort(_PORT_HANDLE);
                }
            }
            return "false";
        }

        private string DetectLabel() {
            // action, tag, antena
            byte[] EPC = new byte[5000];
            byte[] maskAdr = GetBytes("0000");
            byte maskLen = Convert.ToByte("00");
            byte[] maskData = GetBytes("00");
            byte Ant = 0;
            int CardNum = 0;
            int longitud = 0;

            int responseReader = StaticClassReaderB.Inventory_G2(ref _IP_HANDLE, (byte)0, maskAdr, maskLen, maskData, (byte)0, (byte)0, (byte)0, (byte)0, EPC, ref Ant, ref longitud, ref CardNum, _PORT_HANDLE);
            if ((responseReader == 1) | (responseReader == 2) | (responseReader == 3) | (responseReader == 4) | (responseReader == 0xFB))
            {

                byte[] daw = new byte[longitud];   //   EPC Extraido en longitud.
                Array.Copy(EPC, daw, longitud);    //   Copia la info del EPC al arreglo de extaracción

                string TAG_EPC = GetString(daw); // Transforma el EPC extraido en string.

                if (CardNum == 0)
                {
                    return "false,0,0"; 
                }
                else
                {
                    int longitudEPC = daw[0] * 2;
                    string tags = TAG_EPC.Substring(2, longitudEPC);

                    if (tags.Length == longitudEPC && tags != "")
                    {
                        return string.Format("true,{0},{1}", tags, Convert.ToString(Ant, 2).Length);
                    }
                    else
                    {
                        return "false,0,0";
                    }
                }
            }
            else
            {
                return "false,0,0";
            }

        }

        #region Metodos de Apoyo
        private byte[] GetBytes(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private string GetString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }
        #endregion

    }
}
