<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dolly_Master.aspx.cs" Inherits="AIS_WEB_APPLICATION.Dolly_Master" %>

<%@ Register Src="~/HeaderUserControl.ascx" TagPrefix="uc1" TagName="HeaderUserControl" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Dolly Master</title>

    <link href="stylesheet/bootstrap.min.css" rel="stylesheet" />
    <link href="Assert/favi.png" rel="shortcut icon" />
    <link href="stylesheet/bootstrap-icons-1.13.1/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            min-height: 100vh;
            background: linear-gradient(135deg,#667eea,#764ba2);
            font-family: 'Segoe UI',sans-serif;
        }

        .glass-card {
            background: rgba(255,255,255,.15);
            backdrop-filter: blur(15px);
            border-radius: 18px;
                 margin-top: 100px!important;
            padding: 25px;
            color: #fff;
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
            color: white !important;
            border: none;
            margin-top: 10px;
        }
    </style>
</head>

<body>
<form id="form1" runat="server">

    <uc1:HeaderUserControl runat="server" ID="HeaderUserControl1" />

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-lg-10">
                <div class="glass-card">

                    <div class="title">
                        <i class="bi bi-box-seam"></i> DOLLY MASTER
                    </div>

                    <asp:HiddenField ID="hfID" runat="server" />

                    <!-- INPUTS -->
                    <div class="row g-3 mb-3">
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDolly" runat="server"
                                CssClass="form-control" Placeholder="Dolly No" />
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtLine" runat="server"
                                CssClass="form-control" Placeholder="Line Name" />
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtQty" runat="server"
                                CssClass="form-control" Placeholder="Quantity" />
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtEmpty" runat="server"
                                CssClass="form-control" Placeholder="Empty Flag" />
                        </div>
                    </div>

                    <!-- BUTTONS -->
                    <div class="row g-2 mb-3">
                        <div class="col-md-3">
                            <asp:Button ID="btnAdd" runat="server"
                                Text="Add" CssClass="btn btn-success w-100"
                                OnClick="btnAdd_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnUpdate" runat="server"
                                Text="Update" CssClass="btn btn-warning w-100"
                                OnClick="btnUpdate_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnDelete" runat="server"
                                Text="Delete" CssClass="btn btn-danger w-100"
                                OnClick="btnDelete_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnClear" runat="server"
                                Text="Clear" CssClass="btn btn-secondary w-100"
                                OnClick="btnClear_Click" />
                        </div>
                    </div>

                    <!-- GRID -->
                    <asp:GridView ID="gvDolly" runat="server"
                        AutoGenerateColumns="false"
                        DataKeyNames="ID"
                        CssClass="table table-dark table-hover"
                        OnSelectedIndexChanged="gvDolly_SelectedIndexChanged">

                        <Columns>
                            <asp:CommandField ShowSelectButton="true" />
                            <asp:BoundField DataField="DTE" HeaderText="Date"
                                DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="DOLLY_NO" HeaderText="Dolly No" />
                            <asp:BoundField DataField="LINE_NAME" HeaderText="Line" />
                            <asp:BoundField DataField="QUANTITY" HeaderText="Qty" />
                            <asp:BoundField DataField="EMPTY_FLAG" HeaderText="Empty" />
                        </Columns>
                    </asp:GridView>

                    <!-- MESSAGE -->
                    <asp:Label ID="lblMsg" runat="server"
                        CssClass="form-control lblmsg"
                        Visible="false" />

                </div>
            </div>
        </div>
    </div>

</form>
</body>
</html>