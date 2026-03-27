<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suffix_Linking.aspx.cs" Inherits="AIS_WEB_APPLICATION.Suffix_Linking" %>

<%@ Register Src="~/HeaderUserControl.ascx" TagPrefix="uc1" TagName="HeaderUserControl" %>




<!DOCTYPE html>
<html>
<head runat="server">
    <title>Suffix Linking</title>

    <!-- Bootstrap -->

        <link href="stylesheet/bootstrap.min.css" rel="stylesheet" />
    <link href="Assert/favi.png" rel="shortcut icon" />
    <link href="stylesheet/bootstrap-icons-1.13.1/bootstrap-icons.css" rel="stylesheet" />
    <style>

        body {
            min-height: 100vh;
            background: linear-gradient(135deg,#667eea,#764ba2);
            font-family: 'Segoe UI', sans-serif;
             overflow: hidden;
        }
        .glass-card {
                 margin-top: 100px!important;
            background: rgba(255,255,255,0.15);
            backdrop-filter: blur(15px);
            border-radius: 18px;
            padding: 25px;
            color: white;
        }
        .title {
            font-size: 26px;
            font-weight: 700;
            text-align: center;
            margin-bottom: 20px;
        }
        .grid-dark {
            background: black;
            color: white;
        }
        .grid-dark th {
            background: #111;
            color: white;
        }
        .grid-dark td {
            background: #222;
            color: white;
        }
        .grid-scroll {
        max-height: 480px;        /* adjust height */
        overflow-y: auto;         /* vertical scroll */
        overflow-x: auto;         /* horizontal scroll if needed */
       
    }

    .grid-scroll table {
        width: 100%;
        border-collapse: collapse;
    }
    </style>
</head>

<body>
    <form runat="server">

        <uc1:HeaderUserControl runat="server" ID="HeaderUserControl1" />
        <div class="container mt-4">
        <div class="glass-card">

            <div class="title">
                <i class="bi bi-link-45deg"></i> SUFFIX LINKING
            </div>

                <!-- TYPE -->
                <asp:DropDownList ID="ddlType" runat="server"
                    CssClass="form-select mb-3"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    <asp:ListItem Text="-- Select Type --" Value="" />
                    <asp:ListItem Text="FD" />
                    <asp:ListItem Text="RD" />
                    <asp:ListItem Text="BCK" />
                    <asp:ListItem Text="WS" />
                    <asp:ListItem Text="RQ" />
                </asp:DropDownList>

                <!-- DROPDOWNS -->
                <asp:DropDownList ID="ddlSuffix" runat="server" CssClass="form-select mb-2" />
                <asp:DropDownList ID="ddlPart" runat="server" CssClass="form-select mb-2" />

                <div class="row mb-2">
                    <div class="col">
                        <asp:DropDownList ID="ddlLH" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col">
                        <asp:DropDownList ID="ddlRH" runat="server" CssClass="form-select" />
                    </div>
                </div>

                <asp:HiddenField ID="hfID" runat="server" />

                <!-- BUTTONS -->
                <div class="row mb-3">
                    <div class="col">
                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                            CssClass="btn btn-success w-100"
                            OnClick="btnAdd_Click" />
                    </div>
                    <div class="col">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"
                            CssClass="btn btn-warning w-100"
                            OnClick="btnUpdate_Click" />
                    </div>
                    <div class="col">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete"
                            CssClass="btn btn-danger w-100"
                            OnClick="btnDelete_Click" />
                    </div>
                    <div class="col">
                        <asp:Button ID="btnClear" runat="server" Text="Clear"
                            CssClass="btn btn-secondary w-100"
                            OnClick="btnClear_Click" />
                    </div>
                </div>

                <!-- GRID -->
          <div id="divGrid" runat="server" class="grid-scroll" style="display:none;">
                <asp:GridView  ID="gvData" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="ID,SUFFIX_ID,PART_ID,LH_PART_ID,RH_PART_ID"
                    CssClass="table table-bordered grid-dark"
                    OnSelectedIndexChanged="gvData_SelectedIndexChanged" OnRowCreated="gvData_RowCreated">

                    <Columns>
                        <asp:CommandField ShowSelectButton="true" />
                        <asp:BoundField DataField="SUFFIX" HeaderText="Suffix" />
                        <asp:BoundField DataField="LH_PART" HeaderText="LH Part" />
                        <asp:BoundField DataField="RH_PART" HeaderText="RH Part" />
                        <asp:BoundField DataField="PART" HeaderText="Part" />
                    </Columns>
                </asp:GridView>
                </div>

                <!-- MESSAGE -->
                <asp:Label ID="lblMsg" runat="server"
                    CssClass="form-control mt-3"
                    Style="background: black; color: white; border: none"
                    Visible="false" />

            </div>
        </div>

    </form>
</body>
</html>
