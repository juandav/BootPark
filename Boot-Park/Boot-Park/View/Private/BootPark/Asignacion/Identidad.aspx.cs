using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boot_Park.Controller.BootPark;
using System.Data;
using Ext.Net;

namespace Boot_Park.View.Private.BootPark.Asignacion
{
    public partial class Identidad : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarUsuarios();
            }
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarUsuarios()
        {
            DataTable datos = parametro.consultarUsuarios();
            SUSUARIO.DataSource = datos;
            SUSUARIO.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetasOUT() {
            SETIQUETAOUT.DataSource = parametro.consultarCarnetsDisponibles();
            SETIQUETAOUT.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetasIN(string usuario) {
            STIQUETAIN.DataSource = parametro.consultarCarnetsEnUso(usuario);
            STIQUETAIN.DataBind();
        }

    }
}