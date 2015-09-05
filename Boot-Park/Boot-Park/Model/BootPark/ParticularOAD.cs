using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class ParticularOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarParticulares() {
            string sql = "SELECT   P.PART_ID,"
                        + "        P.PART_IDENTIFICACION,"
                        + "        P.PART_NOMBRE,"
                        + "        P.PART_APELLIDO,"
                        + "        P.PART_REGISTRADOPOR,"
                        + "        P.PART_FECHACAMBIO"
                        + " FROM  " 
                        + "        BOOTPARK.PARTICULAR P";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public bool registrarParticulares() {
            List<string> sql = new List<string>() {
                "INSERT INTO BOOTPARK.PARTICULAR (PART_ID, PART_IDENTIFICACION, PART_NOMBRE, PART_APELLIDO, PART_REGISTRADOPOR, PART_FECHACAMBIO) VALUES (7, 1117515612, 'Lineth Johana', 'Figueroa', '53233', CURRENT_DATE())",
                "INSERT INTO BOOTPARK.PARTICULAR (PART_ID, PART_IDENTIFICACION, PART_NOMBRE, PART_APELLIDO, PART_REGISTRADOPOR, PART_FECHACAMBIO) VALUES (6, 1117515612, 'Lineth Johana', 'Figueroa', '53233', CURRENT_DATE())"
            };
            return connection.sendSetDataTransaction(sql);
        }

        //int id, int identificacion, string nombre, string apellido, string registradoPor
        public bool registrarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor) {
            string sql = "INSERT INTO BOOTPARK.PARTICULAR"
                        + "             (PART_ID,"
                        + "              PART_IDENTIFICACION,"
                        + "              PART_NOMBRE,"
                        + "              PART_APELLIDO,"
                        + "              PART_REGISTRADOPOR,"
                        + "              PART_FECHACAMBIO)"
                        + " VALUES      (" + id + ","
                        + "              " + identificacion + ","
                        + "             '" + nombre  + "',"
                        + "             '" + apellido + "',"
                        + "             '" + registradoPor + "',"
                        + "              CURRENT_DATE()) ";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor)
        {
            string sql = "UPDATE BOOTPARK.PARTICULAR"
                        + " SET "
                        + "      PART_IDENTIFICACION = " + identificacion + ","
                        + "      PART_NOMBRE = '" + nombre + "',"
                        + "      PART_APELLIDO = '" +apellido + "',"
                        + "      PART_REGISTRADOPOR = '" + registradoPor + "',"
                        + "      PART_FECHACAMBIO = CURRENT_DATE()"
                        + " WHERE"
                        +"       PART_ID = " + id;
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarParticular(string id)
        {
            string sql = "DELETE"
                        + "  FROM   BOOTPARK.PARTICULAR"
                        + "  WHERE  "
                        + "         PART_ID = " + id;
            return connection.sendSetDataMariaDB(sql);
        }

    }
}