using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Model
{
    public class Airline
    {
        public string AirlineName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactAddress { get; set; }
        public bool Status { get; set; }
        public DateTime Createdat { get; set; }


        public string AddAirline(Airline airline, IConfiguration Config)
        {
            string Msg = string.Empty;


            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[AirlineAdd]";
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = airline.AirlineName;
            cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = airline.ContactNumber;
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = airline.ContactAddress;
            cmd.Parameters.Add("@status", SqlDbType.Bit).Value = airline.Status;
            cmd.Parameters.Add("@Createdat", SqlDbType.DateTime).Value = airline.Createdat;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Msg =  cmd.Parameters["@Id"].Value.ToString();

               

            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            finally
            {
                con.Close();
                con.Dispose();
            }


            return Msg;
        }




    }
}
