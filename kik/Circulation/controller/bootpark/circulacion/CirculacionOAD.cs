using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Circulation.conecction;

namespace Circulation.controller.bootpark.circulacion
{
    public class CirculacionOAD
    {
        private Connect _CONN = new Connect();

        public DataTable ValidateUser() {
            string sql = "";
            return _CONN.GetData(sql).Tables[0];
        }

        public DataTable ValidateLabelAndFingerprint() {
            string sql = "";
            return _CONN.GetData(sql).Tables[0];
        }

        public DataTable CheckTypeTerminal() {
            string sql = "";
            return _CONN.GetData(sql).Tables[0];
        }

        public DataTable FindUser(string user) {

            string sql = "";
            return _CONN.GetData(sql).Tables[0];
        }

        public bool CreateCirculation(string user, string vehicle, string circulartion, string type, string pege) {

            string sql = "";
            return _CONN.SendData(sql);

        }
        public DataTable CheckTypeCirculation(string vehicle) {

            string sql = string.Format
            (
                @"SELECT 
                    c.CIRC_TIPO
                 FROM
                     bootpark.circulacion c
                 WHERE
                    c.VEHI_ID='{1}' 
                 ORDER BY
                    CIRC_ID ASC
                 LIMIT 1", vehicle
            );
            return _CONN.GetData(sql).Tables[0];
        }
    }
}