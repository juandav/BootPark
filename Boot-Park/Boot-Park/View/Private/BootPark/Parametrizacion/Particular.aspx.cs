using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boot_Park.Controller.BootPark;
using System.Data;
using Ext.Net;

namespace Boot_Park.View.Private.BootPark.Parametrizacion
{
    public partial class Particular : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cargarParticulares();
            }
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarParticulares() {
            BindData();
        }

        [DirectMethod(Namespace = "parametro")]
        public void crearParticular(string identificacion, string nombre, string apellido) {
            bool response = parametro.registrarParticular(identificacion, nombre, apellido, pegeId);

            if (response)
            {
                X.Msg.Notify("Notificación","Particular agregado exitosamente!").Show();
            }
            else {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }

            BindData();
        }

        [DirectMethod(Namespace = "parametro")]
        public void modificarParticular(string id, string identificacion, string nombre, string apellido) {
            bool response = parametro.actualizarParticular(identificacion, nombre, apellido, pegeId);

            if (response)
            {
                X.Msg.Notify("Notificación", "Particular actualizado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            GPPARTICULAR.Store.Primary.CommitChanges();
        }

        [DirectMethod(Namespace = "parametro")]
        public void eliminarParticular(string id) {

            bool response = parametro.eliminalParticular(id);

            if (response)
            {
                X.Msg.Notify("Notificación", "Particular eliminado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            BindData();
        }

        private void BindData()
        {
            GPPARTICULAR.Store.Primary.DataSource = parametro.consultarParticulares();
            GPPARTICULAR.Store.Primary.DataBind();
        }
    }
}