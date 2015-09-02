using Boot_Park.Controller.BootPark;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boot_Park.View.Private.BootPark.Autorizacion
{
    public partial class AutorizacionAdmin : System.Web.UI.Page
    {
        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarUsuariosChaira();
        }
        public void cargarUsuariosChaira()
        {
            DataTable datos = parametro.consultarUsuariosChaira();
            SUSUARIO.DataSource = datos;
            SUSUARIO.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculosOUT()
        {
            SVEHICULOOUT.DataSource = parametro.consultarVehiculosDisponibles();
            SVEHICULOOUT.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculosIN(string usuario)
        {
            SVEHICULOIN.DataSource = parametro.consultarVehiculosEnUso(usuario);
            SVEHICULOIN.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public bool vincularVehiculoAlUsuario(string id, string usuario)
        {
            return parametro.registrarVehiculoUsuario(id, usuario, "", pegeId);
        }
        [DirectMethod(Namespace = "parametro")]
        public bool desvincularVehiculoAlUsuario(string id, string usuario)
        {
            return parametro.desvincularVehiculoUsuario(id, usuario);
        }
    }
}