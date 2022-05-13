using Microsoft.AspNetCore.Mvc;
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
                Msg = cmd.Parameters["@Id"].Value.ToString();



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

        public string GetAirline(IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[getairline]";
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);
               

                List<GetAirline> getAirlines = new List<GetAirline>();

                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        GetAirline ga = new GetAirline();
                        ga.Id = mDataSet.Tables[0].Rows[i]["Airline_ID"].ToString();
                        ga.AirlineName = mDataSet.Tables[0].Rows[i]["Airline_Name"].ToString();
                        ga.ContactNumber = mDataSet.Tables[0].Rows[i]["ContactNumber"].ToString();
                        ga.ContactAddress = mDataSet.Tables[0].Rows[i]["ContactAddress"].ToString();
                        ga.Status = mDataSet.Tables[0].Rows[i]["status"].ToString();
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

        public string GetFlight(IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[getflights]";
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);


                List<GetFlight> getAirlines = new List<GetFlight>();

                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        GetFlight ga = new GetFlight();
                        ga.Id = mDataSet.Tables[0].Rows[i]["FlightID"].ToString();
                        ga.Airline_Name = mDataSet.Tables[0].Rows[i]["Airline_Name"].ToString();
                        ga.FromLocation = mDataSet.Tables[0].Rows[i]["FromLocation"].ToString();
                        ga.ToLocation = mDataSet.Tables[0].Rows[i]["ToLocation"].ToString();
                        ga.Price = mDataSet.Tables[0].Rows[i]["Price"].ToString();
                        ga.Sheduled = mDataSet.Tables[0].Rows[i]["Sheduled"].ToString();
                        ga.status = mDataSet.Tables[0].Rows[i]["status"].ToString();
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

        public bool Deleteairline(string Id, string Status, IConfiguration Config)
        {
            bool Msg = false;
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[Deleteairline]";
            cmd.Parameters.Add("@Aid", SqlDbType.VarChar).Value = Id;
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = Status;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Msg = true;
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

        public bool Deleteflight(string Id, string Status, IConfiguration Config)
        {
            bool Msg = false;
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[Deleteflight]";
            cmd.Parameters.Add("@Fid", SqlDbType.VarChar).Value = Id;
            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = Status;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Msg = true;
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
