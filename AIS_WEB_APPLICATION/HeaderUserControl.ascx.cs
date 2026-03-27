using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIS_WEB_APPLICATION
{
    public partial class HeaderUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_NAME"] != null)
            {
                lblUserName.Text = Session["USER_NAME"].ToString();
               // lblUserName.ForeColor = System.Drawing.Color.White;

                if (Session["USER_NAME"].ToString() == "Admin")
                    phASN.Visible = true;
                else
                    phASN.Visible = false;
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}