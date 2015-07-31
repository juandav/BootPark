using ReaderB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BootParkRFID
{
    [ComVisibleAttribute(true)]
    [Guid("81B67E9C-4363-487E-9BF6-07F778DBD90E")]
    [ProgId("BootParkRFID.RFID")]
    public class RFID
    {

       
        /// <summary>
        ///     Función de prueba para la conexión del plugin RFID.
        /// </summary>
      
        [ComVisible(true)]
        public void ConectarConTerminal(string ip, string puerto, string tipo)
        {
            byte readerAdress = 0xFF; //comAdress
            int portReturned = 0;
         
            if (tipo == "RFID")
            {
                try   // Excepcion Controlar No se encuentra conectado el Dispositivo
                {

               
                int existeConexionRFID = StaticClassReaderB.OpenNetPort(Convert.ToInt32(puerto), ip, ref readerAdress, ref portReturned);
                StaticClassReaderB.SetBeepNotification(ref readerAdress, 0, portReturned);

                if (existeConexionRFID == 0)
                {
                    MessageBox.Show("Conexion Exitosa ", "EXITO");                    
                   
                }else
                {
                    MessageBox.Show("TCP/IP erroneo", "Information");
                    StaticClassReaderB.CloseNetPort(portReturned);
                    
                }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

         [ComVisible(true)]
        public string TextoPrueba(string texto)
        {
            return "El Texto Prueba concatenado con: " + texto + " de JavaScript";
        }
    }
}
