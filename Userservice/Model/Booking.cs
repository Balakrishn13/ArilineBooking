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

        public List<BookingPerson> bookings { get; set; }

        public int UserId { get; set; }
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


                   
                    List<BookingPerson> people = booking.bookings;
                    int Count = people.Count;
                    for (int i = 0; i < Count; i++)
                    {

                        SqlConnection Pcon = new SqlConnection(strConnString);
                        SqlCommand pcmd = new SqlCommand();
                        pcmd.CommandType = CommandType.StoredProcedure;
                        pcmd.CommandText = "[dbo].[spBookingPerson]";
                        pcmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = people[i].Name;
                        pcmd.Parameters.Add("@age", SqlDbType.VarChar).Value = people[i].Age;
                        pcmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = people[i].Gender;
                        pcmd.Parameters.Add("@Food", SqlDbType.VarChar).Value = people[i].Food;
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
                return pnr;
            }
            else
            {
                return Msg;
            }
        }

        public string RanGenerate()
        {
            Random rnd = new Random(8);

            string PNR = "PNR" + rnd.Next().ToString();

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

        public string PNRDelete(string pnr, IConfiguration Config)
        {
            string Msg = string.Empty;


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
                Msg = pnr;
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
