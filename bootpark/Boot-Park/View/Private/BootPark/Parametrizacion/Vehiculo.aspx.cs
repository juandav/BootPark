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
    public partial class Vehiculo : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarVehiculos();
                cargarMarcas();
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
        public void cargarMarcas()
        {
            DataTable datos = parametro.consultarMarcaVehiculo();
            SMARCA.DataSource = datos;
            SMARCA.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void crearVehiculo(string observacion, string placa, string modelo, string marca, string color)
        {
            bool response = parametro.registrarVehiculo(observacion, placa, modelo, marca, color, pegeId);

            if (response)
            {
                WREGISTRO.Hide();
                X.Msg.Notify("Notificación", "Vehiculo agregado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }

            BindData();
        }

        [DirectMethod(Namespace = "parametro")]
        public void modificarVehiculo(string id, string observacion, string placa, string modelo, string marca, string color)
        {
            bool response = parametro.actualizarVehiculo(id, observacion, placa, modelo, marca, color, pegeId);

            if (response)
            {
                X.Msg.Notify("Notificación", "Vehiculo actualizado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            GPVEHICULO.Store.Primary.CommitChanges();
        }

        [DirectMethod(Namespace = "parametro")]
        public void eliminarVehiculo(string id)
        {

            bool response = parametro.eliminalVehiculo(id);

            if (response)
            {
                X.Msg.Notify("Notificación", "Vehiculo eliminado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            BindData();
        }

        private void BindData()
        {
            GPVEHICULO.Store.Primary.DataSource = parametro.consultarVehiculos();
            GPVEHICULO.Store.Primary.DataBind();
        }

    }
}