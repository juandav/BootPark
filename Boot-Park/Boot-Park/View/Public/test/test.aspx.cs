using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using JSONLibrary;
using rfid.io;
using Boot_Park.Controller.BootPark;

namespace Boot_Park.View.Public.test
{
    public partial class test : System.Web.UI.Page
    {

        private ParametrizacionCOD _integracion = new ParametrizacionCOD();
        private string pegeId = "SYSTEM"; // EL SISTEMA ES EL QUE MANEJA ESTE MODULO.

        protected void Page_Load(object sender, EventArgs e) // MAIN: PRIMERA CARGA DE LA PAGINA WEB
        {
            if (!IsPostBack) {

            }
        }

        [DirectMethod(Namespace = "TEST")]
        public bool ValidaAspirante(string user) // VALIDA EL ASPIRANTE QUE INGRESO UNA HUELLA O UNA TARJETA EN LA BASE DE DATOS
        {
            DataTable data = _integracion.ValidarUsuario(user); // VERIFICA EN LA TABLA AUTORIZACION SI EL USUARIO ESTA AUTORIZADO.
            if(data.Rows[0]["EXISTENCIA"].ToString() == "1")
            {
                return true;
            }else
            {
                return false;
            }
        }

        [DirectMethod(Namespace = "TEST")]
        public string DetectarTag() 
        {

            RFID r = new RFID("192.168.1.250", "27011");
            string response = r.iniciarDeteccion();

            if (response.Equals("YES"))
            {
                return r.Tag;
            }
           
            return "";
        }

        [DirectMethod(Namespace = "TEST")]
        public bool ValidarVehiculo(string user, string tag) // VALIDA EL VEHICULO QUE TIENE UN TAG DETECTADO
        {
            DataTable data = _integracion.ValidarTagAndHuella(user, tag, "");// VALIDA SI EL USUARIO LE CORRESPONDE EL TAG

            if (data.Rows[0]["VALIDACION"].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        [DirectMethod(Namespace = "TEST")]
        public void CargarAspirante(string user) // CARGA EL USUARIO DEL BIOMETRICO
        {
            /*
                + "	U.IDENTIFICACION, "
                + "	U.NOMBRE, "
                + "	U.APELLIDO, "
                + "	U.TIPOUSUARIO "
            */
            DataTable aspirante = _integracion.ConsultarUsuarioCirculacion(user); // USUARIO DE CIRCULACION
            SDATOS.DataSource = aspirante;
            SDATOS.DataBind();
        }
        [DirectMethod(Namespace = "TEST")]
        public void CargarVehiculo(string tag) // CARGA EL VEHICULO DEL RFID
        {
            DataTable data = _integracion.ConsultarVehiculoCirculacion(tag);
            SVEHICULOS.DataSource = data;
            SVEHICULOS.DataBind();
        }
        [DirectMethod(Namespace = "TEST")]
        public void RegistrarCirculacion(string user, string tag) // REGISTRA LA CIRCULACION DEL VEHICULO
        {
            string tipo = "SALIDA";
            string vehiculo = _integracion.ConsultarVehiculoTagAsignado(tag).Rows[0]["VEHI_ID"].ToString().ToUpper();
            if (_integracion.ConsultarTipoEntradaVehiculo(vehiculo).Rows.Count==0)
            {
                tipo = "ENTRADA";
            }
            
            if (tipo == "ENTRADA")
            {
                
                if (_integracion.RegistrarCirculacion(user, vehiculo, "SALIDA")) //REGISTRO LA CIRCULACION SALIDA DEL PARQUEADERO
                {
                    Entrar();
                }

            }
            else
            {
                
                if (_integracion.RegistrarCirculacion(user, vehiculo, "ENTRADA"))//REGISTRO LA CIRCULACION ENTRADA PARQUEADERO
                {
                    Salir();
                }

            }
        }
        [DirectMethod(Namespace = "TEST")]
        public void AbrirPuerta() // ABRE LA PUERTA PARA QUE PASE EL VEHICULO. // SEÑAL DE APERTURA
        {
            X.Msg.Notify("Notificación","Sale el vehiculo.").Show();
        }
        [DirectMethod(Namespace = "TEST")]
        private void CerrarPuerta() // CIERRA LA PUERTA UNA VES EL SENSOR DE PROXIMIDAD DETECTA EL VEHICULO A UNA DISTANCIA INFERIROR A 10 cm. // SEÑAL DE CIERRA
        {
            X.Msg.Notify("Notificación", "entra el vehiculo.").Show();
        }

        [DirectMethod(Namespace = "TEST")]
        public void Salir() // ABRE LA PUERTA PARA QUE PASE EL VEHICULO. // SEÑAL DE APERTURA
        {
            X.Msg.Notify("Notificación", "Sale el vehiculo.").Show();
        }
        [DirectMethod(Namespace = "TEST")]
        private void Entrar() // CIERRA LA PUERTA UNA VES EL SENSOR DE PROXIMIDAD DETECTA EL VEHICULO A UNA DISTANCIA INFERIROR A 10 cm. // SEÑAL DE CIERRA
        {
            X.Msg.Notify("Notificación", "entra el vehiculo.").Show();
        }
    }


}