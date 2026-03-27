<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Master_Data.aspx.cs"
    Inherits="AIS_WEB_APPLICATION.Master_Data" %>

<%@ Register Src="~/HeaderUserControl.ascx" TagPrefix="uc1" TagName="HeaderUserControl" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Master Data</title>
    <link href="Assert/favi.png" rel="shortcut icon" />
    <link href="stylesheet/bootstrap.min.css" rel="stylesheet" />
    <link href="stylesheet/bootstrap-icons-1.13.1/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            min-height: 100vh;
            background: linear-gradient(135deg,#667eea,#764ba2);
            font-family: 'Segoe UI',sans-serif;
            overflow: hidden;
        }

        .glass-card {
            background: rgba(255,255,255,.15);
            backdrop-filter: blur(15px);
            border-radius: 18px;
            padding: 25px;
            color: #fff;
            margin-top: 100px !important;
            box-shadow: 0 15px 40px rgba(0,0,0,.3);
        }

        .title {
            font-size: 26px;
            font-weight: 700;
            margin-bottom: 20px;
            text-align: center;
        }

        .lblmsg {
            background-color: #667eea;
            color: white;
            border: none;
            padding: 6px;
        }
    </style>
</head>

<body>
    <form runat="server">

        <uc1:HeaderUserControl runat="server" ID="HeaderUserControl1" />

        <div class="container mt-4">
            <div class="glass-card">

                <div class="title">
                    <i class="bi bi-box-seam"></i>MASTER DATA
                </div>

                <!-- TYPE -->
                <div class="row mb-3">
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlType" runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                            <asp:ListItem Text="-- Select --" Value="" />
                            <asp:ListItem Text="FD" />
                            <asp:ListItem Text="RD" />
                            <asp:ListItem Text="BCK" />
                            <asp:ListItem Text="WS" />
                            <asp:ListItem Text="RQ" />
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:HiddenField ID="hfID" runat="server" />

                <!-- INPUTS -->
                <div class="row g-3 mb-3">
                    <div class="col-md-3">
                        <asp:TextBox ID="txtPartCode" runat="server"
                            CssClass="form-control" placeholder="Part Code" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtPartName" runat="server"
                            CssClass="form-control" placeholder="Part Name" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtColorCode" runat="server"
                            CssClass="form-control" placeholder="Color Code" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtSide" runat="server"
                            CssClass="form-control" placeholder="Side" />
                    </div>
                </div>

                <!-- BUTTONS -->
                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Button ID="btnAdd" runat="server"
                            Text="Add" CssClass="btn btn-success w-100"
                            OnClick="btnAdd_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnUpdate" runat="server"
                            Text="Update" CssClass="btn btn-warning w-100"
                            OnClick="btnUpdate_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnDelete" runat="server"
                            Text="Delete" CssClass="btn btn-danger w-100"
                            OnClick="btnDelete_Click" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnClear" runat="server"
                            Text="Clear" CssClass="btn btn-secondary w-100"
                            OnClick="btnClear_Click" />
                    </div>
                </div>

                <asp:GridView ID="gvMaster" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="ID"
                    CssClass="table table-dark table-hover"
                    OnSelectedIndexChanged="gvMaster_SelectedIndexChanged" OnRowCreated="gvMaster_RowCreated">

                    <Columns>
                        <asp:CommandField ShowSelectButton="true" />

                        <asp:BoundField DataField="PART_CODE" HeaderText="PART CODE" />
                        <asp:BoundField DataField="PART_NAME" HeaderText="PART NAME" />
                        <asp:BoundField DataField="COLOR_CODE" HeaderText="COLOR CODE" />
                        <asp:BoundField DataField="SIDE" HeaderText="SIDE" />
                    </Columns>
                </asp:GridView>


                <!-- MESSAGE -->
                <div class="mt-3 col-md-4">
                    <asp:Label ID="lblMsg" runat="server"
                        CssClass="lblmsg form-control" Visible="false" />
                </div>

            </div>
        </div>

    </form>
</body>
</html>
