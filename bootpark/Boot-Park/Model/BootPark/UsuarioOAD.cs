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

         public DataTable consultarUsuariosHuellas()
        {
            string sql = "SELECT "
                        + "    U.PEGE_ID         AS ID, "
                        + "    U.IDENTIFICACION AS IDENT, "
                        + "    U.NOMBRE         AS NOMBRE, "
                        + "    U.APELLIDO       AS APELLIDO, "
                        + "    U.TIPOUSUARIO AS TIPO, "
                        + "IF( "
                        + "    ( "
                        + "        SELECT "
                        + "            COUNT(*) "
                        + "        FROM "
                        + "            HUELLA "
                        + "        WHERE "
                        + "            USUA_ID = U.PEGE_ID "
                        + "    ) "
                        + "    != 0, 'EXISTE','NO_EXISTE') AS HUEL_ESTADO "
                        + "FROM "
                        + "    USUARIO U";
            
                         
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
                        + " WHERE U.TIPOUSUARIO !='NOCHAIRA'";
      
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
        public DataTable consultarPermisoUsuarioOut(string codvehiculo) {

            string sql = @"SELECT
                            U.PEGE_ID        AS ID,
                            U.IDENTIFICACION AS IDENT,
                            U.NOMBRE         AS NOMBRE,
                            U.APELLIDO       AS APELLIDO,
                            U.TIPOUSUARIO    AS TIPO
                        FROM
                            USUARIO U
                        LEFT JOIN 
                            
                            (
                                SELECT
                                    A.USUA_ID
                                FROM
                                    AUTORIZACION A
                                WHERE
                                    A.VEHI_ID='" + codvehiculo + @"' 
                            ) R
                            ON U.PEGE_ID = R.USUA_ID WHERE R.USUA_ID IS NULL";
            return connection.getDataMariaDB(sql).Tables[0];
      }
    
        public DataTable consultarPermisoUsuarioIn(string codvehiculo)
        {
            string sql = @"SELECT
                            U.PEGE_ID        AS ID,
                            U.IDENTIFICACION AS IDENT,
                            U.NOMBRE         AS NOMBRE,
                            U.APELLIDO       AS APELLIDO,
                            U.TIPOUSUARIO    AS TIPO
                        FROM
                            USUARIO U
                        INNER JOIN
                            (
                                SELECT
                                    A.USUA_ID
                                FROM
                                    AUTORIZACION A
                                WHERE
                                    A.VEHI_ID='" + codvehiculo + @"'
                                AND A.AUTO_TIPO='PARTICULAR'
                            )
                            R
                        ON
                            U.PEGE_ID= R.USUA_ID";

            return connection.getDataMariaDB(sql).Tables[0];
        }

    }
}