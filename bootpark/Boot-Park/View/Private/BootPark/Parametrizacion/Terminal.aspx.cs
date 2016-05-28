using Boot_Park.Controller.BootPark;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boot_Park.View.Private.BootPark.Parametrizacion
{
    public partial class Terminal : System.Web.UI.Page
    {
        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                pegeId = Convert.ToString(Session["accountSessionId"]);
                cargarTerminales();
            }
            
        }
        [DirectMethod(Namespace = "parametro")]
        public void cargarTerminales()
        {
            DataTable datos = parametro.consultarTerminales();
            STERMINAL.DataSource = datos;
            STERMINAL.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void crearTerminal(string puerto, string ip,string tipo)
        {
            bool response = parametro.registrarTerminal(puerto, ip,tipo , pegeId);

            if (response)
            {
                WREGISTRO.Hide();
                X.Msg.Notify("Notificación", "Terminal agregado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }

            BindData();
        }

        [DirectMethod(Namespace = "parametro")]
        public void modificarTerminal(string id,string puerto, string ip, string tipo)
        {
            bool response = parametro.actualizarTerminal(id,puerto,ip, tipo, pegeId);

            if (response)
            {
                X.Msg.Notify("Notificación", "Terminal actualizado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            GPTERMINAL.Store.Primary.CommitChanges();
        }

        [DirectMethod(Namespace = "parametro")]
        public void eliminarTerminal(string id)
        {

            bool response = parametro.eliminaTerminal(id);

            if (response)
            {
                X.Msg.Notify("Notificación", "Terminal eliminado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            BindData();
        }

        private void BindData()
        {
            GPTERMINAL.Store.Primary.DataSource = parametro.consultarTerminales();
            GPTERMINAL.Store.Primary.DataBind();
        }
    }
}