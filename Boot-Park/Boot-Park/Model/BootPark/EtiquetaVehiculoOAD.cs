using Boot_Park.Conections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boot_Park.Model.BootPark
{
    public class EtiquetaVehiculoOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarEtiquetaDisponible()
        {
            string sql = "SELECT"
                        + "    E.ETIQ_ID,"
                        + "    E.ETIQ_TIPO,"
                        + "    E.ETIQ_ETIQUETA,"
                        + "    E.ETIQ_DESCRIPCION,"
                        + "    E.ETIQ_OBSERVACION"
                        + " FROM"
                        + "    BOOTPARK.ETIQUETA E"
                        + " WHERE "
                        + "    E.ETIQ_ESTADO = 'DISPONIBLE' AND"
                        + "    E.ETIQ_TIPO = 'TAG' AND"
                        + "    E.ETIQ_ID NOT IN (SELECT EV.ETIQ_ID FROM BOOTPARK.ETIQUETAVEHICULO EV)";
            return connection.getDataMariaDB(sql).Tables[0];

        }

        public DataTable consultarEtiquetaVehiculoEnUso(string vehiculo)
        {
            string sql = "SELECT"
                        + "     E.ETIQ_ID,"
                        + "     E.ETIQ_TIPO,"
                        + "     E.ETIQ_ETIQUETA,"
                        + "     E.ETIQ_DESCRIPCION,"
                        + "     E.ETIQ_OBSERVACION,"
                        + "     EU.ETUS_MOTIVO,"
                        + "     EU.ETUS_FECHACADUCIDAD"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETAUSUARIO EU"
                        + " INNER JOIN BOOTPARK.ETIQUETA E"
                        + " ON"
                        + "     ("
                        + "         EU.ETIQ_ID=E.ETIQ_ID"
                        + "     )"
                        + " WHERE"
                        + "     EU.USUA_ID = '" + vehiculo + "'"
                        + " AND E.ETIQ_TIPO = 'TAG'";
            return connection.getDataMariaDB(sql).Tables[0];
        }


        public bool registrarEtiquetaVehiculo(string id, string tipo, string vehiculo, string motivo, string caducidad, string registradoPor)
        {
            string sql = "INSERT"
                        + " INTO"
                        + "     BOOTPARK.ETIQUETAVEHICULO"
                        + "     ("
                        + "         ETIQ_ID,"
                        + "         ETIQ_TIPO,"
                        + "         USUA_ID,"
                        + "         ETUS_MOTIVO,"
                        + "         ETUS_FECHACADUCIDAD,"
                        + "         ETUS_REGISTRADOPOR,"
                        + "         ETUS_FECHACAMBIO"
                        + "     )"
                        + "     VALUES"
                        + "     ("
                        + "         " + id + ","
                        + "         '" + tipo + "',"
                        + "         '" + vehiculo + "',"
                        + "         '" + motivo + "',"
                        + "         '" + caducidad + "',"
                        + "         '" + registradoPor + "',"
                        + "              CURRENT_DATE()"
                        + "     )";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarEtiquetaVehiculo(string id, string tipo, string vehiculo, string observacion, string registradoPor)
        {
            string sql = "UPDATE"
                        + "     BOOTPARK.ETIQUETAUSUARIO"
                        + " SET"
                        + "     ETVE_OBSERVACION = '" + observacion + "',"
                        + "     ETUS_REGISTRADOPOR = '" + registradoPor + "',"
                        + "     ETUS_FECHACAMBIO = CURRENT_DATE()"
                        + " WHERE"
                        + "     VEHI_ID = '" + id + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     VEHI_ID = '" + vehiculo + "'";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarEtiquetaVehiculo(string id, string tipo, string vehiculo)
        {
            string sql = "DELETE"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETAVEHICULO"
                        + " WHERE"
                        + "     ETIQ_ID = '" + id + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     VEHI_ID = '" + vehiculo + "'";
            return connection.sendSetDataMariaDB(sql);
        }
    }
}