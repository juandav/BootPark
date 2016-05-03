using System.Data;

namespace Circulation.controller.bootpark.circulacion
{
    public class CirculacionCOD
    {
        private CirculacionOAD _CIRCULATION = new CirculacionOAD();


        public DataTable ValidateUser()
        {
            return null;
        }

        public DataTable ValidateLabelAndFingerprint()
        {
            return null;
        }

        public DataTable CheckTypeTerminal()
        {
            return null;
        }

        public DataTable FindUser(string user)
        {
            return null;
        }

        public bool CreateCirculation(string user, string vehicle, string circulartion, string type, string pege)
        {
            return false;
        }
        public DataTable CheckTypeCirculation(string vehicle)
        {
            return null;
        }

    }
}