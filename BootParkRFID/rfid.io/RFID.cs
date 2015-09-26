using System;
using System.Threading;
using ReaderB;

namespace rfid.io
{
    //Libreria de puente para el RFID
    public class RFID
    {
        // Contiene el identificador unico del tag.
        /// <summary>
        ///     Contiene metodos de get y set, que permite asignar un valor a la etiqueta, como el de obtener una etiqueta leida.
        /// </summary>
        public string Tag
        {
            get
            {
                return Tag;
            }
            set
            {
                Tag = value;
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
                return Antena;
            }
            set
            {
                Antena = value;
            }
        }

        private byte ipHandle = 0xFF; // Ip del lector en Memoria.
        private int portHandle; // Puerto del lector en Memoria.

        private string ip = ""; // Ip Logica del lector.
        private string port = ""; // Puerto Logico del lector.

        public RFID(string ip, string port) {
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
        public bool conectarRFID(string ip, string puerto) {

            int creaConexion = StaticClassReaderB.OpenNetPort(Convert.ToInt32(puerto), ip, ref ipHandle, ref portHandle); // Metodo que conecta con el RFID
            StaticClassReaderB.SetBeepNotification(ref ipHandle, 0, portHandle); // Notifica al RFID
            if (creaConexion != 0) // Si ocurre un error en la conexion TCP/IP, se cierra la conexion.
            {
                StaticClassReaderB.CloseNetPort(portHandle); // Metodo para cerrar la conexion.
                return false;
            }
            else {
                return true;
            }
        }

        // Metodo de desconexion al RFID
        /// <summary>
        ///     Desconecta el RFID
        /// </summary>
        /// <returns>true si desconecto o false si ocurrio un error.</returns>
        public bool desconectarRFID() {
           int desconectaRFID = StaticClassReaderB.CloseNetPort(portHandle);

            if (desconectaRFID != 0) {
                return false;
            }
            else{
                return true;
            }
        }

        // Metodo para iniciar la detección de la etiqueta del vehiculo.
        public string iniciarDeteccion() {

            bool detecion = true; 

            try
            {
                while (!detecion) {
                    detecion = detectarEtiqueta();
                    Thread.Sleep(100);
                }

                return "YES";

            }
            catch {
                return "Ha ocurrido errores en la detección de la etiqueta.";
            }
            finally
            {
                desconectarRFID();
            }
        }

        /// <summary>
        ///     Metodo para detectar la etiqueta del vehiculo.
        /// </summary>
        private bool detectarEtiqueta()
        {
            byte[] longitud = new byte[9];
            int algo = StaticClassReaderB.InventorySingle_6B(ref ipHandle, longitud,  portHandle);
            

            return false; // Es false si no la ha detectado.
            //return true; // Es true cuando el tag fue detectado.
        }
    }
}
