using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ext.Net;
using JSONLibrary;
using rfid.io;
using Boot_Park.Controller.BootPark;

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
        ///     VALIDA SI EL USUARIO TIENE AUTORIZACION
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public bool ValidarUsuario(string user, string type)
        {
            if (user.Equals("undefined")) {
                return false;
            }

            Session["BT-Usuario"] = user;
            Session["BT-Tipo"] = type;
            return true;
        }

        /// <summary>
        ///     Valida el tag en la base de datos.
        /// </summary>
        [DirectMethod(Namespace = "parametro")]
        public string ValidarTag()
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

                string message = _integracion.ValidarTagAndHuella(user, etiqueta, tipo);

                X.Msg.Alert("Notificación", etiqueta).Show();
                return etiqueta;
            }

            return response;
        }

        /// <summary>
        ///     Carga un datatable de los usuarios que coinciden con un tag y huella
        /// </summary>
        [DirectMethod]
        public void CargarUsuario() {

        }

        /// <summary>
        ///     Registra el movimiento del vehiculo.
        /// </summary>
        [DirectMethod]
        public void RegistrarCirculacion()
        {

        }


    }
}