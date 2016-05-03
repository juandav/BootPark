﻿using Boot_Park.Conections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boot_Park.Model.BootPark
{
    public class EtiquetaVehiculoOAD
    {

        private ConexionMariaDB connection = new ConexionMariaDB();

        public DataTable consultarEtiquetaDisponible()
        {
            string sql = "SELECT"
                        + "    E.ETIQ_ID,"
                        + "    E.ETIQ_TIPO,"
                        + "    E.ETIQ_ETIQUETA,"
                        + "    E.ETIQ_DESCRIPCION,"
                        + "    E.ETIQ_OBSERVACION"
                        + " FROM"
                        + "    BOOTPARK.ETIQUETA E"
                        + " WHERE "
                        + "    E.ETIQ_ESTADO = 'DISPONIBLE' AND"
                        + "    E.ETIQ_TIPO = 'TAG' AND"
                        + "    E.ETIQ_ID NOT IN (SELECT EV.ETIQ_ID FROM BOOTPARK.ETIQUETAVEHICULO EV)";
            return connection.getDataMariaDB(sql).Tables[0];

        }

        public DataTable consultarEtiquetaVehiculoEnUso(string vehiculo)
        {
            string sql = "SELECT"
                        + "    EV.VEHI_ID, "
                        + "    EV.ETIQ_ID, "
                        + "    E.ETIQ_ETIQUETA, "
                        + "    E.ETIQ_DESCRIPCION, "
                        + "    EV.ETVE_OBSERVACION "
                        + "FROM "
                        + "    BOOTPARK.ETIQUETAVEHICULO EV "
                        + "INNER JOIN BOOTPARK.ETIQUETA E "
                        + "ON "
                        + "    ( "
                        + "        EV.ETIQ_ID=E.ETIQ_ID "
                        + "    ) "
                        + "WHERE "
                        + "    EV.VEHI_ID = '" + vehiculo + "' "
                        + "AND EV.ETIQ_TIPO = 'TAG'";
                        
            return connection.getDataMariaDB(sql).Tables[0];
        }


        public bool registrarEtiquetaVehiculo(string etiqueta, string tipo, string vehiculo, string observacion, string registradoPor)
        {
            List<string> sentencia = new List<string>();

            string sql = "INSERT"
                        + " INTO"
                        + "     BOOTPARK.ETIQUETAVEHICULO"
                        + "  ( "
                        + "        ETIQ_ID, "
                        + "        ETIQ_TIPO, "
                        + "        VEHI_ID, "
                        + "        ETVE_OBSERVACION, "
                        + "        ETVE_FECHACAMBIO, "
                        + "        ETVE_REGISTRADOPOR "
                        + "    )"
                        + "     VALUES"
                        + "     ("
                        + "         " + etiqueta + ","
                        + "         '" + tipo + "',"
                        + "         '" + vehiculo + "',"
                        + "         '" + observacion + "',"
                        + "              CURRENT_DATE(),"
                        + "          " + registradoPor
                        + "     )";
            sentencia.Add(sql);
            sentencia.Add(ActualizarTagEstado(etiqueta, tipo, "ENUSO"));
            return connection.sendSetDataTransaction(sentencia);
        }
        public string ActualizarTagEstado(string etiqueta, string tipo, string estado) {
            string sql = "UPDATE "
                    + "    ETIQUETA "
                    + "SET "
                    + "    ETIQ_ESTADO = '" + estado + "' "
                    + "WHERE "
                    + "    ETIQ_ID = '" + etiqueta + "' "
                    + "AND ETIQ_TIPO = '" + tipo + "'";
            return sql;
        }
        
        public bool actualizarEtiquetaVehiculo(string etiqueta, string tipo, string vehiculo, string observacion, string registradoPor)
        {
            string sql = "UPDATE"
                        + "     BOOTPARK.ETIQUETAVEHICULO "
                        + " SET"
                        + "     ETVE_OBSERVACION = '" + observacion + "',"
                        + "     ETVE_REGISTRADOPOR = '" + registradoPor + "',"
                        + "     ETVE_FECHACAMBIO = CURRENT_DATE()"
                        + " WHERE"
                        + "     ETIQ_ID = '" + etiqueta + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     VEHI_ID = '" + vehiculo + "'";
            return connection.sendSetDataMariaDB(sql);
        }

        public bool eliminarEtiquetaVehiculo(string etiqueta, string tipo, string vehiculo)
        {
            List<string> sentencia = new List<string>();
            string sql = "DELETE"
                        + " FROM"
                        + "     BOOTPARK.ETIQUETAVEHICULO"
                        + " WHERE"
                        + "     ETIQ_ID = '" + etiqueta + "' AND"
                        + "     ETIQ_TIPO = '" + tipo + "' AND"
                        + "     VEHI_ID = '" + vehiculo + "'";
            sentencia.Add(sql);
            sentencia.Add(ActualizarTagEstado(etiqueta, tipo, "DISPONIBLE"));
            return connection.sendSetDataTransaction(sentencia);
        }
        public DataTable ConsultarVehiculoTagAsignado(string tag) {
           /*string sql = "SELECT   "
                        + "CONCAT('', v.VEHI_PLACA, ' ', v.VEHI_MARCA, ' ', v.VEHI_MODELO) AS IdVehiculo,v.VEHI_ID "
                        + "FROM   bootpark.vehiculo v "
                        + "       INNER JOIN bootpark.etiquetavehiculo ev "
                        + "               ON v.vehi_id = ev.vehi_id "
                        + "       INNER JOIN bootpark.etiqueta e "
                        + "               ON ev.etiq_id = e.etiq_id "
                        + "WHERE  e.etiq_tipo = 'TAG' "
                        + "       AND e.etiq_estado = 'ENUSO' "
                        + "       AND e.etiq_etiqueta = '" + tag + "'";*/

           string sql = @" SELECT
                                CONCAT('', v.VEHI_PLACA, ' ', mv.MAVE_MARCA, ' ', v.VEHI_MODELO) AS IdVehiculo,
                                v.VEHI_ID
                            FROM
                                bootpark.vehiculo v
                            INNER JOIN bootpark.etiquetavehiculo ev
                            ON
                                v.vehi_id = ev.vehi_id
                            INNER JOIN bootpark.etiqueta e
                            ON
                                ev.etiq_id = e.etiq_id
                            INNER JOIN bootpark.marcavehiculo mv
                            ON
                                v.MAVE_ID = mv.MAVE_ID
                            WHERE
                                e.etiq_tipo = 'TAG'
                            AND e.etiq_estado = 'ENUSO'
                            AND e.etiq_etiqueta =" + "'" + tag + "'";
            return connection.getDataMariaDB(sql).Tables[0];
        }
        public DataTable ConsultarVehiculoValidacion(string tag) // ME TRAE EL VEHICULO UNA VEZ VALIDADO EN EL MODULO DE CIRCULACION 
        {
            string sql = "SELECT   "
                         + "v.VEHI_PLACA AS PLACA, v.VEHI_MODELO AS MODELO, v.VEHI_OBSERVACION AS OBSERVACION "
                         + "FROM   bootpark.vehiculo v "
                         + "       INNER JOIN bootpark.etiquetavehiculo ev "
                         + "               ON v.vehi_id = ev.vehi_id "
                         + "       INNER JOIN bootpark.etiqueta e "
                         + "               ON ev.etiq_id = e.etiq_id "
                         + "WHERE  e.etiq_tipo = 'TAG' "
                         + "       AND e.etiq_estado = 'ENUSO' "
                         + "       AND e.etiq_etiqueta = '" + tag + "'";
            return connection.getDataMariaDB(sql).Tables[0];
        }
    }
}