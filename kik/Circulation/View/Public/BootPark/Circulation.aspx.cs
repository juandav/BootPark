using System;
using System.Data;
using Ext.Net;
using Circulation.controller.bootpark.circulacion;
using System.Threading;

namespace Circulation.View.Public.BootPark
{
    public partial class Circulation : System.Web.UI.Page
    {

        private CirculacionCOD c = new CirculacionCOD();

        protected void Page_Load(object sender, EventArgs e) {  }

        // Cargar la información del usuario, si tiene uno en BootPark.
        [DirectMethod]
        public bool CargarUsuario(string user) {
            LESTADO.Text = "Verificando.....";
            DataTable boot_user = c.FindUser(user);
            bool exist = boot_user.Rows.Count > 0;
            if (exist)
            {
                LIDENTIFICACION.Text = boot_user.Rows[0]["IDENTIFICACION"].ToString();
                LNOMBRE.Text = boot_user.Rows[0]["NOMBRE"].ToString() + " " + boot_user.Rows[0]["APELLIDO"];
            }
            return exist;
        }
        // Cargar el vehiculo si y solo el tag detectado, se encuentra autorizado al usuario encontrado.
        [DirectMethod]
        public bool CargarVehiculo(string tag, string user) {
            DataTable boot_vehicle = c.FindVehicle(tag, user);
            //X.Msg.Notify("Notificación", tag).Show();
            bool exist = boot_vehicle.Rows.Count > 0;
            if (exist)
            {
                LVEHICULO.Text = boot_vehicle.Rows[0]["mave_marca"].ToString() + " - " + boot_vehicle.Rows[0]["VEHI_PLACA"].ToString();
                LHORATIPO.Text = DateTime.Now.ToString();
            }
            return exist;
        }
        // Registrar el historial de ingreso y salida de vehiculos, si y solo si el sistema lo permite.
        [DirectMethod]
        public bool RegistarCiculacion(string tag, string user) {
            
            bool exist = c.CreateCirculation(tag, user);
            Thread.Sleep(2000);
            LimpiaDatos();
            return exist;
        }

        [DirectMethod]
        public string QueTipoEs() {
            string foo = c.QueTipoEs().Rows[0]["TIPO"].ToString() + ", " + c.QueTipoEs().Rows[0]["MENSAJE"].ToString();
            return foo;
        }

        [DirectMethod(Namespace = "parametro")]
        public void LimpiaDatos()
        {
            LESTADO.Text = "Esperando Usuario....";
            LVEHICULO.Text = "";
            LHORATIPO.Text = "";
            LNOMBRE.Text = "";
            LIDENTIFICACION.Text = "";
        }
      
    }
}