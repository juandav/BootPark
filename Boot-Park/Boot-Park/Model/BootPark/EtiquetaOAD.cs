using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class EtiquetaOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarEtiquetas()
        {
            string sql = "SELECT"
                        + "     E.ETIQ_ID,"
                        + "     E.ETIQ_TIPO,"
                        + "     E.ETIQ_ETIQUETA,"
                        + "     E.ETIQ_DESCRIPCION,"
                        + "     E.ETIQ_OBSERVACION,"
                        + "     E.ETIQ_ESTADO"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETA E";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public DataTable consultarEtiquetas(string estado) {
            string sql = "SELECT"
                        + "     E.ETIQ_ID,"
                        + "     E.ETIQ_TIPO,"
                        + "     E.ETIQ_ETIQUETA,"
                        + "     E.ETIQ_DESCRIPCION,"
                        + "     E.ETIQ_OBSERVACION"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETA E"
                        + " WHERE "
                        + "     E.ETIQ_ESTADO = '" + estado + "'";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public bool registrarEtoquetas()
        {
            List<string> sql = new List<string>() {
                "",
                ""
            };
            return connection.sendSetDataTransaction(sql);
        }

        public bool registrarEtiqueta(string id, string tipo, string etiqueta, string descripcion, string observacion, string estado, string registradoPor)
        {
            string sql = "INSERT"
                        + " INTO"
                        + "     BOOTPARK.ETIQUETA"
                        + "     ("
                        + "         ETIQ_ID,"
                        + "         ETIQ_TIPO,"
                        + "         ETIQ_ETIQUETA,"
                        + "         ETIQ_DESCRIPCION,"
                        + "         ETIQ_OBSERVACION,"
                        + "         ETIQ_ESTADO,"
                        + "         ETIQ_REGISTRADOPOR,"
                        + "         ETIQ_FECHACAMBIO"
                        + "     )"
                        + "     VALUES"
                        + "     ("
                        + "         "  + id + ","
                        + "         '" + tipo + "',"
                        + "         '" + etiqueta + "',"
                        + "         '" + descripcion + "',"
                        + "         '" + observacion + "',"
                        + "         '" + estado + "',"
                        + "         '" + registradoPor + "',"
                        + "              CURRENT_DATE()"
                        + "     )";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarEtiqueta(string id, string tipo, string etiqueta, string descripcion, string observacion, string estado, string registradoPor)
        {
            string sql = "UPDATE"
                        + "     BOOTPARK.ETIQUETA"
                        + " SET"
                        + "     ETIQ_ETIQUETA = '" + etiqueta + "',"
                        + "     ETIQ_DESCRIPCION = '" + descripcion + "',"
                        + "     ETIQ_OBSERVACION = '" + observacion + "',"
                        + "     ETIQ_ESTADO = '" + estado + "',"
                        + "     ETIQ_REGISTRADOPOR = '" + registradoPor + "',"
                        + "     ETIQ_FECHACAMBIO = CURRENT_DATE()"
                        + " WHERE"
                        + "     ETIQ_ID = '" + id + "'"
                        + " AND ETIQ_TIPO = '" + tipo +"'";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarEtiqueta(string id, string tipo)
        {
            string sql = "DELETE"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETA"
                        + " WHERE"
                        + "     ETIQ_ID = '" + id + "'"
                        + " AND ETIQ_TIPO = '" + tipo + "'";
            return connection.sendSetDataMariaDB(sql);
        }

    }
}