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

        public DataTable FindUser(string user) {

            string sql = @"
                SELECT *
                FROM   usuario U
                WHERE  U.tipousuario = 'DOCENTE'
                       AND U.pege_id = '" + user + @"' 
            ";
            return _CONN.GetData(sql).Tables[0];
        }

        public DataTable FindVehicle(string tag, string user) {
            string sql = @"
                SELECT *
                FROM   vehiculo V
                       INNER JOIN etiqueta E
                               ON ( v.vehi_id = E.etiq_id )
                       INNER JOIN autorizacion A
                               ON ( V.vehi_id = A.vehi_id )
                WHERE  E.etiq_etiqueta = '" + tag + @"'
                       AND A.usua_id = '" + user + @"' 
            ";
            return _CONN.GetData(sql).Tables[0];
        }

        public bool CreateCirculation(string tag, string user)
        {
            DataTable Max = GetMaxCirculation(tag, user);
            bool isEmpty = Max.Rows[0]["CIRC_ID"].ToString() == "";
            string type;

            if (isEmpty)
            {
                type = "ENTRADA";
            }
            else {
                string sql2 = @"
                    SELECT C.CIRC_TIPO
                    FROM   circulacion C
                    WHERE  C.CIRC_ID = '" + Max.Rows[0]["CIRC_ID"].ToString() + @"' 
                ";
                string circ = _CONN.GetData(sql2).Tables[0].Rows[0]["CIRC_TIPO"].ToString() ;

                if (circ == "ENTRADA")
                {
                    type = "SALIDA";
                }
                else {
                    type = "ENTRADA";
                }
            }

            string sql = @"
                INSERT INTO `circulacion`
                            (`circ_tipo`,
                             `circ_observacion`,
                             `circ_fechacircula`,
                             `circ_registradopor`,
                             `circ_fechacambio`,
                             `vehi_id`,
                             `usua_id`,
                             `term_id`)
                VALUES      ('" + type + @"',
                             'SYSTEM',
                             NOW(),
                             'SYSTEM',
                             NOW(),
                             " + @"(SELECT V.vehi_id
                                    FROM vehiculo V
                                        INNER JOIN etiqueta E ON ( V.vehi_id = E.etiq_id)
                                        WHERE E.etiq_etiqueta = '" + tag + @"'" + @"),
                             '" + user + @"',
                             '1') 
            ";
            return _CONN.SendData(sql);

        }

        private DataTable GetMaxCirculation(string tag, string user) {
            string sql = @"
                SELECT Max(C.circ_id) AS CIRC_ID
                FROM   circulacion C
                WHERE  C.usua_id = '" + user + @"'
                       AND C.vehi_id = (SELECT V.vehi_id
                                        FROM   vehiculo V
                                               INNER JOIN etiqueta E ON ( V.vehi_id = E.etiq_id )
                                        WHERE E.etiq_etiqueta = '" + tag + @"')
            ";
            return _CONN.GetData(sql).Tables[0];
        }
        
    }
}