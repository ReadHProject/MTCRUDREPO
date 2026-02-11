using PayrollApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PayrollApp.DAL
{
    public class DBHelper
    {
        // DB CONNECTION STRING
        private static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DBCONSTR"].ConnectionString);
        }

        // ATTACH PARAMETERS
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter param in commandParameters)
            {
                if (param != null)
                    command.Parameters.Add(param);
            }
        }

        // CREATE COMMAND
        private static SqlCommand GetCommand(string cmdTxt,SqlConnection connection, SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand(cmdTxt, connection);

            if (cmdTxt.ToLower().StartsWith("sp_") || cmdTxt.ToLower().StartsWith("pr"))
                command.CommandType = CommandType.StoredProcedure;

            if (commandParameters != null)
                AttachParameters(command, commandParameters);

            return command;
        }

        // GET DATATABLE
        public static DataTable GetDataTable(string cmdTxt, SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }

        // GET DATASET
        public static DataSet GetDataSet(string cmdTxt, SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet(command.CommandText);
                da.Fill(ds);
                return ds;
            }
        }

        // EXECUTE NON QUERY
        public static int ExecuteNonQuery(string cmdTxt, SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                return command.ExecuteNonQuery();
            }
        }
    }

}
