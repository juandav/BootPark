using Boot_Park.Conections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boot_Park.Model.BootPark
{
    public class TerminalOAD
    {
        private ConexionMariaDB connection = new ConexionMariaDB();
        public DataTable consultarTerminales()
        {
            string sql = "SELECT "
                       + "    TERM_ID, "
                       + "    TERM_PUERTO, "
                       + "    TERM_IP, "
                       + "    TERM_TIPO "
                       + "FROM "
                       + "    BOOTPARK.TERMINAL";
            return connection.getDataMariaDB(sql).Tables[0];
        }
        public bool registrarTerminal(string id, string puerto, string ip, string tipo, string registradoPor)
        {
            string sql = "INSERT "
                       + "INTO "
                       + "    TERMINAL "
                       + "    ( "
                       + "        TERM_ID, "
                       + "        TERM_PUERTO, "
                       + "        TERM_IP, "
                       + "        TERM_TIPO, "
                       + "        TERM_REGISTRADOPOR, "
                       + "        TERM_FECHACAMBIO "
                       + "    ) "
                       + "    VALUES "
                       + "    ( "
                       + "        '" + id + "', "
                       + "        '" + puerto + "', "
                       + "        '" + ip + "', "
                       + "        '" + tipo + "', "
                       + "        '" + registradoPor + "', "
                       + "         CURRENT_DATE() "
                       + "    )";
            return connection.sendSetDataMariaDB(sql);
        }
        public bool actualizarTerminal(string id, string puerto, string ip, string tipo, string registradoPor)
        {
            string sql = "UPDATE "
                       + "    TERMINAL "
                       + "SET "
                       + "    TERM_PUERTO = '" + puerto + "', "
                       + "    TERM_IP = '" + ip + "', "
                       + "    TERM_REGISTRADOPOR = '" + registradoPor + "', "
                       + "    TERM_FECHACAMBIO = CURRENT_DATE(), "
                       + "    TERM_TIPO = '" + tipo + "' "
                       + "WHERE "
                       + "    TERM_ID = '" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }
        public bool eliminarTerminal(string id)
        {
            string sql = "DELETE "
                       + "FROM "
                       + "    TERMINAL "
                       + "WHERE "
                       + "    TERM_ID = '" + id + "'";
            return connection.sendSetDataMariaDB(sql);
        }

    }
}