using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Circulation.conecction;
using Ext.Net;

namespace Circulation.controller.bootpark.circulacion
{
    public class CirculacionOAD
    {
        private Connect _CONN = new Connect();

        public DataTable FindUser(string user) {

            string sql = @"
                SELECT *
                FROM   usuario U
                WHERE  U.pege_id = '" + user + @"' 
            ";
            return _CONN.GetData(sql).Tables[0];
        }

        public DataTable FindVehicle(string tag, string user) {
            string sql = @"
                        SELECT VEH.*, 
                                   MV.mave_marca 
                            FROM   (SELECT DISTINCT EV.vehi_id 
                                    FROM   etiqueta E 
                                           INNER JOIN etiquetavehiculo EV 
                                                   ON EV.etiq_id = E.etiq_id 
                                    WHERE  E.etiq_etiqueta = '"+ tag + @"') AS V 
                                   INNER JOIN autorizacion AU 
                                           ON AU.vehi_id = V.vehi_id 
                                   INNER JOIN vehiculo VEH 
                                           ON AU.vehi_id = VEH.vehi_id 
                                   INNER JOIN marcavehiculo MV 
                                           ON VEH.mave_id = MV.mave_id 
                            WHERE  AU.usua_id = '"+  user + @"' 
            ";
            return _CONN.GetData(sql).Tables[0];
        }

        public bool CreateCirculation(string tag, string user)
        {
            DataTable Max = GetMaxCirculation(tag, user);
            bool isEmpty = Max.Rows[0]["CIRC_ID"].ToString() == "" ? true: false ;
            string clock = DateTime.Now.ToString("hh:mm:ss");
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
                             `circ_horacircula`,                             
                             `circ_registradopor`,
                             `circ_fechacambio`,
                             `vehi_id`,
                             `usua_id`)
                VALUES      ('" + type + @"',
                             'SYSTEM',
                             NOW(),
                             '" + clock + @"',                                   
                             'SYSTEM',
                             NOW(),
                             " + @"(SELECT DISTINCT
                                        EV.VEHI_ID
                                    FROM
                                        ETIQUETA E
                                    INNER JOIN ETIQUETAVEHICULO EV
                                    ON
                                        EV.ETIQ_ID= E.ETIQ_ID
                                    WHERE
                                        E.ETIQ_ETIQUETA = '" + tag + @"'" + @"),
                             '" + user + @"') 
            ";

            X.AddScript(" App.LESTADO.setText('" + type + ": " + clock + "');");
            return _CONN.SendData(sql);

        }

        public DataTable QueTipoEs() {
            string sql = @"
                SELECT C.circ_tipo         AS TIPO, 
                        Concat('Fecha: ', Date_format(C.circ_fechacircula, '%d %b %y'), 
                        ', Hora: ', 
                        C.circ_horacircula) AS MENSAJE
                FROM   circulacion C 
                WHERE  C.circ_id = (SELECT Max(C2.circ_id) 
                                    FROM   circulacion C2) 
            ";
            return _CONN.GetData(sql).Tables[0];
        }

        private DataTable GetMaxCirculation(string tag, string user) {
            string sql = @"
                SELECT Max(C.circ_id) AS CIRC_ID
                FROM   circulacion C
                WHERE  C.usua_id = '" + user + @"'
                           AND C.vehi_id = (SELECT DISTINCT
                                            EV.VEHI_ID
                                        FROM
                                            ETIQUETA E
                                        INNER JOIN ETIQUETAVEHICULO EV
                                        ON
                                            EV.ETIQ_ID= E.ETIQ_ID
                                        WHERE
                                            E.ETIQ_ETIQUETA = '" + tag + @"')
            ";
            return _CONN.GetData(sql).Tables[0];
        }
        
    }
}