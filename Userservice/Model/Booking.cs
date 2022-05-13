using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Userservice.Model
{
    public class Booking
    {
        public string FlightId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime JourneyDate { get; set; }

        //public List<BookingPerson> bookings { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Food { get; set; }
        


        public string UserId { get; set; }
        public DateTime Createateat { get; set; }

        public string TicketBooking(Booking booking, IConfiguration Config)
        {
            string Msg = string.Empty;

            string pnr = RanGenerate();

            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[TicketBooking]";
            cmd.Parameters.Add("@FlightId", SqlDbType.VarChar).Value = booking.FlightId;
            cmd.Parameters.Add("@From", SqlDbType.VarChar).Value = booking.From;
            cmd.Parameters.Add("@To", SqlDbType.VarChar).Value = booking.To;
            cmd.Parameters.Add("@Journeydate", SqlDbType.DateTime).Value = booking.JourneyDate;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = booking.UserId;
            cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = pnr;
            cmd.Parameters.Add("@Createdat", SqlDbType.DateTime).Value = booking.Createateat;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Msg = cmd.Parameters["@Id"].Value.ToString();

                if (Msg != "")
                {
                    con.Close();
                    con.Dispose();
                    SqlConnection Pcon = new SqlConnection(strConnString);
                    SqlCommand pcmd = new SqlCommand();
                    pcmd.CommandType = CommandType.StoredProcedure;
                    pcmd.CommandText = "[dbo].[spBookingPerson]";
                    pcmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = booking.Name;
                    pcmd.Parameters.Add("@age", SqlDbType.VarChar).Value = booking.Age;
                    pcmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = booking.Gender;
                    pcmd.Parameters.Add("@Food", SqlDbType.VarChar).Value = booking.Food;
                    pcmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = Msg;

                    pcmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    pcmd.Connection = Pcon;
                    try
                    {
                        Pcon.Open();
                        pcmd.ExecuteNonQuery();
                        string pMsg = pcmd.Parameters["@Id"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {

                        Pcon.Close();
                        Pcon.Dispose();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }

            if (Msg != "")
            {
                PNR pNR = new PNR();
                pNR.PNRNumber = pnr;
                Msg = JsonConvert.SerializeObject(pNR);
                return Msg; ;
            }
            else
            {
                return Msg;
            }
        }

        public string RanGenerate()
        {
            Random rnd = new Random(8);

            string PNR = "PNR" + DateTime.Now.ToString("G");

            return PNR;

        }


        public string GetPNR(string PNR, IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[PNRSearch]";
            cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = PNR;

            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);


                List<PNRData> getAirlines = new List<PNRData>();

                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        PNRData ga = new PNRData();
                        ga.FlightId = mDataSet.Tables[0].Rows[i]["FlightId"].ToString();
                        ga.Journyfrom = mDataSet.Tables[0].Rows[i]["Journyfrom"].ToString();
                        ga.Journeyto = mDataSet.Tables[0].Rows[i]["Journeyto"].ToString();
                        ga.Journeydate = mDataSet.Tables[0].Rows[i]["Journeydate"].ToString();
                        ga.PNR = mDataSet.Tables[0].Rows[i]["PNR"].ToString();
                        ga.Name = mDataSet.Tables[0].Rows[i]["Name"].ToString();
                        ga.Age = mDataSet.Tables[0].Rows[i]["Age"].ToString();
                        ga.Gender = mDataSet.Tables[0].Rows[i]["Gender"].ToString();
                        ga.Food = mDataSet.Tables[0].Rows[i]["Food"].ToString();

                        ga.Status = mDataSet.Tables[0].Rows[i]["Status"].ToString();
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

        public bool PNRDelete(string pnr, IConfiguration Config)
        {
            bool Msg = false;


            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[PNRDelete]";
            cmd.Parameters.Add("@PNR", SqlDbType.VarChar).Value = pnr;
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
