using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using zkemkeeper;
using System.Windows.Forms;
using JSONLibrary;

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
        private int Dispositivo = 1;
        int iFingerIndex = 2;
        int iFlag = 2;
        /// <summary>
        ///    Objeto que instancia al dispositivo biometrico
        /// </summary>
        private CZKEM lectorObject = new CZKEM();
        #region VARIABLES
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
        #endregion  VARIABLES
        #region METODOS
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
        public bool ConectarConTerminal(string ip, string puerto, string tipo)
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
                    bool existenEventos = lectorObject.RegEvent(Dispositivo, 65535);

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
                return existeConexionBiometrico;
            }
            return false;

        }
        /// <summary>
        ///   Desconectar el dispositivo biometrico
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="puerto"></param>
        /// <returns></returns>
        [ComVisible(true)]
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
        /// COnfigura el Lector en modo Captura.
        ///  Nota: El usuarioId interno del Lector solo acepta como maximo 9 caracteres.
        /// De lo contrario a partir 10 seran ignorados.
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        [ComVisible(true)]
        public bool CapturarHuella(String usuarioID)
        {
            lectorObject.CancelOperation();
            if (lectorObject.StartEnrollEx(usuarioID, iFingerIndex, iFlag))
            {
                lectorObject.StartIdentify();
                lectorObject.RefreshData(Dispositivo);
                lectorObject.EnableDevice(Dispositivo, true);
                return true;
            }
            lectorObject.RefreshData(Dispositivo);
            return false;
        }
        /// <summary>
        /// Recupera la huella de un UsuarioID especifico.
        /// Nota: El usuarioId Interno del lector no debe superar los 9 caracteres
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public string RecuperarHuella(string usuarioID)
        {

            int iTmpLength;
            string ibyTmpData;
            lectorObject.EnableDevice(Dispositivo, false);
            lectorObject.ReadAllTemplate(Dispositivo);
            if (lectorObject.GetUserTmpExStr(Dispositivo, usuarioID, iFingerIndex, out iFlag, out ibyTmpData, out iTmpLength))
            {
                HuellaDactilar h = new HuellaDactilar();
                lectorObject.RefreshData(Dispositivo);
                lectorObject.EnableDevice(Dispositivo, true);
                lectorObject.PlayVoiceByIndex(9);
                h.Identidad = usuarioID;
                h.FingerIndex = iFingerIndex;
                h.byTmpData = ibyTmpData;
                h.TmpLength = iTmpLength;
                return h.ToJSON();
            }
            else
            {
                lectorObject.RefreshData(Dispositivo);
                lectorObject.EnableDevice(Dispositivo, true);
                return null;
            }
        }
        /// <summary>
        /// Vinculo El carnet al Usuario en el dispositivo
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="Carnet"></param>
        /// <returns></returns>
        public bool RegistrarCarnet(string Carnet, string Nombre, string usuarioID)
        {
            bool estado = false;
            lectorObject.EnableDevice(Dispositivo, false);
            lectorObject.SetStrCardNumber(Carnet);
            if (lectorObject.SSR_SetUserInfo(Dispositivo, usuarioID, Nombre, usuarioID, 0, true)) //SSR_SetUserInfo(dispositivo,IdInterno,NombreEtiqueta,Contraseña,privilegio,estado);
            {
                lectorObject.RefreshData(Dispositivo);
                lectorObject.EnableDevice(Dispositivo, true);
                lectorObject.PlayVoiceByIndex(9);
                estado = true;
            }
            else
            {
                estado = false;
            }

            return estado;
        }
        /// <summary>
        ///  Desconecta el dispositivo biometrico.
        /// </summary>
        public bool Desconectar()
        {
            lectorObject.Disconnect();
            return false;
        }
        #endregion
        #region EVENTOS
        ///////// *********EVENTOS**********/////////

        private void ObtenerUsuarioEvent(int usuarioEvent)
        {
            this.usuario = Convert.ToString(usuarioEvent);

        }

        private void ObtenerTarjetaEvent(int tarjetaEvent)
        {
            this.tarjeta = Convert.ToString(tarjetaEvent);

        }

        private void ObtenerHuellaEvent()
        {

            /// Puedo validar si la huella en el dispositivo corresponde a la que hay en base de datos.
        }
        #endregion
        #region ENTIDADES
        public class HuellaDactilar
        {
            public string Identidad { get; set; }
            public int FingerIndex { get; set; }
            public int Flag { get; set; }
            public string byTmpData { get; set; }
            public int TmpLength { get; set; }
        }
        #endregion

    }
}
