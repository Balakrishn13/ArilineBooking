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
    public class Login
    {
        public string UserName { get; set; }

        public string Password { get; set; }


        public string Userlogin(Login login, IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[UserCheck]";
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = login.UserName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = login.Password;
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);
                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    Userdata user = new Userdata();

                    user.ID = mDataSet.Tables[0].Rows[0]["ID"].ToString();
                    user.Name = mDataSet.Tables[0].Rows[0]["Name"].ToString();
                    user.Role = mDataSet.Tables[0].Rows[0]["role"].ToString();
                    Msg = JsonConvert.SerializeObject(user);
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

        public string PNR(UserPnr userPnr, IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[PNR]";
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userPnr.UserId;
           
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);
                List<PNR> getpnr = new List<PNR>();
                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        PNR user = new PNR();
                        user.PNRNumber = mDataSet.Tables[0].Rows[i]["pnr"].ToString();
                        getpnr.Add(user);
                    }
                    

                    
                   
                    Msg = JsonConvert.SerializeObject(getpnr);
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

        public string userdata(UserPnr userPnr, IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[GetuserData]";
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userPnr.UserId;

            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);
                List<BookingData> getpnr = new List<BookingData>();
                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        BookingData user = new BookingData();
                        user.FlightId = mDataSet.Tables[0].Rows[i]["FlightId"].ToString();
                        user.Journyfrom = mDataSet.Tables[0].Rows[i]["Journyfrom"].ToString();
                        user.Journeyto = mDataSet.Tables[0].Rows[i]["Journeyto"].ToString();
                        user.PNR = mDataSet.Tables[0].Rows[i]["PNR"].ToString();
                        user.Status = mDataSet.Tables[0].Rows[i]["Status"].ToString();

                        user.Name = mDataSet.Tables[0].Rows[i]["Name"].ToString();
                        user.Age = mDataSet.Tables[0].Rows[i]["Age"].ToString();
                        user.Gender = mDataSet.Tables[0].Rows[i]["Gender"].ToString();
                        user.Food = mDataSet.Tables[0].Rows[i]["Food"].ToString();
                        getpnr.Add(user);
                    }




                    Msg = JsonConvert.SerializeObject(getpnr);
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
