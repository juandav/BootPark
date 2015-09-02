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
    public partial class Autorizacion : System.Web.UI.Page
    {
        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";
        private string idPropietario = "4";   // El Idusuario se obtiene cuando inicia session en la plataforma Chaira.
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarUsuarios();
        }
        public void cargarUsuarios()
        {
            DataTable datos = parametro.consultarUsuarios(idPropietario);
            SUSUARIO.DataSource = datos;
            SUSUARIO.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculosOUT()
        {
            SVEHICULOOUT.DataSource = parametro.consultarVehiculosDisponiblesPropietario(idPropietario);
            SVEHICULOOUT.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculosIN(string particular)
        {
            SVEHICULOIN.DataSource = parametro.consultarVehiculosEnUsoPropietario(idPropietario,particular);
            SVEHICULOIN.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public bool vincularVehiculoAlUsuario(string id, string usuario)
        {
            return parametro.registrarVehiculoUsuarioPropietario(id, usuario, "", pegeId);
        }
        [DirectMethod(Namespace = "parametro")]
        public bool desvincularVehiculoAlUsuario(string id, string usuario)
        {
            return parametro.desvincularVehiculoUsuario(id, usuario);
        }
    }
}