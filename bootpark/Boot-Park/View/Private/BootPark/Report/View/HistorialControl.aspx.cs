using Boot_Park.Controller.BootPark;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boot_Park.View.Private.BootPark.Report.View
{
    public partial class HistorialControl : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                ListadoVehiculoRegistrados();
            }
        }

        public void ListadoVehiculoRegistrados() {

            DataTable dt = parametro.ListarVehiculosRegistrados();

            var groubVehiculo = from b in dt.AsEnumerable()
                              group b by b.Field<string>("VEHICULO") into g
                              select new
                              {
                                  VEHICULO = g.Key
                                  
                              };
            groubVehiculo.GetEnumerator();

            List<object> data = new List<object>();

            foreach (var item in groubVehiculo)
            {
                List<object> customers = new List<object>();

                DataRow[] filtroVehiculo = dt.Select("VEHICULO = '" + item.VEHICULO.ToString() + "'");
                foreach (DataRow row in filtroVehiculo)
                {
                    customers.Add(new
                    {
                        VEHICULO =      row[0].ToString(),
                        ENTRADA =       row[1].ToString(),
                        USUARIOENTRADA =row[2].ToString(),
                        FECHAINI =      row[3].ToString(),
                        HORAINI =       row[4].ToString(),
                        SALIDA =        row[5].ToString(),
                        USUARIOSALIDA = row[6].ToString(),
                        FECHASAL =      row[7].ToString(),
                        HORAFIN =       row[8].ToString(),
                        TIEMPO =        row[9].ToString()
                    });
                }
                data.Add(new { Letter = item.VEHICULO.ToString(), Customers = customers });
            }

            this.dsReport.DataSource = data;
            this.dsReport.DataBind();


            //foreach (DataRow p in query)
            //{
            //    X.AddScript("console.log('" + p.Field<string>("VEHICULO") + "');");
            //}

        }
    }
}