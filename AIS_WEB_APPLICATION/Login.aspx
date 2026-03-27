<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AIS_WEB_APPLICATION.Login" %>




<!DOCTYPE html>
<html>
<head runat="server">
    <title>User Login</title>

    <link href="stylesheet/bootstrap.min.css" rel="stylesheet" />
    <link href="stylesheet/bootstrap-icons-1.13.1/bootstrap-icons.css" rel="stylesheet" />
        <link href="Assert/favi.png" rel="shortcut icon" />

    <style>
        body {
            height: 100vh;
            background: linear-gradient(135deg,#667eea,#764ba2);
            display: flex;
            justify-content: center;
            align-items: center;
            font-family: 'Segoe UI', sans-serif;
        }


    
        /* =====================
           FIXED HEADER
        ===================== */
        .fixed-header {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            height: 80px;
            background: rgba(255,255,255,0.9);
            backdrop-filter: blur(10px);
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 20px;
            font-size: 28px;
            font-weight: 700;
            z-index: 1000;
            box-shadow: 0 6px 20px rgba(0,0,0,0.2);
        }

            .fixed-header .logo {
                height: 55px;
            }



        .login-card {
            background: rgba(255,255,255,0.15);
            backdrop-filter: blur(15px);
            border-radius: 18px;
            padding: 30px;
            width: 350px;
            color: white;
            box-shadow: 0 15px 40px rgba(0,0,0,0.3);
        }

        .login-title {
            font-size: 24px;
            font-weight: 700;
            text-align: center;
            margin-bottom: 20px;
        }

        .lblmsg {
            background: black;
            color: white;
            border: none;
            margin-top: 10px;
            padding: 6px;
            text-align: center;
        }
    </style>
</head>

<body>
    <form runat="server">
        <!-- FIXED HEADER -->
        <div class="asahi-header fixed-header">
            <div class="header-left">
                <img src="Assert/AAPL.jpeg" class="logo" />
            </div>

            <div class="header-center">
                ASAHI INDIA GLASS LTD.
            </div>

            <div class="header-right">
                <img src="Assert/asahi-india-glass-ltd-logo.png" class="logo" />
            </div>
        </div>
        <div class="login-card">

            <div class="login-title">
                <i class="bi bi-person-circle"></i>USER LOGIN
            </div>

            <asp:TextBox ID="txtName" runat="server"
                CssClass="form-control mb-3"
                placeholder="User Name" />

            <asp:TextBox ID="txtPassword" runat="server"
                CssClass="form-control mb-3"
                TextMode="Password"
                placeholder="Password" />

            <asp:Button ID="btnLogin" runat="server"
                Text="Login"
                CssClass="btn btn-success w-100"
                OnClick="btnLogin_Click" />

            <asp:Label ID="lblMsg" runat="server"
                CssClass="lblmsg form-control"
                Visible="false" />

        </div>

    </form>
</body>
</html>
