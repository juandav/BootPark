using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class ParticularOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarParticulares() {
            string sql = "SELECT * FROM BOOTPARK.PARTICULAR";
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
        public bool registrarParticular() {
            string sql = "INSERT INTO BOOTPARK.particular (PART_ID, PART_IDENTIFICACION, PART_NOMBRE, PART_APELLIDO, PART_REGISTRADOPOR, PART_FECHACAMBIO) VALUES (2, 1117515612, 'Lineth Johana', 'Figueroa', '53233', CURRENT_DATE())";
            return connection.sendSetDataMariaDB(sql);
        }

    }
}