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
    public class User
    {

       // public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string Mobile { get; set; }

        public bool Status { get; set; }

        public DateTime Createat { get; set; }


        public string AddUser(User user, IConfiguration Config)
        {
            string Msg = string.Empty;


            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[AddUser]";
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = user.Name;
            cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = user.Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.Email;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
            cmd.Parameters.Add("@Createdat", SqlDbType.DateTime).Value = user.Createat;
            cmd.Parameters.Add("@status", SqlDbType.Bit).Value = user.Status;
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


        public string getUser(GetUser user, IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[GetUser]";
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
            
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
