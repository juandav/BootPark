using Boot_Park.Conections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boot_Park.Model.BootPark
{
    public class HuellaOAD
    {
        private ConexionMariaDB connection = new ConexionMariaDB();
        public bool registraHuella(string IdUsuario, int FingerIndex, string Huella, int Flag, int Length, string registradoPor)
        {
            String sql = "INSERT "
                         + "INTO "
                         + "    HUELLA "
                         + "    ( "
                         + "        HUEL_FLAG, "
                         + "        HUEL_FINGERINDEX, "
                         + "        HUEL_HUELLA, "
                         + "        HUEL_REGISTRADOPOR, "
                         + "        HUEL_FECHACAMBIO, "
                         + "        USUA_ID, "
                         + "        HUEL_LENGTH "
                         + "    ) "
                         + "    VALUES "
                         + "    ( "
                         + "        " + Flag + ", "
                         + "        " + FingerIndex + ", "
                         + "        '" + Huella + "' , "
                         + "       '" + registradoPor + "', "
                         + "        CURRENT_DATE(), "
                         + "       '" + IdUsuario + "', "
                         + "        " + Length + " "
                         + "    )";
            return connection.sendSetDataMariaDB(sql);

        }
        /// <summary>
        /// Consulta todas las Huellas, para registrarlas en el lector Biometrico.
        /// </summary>
        /// <returns></returns>
        public DataTable uploadHuellaRegistradas() {
            string sql = "SELECT "
                        + "    HUEL_FINGERINDEX, "
                        + "    HUEL_HUELLA, "
                        + "    USUA_ID, "
                        + "    HUEL_LENGTH, "
                        + "    HUEL_FLAG "
                        + "FROM "
                        + "    HUELLA";
            return connection.getDataMariaDB(sql).Tables[0];
        }
        /// <summary>
        /// Toma una huella de la BD para Registrarla en el lector Biometrico
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public DataTable uploadHuellaUsuario(string usuario) {
            string sql = "SELECT "
                      + "    HUEL_FINGERINDEX, "
                      + "    HUEL_HUELLA, "
                      + "    USUA_ID, "
                      + "    HUEL_LENGTH, "
                      + "    HUEL_FLAG "
                      + "FROM "
                      + "    HUELLA "
                      + "WHERE USUA_ID='" + usuario + "'";
            return connection.getDataMariaDB(sql).Tables[0];
        }
    }
}