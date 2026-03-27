using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AIS_WEB_APPLICATION
{
    public partial class Start_ASN_Set : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_NAME"] == null || Session["USER_NAME"].ToString() != "Admin")
            {
                Response.Redirect("Landing.aspx");
            }
            if (!IsPostBack)
                lblMsg.Visible = false;
        }

        protected void btnRD_Click(object sender, EventArgs e)
        {
            RunASN("RD", txtRD.Text);
        }

        protected void btnFD_Click(object sender, EventArgs e)
        {
            RunASN("FD", txtFD.Text);
        }

        protected void btnRQ_Click(object sender, EventArgs e)
        {
            RunASN("RQ", txtRQ.Text);
        }

        protected void btnWS_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("ASN_SET_WS", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ASN", txtWS.Text);

                    SqlParameter msg = new SqlParameter("@MSG", SqlDbType.VarChar, 200);
                    msg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(msg);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    string result = msg.Value.ToString();

                    if (result == "SUCCESS")
                        ShowMessage("WS AND BCK UPDATED SUCCESSFULLY");
                    else
                        ShowMessage(result);

                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                ClearAll();
            }
        }

        void RunASN(string type, string asn)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("ASN_SET", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TYPE", type);
                    cmd.Parameters.AddWithValue("@ASN", asn);

                    SqlParameter msg = new SqlParameter("@MSG", SqlDbType.VarChar, 200);
                    msg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(msg);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    string result = msg.Value.ToString();

                    if (result == "SUCCESS")
                        ShowMessage(type + " UPDATED SUCCESSFULLY");
                    else
                        ShowMessage(result);

                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                ClearAll();
            }
        }

        void ShowMessage(string msg)
        {
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }

        void ClearAll()
        {
            txtRD.Text = "";
            txtFD.Text = "";
            txtRQ.Text = "";
            txtWS.Text = "";

            txtRD.Focus();
        }
    }
}