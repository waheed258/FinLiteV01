using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using context = System.Web.HttpContext;

/// <summary>
/// Summary description for ExceptionLogging
/// </summary>

    public static class ExceptionLogging 
    {

        private static String exepurl;
        static SqlConnection con;

        private static void connecttion()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            con = new SqlConnection(constr);
            con.Open();
        }
        public static void SendExcepToDB(Exception exdb)
        {
            int userid= Convert.ToInt32(HttpContext.Current.Session["UserLoginId"].ToString());
            connecttion();
            exepurl = context.Current.Request.Url.ToString();
            SqlCommand com = new SqlCommand("Exception_ExceptionLoggingToDataBase", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
            com.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
            com.Parameters.AddWithValue("@ExceptionURL", exepurl);
            com.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
            com.Parameters.AddWithValue("@UserId", userid);
            com.ExecuteNonQuery();



        }


    }  
