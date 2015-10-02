using System.Data;
using Boot_Park.Conections;
using System.Collections.Generic;

namespace Boot_Park.Model.BootPark
{
    public class CirculacionOAD
    {

        private ConexionMariaDB _connection = new ConexionMariaDB();

        public DataTable ValidarUsuario(string user) {
            string sql = ""
                + "SELECT DISTINCT "
                + " "
                + "IF ( "
                + "	COUNT(A.USUA_ID) > 0, "
                + "	TRUE, "
                + "	FALSE "
                + ") AS EXISTENCIA "
                + "FROM "
                + "	BOOTPARK.AUTORIZACION A "
                + "WHERE "
                + "	A.USUA_ID = " + user;
            return _connection.getDataMariaDB(sql).Tables[0];
        }

        /// <summary>
        ///     Valida el usuario del biometrico con el tag registrado en base de datos
        /// </summary>
        public DataTable ValidarTagAndHuella(string user, string tag, string tipo) {
            string sql = ""
                + "SELECT DISTINCT "
                + "IF ( "
                + "	COUNT(A.USUA_ID) > 0, "
                + "	TRUE, "
                + "	FALSE "
                + ") AS VALIDACION, "
                + " A.VEHI_ID AS VEHICULO "
                + "FROM "
                + "	BOOTPARK.ETIQUETA E "
                + "INNER JOIN BOOTPARK.ETIQUETAVEHICULO EV ON (E.ETIQ_ID = EV.ETIQ_ID) "
                + "INNER JOIN BOOTPARK.AUTORIZACION A ON (EV.VEHI_ID = A.VEHI_ID) "
                + "WHERE "
                + "	E.ETIQ_ETIQUETA = '" + tag + "' "
                + "AND A.USUA_ID = " + user;

            return _connection.getDataMariaDB(sql).Tables[0];
        }

        public DataTable ComprobarTipoTerminal(string ip, string puerto) {
            string sql = ""
                + "SELECT DISTINCT "
                + "	 "
                + "IF ( "
                + "	COUNT(T.TERM_ID) > 0, "
                + "	TRUE, "
                + "	FALSE "
                + ") AS EXISTENCIA, "
                + " "
                + "T.TERM_TIPO AS TIPO "
                + "FROM "
                + "	BOOTPARK.TERMINAL T "
                + "WHERE "
                + "	T.TERM_IP = '" + ip + "' "
                + "AND T.TERM_PUERTO = '" + puerto + "'";
            return _connection.getDataMariaDB(sql).Tables[0];
        }

        public DataTable ConsultarUsuarioCirculacion(string user) {
            string sql = ""
                + "SELECT "
                + "	U.IDENTIFICACION, "
                + "	U.NOMBRE, "
                + "	U.APELLIDO, "
                + "	U.TIPOUSUARIO "
                + "FROM "
                + "	BOOTPARK.USUARIO U "
                + "WHERE "
                + "	U.PEGE_ID = ''";
            return _connection.getDataMariaDB(sql).Tables[0];
        }

        public bool RegistrarCirculacion(string user, string vehiculo, string circId, string tipo, string pegeId) {
            string sql = ""
                + "INSERT INTO BOOTPARK.CIRCULACION ( "
                + "	CIRCULACION.CIRC_ID, "
                + "	CIRCULACION.CIRC_OBSERVACION, "
                + "	CIRCULACION.CIRC_TIPO, "
                + "	CIRCULACION.CIRC_FECHACAMBIO, "
                + "	CIRCULACION.CIRC_FECHACIRCULA, "
                + "	CIRCULACION.CIRC_REGISTRADOPOR, "
                + "	CIRCULACION.USUA_ID, "
                + "	CIRCULACION.VEHI_ID "
                + ") "
                + "VALUES "
                + "	( "
                + "		'" + circId + "', "
                + "		'AUTOMATICO DE CHAIRA', "
                + "		'" + tipo + "', "
                + "		CURRENT_DATE (), "
                + "		CURRENT_DATE (), "
                + "		'" + pegeId + "', "
                + "		'" + user + "', "
                + "		'" + vehiculo + "' "
                + "	)";
            return _connection.sendSetDataMariaDB(sql);
        }
    }
}