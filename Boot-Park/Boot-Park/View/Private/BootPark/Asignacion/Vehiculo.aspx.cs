using Boot_Park.Controller.BootPark;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boot_Park.View.Private.BootPark.Asignacion
{
    public partial class Vehiculo : System.Web.UI.Page
    {
        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarVehiculos();
            }
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarVehiculos()
        {
            DataTable datos = parametro.consultarVehiculos();
            SVEHICULO.DataSource = datos;
            SVEHICULO.DataBind();

        }
        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetasOUT()
        {
            DataTable datos = parametro.consultarEtiquetaDisponible();
            SETIQUETAOUT.DataSource = datos;
            SETIQUETAOUT.DataBind();

        }
        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetasIN(string vehiculo)
        {
            DataTable datos = parametro.consultarEtiquetaVehiculoEnUso(vehiculo);
            STIQUETAIN.DataSource = datos;
            STIQUETAIN.DataBind();

        }
        public bool vincularTagAlVehiculo(string etiqueta, string usuario, string observacion)
        {
            return false;
        }


    }
}