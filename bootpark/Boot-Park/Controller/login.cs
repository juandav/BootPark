using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Boot_Park.Conections;

namespace Boot_Park.Controller
{
    public class login
    {

        private ConexionMariaDB connect = new ConexionMariaDB();

        public DataTable FindAccount() {

            string sql = @"
                SELECT * FROM BOOTPARK.USUARIO
            ";
            return connect.getDataMariaDB(sql).Tables[0];
        }
    }
}