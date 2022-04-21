using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace Userservice.Model
{
    public class Log
    {
        public void AppendToErrorLog(String strErrorMsg, String strFileName, String strFunctionName, String strUserName)
        {


            Random rn = new Random();
            int Order = rn.Next(10000000, 99999999);

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\log_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + Order + ".txt";
            if (!File.Exists(filepath))
            {
                try
                {
                    // Create a file to write to.
                    using (StreamWriter writer = File.CreateText(filepath))
                    {
                        writer.WriteLine(strErrorMsg);
                        writer.WriteLine(strFileName);
                        writer.WriteLine(strFunctionName);
                        writer.WriteLine(strUserName);
                    }
                }
                catch (Exception exp)
                {
                    Console.Write(exp.Message);
                }
            }
        }


        public void ExecuteQueryHardCoded(String strQuery, String strFileName, String strFunctionName, String strUserName)
        {
            SqlConnection mSqlConnection = null;
            SqlCommand mSqlCommand = null;

            try
            {

                DateTime mDateTime = new DateTime();

                mSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                mSqlCommand = new SqlCommand();
                mSqlCommand.Connection = mSqlConnection;
                mSqlConnection.Open();
                mSqlCommand.CommandType = CommandType.Text;
                mSqlCommand.CommandText = strQuery;
                mSqlCommand.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                throw exc;

            }
            finally
            {
                try
                {
                    mSqlCommand.Dispose();
                    mSqlConnection.Close();
                }

                catch (Exception e)
                { }
            }
        }

        public void ExecuteQuery(String strQuery, String strFileName, String strFunctionName, String strUserName)
        {
            SqlConnection mSqlConnection = null;
            SqlCommand mSqlCommand = null;

            try
            {

                DateTime mDateTime = new DateTime();
                String conn = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
                mSqlConnection = new SqlConnection(conn);
                mSqlCommand = new SqlCommand();
                mSqlCommand.Connection = mSqlConnection;
                mSqlConnection.Open();
                mSqlCommand.CommandType = CommandType.Text;
                mSqlCommand.CommandText = strQuery;
                mSqlCommand.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                throw exc;

            }
            finally
            {
                try
                {
                    mSqlCommand.Dispose();
                    mSqlConnection.Close();
                }
                catch (Exception e)
                { }
            }
        }


        public int UpdateQuery(String strQuery, String strFileName, String strFunctionName, String strUserName)
        {
            SqlConnection mSqlConnection = null;
            SqlCommand mSqlCommand = null;

            try
            {

                DateTime mDateTime = new DateTime();

                mSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                mSqlCommand = new SqlCommand();
                mSqlCommand.Connection = mSqlConnection;
                mSqlConnection.Open();
                mSqlCommand.CommandType = CommandType.Text;
                mSqlCommand.CommandText = strQuery;
                return mSqlCommand.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                //AppendToErrorLog(exc.Message, strFileName, strFunctionName, strUserName);
                return -1;
            }
            finally
            {
                try
                {
                    mSqlCommand.Dispose();
                    mSqlConnection.Close();
                }
                catch (Exception e)
                { }
            }
        }

        public System.Data.DataSet getResultset(String strQuery, String strFileName, String strFunctionName, String strUserName)
        {
            SqlConnection mSqlConnection = null;
            SqlCommand mSqlCommand = null;
            SqlDataAdapter mNpgsqlDataAdapter = null;
            System.Data.DataSet mDataSet = new System.Data.DataSet();


            try
            {

                mSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                mSqlCommand = new SqlCommand();
                mSqlCommand.Connection = mSqlConnection;
                mSqlConnection.Open();
                mSqlCommand.CommandType = CommandType.Text;
                mSqlCommand.CommandText = strQuery;
                mNpgsqlDataAdapter = new SqlDataAdapter(mSqlCommand);
                mNpgsqlDataAdapter.Fill(mDataSet);
                return mDataSet;
            }
            catch (Exception exc)
            {
                //AppendToErrorLog(exc.Message.ToString(), exc.StackTrace.ToString(), "", "");
                return null;
            }
            finally
            {
                try
                {
                    mSqlCommand.Dispose();
                    mSqlConnection.Close();
                }
                catch (Exception e)
                { }
            }

        }
    }
}