using AIS_WEB_APPLICATION.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIS_WEB_APPLICATION
{
    public partial class Login : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AuditLogger.Log(
                    Session["USER_NAME"]?.ToString(),
                    "Login",
                    "VIEW",
                    "Visited Login page");
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowMessage("Please enter User ID and Password");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("USER_LOGIN_VALIDATE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USER_NAME", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    ShowMessage("Invalid User Name or Password");
                    return;
                }

                // ✅ STORE SESSION
                Session["USER_ID"] = dt.Rows[0]["USER_ID"].ToString();
                Session["USER_NAME"] = dt.Rows[0]["USER_NAME"].ToString();
                Session["LINE_NAME"] = dt.Rows[0]["LINE_NAME"].ToString();

                Session["USER_NAME"] = txtName.Text.Trim();

                AuditLogger.Log(
                    Session["USER_NAME"].ToString(),
                    "Login",
                    "LOGIN",
                    "User logged into application");


                // ✅ REDIRECT TO LANDING PAGE
                Response.Redirect("Landing.aspx");
            }
        }

        void ShowMessage(string msg)
        {
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }
}