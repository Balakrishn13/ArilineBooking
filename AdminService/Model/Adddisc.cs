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
    public class Adddisc
    {
        public string DName{get;set;}
        public string DPrice { get; set; }


        public string Add(Adddisc adddisc, IConfiguration Config)
        {
            string Msg = string.Empty;

            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[Adddisc]";
            cmd.Parameters.Add("@CName", SqlDbType.VarChar).Value = adddisc.DName;
            cmd.Parameters.Add("@Price", SqlDbType.VarChar).Value = adddisc.DPrice;            
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

        public string GetDisc(IConfiguration Config)
        {
            string Msg = string.Empty;

            DataSet mDataSet = new DataSet();
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[getdisc]";
            cmd.Connection = con;
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(mDataSet);


                List<getdisc> getdiscs = new List<getdisc>();

                if (mDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        getdisc ga = new getdisc();
                        ga.Did = mDataSet.Tables[0].Rows[i]["did"].ToString();
                        ga.DCou = mDataSet.Tables[0].Rows[i]["dcoupon"].ToString();
                        ga.DPrice = mDataSet.Tables[0].Rows[i]["damount"].ToString();
                        ga.status = mDataSet.Tables[0].Rows[i]["status"].ToString();                        
                        getdiscs.Add(ga);
                    }

                    Msg = JsonConvert.SerializeObject(getdiscs);

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

        public bool Delete(string Id,string Status, IConfiguration Config)
        {
            bool Msg = false;
            string strConnString = Config.GetConnectionString("Database");
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[Deletedisc]";
            cmd.Parameters.Add("@Did", SqlDbType.VarChar).Value = Id;
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
