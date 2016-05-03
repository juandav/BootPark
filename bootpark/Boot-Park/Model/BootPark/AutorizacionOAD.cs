﻿using System.Data;
using Boot_Park.Conections;

namespace Boot_Park.Model.BootPark
{
    public class AutorizacionOAD
    {
        private ConexionMariaDB connection = new ConexionMariaDB();
        #region AutorizacionAdmin

        public DataTable consultarVehiculosEnUso(string usuario)
        {

            string sql = "SELECT "
                        + "    V.VEHI_ID, "
                        + "    V.VEHI_PLACA, "
                        + "    M.MAVE_MARCA AS VEHI_MARCA, "
                        + "    V.VEHI_MODELO, "
                        + "    V.VEHI_OBSERVACION "
                        + "FROM "
                        + "    BOOTPARK.AUTORIZACION A "
                        + "INNER JOIN BOOTPARK.VEHICULO V "
                        + "ON "
                        + "    ( "
                        + "        A.VEHI_ID=V.VEHI_ID "
                        + "    ) "
                        + "INNER JOIN BOOTPARK.MARCAVEHICULO M "
                        + "ON V.MAVE_ID = M.MAVE_ID "
                        + "WHERE "
                        + "    A.USUA_ID='" + usuario + "' "
                        + "AND AUTO_TIPO ='PROPIETARIO'";

            return connection.getDataMariaDB(sql).Tables[0];
        }
        public DataTable consultarVehiculosStock()
        {

            string sql = "SELECT "
                        + "    V.VEHI_ID, "
                        + "    V.VEHI_PLACA, "
                        + "    B.MAVE_MARCA AS VEHI_MARCA, "
                        + "    V.VEHI_MODELO, "
                        + "    V.VEHI_OBSERVACION "
                        + "FROM "
                        + "    BOOTPARK.VEHICULO V "
                        + "INNER JOIN BOOTPARK.MARCAVEHICULO B "
                        + "ON "
                        + "    V.MAVE_ID = B.MAVE_ID "
                        + "WHERE "
                        + "    V.VEHI_ID NOT IN "
                        + "    ( "
                        + "        SELECT "
                        + "            A.VEHI_ID "
                        + "        FROM "
                        + "            BOOTPARK.AUTORIZACION A "
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

        public bool desvincularVehiculoUsuario(string id, string usuario)
        {
            string sql = "DELETE "
                         + "FROM "
                         + "    BOOTPARK.AUTORIZACION "
                         + "WHERE "
                         + "    USUA_ID ='" + usuario + "' "
                         + "AND VEHI_ID ='" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }
        #endregion

        #region AutorizacionPropietario

        public DataTable consultarVehiculosEnUsoPropietario(string IdPropietario,string IdParticular)
        {

            string sql = "SELECT "
                            + "    V.VEHI_ID, "
                            + "    V.VEHI_PLACA, "
                            + "    M.MAVE_MARCA AS VEHI_MARCA, "
                            + "    V.VEHI_MODELO, "
                            + "    V.VEHI_OBSERVACION "
                            + "FROM "
                            + "    BOOTPARK.AUTORIZACION a "
                            + "INNER JOIN "
                            + "    ( "
                            + "        SELECT "
                            + "            VEHI_ID "
                            + "        FROM "
                            + "            BOOTPARK.AUTORIZACION a1 "
                            + "        WHERE "
                            + "            a1.AUTO_TIPO ='PROPIETARIO' "
                            + "        AND a1.USUA_ID= '" + IdPropietario + "' "
                            + "    ) "
                            + "    P "
                            + "ON "
                            + "    a.VEHI_ID = P.VEHI_ID "
                            + "INNER JOIN BOOTPARK.VEHICULO V "
                            + "ON "
                            + "    a.VEHI_ID = V.VEHI_ID "
                            + "INNER JOIN BOOTPARK.MARCAVEHICULO M "
                            + "ON "
                            + "    V.MAVE_ID = M.MAVE_ID "
                            + "WHERE "
                            + "    a.AUTO_TIPO='PARTICULAR' "
                            + "AND a.USUA_ID= '" + IdParticular + "'";

                         
                         


            return connection.getDataMariaDB(sql).Tables[0];
        }
        public DataTable consultarVehiculosStockPropietario(string Idpropietario,string Idparticular)
        {

            string sql = "SELECT "
                         + "    V.VEHI_ID, "
                         + "    V.VEHI_PLACA, "
                         + "    M.MAVE_MARCA AS VEHI_MARCA, "
                         + "    V.VEHI_MODELO, "
                         + "    V.VEHI_OBSERVACION "
                         + "FROM BOOTPARK.VEHICULO V "
                         + "INNER JOIN "
                         + "(SELECT "
                         + "    PART.VEHI_ID "
                         + "FROM "
                         + "    BOOTPARK.AUTORIZACION PART "
                         + "WHERE "
                         + "    PART.AUTO_TIPO ='PROPIETARIO' "
                         + "AND PART.USUA_ID = '" + Idpropietario + "' "
                         + "AND PART.VEHI_ID NOT IN "
                         + "    ( "
                         + "        SELECT "
                         + "            A.VEHI_ID "
                         + "        FROM "
                         + "            BOOTPARK.AUTORIZACION A "
                         + "        WHERE "
                         + "            A.AUTO_TIPO ='PARTICULAR' "
                         + "        AND A.USUA_ID='" + Idparticular + "' "
                         + "    ) "
                         + ") AU "
                         + "ON V.VEHI_ID= AU.VEHI_ID "
                         + "INNER JOIN BOOTPARK.MARCAVEHICULO M "
                         + "ON "
                         + "    V.MAVE_ID = M.MAVE_ID ";

            return connection.getDataMariaDB(sql).Tables[0];
        }
        public bool registrarVehiculoUsuarioPropietario(string id, string usuario, string descripcion, string registradoPor, string tipo, string estado)
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

        public bool desvincularVehiculoUsuarioPropietario(string id, string usuario)
        {
            string sql = "DELETE "
                         + "FROM "
                         + "    BOOTPARK.AUTORIZACION "
                         + "WHERE "
                         + "    USUA_ID ='" + usuario + "' "
                         + "AND VEHI_ID ='" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }
        #endregion

    }
}