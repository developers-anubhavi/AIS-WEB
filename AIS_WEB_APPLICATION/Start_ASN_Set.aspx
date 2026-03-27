<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start_ASN_Set.aspx.cs" Inherits="AIS_WEB_APPLICATION.Start_ASN_Set" %>

<%@ Register Src="~/HeaderUserControl.ascx" TagPrefix="uc1" TagName="HeaderUserControl" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>START ASN SET</title>

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
            width: 420px;
            background: rgba(255,255,255,.15);
            backdrop-filter: blur(15px);
            border-radius: 18px;
            margin: 250px auto;
            padding: 25px;
            color: #fff;
            box-shadow: 0 15px 40px rgba(0,0,0,.3);
        }

        .title {
            font-size: 26px;
            font-weight: 700;
            margin-bottom: 25px;
            text-align: center;
        }

        .asn-row {
            display: flex;
            align-items: center;
            gap: 15px;
            margin-bottom: 15px;
        }

        .asn-row .form-control {
            height: 42px;
            font-size: 18px;
            text-align: center;
            font-weight: 600;
        }

        .asn-row .btn {
            min-width: 120px;
            font-weight: 600;
        }

        .msg {
            display: block;
            text-align: center;
            margin-top: 15px;
            padding: 8px;
            background-color: #667eea;
            color: #fff;
            border-radius: 6px;
        }
    </style>
</head>

<body>

<form id="form1" runat="server">

    <!-- Header -->
    <uc1:HeaderUserControl runat="server" ID="HeaderUserControl1" />

    <div class="glass-card">

        <div class="title">
            <i class="bi bi-box-seam"></i> ASN SETTINGS
        </div>

        <!-- RD -->
        <div class="asn-row">
            <asp:TextBox ID="txtRD" runat="server"
                CssClass="form-control" MaxLength="3"
                placeholder="RD Start ASN" />

            <asp:Button ID="btnRD" runat="server"
                CssClass="btn btn-light"
                Text="RD SET"
                OnClick="btnRD_Click" />
        </div>

        <!-- FD -->
        <div class="asn-row">
            <asp:TextBox ID="txtFD" runat="server"
                CssClass="form-control" MaxLength="3"
                placeholder="FD Start ASN" />

            <asp:Button ID="btnFD" runat="server"
                CssClass="btn btn-light"
                Text="FD SET"
                OnClick="btnFD_Click" />
        </div>

        <!-- RQ -->
        <div class="asn-row">
            <asp:TextBox ID="txtRQ" runat="server"
                CssClass="form-control" MaxLength="3"
                placeholder="RQ Start ASN" />

            <asp:Button ID="btnRQ" runat="server"
                CssClass="btn btn-light"
                Text="RQ SET"
                OnClick="btnRQ_Click" />
        </div>

        <!-- WS -->
        <div class="asn-row">
            <asp:TextBox ID="txtWS" runat="server"
                CssClass="form-control" MaxLength="3"
                placeholder="WS Start ASN" />

            <asp:Button ID="btnWS" runat="server"
                CssClass="btn btn-light"
                Text="WS SET"
                OnClick="btnWS_Click" />
        </div>

        <!-- Message -->
        <asp:Label ID="lblMsg" runat="server"
            CssClass="msg"
            Visible="false" />

    </div>

</form>

</body>
</html>