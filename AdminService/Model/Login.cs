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
    public class Login
    {
      public  string UserName { get; set; }

        public string Password { get; set; }


        public string Admin(Login login, IConfiguration Config)
        {
            string Msg = string.Empty;
          
            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[AdminCheck]";
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = login.UserName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = login.Password;            
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
