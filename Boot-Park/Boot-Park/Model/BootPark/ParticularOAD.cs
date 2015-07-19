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
                        + "        BOOTPARK.particular P";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public bool registrarParticulares() {
            List<string> sql = new List<string>() {
                "INSERT INTO BOOTPARK.particular (PART_ID, PART_IDENTIFICACION, PART_NOMBRE, PART_APELLIDO, PART_REGISTRADOPOR, PART_FECHACAMBIO) VALUES (7, 1117515612, 'Lineth Johana', 'Figueroa', '53233', CURRENT_DATE())",
                "INSERT INTO BOOTPARK.particular (PART_ID, PART_IDENTIFICACION, PART_NOMBRE, PART_APELLIDO, PART_REGISTRADOPOR, PART_FECHACAMBIO) VALUES (6, 1117515612, 'Lineth Johana', 'Figueroa', '53233', CURRENT_DATE())"
            };
            return connection.sendSetDataTransaction(sql);
        }

        //int id, int identificacion, string nombre, string apellido, string registradoPor
        public bool registrarParticular(string id, string identificacion, string nombre, string apellido, string registradoPor) {
            string sql = "INSERT INTO bootpark.particular"
                        + "             (part_id,"
                        + "              part_identificacion,"
                        + "              part_nombre,"
                        + "              part_apellido,"
                        + "              part_registradopor,"
                        + "              part_fechacambio)"
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
            string sql = "UPDATE bootpark.particular"
                        + " SET "
                        + "      part_identificacion = " + identificacion + ","
                        + "      part_nombre = '" + nombre + "',"
                        + "      part_apellido = '" +apellido + "',"
                        + "      part_registradopor = '" + registradoPor + "',"
                        + "      part_fechacambio = CURRENT_DATE()"
                        + " WHERE"
                        +"       part_id = " + id;
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarParticular(string id)
        {
            string sql = "DELETE"
                        + "  FROM   bootpark.PARTICULAR"
                        + "  WHERE  "
                        + "         PART_ID = " + id;
            return connection.sendSetDataMariaDB(sql);
        }

    }
}