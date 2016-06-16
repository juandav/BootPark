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
        private string pegeId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pegeId = Convert.ToString(Session["accountSessionId"]);
                cargarUsuariosChaira();
            }
           
        }
        #region PROPIETARIO
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
        public bool desvincularVehiculoAlUsuario(string idvehiculo, string usuario)
        {
            int datos = parametro.consultarPermisoUsuarioIn(idvehiculo).Rows.Count;
            if (datos>0)
            {
                return false;
            }
           
                return parametro.desvincularVehiculoUsuario(idvehiculo, usuario);
           
        }

        #endregion

        #region ASIGNACION PARTICULARES 

        [DirectMethod(Namespace = "parametro")]
        public bool vincularVehiculoAlParticular(string idvehiculo, string usuario)
        {
            return parametro.registrarVehiculoUsuarioPropietario(idvehiculo, usuario, "", pegeId);
        }
        [DirectMethod(Namespace = "parametro")]
        public bool desvincularVehiculoAlParticular(string idvehiculo, string usuario)
        {
                return parametro.desvincularVehiculoUsuario(idvehiculo, usuario);
        }
        [DirectMethod(Namespace = "parametro")]
        public void cargarusuarioIn(String codvehiculo)
        {
            DataTable datos = parametro.consultarPermisoUsuarioIn(codvehiculo);
            SUSERIN.DataSource = datos;
            SUSERIN.DataBind();
        }
        [DirectMethod(Namespace = "parametro")]
        public void cargarusuarioOut(String codvehiculo)
        {
            DataTable datos = parametro.consultarPermisoUsuarioOut(codvehiculo);
            SUSEROUT.DataSource = datos;
            SUSEROUT.DataBind();
        }

        #endregion
    }
}