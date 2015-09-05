using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class UsuarioOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarUsuarios() {
            string sql = "SELECT"
                        + "     U.PEGE_ID AS ID,"
                        + "     U.IDENTIFICACION AS IDENT,"
                        + "     U.NOMBRE AS NOMBRE,"
                        + "     U.APELLIDO AS APELLIDO,"
                        + "     U.TIPOUSUARIO"
                        + " FROM"
                        + "     USUARIO U";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public DataTable consultarUsuariosChaira()
        {
            string sql = "SELECT"
                        + "     U.PEGE_ID AS ID,"
                        + "     U.IDENTIFICACION AS IDENT,"
                        + "     U.NOMBRE AS NOMBRE,"
                        + "     U.APELLIDO AS APELLIDO,"
                        + "     U.TIPOUSUARIO"
                        + " FROM"
                        + "     USUARIO U"
                        + " WHERE U.TIPOUSUARIO='DOCENTE'";
      
            return connection.getDataMariaDB(sql).Tables[0];
        }
        
        public DataTable consultarUsuarios(string usuario)
        {
            string sql = "SELECT"
                        + "     U.PEGE_ID AS ID,"
                        + "     U.IDENTIFICACION AS IDENT,"
                        + "     U.NOMBRE AS NOMBRE,"
                        + "     U.APELLIDO AS APELLIDO,"
                        + "     U.TIPOUSUARIO AS TIPO"
                        + " FROM "
                        + "    USUARIO U "
                        + " WHERE U.PEGE_ID <> '" + usuario + "'";
                        ;

            return connection.getDataMariaDB(sql).Tables[0];
        }

    }
}