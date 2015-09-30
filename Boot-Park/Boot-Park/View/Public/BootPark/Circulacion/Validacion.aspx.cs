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

namespace Boot_Park.View.Public.BootPark.Circulacion
{
    public partial class Validacion : System.Web.UI.Page
    {
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
                string etiqueta = r.Tag;
                string antena = r.Antena;
                X.Msg.Alert("Notificación", etiqueta).Show();
                return etiqueta + "" + antena;
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