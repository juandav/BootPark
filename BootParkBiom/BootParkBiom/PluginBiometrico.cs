using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using zkemkeeper;
using System.Windows.Forms;

namespace BootParkBiom
{
    [ComVisibleAttribute(true)]
    [Guid("B9EB7A49-0E1D-4175-A554-0CD5CC5D406D")]
    [ProgId("BootParkBiom.PluginBiometrico")]
    public class PluginBiometrico
    {
        private string tarjeta;
        private string huella;
        private string usuario;
        private bool conexion;

        /// <summary>
        ///     Función de prueba para verificar la conexión del plugin
        /// </summary>
        /// <returns>Texto</returns>
        [ComVisible(true)]
        public string TextoPrueba()
        {
            return "Reponde desde el Plugin";
        }

        /// <summary>
        ///   Probando la Funcionalidad del Plugin en el envio por parametro.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Texto</returns>
        [ComVisible(true)]
        public string ParametroPrueba(string texto)
        {
            return texto;
        }


        #region
        ////////***********VARIABLES*************//////////
        public string Tarjeta
        {
            get
            {
                return tarjeta;
            }
            set
            {
                tarjeta = value;
            }
        }

        public string Huella
        {
            get
            {
                return huella;
            }
            set
            {
                huella = value;
            }
        }

        public string Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        public bool Conexion
        {
            get
            {
                return conexion;
            }
            set
            {
                conexion = value;
            }
        }
        #endregion


        /// <summary>
        ///    Objeto que instancia al dispositivo biometrico
        /// </summary>
        private CZKEM lectorObject = new CZKEM();

        /// <summary>
        ///     Función de prueba para verificar la conexión del control ActiveX
        /// </summary>
        /// <param name="texto">Texto JavaScript</param>
        /// <returns>Texto del dll concatenado con el de JavaScript</returns>
        public string TextoPrueba(string texto)
        {
            return "El Texto Prueba concatenado con: " + texto + " de JavaScript";
        }

        /// <summary>
        ///    Permite conectar con una terminal:
        ///      1. Lector Biometrico
        ///      2. Lector RFID
        ///      3. Camara 
        /// </summary>
        /// <param name="ip">
        ///     La ip de la terminal a conectar
        /// </param>
        /// <param name="puerto">
        ///     Puerto por donde transmite la información
        /// </param>
        /// <param name="tipo">
        ///     tipo de terminal ya sea:
        ///      1. (Biometrico)
        ///      2. (RFID)
        ///      3. (Camara)
        /// </param>
        public void ConectarConTerminal(string ip, string puerto, string tipo)
        {

            if (tipo == "Biometrico")
            {
                #region
                /// <summary>
                ///     Connect_Net: Permite conectar con el dispositivo a través de la dirección IP y configurar una conexión de red con el dispositivo.
                /// </summary>
                /// <param name="IPAdd">
                ///      Ip del dispositivo
                /// </param>
                /// <param name="Port">
                ///      Puerto usado para conectar con el dispositivo. El puerto por defecto es el 4370.
                /// </param>
                /// <returns>
                ///      Deevuelve True o False. Verificando si el dispositivo pudo conectarse o no.
                /// </returns>
                #endregion
                bool existeConexionBiometrico = lectorObject.Connect_Net(ip, Convert.ToInt32(puerto));

                if (existeConexionBiometrico == true)
                {
                    conexion = true;

                    /// <summary>
                    ///     RegEvent: Permite registrar los eventos en tiempo real.
                    /// </summary>
                    /// <param name="dwMachineNumber">Número de dispositivos</param>
                    /// <param name="EventMask">Código de un evento. Los valores son los siguientes:
                    ///     1. (1):     OnAttTransaction, OnAttTransactionEx  (Este evento se desencadena después de la verificación tiene éxito.)
                    ///     2. (2):     OnFinger (Este evento se activa cuando una huella dactilar se coloca sobre el sensor de huellas digitales del dispositivo. No se devuelve ningún valor.)
                    ///     3. (4):     OnNewUser (Este evento se activa cuando un nuevo usuario se inscribió con éxito.)
                    ///     4. (8):     OnEnrollFinger (Este evento se activa Cuando Se ha Registrado Una Huella digital.)
                    ///     5. (16):    OnKeyPress (No existe documentación para la función)
                    ///     6. (256):   OnVerify (Este evento se activa cuando se verifica un usuario)
                    ///     7. (512):   OnFingerFeature (Este evento se activa cuando un usuario coloca un dedo y el dispositivo registra la huella dactilar.)
                    ///     8. (1024):  OnDoor (Este evento se activa cuando el dispositivo se abre la puerta.), 
                    ///                 OnAlarm (Este evento se activa cuando el dispositivo informa de una alarma.) 
                    ///     9. (2048):  OnHIDNum (Este evento se activa cuando se pasa una tarjeta.) 
                    ///     10.(4096):  OnWriteCard (Este evento se activa cuando el dispositivo registra una carta.)
                    ///     11.(8192):  OnEmptyCard (Este evento se activa cuando una tarjera esta vacia o sin codigo asignado.)
                    ///     12.(16384): OnDeleteTemplate (No existe documentación para la función.)
                    ///     13.(65535): Para registrar todos los eventos.
                    /// </param>
                    /// <returns>
                    ///    Devuelve True o False: Dependiendo si tiene exito en registrar los eventos u ocurre errores.
                    /// </returns>
                    bool existenEventos = lectorObject.RegEvent(1, 65535);

                    if (existenEventos == true)
                    {
                        lectorObject.OnVerify += new _IZKEMEvents_OnVerifyEventHandler(ObtenerUsuarioEvent);
                        lectorObject.OnHIDNum += new _IZKEMEvents_OnHIDNumEventHandler(ObtenerTarjetaEvent);
                        lectorObject.OnFinger += new _IZKEMEvents_OnFingerEventHandler(ObtenerHuellaEvent);
                    }
                    else
                    {
                        //Muestre un mensaje que no se puede registrar los eventos.
                    }
                }
                else
                {
                    //Muestre un Mensaje de que no se puede conectar con el dispositivo.
                }
            }
        }

        /// <summary>
        ///   Desconectar el dispositivo biometrico
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="puerto"></param>
        /// <returns></returns>
        public bool Conectar(string ip, string puerto)
        {
            bool existeConexionBiometrico = lectorObject.Connect_Net(ip, Convert.ToInt32(puerto));

            if (existeConexionBiometrico == true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        ///  Desconecta el dispositivo biometrico.
        /// </summary>
        public bool Desconectar()
        {
            lectorObject.Disconnect();
            return false;
        }



        #region EVENTOS
        ///////// *********EVENTOS**********/////////

        private void ObtenerUsuarioEvent(int usuarioEvent)
        {
            this.usuario = Convert.ToString(usuarioEvent);
            //MessageBox.Show("El usuario es: " + usuario, "EXTITO");
        }

        private void ObtenerTarjetaEvent(int tarjetaEvent)
        {
            this.tarjeta = Convert.ToString(tarjetaEvent);
            //MessageBox.Show("Entro en la tarjeta: " + tarjeta, "EXTITO");
        }

        private void ObtenerHuellaEvent()
        {
            MessageBox.Show("Entro en el evento de captura de huella.", "EXTITO");
            /// Puedo validar si la huella en el dispositivo corresponde a la que hay en base de datos.
        }
        #endregion


    }
}
