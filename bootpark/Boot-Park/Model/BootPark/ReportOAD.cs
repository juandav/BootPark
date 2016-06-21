using Boot_Park.Conections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boot_Park.Model.BootPark
{
    public class ReportOAD
    {
        private ConexionMariaDB connection = new ConexionMariaDB();
        public DataTable ListarVehiculosRegistrados()
        {
            string sql = @"SELECT DISTINCT
                                UPPER(CONCAT(MV.MAVE_MARCA,' ',V.VEHI_PLACA)) AS VEHICULO,
                                CE.CIRC_TIPO                                  AS ENTRADA,
                                CONCAT(UN.NOMBRE,' ',UN.APELLIDO)             AS USUARIOENTRADA,
                                Date_format( CE.CIRC_FECHACIRCULA,'%d-%m-%Y') AS FECHAINI,
                                CE.CIRC_HORACIRCULA                           AS HORAINI,
                                CS.CIRC_TIPO                                  AS SALIDA,
                                CS.USUARIOSALIDA                              AS USUARIOSALIDA,
                                Date_format(CS.CIRC_FECHACIRCULA,'%d-%m-%Y')  AS FECHASAL,
                                CS.CIRC_HORACIRCULA                           AS HORAFIN,
                                IF(CS.CIRC_HORACIRCULA=NULL,CS.CIRC_HORACIRCULA,timediff(CS.CIRC_HORACIRCULA,
                                CE.CIRC_HORACIRCULA)) AS TIEMPO
                            FROM
                                CIRCULACION CE
                            LEFT JOIN
                                (
                                    SELECT
                                        C.*,
                                        CONCAT(U.NOMBRE,' ',U.APELLIDO) AS USUARIOSALIDA
                                    FROM
                                        CIRCULACION C
                                    INNER JOIN USUARIO U
                                    ON
                                        C.USUA_ID=U.PEGE_ID
                                    WHERE
                                        C.CIRC_TIPO='SALIDA'
                                ) AS CS
                            ON
                                CE.VEHI_ID = CS.VEHI_ID
                            AND CE.CIRC_FECHACIRCULA = CS.CIRC_FECHACIRCULA
                            INNER JOIN USUARIO UN
                            ON
                                CE.USUA_ID = UN.PEGE_ID
                            INNER JOIN VEHICULO V
                            ON
                                CE.VEHI_ID = V.VEHI_ID
                            INNER JOIN MARCAVEHICULO MV
                            ON
                                V.MAVE_ID = MV.MAVE_ID
                            WHERE
                                CE.CIRC_TIPO='ENTRADA'";
            return connection.getDataMariaDB(sql).Tables[0];
        }
        public DataTable ConsultarVehiculoActuales() {
            string sql = @"SELECT DISTINCT
                                CE.CIRC_ID AS CODIGO,
                                UPPER(CONCAT(MV.MAVE_MARCA, ' ', V.VEHI_PLACA)) AS VEHICULO,
                                CE.CIRC_TIPO AS ENTRADA,
                                CONCAT(UN.NOMBRE, ' ', UN.APELLIDO)             AS USUARIOENTRADA,
                                Date_format( CE.CIRC_FECHACIRCULA,'%d-%m-%Y') AS FECHAINI,
                                CE.CIRC_HORACIRCULA AS HORAINI
                            FROM
                                CIRCULACION CE
                            LEFT JOIN
                                (
                                    SELECT
                                        C.*
                                    FROM
                                        CIRCULACION C
                                    WHERE
                                        C.CIRC_TIPO = 'SALIDA'
                                ) AS CS
                            ON
                                CE.VEHI_ID = CS.VEHI_ID
                            AND CE.CIRC_FECHACIRCULA = CS.CIRC_FECHACIRCULA
                            INNER JOIN USUARIO UN
                            ON
                                CE.USUA_ID = UN.PEGE_ID
                            INNER JOIN VEHICULO V
                            ON
                                CE.VEHI_ID = V.VEHI_ID
                            INNER JOIN MARCAVEHICULO MV
                            ON
                                V.MAVE_ID = MV.MAVE_ID
                            WHERE
                                CE.CIRC_TIPO = 'ENTRADA' AND CS.VEHI_ID IS NULL";
            return connection.getDataMariaDB(sql).Tables[0];
        }
    }
}