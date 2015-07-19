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
                        + "     U.IDBOOTPARK AS ID,"
                        + "     U.PART_IDENTIFICACION AS IDENT,"
                        + "     U.PART_NOMBRE AS NOMBRE,"
                        + "     U.PART_APELLIDO AS APELLIDO,"
                        + "     U.TIPO"
                        + " FROM"
                        + "     USUARIO U";
            return connection.getDataMariaDB(sql).Tables[0];
        }

    }
}