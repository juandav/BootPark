using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ext.Net;

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
        [DirectMethod(Namespace = "VALIDACION")]
        public bool ValidarUsuario(string user, string type)
        {
            return true;
        }

        /// <summary>
        ///     Valida el tag en la base de datos.
        /// </summary>
        [DirectMethod]
        public void validarTag()
        {

        }

        /// <summary>
        ///     Carga un datatable de los usuarios que coinciden con un tag y huella
        /// </summary>
        [DirectMethod]
        public void cargarUsuario(string huella, string tag) {

        }

        /// <summary>
        ///     Verifica si el usuario se encuemtra autorizado de sacar un vehiculo.
        /// </summary>
        [DirectMethod]
        public void verificarAutorizacion()
        {

        }
  
        /// <summary>
        ///     Verifica si es una terminal de ingreso o salida de un vehiculo.
        /// </summary>
        [DirectMethod]
        public void verificarTerminal()
        {

        }

        /// <summary>
        ///     Registra el movimiento del vehiculo.
        /// </summary>
        [DirectMethod]
        public void registrarCirculacion()
        {

        }


    }
}