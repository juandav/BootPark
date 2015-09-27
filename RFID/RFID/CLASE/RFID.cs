using ReaderB;
using System;
using System.Text;
using System.Threading;

namespace RFID.CLASE
{
    public class RFID
    {
        private string tag;
        private string antena;
        // Contiene el identificador unico del tag.
        /// <summary>
        ///     Contiene metodos de get y set, que permite asignar un valor a la etiqueta, como el de obtener una etiqueta leida.
        /// </summary>
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
        // Guarda el numero de antena por el que ingresa y salen los datos.
        /// <summary>
        ///     Me devuelve el Numero de Antena donde entra y salen los datos.
        /// </summary>
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

        private byte ipHandle = 0xFF; // Ip del lector en Memoria.
        private int portHandle; // Puerto del lector en Memoria.

        private string ip = ""; // Ip Logica del lector.
        private string port = ""; // Puerto Logico del lector.

        private bool detecion = false;

        public RFID(string ip, string port)
        {
            this.ip = ip;
            this.port = port;
        }

        // Metodo de Conexion al RFID
        /// <summary>
        ///     Conecta el dispositivo RFID
        /// </summary>
        /// <param name="ip">dirección ip del RFID</param>
        /// <param name="puerto">puerto el RFID</param>
        /// <returns>true si conecta el RFID o false si ocurre un error</returns>
        private bool conectarRFID()
        {

            int creaConexion = StaticClassReaderB.OpenNetPort(Convert.ToInt32(port), ip, ref ipHandle, ref portHandle); // Metodo que conecta con el RFID
            StaticClassReaderB.SetBeepNotification(ref ipHandle, 0, portHandle); // Notifica al RFID
            if (creaConexion != 0) // Si ocurre un error en la conexion TCP/IP, se cierra la conexion.
            {
                StaticClassReaderB.CloseNetPort(portHandle); // Metodo para cerrar la conexion.
                return false;
            }
            else
            {
                return true;
            }
        }

        // Metodo de desconexion al RFID
        /// <summary>
        ///     Desconecta el RFID
        /// </summary>
        /// <returns>true si desconecto o false si ocurrio un error.</returns>
        private bool desconectarRFID()
        {
            int desconectaRFID = StaticClassReaderB.CloseNetPort(portHandle);

            if (desconectaRFID != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Metodo para iniciar la detección de la etiqueta del vehiculo.
        /// <summary>
        ///     Inicia la detección con el Dispositivo RFID especificado en la estancia del objeto.
        /// </summary>
        /// <returns>YES or err</returns>
        public string iniciarDeteccion()
        {
            if (conectarRFID()) // Conecta con el RFID
            {
                try
                {
                    while (!detecion)
                    {
                        detecion = detectarEtiqueta();
                        Thread.Sleep(100);
                    }

                    return "YES";

                }
                catch
                {
                    return "Ha ocurrido errores en la detección de la etiqueta.";
                }
                finally
                {
                    desconectarRFID();
                }
            }
            else {
                return "Ha ocurrido un error al conectar con el dispositivo";
            }
        }

        /// <summary>
        ///     Detiene la deteccion del TAG del dispositivo RFID.
        /// </summary>
        public void detenerDeteccion() {
            detecion = true;
        }

        /// <summary>
        ///     Metodo para detectar la etiqueta del vehiculo.
        /// </summary>
        private bool detectarEtiqueta()
        {
           
                byte[] EPC = new byte[5000];
                //byte maskMemory = 0;
                byte[] maskAdr = HexStringToByteArray("0000");
                byte maskLen = Convert.ToByte("00");
                byte[] maskData = HexStringToByteArray("00");
                //byte maskFlag = 0;
                //byte adrTID = 0;
                //byte lenTID = 0;
                //byte tIDFlag = 0;
                byte Ant = 0;
                int CardNum = 0;
                int longitud = 0;

                int responseReader = StaticClassReaderB.Inventory_G2(ref ipHandle, (byte)0, maskAdr, maskLen, maskData, (byte)0, (byte)0, (byte)0, (byte)0, EPC, ref Ant, ref longitud, ref CardNum, portHandle);
                if ((responseReader == 1) | (responseReader == 2) | (responseReader == 3) | (responseReader == 4) | (responseReader == 0xFB))
                {

                    byte[] daw = new byte[longitud];   //EPC Extraido en longitud.
                    Array.Copy(EPC, daw, longitud);    //Copia la info del EPC al arreglo de extaracción

                    string TAG_EPC = ByteArrayToHexString(daw); // Transforma el EPC extraido en string.

                    if (CardNum == 0)
                    {
                        return false; // TAG NO DETECTADO
                    }
                    else
                    {
                        int longitudEPC = daw[0] * 2;
                        string tags = TAG_EPC.Substring(2, longitudEPC);

                        if (tags.Length == longitudEPC && tags != "")
                        {
                            this.Tag = tags; // Metodo de retorno
                            this.Antena = Convert.ToString(Ant, 2); // Metodo de retorno
                        }
                        else
                        {
                            return false; // TAG NO DETECTADO
                        }
                    }
                    return true; // TAG DETECTADO
                }
                else
                {
                    return false; // TAG NO DETECTADO
                }
          
            //return false; // Es false si no la ha detectado.
            //return true; // Es true cuando el tag fue detectado.
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
    }
}