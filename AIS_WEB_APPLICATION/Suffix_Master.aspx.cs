using AIS_WEB_APPLICATION.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AIS_WEB_APPLICATION
{
    public partial class Suffix_Master : System.Web.UI.Page
    {
        private readonly string conStr =
            ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSuffixList();

                //AuditLogger.Log(
                //    Session["USER_NAME"]?.ToString(),
                //    "Suffix_Master",
                //    "VIEW",
                //    "Visited Suffix_Master page");
            }
        }
        void LoadSuffixList()
        {
            lstSuffix.Items.Clear();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("dbo.SUFFIX_MASTER_Select", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstSuffix.Items.Add(
                        new ListItem(
                            dr["SUFFIX"].ToString(),
                            dr["ID"].ToString()));
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

            if (txtSuffix.Text.Trim() == "")
            {
                ShowMessage("Please enter suffix value");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("dbo.SUFFIX_MASTER_Insert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SUFFIX", txtSuffix.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ AUDIT LOG - INSERT
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Suffix Master",
                "INSERT",
                $"Suffix Added = {txtSuffix.Text.Trim()}"
            );

            ShowMessage("Suffix added successfully");
            ClearForm();
            LoadSuffixList();
        }
        protected void lstSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSuffix.SelectedItem == null)
                return;

            hfSuffixId.Value = lstSuffix.SelectedValue;
            txtSuffix.Text = lstSuffix.SelectedItem.Text;
            btnAdd.Enabled = false;
            ViewState["OLD_SUFFIX"] = txtSuffix.Text.Trim();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (hfSuffixId.Value == "")
            {
                ShowMessage("Please select a suffix to update");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSuffix.Text))
            {
                ShowMessage("Please enter suffix value");
                return;
            }

            string oldValue = ViewState["OLD_SUFFIX"]?.ToString();

            if (oldValue == txtSuffix.Text.Trim())
            {
                ShowMessage("No changes detected");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("dbo.SUFFIX_MASTER_Update", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", hfSuffixId.Value);
                cmd.Parameters.AddWithValue("@SUFFIX", txtSuffix.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ AUDIT LOG - UPDATE
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Suffix Master",
                "UPDATE",
                $"Suffix Updated: OLD={oldValue}, NEW={txtSuffix.Text.Trim()}"
            );

            ShowMessage("Suffix updated successfully");
            ClearForm();
            LoadSuffixList();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

            if (hfSuffixId.Value == "")
            {
                ShowMessage("Please select a suffix to delete");
                return;
            }

            string deletedSuffix = txtSuffix.Text.Trim();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("dbo.SUFFIX_MASTER_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", hfSuffixId.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ AUDIT LOG - DELETE
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Suffix Master",
                "DELETE",
                $"Suffix Deleted = {deletedSuffix}"
            );

            ShowMessage("Suffix deleted successfully");
            ClearForm();
            LoadSuffixList();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMsg.Visible = false;
            btnAdd.Enabled = true;
        }

        void ClearForm()
        {
            hfSuffixId.Value = "";
            txtSuffix.Text = "";
            ViewState["OLD_SUFFIX"] = null;
            lstSuffix.ClearSelection();
        }
        void ShowMessage(string message)
        {
            lblMsg.Text = message;
            lblMsg.Visible = true;
        }
    
    }
}