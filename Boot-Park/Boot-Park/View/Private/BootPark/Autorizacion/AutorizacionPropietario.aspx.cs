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
        private string pegeId = "2"; // Usuario Chaira al iniciar session
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarUsuarios();
            DataTable data = parametro.ConsultarUsuarioCirculacion(pegeId);
            if (data.Rows.Count > 0)
            {
                GPUSUARIO.Title = data.Rows[0]["NOMBRE"].ToString() + " " + data.Rows[0]["APELLIDO"].ToString();
            }
            
        }
        public void cargarUsuarios()
        {
            DataTable datos = parametro.consultarUsuarios(pegeId);
            SUSUARIO.DataSource = datos;
            SUSUARIO.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculosOUT(string particular)
        {
            SVEHICULOOUT.DataSource = parametro.consultarVehiculosDisponiblesPropietario(pegeId ,particular);
            SVEHICULOOUT.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculosIN(string particular)
        {
            SVEHICULOIN.DataSource = parametro.consultarVehiculosEnUsoPropietario(pegeId, particular);
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