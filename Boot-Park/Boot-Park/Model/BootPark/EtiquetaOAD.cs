using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class EtiquetaOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarEtiquetas()
        {
            string sql = "";
            return connection.getDataMariaDB(sql).Tables[0];
        }

        public bool registrarEtoquetas()
        {
            List<string> sql = new List<string>() {
                "",
                ""
            };
            return connection.sendSetDataTransaction(sql);
        }

        public bool registrarEtiqueta()
        {
            string sql = "";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool actualizarEtiqueta()
        {
            string sql = "";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarEtiqueta()
        {
            string sql = "";
            return connection.sendSetDataMariaDB(sql);
        }

    }
}