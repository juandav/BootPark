using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ext.Net;
using JSONLibrary;
using Boot_Park.Controller.BootPark;
using Boot_Park.Class;

namespace Boot_Park.View.Public.BootPark.Circulacion
{
    public partial class Validacion : System.Web.UI.Page
    {

        private ParametrizacionCOD _integracion = new ParametrizacionCOD();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

            }
        }

        /// <summary>
        ///     VALIDA SI EL USUARIO ESTA AUTORIZADO
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool ValidarUsuario(string user, string type)
        {
            if (user.Equals("undefined")) {
                return false;
            }

            DataTable datos = _integracion.ValidarUsuario(user);
            if (Convert.ToInt32(datos.Rows[0]["EXISTENCIA"].ToString()) == 1)
            {
                Session["BT-Usuario"] = user;
                Session["BT-Tipo"] = type;
                return true;
            }
            else {
                return false;
            }
    
        }

        /// <summary>
        ///    VALIDA SI EL USUARIO AUTORIZADO, SACA EL CARROLO AL QUE TIENE AUTORIZACION
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool ValidarTag()
        {
            RFID r = new RFID("192.168.1.250", "27011");
            string response = r.iniciarDeteccion();

            if (response.Equals("YES"))
            {
                //string antena = r.Antena; // Numero de antena de entrada y salida de datos.

                string etiqueta = r.Tag;
                Session["BT-Etiqueta"] = etiqueta;
                string user = Convert.ToString(Session["BT-Usuario"]);
                string tipo = Convert.ToString(Session["BT-Tipo"]);

                DataTable datos = _integracion.ValidarTagAndHuella(user, etiqueta, tipo);

                if (Convert.ToInt32(datos.Rows[0]["VALIDACION"].ToString()) == 1)
                {
                    Session["BT-VEHICULO"] = datos.Rows[0]["VEHICULO"].ToString();
                    return true;
                }
                else {
                    Session["BT-VEHICULO"] = "";
                    return false;
                }

                //X.Msg.Alert("Notificación", etiqueta).Show();

            }else {

                return false;
            }
        }

        // DETERMINA SI LA CIRCULACION ES DE ENTRADA O SALIDA DEPENDIENDO LA UBICACION DEL LECTOR
        // SIN USO AUN
        public bool VerificarTipoCirculacion(string ip, string puerto) {
            DataTable data = _integracion.ComprobarTipoTerminal(ip, puerto);

            if (Convert.ToInt32(data.Rows[0]["EXISTENCIA"].ToString()) == 1) {
                Session["BT-TIPOTERMINAL"] = data.Rows[0]["TIPO"].ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Carga un datatable de los usuarios que coinciden con un tag y huella
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public void CargarUsuario() {
            string user = Convert.ToString(Session["BT-Usuario"]);
            DataTable data = _integracion.ConsultarUsuarioCirculacion(user); // USUARIO DE CIRCULACION
            USUARIO.DataSource = data;
            USUARIO.DataBind();
        }

        /// <summary>
        ///     Registra el movimiento del vehiculo.
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool RegistrarCirculacion()
        {
            string user = Convert.ToString(Session["BT-Usuario"]);
            string vehiculo = Convert.ToString(Session["BT-VEHICULO"]);
            string tipo = Convert.ToString(Session["BT-TIPOTERMINAL"]);
            bool message = _integracion.RegistrarCirculacion(user, vehiculo, tipo); //REGISTRO LA CIRCULACION
            return message;  
        }

        #region HELPER
        /// <summary>
        ///  ABRE LA PUERTA
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool SeñalDeApertura() {
            X.Msg.Notify("SIMULACIÓN","Simulando Apertura de Puerta").Show();
            return true;
        }

        /// <summary>
        ///  CIERRA LA PUERTA
        /// </summary
        [DirectMethod(Namespace = "parametro")]
        public bool SeñalDeCierre() {
            X.Msg.Alert("SIMULACIÓN", "Simulando Cierre de Puerta").Show();
            return true;
        }
        #endregion


    }
}