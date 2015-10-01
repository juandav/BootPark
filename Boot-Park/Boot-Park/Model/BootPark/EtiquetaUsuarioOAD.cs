using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class EtiquetaUsuarioOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarCarnetsStock() {
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
                        + "    E.ETIQ_TIPO = 'CARNET' AND"
                        + "    E.ETIQ_ID NOT IN (SELECT EU.ETIQ_ID FROM BOOTPARK.ETIQUETAUSUARIO EU)";
            return connection.getDataMariaDB(sql).Tables[0];

        }

        public DataTable consultarCarnetEnUso(string usuario) {
            string sql = "SELECT"
                        + "     E.ETIQ_ID,"
                        + "     E.ETIQ_TIPO,"
                        + "     E.ETIQ_ETIQUETA,"
                        + "     E.ETIQ_DESCRIPCION,"
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
                        + "     EU.USUA_ID = '" + usuario + "'"
                        + " AND E.ETIQ_TIPO = 'CARNET'";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public bool registrarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor) {
            List<string> sentencia =new List<string>();
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
                        + "          " + caducidad + ","
                        + "         '" + registradoPor + "',"
                        + "              CURRENT_DATE()"
                        + "     )";
            sentencia.Add(sql);
            sentencia.Add(ActualizarEtiquetaEstado(id,tipo, "ENUSO"));
            return connection.sendSetDataTransaction(sentencia);
        }

        public string ActualizarEtiquetaEstado(string id, string tipo,string estado) {
            string sql = "UPDATE "
                      + "    ETIQUETA "
                      + "SET "
                      + "    ETIQ_ESTADO = '" +estado + "' "
                      + "WHERE "
                      + "    ETIQ_ID = '" + id + "' "
                      + "AND ETIQ_TIPO = '" + tipo + "'";
            return sql;
        }

        public bool actualizarEtiquetaUsuario(string id, string tipo, string usuario, string motivo, string caducidad, string registradoPor)
        {
            string sql = "UPDATE"
                        + "     BOOTPARK.ETIQUETAUSUARIO"
                        + " SET"
                        + "     ETUS_MOTIVO = '" + motivo + "',"
                        + "     ETUS_FECHACADUCIDAD = " + caducidad + ","
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
            List<string> sentencia = new List<string>();

            string sql = "DELETE"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETAUSUARIO"
                        + " WHERE"
                        + "     ETIQ_ID = '" + id + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     USUA_ID = '" + usuario + "'";
            sentencia.Add(sql);
            sentencia.Add(ActualizarEtiquetaEstado(id, tipo, "DISPONIBLE"));
            return connection.sendSetDataTransaction(sentencia);
        }


    }
}