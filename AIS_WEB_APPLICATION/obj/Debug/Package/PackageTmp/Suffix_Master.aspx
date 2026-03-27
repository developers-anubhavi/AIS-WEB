<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suffix_Master.aspx.cs" Inherits="AIS_WEB_APPLICATION.Suffix_Master" %>

<%@ Register Src="~/HeaderUserControl.ascx" TagPrefix="uc1" TagName="HeaderUserControl" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>Suffix Master</title>

    <link href="stylesheet/bootstrap.min.css" rel="stylesheet" />
    <link href="stylesheet/bootstrap-icons-1.13.1/bootstrap-icons.css" rel="stylesheet" />
    <link href="Assert/favi.png" rel="shortcut icon" />

    <style>
        body {
            min-height: 100vh;
            background: linear-gradient(135deg,#667eea,#764ba2);
            font-family: 'Segoe UI', sans-serif;
        }

        .glass-card {
            background: rgba(255,255,255,.15);
            backdrop-filter: blur(15px);
            border-radius: 18px;
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

        .listcolor {
            background-color: black;
            color: white;
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

    <div class="container-fluid min-vh-100 d-flex align-items-center">
        <div class="row justify-content-center w-100">
            <div class="col-md-5">
                <div class="glass-card">

                    <div class="title">
                        <i class="bi bi-tags"></i> SUFFIX MASTER
                    </div>

                    <asp:HiddenField ID="hfSuffixId" runat="server" />

                    <!-- INPUT -->
                    <div class="mb-3">
                        <label class="form-label text-white">Suffix</label>
                        <asp:TextBox ID="txtSuffix" runat="server"
                            CssClass="form-control" />
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

                    <!-- LIST -->
                    <asp:ListBox ID="lstSuffix" runat="server"
                        CssClass="form-control listcolor"
                        Height="220px"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="lstSuffix_SelectedIndexChanged" />

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