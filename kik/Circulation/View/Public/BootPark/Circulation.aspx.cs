using System;
using System.Data;
using Ext.Net;
using Circulation.controller.bootpark.circulacion;

namespace Circulation.View.Public.BootPark
{
    public partial class Circulation : System.Web.UI.Page
    {

        private CirculacionCOD c = new CirculacionCOD();

        protected void Page_Load(object sender, EventArgs e) { }

        // Cargar la información del usuario, si tiene uno en BootPark.
        [DirectMethod]
        public bool CargarUsuario(string user) {
            DataTable boot_user = c.FindUser(user);
            bool exist = boot_user.Rows.Count > 0;
            return exist;
        }
        // Cargar el vehiculo si y solo el tag detectado, se encuentra autorizado al usuario encontrado.
        [DirectMethod]
        public bool CargarVehiculo(string tag, string user) {
            DataTable boot_vehicle = c.FindVehicle(tag, user);
            bool exist = boot_vehicle.Rows.Count > 0;
            return exist;
        }
        // Registrar el historial de ingreso y salida de vehiculos, si y solo si el sistema lo permite.
        [DirectMethod]
        public bool RegistarCiculación(string tag, string user) {
            bool exist = c.CreateCirculation(tag, user);
            if (exist) {
                // Aca muestre la data en los labels
            }
            return exist;
        }
    }
}