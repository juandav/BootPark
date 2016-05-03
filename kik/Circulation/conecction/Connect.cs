using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;

namespace Circulation.conecction
{
    public class Connect
    {
        #region Cadena de Conexión
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString;
        }
        #endregion

        /// <summary>
        ///     Para consutas de tipo Select
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>dataset</returns>
        public DataSet GetData(string sql)
        {
            DataSet dataSet = new DataSet();
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection))
                {
                    adapter.Fill(dataSet);
                }
                connection.Close();
                connection.Dispose();
            }
            return dataSet;
        }

        /// <summary>
        ///     Para consultas de tipo: Insert, Update y Delete
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>ture or false</returns>
        public bool SendData(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(sql))
                {
                    command.Connection = connection;
                    connection.Open();

                    try
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                    }
                    catch (System.Exception e)
                    {
                        writeConnectionLogs("La sentencia sql:" + sql + "<------->" + " presenta el siguiente error: " + e.Message);
                    }
                }
                connection.Close();
                connection.Dispose();
            }
            return false;
        }

        /// <summary>
        ///     Para transacciones de tipo: Insert, Update y Delete
        /// </summary>
        /// <param name="sql">List string sql</param>
        /// <returns>true or false</returns>
        public bool SendDataTransaction(List<string> sql)
        {
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        command.Transaction = transaction;
                        try
                        {
                            int ammountsql = sql.Count;
                            int correctsql = 0;

                            for (int i = 0; i < sql.Count; i++)
                            {
                                command.CommandText = sql[i].ToString();
                                if (command.ExecuteNonQuery() > 0)
                                {
                                    correctsql = correctsql + 1;
                                }
                            }

                            if (ammountsql == correctsql)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (System.Exception e)
                        {
                            transaction.Rollback();
                            writeConnectionLogs("La lista de sentencias sql:" + sql + "<------->" + " presenta el siguiente error: " + e.Message);
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
            return false;
        }

        #region Write Logs
        private void writeConnectionLogs(string mensaje)
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter sw = new StreamWriter(mydocpath + @"\logs.txt"))
            {
                sw.WriteLine(mensaje);
            }
        }
        #endregion
    }
}