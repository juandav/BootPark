using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model
{
    public class General
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public string  nextPrimaryKey(string TABLA, string TABL_ID)
        {
            //string sql = "SELECT"
            //            + "     (NVL(MAX(T." + TABL_ID + "),0)+1) AS PK"
            //            + " FROM"
            //            + "     GENERAL." + TABLA + " T";
            string sql = "SELECT"
                        + "     IFNULL(NULL, MAX(T." + TABL_ID + ") + 1) AS PK"
                        + " FROM "
                        +       TABLA + " T";
            return connection.getDataMariaDB(sql).Tables[0].Rows[0]["PK"].ToString();
        }

    }
}