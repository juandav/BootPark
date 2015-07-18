using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boot_Park.Controller.BootPark;
using System.Data;

namespace Boot_Park.View.Private.BootPark.Parametrizacion
{
    public partial class Particular : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                DataTable j = parametro.consultarParticulares();
                bool u = parametro.registrarParticular();
                bool m = parametro.registrarParticulares();
            }
        }
    }
}