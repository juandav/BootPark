using Boot_Park.Controller.BootPark;
using Ext.Net;
using rfid.io;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boot_Park.View.Private.BootPark.Circulacion
{
    public partial class Circulacion : System.Web.UI.Page
    {

        private ParametrizacionCOD _integracion = new ParametrizacionCOD();
        RFID r = new RFID("192.168.1.250", "27011");
        private string pegeId = "53233";


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        ///     VALIDA SI EL USUARIO ESTA AUTORIZADO
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool ValidarUsuario(string user, string type)
        {
            if (user.Equals("undefined"))
            {
                return false;
            }

            DataTable datos = _integracion.ValidarUsuario(user);
            if (Convert.ToInt32(datos.Rows[0]["EXISTENCIA"].ToString()) == 1)
            {
                Session["BT-Usuario"] = user;
                Session["BT-Tipo"] = type;
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        ///    VALIDA SI EL USUARIO AUTORIZADO, SACA EL CARROLO AL QUE TIENE AUTORIZACION
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public void detectarTag()
        {
            X.Msg.Notify("SIMULACIÓN", "").Show();
            //if (HiddenTag.Text == "")
            //{
             
                string response = r.iniciarDeteccion();
                if (response.Equals("YES"))
                {
                    string antena = r.Antena; // Numero de antena de entrada y salida de datos.
                    X.Msg.Notify("SIMULACIÓN", "" + r.Antena).Show();
                    string etiqueta = r.Tag;
                    HiddenTag.Text = r.Tag;
                    X.Msg.Notify("SIMULACIÓN", r.Tag).Show();
                    MensajePlaca.Text = _integracion.ConsultarVehiculoTagAsignado(r.Tag).Rows[0]["IdVehiculo"].ToString().ToUpper();
                    Session["BT-VEHICULO"] = _integracion.ConsultarVehiculoTagAsignado(r.Tag).Rows[0]["VEHI_ID"].ToString().ToUpper();
                   r.detenerDeteccion();
                }
               
            //}

        }

        // DETERMINA SI LA CIRCULACION ES DE ENTRADA O SALIDA DEPENDIENDO LA UBICACION DEL LECTOR
        // SIN USO AUN
        public bool VerificarTipoCirculacion(string ip, string puerto)
        {
            DataTable data = _integracion.ComprobarTipoTerminal(ip, puerto);

            if (Convert.ToInt32(data.Rows[0]["EXISTENCIA"].ToString()) == 1)
            {
                Session["BT-TIPOTERMINAL"] = data.Rows[0]["TIPO"].ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Carga un datatable de los usuarios que coinciden con un tag y huella
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public void CargarUsuario()
        {
            string user = Convert.ToString(Session["BT-Usuario"]);
            string users = HiddenUsuario.Text;
            DataTable data = _integracion.ConsultarUsuarioCirculacion(HiddenUsuario.Text); // USUARIO DE CIRCULACION
            string dataUsuario = data.Rows[0]["NOMBRE"].ToString() + " " + data.Rows[0]["APELLIDO"].ToString() + " (" + data.Rows[0]["TIPOUSUARIO"].ToString() + " )";
            LUSUARIO.Text = dataUsuario.ToUpper();
        }

        /// <summary>
        ///     Registra el movimiento del vehiculo.
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public void RegistrarCirculacion()
        {
            string user = Convert.ToString(Session["BT-Usuario"]); //cambio a hiddenUsuario
            string vehiculo = Convert.ToString(Session["BT-VEHICULO"]);
            string tipo = _integracion.ConsultarTipoEntradaVehiculo(vehiculo).Rows[0]["CIRC_TIPO"].ToString();
            if (tipo == "ENTRADA")
            {
                if (_integracion.RegistrarCirculacion(user, vehiculo, "SALIDA")) //REGISTRO LA CIRCULACION SALIDA DEL PARQUEADERO
                {
                    SeñalDeApertura();
                    SeñalDeCierre();
                }
               
            }
            else
            {
                if (_integracion.RegistrarCirculacion(user, vehiculo, "ENTRADA"))//REGISTRO LA CIRCULACION ENTRADA PARQUEADERO
                {
                    SeñalDeApertura();
                    SeñalDeCierre();
                }

            }
            //LVEHICULO.Text = "";
            //LUSUARIO.Text = "";
        }

        #region HELPER
        /// <summary>
        ///  ABRE LA PUERTA
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool SeñalDeApertura()
        {
            X.Msg.Notify("SIMULACIÓN", "Simulando Apertura de Puerta").Show();
            return true;
        }

        /// <summary>
        ///  CIERRA LA PUERTA
        /// </summary
        [DirectMethod(Namespace = "parametro")]
        public bool SeñalDeCierre()
        {
            X.Msg.Notify("SIMULACIÓN", "Simulando Cierre de Puerta").Show();
            return true;
        }
        #endregion


    }
}