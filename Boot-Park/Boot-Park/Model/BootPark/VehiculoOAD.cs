using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class VehiculoOAD
    {
        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarVehiculos()
        {
            string sql = "";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public bool registrarVehiculos()
        {
            List<string> sql = new List<string>() {
                "",
                ""
            };
            return connection.sendSetDataTransaction(sql);
        }

        public bool registrarVehiculo()
        {
            string sql = "";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarVehiculo()
        {
            string sql = "";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarVehiculo()
        {
            string sql = "";
            return connection.sendSetDataMariaDB(sql);
        }
    }
}