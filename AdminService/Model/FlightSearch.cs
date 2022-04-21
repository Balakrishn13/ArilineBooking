using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Model
{
    public class FlightSearch
    {
        public DateTime SearchDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime RoundTripDate { get; set; }


        public string SearchFlight(FlightSearch Ft, IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SearchFlight]";
            cmd.Parameters.Add("@SearchDate", SqlDbType.DateTime).Value = Ft.SearchDate;
            cmd.Parameters.Add("@From", SqlDbType.VarChar).Value = Ft.FromLocation;
            cmd.Parameters.Add("@To", SqlDbType.VarChar).Value = Ft.ToLocation;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);
                Msg = JsonConvert.SerializeObject(mDataSet);
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
