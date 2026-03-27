using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace AIS_WEB_APPLICATION.Common
{
    public static class AuditLogger
    {
        static string conStr =
            ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public static void Log(
            string userName,
            string pageName,
            string eventType,
            string eventDesc)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd =
                    new SqlCommand("USER_EVENT_LOG_INSERT", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@USER_NAME", userName);
                    cmd.Parameters.AddWithValue("@PAGE_NAME", pageName);
                    cmd.Parameters.AddWithValue("@EVENT_TYPE", eventType);
                    cmd.Parameters.AddWithValue("@EVENT_DESC", eventDesc);
                    cmd.Parameters.AddWithValue("@IP_ADDRESS",
                        HttpContext.Current.Request.UserHostAddress);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // NEVER break main flow if logging fails
            }
        }
    }
}