using AIS_WEB_APPLICATION.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AIS_WEB_APPLICATION
{
    public partial class Master_Data : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            // if (!IsPostBack)
            // {
            //AuditLogger.Log(
            //    Session["USER_NAME"]?.ToString(),
            //    "Master_Data",
            //    "VIEW",
            //    "Visited Master_Data page"
            //);
            //}
        }

        string TableName
        {
            get { return ViewState["TABLE_NAME"]?.ToString(); }
            set { ViewState["TABLE_NAME"] = value; }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlType.SelectedValue))
            {
                ShowMessage("Please select type");
                return;
            }

            // Set selected table name
            TableName = "MASTER_" + ddlType.SelectedValue;

            // Clear previous selection & fields FIRST
            ClearFields();

            // Load grid for selected type
            LoadGrid();

            // Hide old messages
            lblMsg.Visible = false;
        }
        protected void gvMaster_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (string.IsNullOrEmpty(TableName))
                return;

            bool dual = TableName != "MASTER_BCK" && TableName != "MASTER_WS";

            // Apply to Header + Data rows
            if (e.Row.RowType == DataControlRowType.Header ||
                e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Visible = dual; // SIDE column
            }
        }
        void LoadGrid()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("MASTER_DATA_Select", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TABLE_NAME", TableName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvMaster.DataSource = dt;
                gvMaster.DataBind();
            }
        }
        protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvMaster.SelectedRow;

            // ✅ Correct way to get ID
            hfID.Value = gvMaster.SelectedDataKey.Value.ToString();

            // ✅ Correct cell indexes
            txtPartCode.Text = row.Cells[1].Text;   // PART_CODE
            txtPartName.Text = row.Cells[2].Text;   // PART_NAME
            txtColorCode.Text = row.Cells[3].Text;  // COLOR_CODE

            if (TableName != "MASTER_BCK" && TableName != "MASTER_WS")
            {
                txtSide.Text = row.Cells[4].Text;   // SIDE
                txtSide.Enabled = true;
            }
            else
            {
                txtSide.Text = "";
                txtSide.Enabled = false;
            }

            btnAdd.Enabled = false;

            // STORE OLD VALUES
            ViewState["OLD_CODE"] = txtPartCode.Text.Trim();
            ViewState["OLD_NAME"] = txtPartName.Text.Trim();
            ViewState["OLD_COLOR"] = txtColorCode.Text.Trim();
            ViewState["OLD_SIDE"] = txtSide.Text.Trim();
        }
       

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfID.Value))
            {
                ShowMessage("Clear selection before adding new record");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPartCode.Text) ||
                string.IsNullOrWhiteSpace(txtPartName.Text))
            {
                ShowMessage("Please enter required values");
                return;
            }

            if (IsDuplicate())
            {
                ShowMessage("This record already exists");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("MASTER_DATA_Insert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TABLE_NAME", TableName);
                cmd.Parameters.AddWithValue("@PART_CODE", txtPartCode.Text.Trim());
                cmd.Parameters.AddWithValue("@PART_NAME", txtPartName.Text.Trim());
                cmd.Parameters.AddWithValue("@COLOR_CODE", txtColorCode.Text.Trim());
                cmd.Parameters.AddWithValue("@SIDE",
                    (TableName == "MASTER_BCK" || TableName == "MASTER_WS")
                    ? (object)DBNull.Value
                    : txtSide.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ INSERT LOG
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Master_Data",
                "INSERT",
                $"Table={TableName}, Code={txtPartCode.Text.Trim()}, Name={txtPartName.Text.Trim()}, Color={txtColorCode.Text.Trim()}, Side={txtSide.Text.Trim()}"
            );

            ShowMessage("Record added successfully");
            LoadGrid();
            ClearFields();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                ShowMessage("Please select record to update");
                return;
            }

            if (ViewState["OLD_CODE"]?.ToString() == txtPartCode.Text.Trim() &&
                ViewState["OLD_NAME"]?.ToString() == txtPartName.Text.Trim() &&
                ViewState["OLD_COLOR"]?.ToString() == txtColorCode.Text.Trim() &&
                ViewState["OLD_SIDE"]?.ToString() == txtSide.Text.Trim())
            {
                ShowMessage("No changes detected");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("MASTER_DATA_Update", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TABLE_NAME", TableName);
                cmd.Parameters.AddWithValue("@ID", hfID.Value);
                cmd.Parameters.AddWithValue("@PART_CODE", txtPartCode.Text.Trim());
                cmd.Parameters.AddWithValue("@PART_NAME", txtPartName.Text.Trim());
                cmd.Parameters.AddWithValue("@COLOR_CODE", txtColorCode.Text.Trim());
                cmd.Parameters.AddWithValue("@SIDE", txtSide.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ UPDATE LOG
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Master_Data",
                "UPDATE",
                $"Table={TableName} | OLD[Code={ViewState["OLD_CODE"]}, Name={ViewState["OLD_NAME"]}, Color={ViewState["OLD_COLOR"]}, Side={ViewState["OLD_SIDE"]}] → " +
                $"NEW[Code={txtPartCode.Text.Trim()}, Name={txtPartName.Text.Trim()}, Color={txtColorCode.Text.Trim()}, Side={txtSide.Text.Trim()}]"
            );

            ShowMessage("Record updated successfully");
            LoadGrid();
            ClearFields();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                ShowMessage("Please select record to delete");
                return;
            }

            string deletedInfo =
                $"Table={TableName}, Code={txtPartCode.Text.Trim()}, Name={txtPartName.Text.Trim()}";

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("MASTER_DATA_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TABLE_NAME", TableName);
                cmd.Parameters.AddWithValue("@ID", hfID.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ DELETE LOG
            AuditLogger.Log(
                Session["USER_NAME"]?.ToString(),
                "Master_Data",
                "DELETE",
                deletedInfo
            );

            ShowMessage("Record deleted successfully");
            LoadGrid();
            ClearFields();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            lblMsg.Visible = false;
        }

        void ClearFields()
        {
            hfID.Value = "";
            txtPartCode.Text = "";
            txtPartName.Text = "";
            txtColorCode.Text = "";
            txtSide.Text = "";
            gvMaster.SelectedIndex = -1;
            btnAdd.Enabled = true;

            ViewState["OLD_CODE"] = null;
            ViewState["OLD_NAME"] = null;
            ViewState["OLD_COLOR"] = null;
            ViewState["OLD_SIDE"] = null;
        }

        void ShowMessage(string msg)
        {
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }

        bool IsDuplicate()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM " + TableName + " WHERE PART_CODE=@PART_CODE", con))
            {
                cmd.Parameters.AddWithValue("@PART_CODE", txtPartCode.Text.Trim());
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
    }
}