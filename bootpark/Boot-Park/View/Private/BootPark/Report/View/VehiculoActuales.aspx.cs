using Boot_Park.Controller.BootPark;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boot_Park.View.Private.BootPark.Report.View
{
    public partial class VehiculoActuales : System.Web.UI.Page
    {
        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

                pegeId = Convert.ToString(Session["accountSessionId"]);
                this.ConsultarVehiculoActuales();
            }
        }

        [DirectMethod(Namespace = "parametro")]
        public void ConsultarVehiculoActuales() {
            SVEHICULO.DataSource =  parametro.ConsultarVehiculoActuales();
            SVEHICULO.DataBind();
        }
    }
}