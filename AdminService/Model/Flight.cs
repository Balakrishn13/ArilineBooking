using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Model
{
    public class Flight
    {
        public string FlightId { get; set; }

        public int AirlineId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public bool Food { get; set; }

       

        public int NoOfBUSeats { get; set; }

        public int NoOfNONBUSeats { get; set; }

        public string Remarks { get; set; }

        public int NoOfRows { get; set; }

        public decimal Price { get; set; }

        public string Sheduled { get; set; }

        public bool Status { get; set; }

        public DateTime Createdat { get; set; }

        public string AddFlight(Flight flight, IConfiguration Config)
        {
            string Msg = string.Empty;
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[AddFlight]";
            cmd.Parameters.Add("@AirlineId", SqlDbType.Int).Value = flight.AirlineId;
            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = flight.FromDate;
            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = flight.ToDate;
            cmd.Parameters.Add("@FromLocation", SqlDbType.VarChar).Value = flight.FromLocation;
            cmd.Parameters.Add("@ToLocation", SqlDbType.VarChar).Value = flight.ToLocation;
            cmd.Parameters.Add("@Food", SqlDbType.Bit).Value = flight.Food;
            cmd.Parameters.Add("@NoOfBUSeats", SqlDbType.Int).Value = flight.NoOfBUSeats;
            cmd.Parameters.Add("@NoOfNONBUSeats", SqlDbType.Int).Value = flight.NoOfNONBUSeats;
            cmd.Parameters.Add("@Remarks", SqlDbType.Text).Value = flight.Remarks;
            cmd.Parameters.Add("@NoOfRows", SqlDbType.Int).Value = flight.NoOfRows;
            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = flight.Price;
            cmd.Parameters.Add("@Sheduled", SqlDbType.VarChar).Value = flight.Sheduled;
            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = flight.Status;
            cmd.Parameters.Add("@Createdat", SqlDbType.DateTime).Value = flight.Createdat;




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

    }
}
