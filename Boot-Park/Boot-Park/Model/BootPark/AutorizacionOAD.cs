using Boot_Park.Conections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boot_Park.Model.BootPark
{
    public class AutorizacionOAD
    {
        private ConexionMariaDB connection = new ConexionMariaDB();
        public DataTable consultarVehiculosEnUso(string usuario)
        {

            string sql = "SELECT "
                        + "    V.VEHI_ID, "
                        + "    V.VEHI_PLACA, "
                        + "    V.VEHI_MODELO, "
                        + "    V.VEHI_MODELO, "
                        + "    V.VEHI_OBSERVACION "
                        + "FROM "
                        + "    BOOTPARK.AUTORIZACION A "
                        + "INNER JOIN BOOTPARK.VEHICULO V "
                        + "ON "
                        + "    ( "
                        + "        A.VEHI_ID=V.VEHI_ID "
                        + "    ) "
                        + "WHERE "
                        + "    A.USUA_ID ='" + usuario + "'";

            return connection.getDataMariaDB(sql).Tables[0];
        }
        public DataTable consultarVehiculosStock(string usuario)
        {

            string sql = "SELECT "
                        + "    V.VEHI_ID, "
                        + "    V.VEHI_PLACA, "
                        + "    V.VEHI_MARCA, "
                        + "    V.VEHI_MODELO, "
                        + "    V.VEHI_OBSERVACION "
                        + "FROM "
                        + "    BOOTPARK.VEHICULO V "
                        + "WHERE "
                        + "    V.VEHI_ID NOT IN "
                        + "    ( "
                        + "        SELECT "
                        + "            A.VEHI_ID "
                        + "        FROM "
                        + "            BOOTPARK.AUTORIZACION A "
                        + "       WHERE A.USUA_ID='" + usuario + "'"
                        + "    )";


            return connection.getDataMariaDB(sql).Tables[0];
        }
        public bool registrarVehiculoUsuario(string id, string usuario, string descripcion, string registradoPor, string tipo, string estado)
        {
            string sql = "INSERT "
                        + "INTO "
                        + "    BOOTPARK.AUTORIZACION "
                        + "    ( "
                        + "        USUA_ID, "
                        + "        VEHI_ID, "
                        + "        AUTO_REGISTRADOPOR, "
                        + "        AUTO_FECHACAMBIO, "
                        + "        AUTO_DESCRIPCION, "
                        + "        AUTO_FECHAAUTORIZACION, "
                        + "        AUTO_TIPO, "
                        + "        AUTO_ESTADO "
                        + "    ) "
                        + "    VALUES "
                        + "    ( "
                        + "        '" + usuario + "', "
                        + "        '" + id + "', "
                        + "        '" + registradoPor + "', "
                        + "        CURRENT_DATE(), "
                        + "        '" + descripcion + "', "
                        + "        CURRENT_DATE(), "
                        + "        '" + tipo + "', "
                        + "        '" + estado + "' "
                        + "    )";

            return connection.sendSetDataMariaDB(sql);
        }

        public bool desvincularVehiculoUsurio(string id, string usuario)
        {
            string sql = "DELETE "
                         + "FROM "
                         + "    BOOTPARK.AUTORIZACION "
                         + "WHERE "
                         + "    USUA_ID ='" + usuario + "' "
                         + "AND VEHI_ID ='" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }


    }
}