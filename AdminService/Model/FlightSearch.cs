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
            cmd.CommandText = "[dbo].[SearchFlightdata]";
            cmd.Parameters.Add("@SearchDate", SqlDbType.DateTime).Value = Ft.SearchDate;
            cmd.Parameters.Add("@From", SqlDbType.VarChar).Value = Ft.FromLocation;
            cmd.Parameters.Add("@To", SqlDbType.VarChar).Value = Ft.ToLocation;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);


                List<AirlineData> getAirlines = new List<AirlineData>();

                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        AirlineData ga = new AirlineData();
                        ga.FlightID = mDataSet.Tables[0].Rows[i]["FlightID"].ToString();
                        ga.AirlineId = mDataSet.Tables[0].Rows[i]["AirlineId"].ToString();
                        ga.Airline_Name = mDataSet.Tables[0].Rows[i]["Airline_Name"].ToString();
                        ga.FromLocation = mDataSet.Tables[0].Rows[i]["FromLocation"].ToString();
                        ga.ToLocation = mDataSet.Tables[0].Rows[i]["ToLocation"].ToString();
                        ga.Price = mDataSet.Tables[0].Rows[i]["Price"].ToString();
                        ga.Food= mDataSet.Tables[0].Rows[i]["Food"].ToString();
                        ga.Status = mDataSet.Tables[0].Rows[i]["Status"].ToString();
                        ga.Sheduled = mDataSet.Tables[0].Rows[i]["Sheduled"].ToString();                        
                        getAirlines.Add(ga);
                    }

                    Msg = JsonConvert.SerializeObject(getAirlines);

                }
                else
                {
                    Msg = "No Data Found";
                }
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
