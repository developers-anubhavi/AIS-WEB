using AIS_WEB_APPLICATION.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AIS_WEB_APPLICATION
{
    public partial class Suffix_Linking : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TYPE = "";
                divGrid.Style["display"] = "none"; // hide on first load
            }
       
        }
        string TYPE
        {
            get { return ViewState["TYPE"]?.ToString(); }
            set { ViewState["TYPE"] = value; }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "")
            {
                ShowMessage("Please select type");
                return;
            }
            lblMsg.Visible = false;
            TYPE = ddlType.SelectedValue;
            LoadSuffix();
            LoadParts();
            LoadGrid();
            ClearSelection();
        
        }

        void LoadSuffix()
        {
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ID, SUFFIX FROM SUFFIX_MASTER ORDER BY SUFFIX", conStr))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlSuffix.DataSource = dt;
                ddlSuffix.DataTextField = "SUFFIX";
                ddlSuffix.DataValueField = "ID";
                ddlSuffix.DataBind();
                ddlSuffix.Items.Insert(0, new ListItem("-- Select Suffix --", ""));
            }
        }

        void LoadParts()
        {
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ID, PART_CODE FROM MASTER_" + TYPE, conStr))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlPart.DataSource = ddlLH.DataSource = ddlRH.DataSource = dt;
                ddlPart.DataTextField = ddlLH.DataTextField = ddlRH.DataTextField = "PART_CODE";
                ddlPart.DataValueField = ddlLH.DataValueField = ddlRH.DataValueField = "ID";

                ddlPart.DataBind();
                ddlLH.DataBind();
                ddlRH.DataBind();
            }

            ddlPart.Items.Insert(0, new ListItem("-- Select Part --", ""));
            ddlLH.Items.Insert(0, new ListItem("-- Select LH Part --", ""));
            ddlRH.Items.Insert(0, new ListItem("-- Select RH Part --", ""));

            bool dual = TYPE == "FD" || TYPE == "RD" || TYPE == "RQ";
            ddlPart.Visible = !dual;
            ddlLH.Visible = ddlRH.Visible = dual;
        }

        void LoadGrid()
        {
            using (SqlCommand cmd = new SqlCommand("Suffix_Linking_Select", new SqlConnection(conStr)))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", TYPE);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvData.DataSource = dt;
                gvData.DataBind();
            }
            // show grid only if data exists
            divGrid.Style["display"] = gvData.Rows.Count > 0 ? "block" : "none";
        }
        protected void gvData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (string.IsNullOrEmpty(TYPE)) return;

            bool dual = TYPE == "FD" || TYPE == "RD" || TYPE == "RQ";

            // Apply to header & data rows
            if (e.Row.RowType == DataControlRowType.Header ||
                e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Visible = dual;      // LH_PART
                e.Row.Cells[3].Visible = dual;      // RH_PART
                e.Row.Cells[4].Visible = !dual;     // PART
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // 🔒 VALIDATIONS
            if (string.IsNullOrEmpty(TYPE))
            {
                ShowMessage("Please select Type");
                return;
            }

            if (ddlSuffix.SelectedIndex <= 0)
            {
                ShowMessage("Please select Suffix");
                return;
            }

            if (ddlPart.Visible && ddlPart.SelectedIndex <= 0)
            {
                ShowMessage("Please select Part");
                return;
            }

            if (ddlLH.Visible && ddlLH.SelectedIndex <= 0)
            {
                ShowMessage("Please select LH Part");
                return;
            }

            if (ddlRH.Visible && ddlRH.SelectedIndex <= 0)
            {
                ShowMessage("Please select RH Part");
                return;
            }

            if (hfID.Value != "")
            {
                ShowMessage("Clear selection before adding");
                return;
            }

            if (IsDuplicate())
            {
                ShowMessage("This record already exists");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("Suffix_Linking_Insert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@SUFFIX_ID", ddlSuffix.SelectedValue);
                cmd.Parameters.AddWithValue("@PART_ID",
                    ddlPart.Visible ? ddlPart.SelectedValue : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LH_PART_ID",
                    ddlLH.Visible ? ddlLH.SelectedValue : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RH_PART_ID",
                    ddlRH.Visible ? ddlRH.SelectedValue : (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ LOG INSERT
            AuditLogger.Log(
                Session["USER_NAME"].ToString(),
                "Suffix Linking",
                "INSERT",
                $"Suffix={ddlSuffix.SelectedItem.Text}, " +
                $"LH={(ddlLH.Visible ? ddlLH.SelectedItem.Text : "-")}, " +
                $"RH={(ddlRH.Visible ? ddlRH.SelectedItem.Text : "-")}, " +
                $"Part={(ddlPart.Visible ? ddlPart.SelectedItem.Text : "-")}"
            );

            ShowMessage("Record added successfully");
            LoadGrid();
            ClearSelection();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                ShowMessage("Select a record to update");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("Suffix_Linking_Update", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@ID", hfID.Value);
                cmd.Parameters.AddWithValue("@SUFFIX_ID", ddlSuffix.SelectedValue);
                cmd.Parameters.AddWithValue("@PART_ID",
                    ddlPart.Visible ? ddlPart.SelectedValue : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LH_PART_ID",
                    ddlLH.Visible ? ddlLH.SelectedValue : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RH_PART_ID",
                    ddlRH.Visible ? ddlRH.SelectedValue : (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ LOG UPDATE
            AuditLogger.Log(
                Session["USER_NAME"].ToString(),
                "Suffix Linking",
                "UPDATE",
                $"Updated record ID={hfID.Value}, " +
                $"Suffix={ddlSuffix.SelectedItem.Text}"
            );

            ShowMessage("Record updated successfully");
            LoadGrid();
            ClearSelection();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hfID.Value == "")
            {
                ShowMessage("Select a record to delete");
                return;
            }

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("Suffix_Linking_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.Parameters.AddWithValue("@ID", hfID.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // ✅ LOG DELETE (your exact requirement)
            AuditLogger.Log(
                Session["USER_NAME"].ToString(),
                "Suffix Linking",
                "DELETE",
                $"Deleted record ID={hfID.Value}"
            );

            ShowMessage("Record deleted successfully");
            LoadGrid();
            ClearSelection();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
            lblMsg.Visible = false;
            btnAdd.Enabled = true;
        }

        
        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            var k = gvData.SelectedDataKey.Values;
            hfID.Value = k["ID"].ToString();
            ddlSuffix.SelectedValue = k["SUFFIX_ID"].ToString();
            SetSafe(ddlPart, k["PART_ID"]);
            SetSafe(ddlLH, k["LH_PART_ID"]);
            SetSafe(ddlRH, k["RH_PART_ID"]);
            btnAdd.Enabled = false;
        }

        void ClearSelection()
        {
            hfID.Value = "";
            gvData.SelectedIndex = -1;
            ddlSuffix.SelectedIndex =
                ddlPart.SelectedIndex =
                ddlLH.SelectedIndex =
                ddlRH.SelectedIndex = 0;
        }

        bool IsDuplicate()
        {
            string table = $"{TYPE}_SUFFIX_LINKING";
            string where = (TYPE == "FD" || TYPE == "RD" || TYPE == "RQ")
                ? "SUFFIX_ID=@S AND ISNULL(LH_PART_ID,0)=ISNULL(@L,0) AND ISNULL(RH_PART_ID,0)=ISNULL(@R,0)"
                : "SUFFIX_ID=@S AND ISNULL(PART_ID,0)=ISNULL(@P,0)";

            using (SqlCommand cmd = new SqlCommand(
                $"SELECT COUNT(*) FROM {table} WHERE {where}",
                new SqlConnection(conStr)))
            {
                cmd.Parameters.AddWithValue("@S", ddlSuffix.SelectedValue);
                cmd.Parameters.AddWithValue("@P", ddlPart.SelectedValue);
                cmd.Parameters.AddWithValue("@L", ddlLH.SelectedValue);
                cmd.Parameters.AddWithValue("@R", ddlRH.SelectedValue);
                cmd.Connection.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        void SetSafe(DropDownList ddl, object val)
        {
            if (val != DBNull.Value && ddl.Items.FindByValue(val.ToString()) != null)
                ddl.SelectedValue = val.ToString();
        }

        void ShowMessage(string msg)
        {
            lblMsg.Text = msg;
            lblMsg.Visible = true;
        
        }
    }
}