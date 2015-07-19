using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class EtiquetaUsuarioOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarEtiquetasDisponibles(string tipo, string usuario) {
            string sql = "SELECT"
                            + "     E.ETIQ_ID,"
                            + "     E.ETIQ_TIPO,"
                            + "     E.ETIQ_ETIQUETA,"
                            + "     E.ETIQ_DESCRIPCION,"
                            + "     E.ETIQ_OBSERVACION"
                            + " FROM"
                            + "     BOOTPARK.ETIQUETA E"
                            + " WHERE"
                            + "     E.ETIQ_ID NOT IN"
                            + "     ("
                            + "         SELECT"
                            + "             EU.ETIQ_ID"
                            + "         FROM"
                            + "             BOOTPARK.ETIQUETAUSUARIO EU"
                            + "         WHERE"
                            + "             EU.ETIQ_TIPO = '" + tipo + "' AND"
                            + "             EU.USUA_ID = " + usuario
                            + "     )";
            return connection.getDataMariaDB(sql).Tables[0];

        }

        public bool registrarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor) {
            string sql = "INSERT"
                        + " INTO"
                        + "     BOOTPARK.ETIQUETAUSUARIO"
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
                        + "         "  + id + ","
                        + "         '" + tipo + "',"
                        + "         '" + usuario + "',"
                        + "         '" + motivo + "',"
                        + "         '" + caducidad + "',"
                        + "         '" + registradoPor + "',"
                        + "              CURRENT_DATE()"
                        + "     )";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor)
        {
            string sql = "UPDATE"
                        + "     BOOTPARK.ETIQUETAUSUARIO"
                        + " SET"
                        + "     ETUS_MOTIVO = '" + motivo + "',"
                        + "     ETUS_FECHACADUCIDAD = '" + caducidad + "',"
                        + "     ETUS_REGISTRADOPOR = '" + registradoPor + "',"
                        + "     ETUS_FECHACAMBIO = CURRENT_DATE()"
                        + " WHERE"
                        + "     ETIQ_ID = '" + id + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     USUA_ID = '" + usuario + "'";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarEtiquetaUsuario(string id, string tipo, string usuario)
        {
            string sql = "DELETE"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETAUSUARIO"
                        + " WHERE"
                        + "     ETIQ_ID = '" + id + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     USUA_ID = '" + usuario + "'";
            return connection.sendSetDataMariaDB(sql);
        }

    }
}