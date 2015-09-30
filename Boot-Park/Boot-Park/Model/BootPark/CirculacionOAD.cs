using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class CirculacionOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        /// <summary>
        ///     Valida el usuario del biometrico con el tag registrado en base de datos
        /// </summary>
        public string ValidarUsuario(string user, string tag, string tipo) {
            string sql = "";

            if (tipo.Equals("Huella")) {
                sql = "";
            }

            if (tipo.Equals("Tag")) {
                sql = "";
            }


            
            return connection.getDataMariaDB(sql).Tables[0].ToString();
        }

    }
}