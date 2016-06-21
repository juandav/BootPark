using System.Data;

namespace Circulation.controller.bootpark.circulacion
{
    public class CirculacionCOD
    {
        private CirculacionOAD _CIRCULATION = new CirculacionOAD();

        public DataTable FindUser(string user)
        {
            return _CIRCULATION.FindUser(user);
        }

        public DataTable FindVehicle(string tag, string user)
        {
            return _CIRCULATION.FindVehicle(tag, user);
        }

        public bool CreateCirculation(string tag, string user)
        {
            return _CIRCULATION.CreateCirculation(tag, user);
        }

        public DataTable QueTipoEs()
        {
            return _CIRCULATION.QueTipoEs();
        }

    }
}