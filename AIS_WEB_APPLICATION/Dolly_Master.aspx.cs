using AIS_WEB_APPLICATION.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AIS_WEB_APPLICATION
{
    public partial class Dolly_Master : System.Web.UI.Page
    {
        string conStr =
            ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();

                // ✅ VIEW LOG
                //AuditLogger.Log(
                //    Session["USER_NAME"]?.ToString(),
                //    "Dolly Master",
                //    "VIEW",
                //    "Visited Dolly Master page"
                //);
            }
        }

        void LoadGrid()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("DOLLY_MASTER_Select", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvDolly.DataSource = dt;
                gvDolly.DataBind();
            }
        }

        protected void gvDolly_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvDolly.SelectedRow;

            hfID.Value = gvDolly.DataKeys[row.RowIndex].Value.ToString();

            txtDolly.Text = row.Cells[2].Text;
            txtLine.Text = row.Cells[3].Text;
            txtQty.Text = row.Cells[4].Text;
            txtEmpty.Text = row.Cells[5].Text;

            btnAdd.Enabled = false;

            // STORE ORIGINAL VALUES
            ViewState["OLD_DOLLY"] = txtDolly.Text.Trim();
            ViewState["OLD_LINE"] = txtLine.Text.Trim();
            ViewState["OLD_QTY"] = txtQty.Text.Trim();
            ViewState["OLD_EMPTY"] = txtEmpty.Text.Trim();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

            if (txtDolly.Text.Trim() == "" ||
                txtLine.Text.Trim() == "" ||
                txtQty.Text.Trim() == "" ||
                txtEmpty.Text.Trim() == "")
            {
                ShowMessage("Please enter all values");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand("DOLLY_MASTER_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DOLLY_NO", txtDolly.Text.Trim());
                    cmd.Parameters.AddWithValue("@LINE_NAME", txtLine.Text.Trim());
                    cmd.Parameters.AddWithValue("@QUANTITY", txtQty.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMPTY_FLAG", txtEmpty.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // ✅ INSERT LOG
                AuditLogger.Log(
                    Session["USER_NAME"]?.ToString(),
                    "Dolly Master",
                    "INSERT",
                    $"Dolly={txtDolly.Text.Trim()}, Line={txtLine.Text.Trim()}, Qty={txtQty.Text.Trim()}, Empty={txtEmpty.Text.Trim()}"
                );

                ShowMessage("Dolly added successfully");
                ClearForm();
                LoadGrid();
            }
            catch (SqlException ex)
            {
                ShowMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                ShowMessage("Please select a dolly to update");
                return;
            }

            // CHECK CHANGES
            if (ViewState["OLD_DOLLY"]?.ToString() == txtDolly.Text.Trim() &&
                ViewState["OLD_LINE"]?.ToString() == txtLine.Text.Trim() &&
                ViewState["OLD_QTY"]?.ToString() == txtQty.Text.Trim() &&
                ViewState["OLD_EMPTY"]?.ToString() == txtEmpty.Text.Trim())
            {
                ShowMessage("No changes detected");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand("DOLLY_MASTER_Update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", hfID.Value);
                    cmd.Parameters.AddWithValue("@DOLLY_NO", txtDolly.Text.Trim());
                    cmd.Parameters.AddWithValue("@LINE_NAME", txtLine.Text.Trim());
                    cmd.Parameters.AddWithValue("@QUANTITY", txtQty.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMPTY_FLAG", txtEmpty.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // ✅ UPDATE LOG
                AuditLogger.Log(
                    Session["USER_NAME"]?.ToString(),
                    "Dolly Master",
                    "UPDATE",
                    $"OLD[Dolly={ViewState["OLD_DOLLY"]}, Line={ViewState["OLD_LINE"]}, Qty={ViewState["OLD_QTY"]}, Empty={ViewState["OLD_EMPTY"]}] → " +
                    $"NEW[Dolly={txtDolly.Text.Trim()}, Line={txtLine.Text.Trim()}, Qty={txtQty.Text.Trim()}, Empty={txtEmpty.Text.Trim()}]"
                );

                ShowMessage("Dolly updated successfully");
                ClearForm();
                LoadGrid();
            }
            catch (SqlException ex)
            {
                ShowMessage(ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

            if (hfID.Value == "")
            {
                ShowMessage("Please select a dolly to delete");
                return;
            }

            string deletedDolly = txtDolly.Text.Trim();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("DOLLY_MASTER_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", hfID.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ DELETE LOG
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Dolly Master",
                "DELETE",
                $"Deleted Dolly={deletedDolly}"
            );

            ShowMessage("Dolly deleted successfully");
            ClearForm();
            LoadGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMsg.Visible = false;
        }

        void ClearForm()
        {
            btnAdd.Enabled = true;
            hfID.Value = "";
            txtDolly.Text = "";
            txtLine.Text = "";
            txtQty.Text = "";
            txtEmpty.Text = "";
            gvDolly.SelectedIndex = -1;
        }

        void ShowMessage(string message)
        {
            lblMsg.Text = message;
            lblMsg.Visible = true;
        }
    }
}