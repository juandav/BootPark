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
            string sql = "SELECT"
                        + "     V.VEHI_ID,"
                        + "     V.VEHI_OBSERVACION,"
                        + "     V.VEHI_PLACA,"
                        + "     V.VEHI_MODELO,"
                        + "     V.VEHI_MARCA,"
                        + "     V.VEHI_COLOR"
                        + " FROM"
                        + "     BOOTPARK.VEHICULO V";
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

        public bool registrarVehiculo(string id, string observacion, string placa, string modelo, string marca, string color, string registradoPor)
        {
            string sql = "INSERT"
                        + " INTO"
                        + "     BOOTPARK.VEHICULO"
                        + "     ("
                        + "         VEHI_ID,"
                        + "         VEHI_OBSERVACION,"
                        + "         VEHI_PLACA,"
                        + "         VEHI_MODELO,"
                        + "         VEHI_MARCA,"
                        + "         VEHI_COLOR,"
                        + "         VEHI_REGISTRADOPOR,"
                        + "         VEHI_FECHACAMBIO"
                        + "     )"
                        + "     VALUES"
                        + "     ("
                        + "          " + id + ","
                        + "         '" + observacion + "',"
                        + "         '" + placa + "',"
                        + "          " + modelo + ","
                        + "         '" + marca + "',"
                        + "         '" + color + "',"
                        + "         '" + registradoPor + "',"
                        + "              CURRENT_DATE()"
                        + "     )";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarVehiculo(string id, string observacion, string placa, string modelo, string marca, string color, string registradoPor)
        {
            string sql = "UPDATE"
                        + "     BOOTPARK.VEHICULO"
                        + " SET"
                        + "     VEHI_OBSERVACION = '" + observacion + "',"
                        + "     VEHI_PLACA = '" + placa + "',"
                        + "     VEHI_MODELO = " + modelo + ","
                        + "     VEHI_MARCA = '" + marca + "',"
                        + "     VEHI_COLOR = '" + color + "',"
                        + "     VEHI_REGISTRADOPOR = '" + registradoPor + "',"
                        + "     VEHI_FECHACAMBIO = CURRENT_DATE()"
                        + " WHERE"
                        + "     VEHI_ID = '" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarVehiculo(string id)
        {
            string sql = "DELETE"
                        + " FROM"
                        + "     BOOTPARK.VEHICULO"
                        + " WHERE"
                        + "     VEHI_ID = '" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }
    }
}