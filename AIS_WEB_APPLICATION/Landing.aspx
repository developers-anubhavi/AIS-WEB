<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="AIS_WEB_APPLICATION.Landing" %>

<%@ Register Src="~/HeaderUserControl.ascx" TagPrefix="uc1" TagName="HeaderUserControl" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASAHI</title>

    <!-- Bootstrap 5 -->
    <link href="stylesheet/bootstrap.min.css" rel="stylesheet" />
    <link href="stylesheet/Landing.css" rel="stylesheet" />
    <!-- Bootstrap Icons -->
    <link href="stylesheet/bootstrap-icons-1.13.1/bootstrap-icons.css" rel="stylesheet" />
    <link href="Assert/favi.png" type="image/x-icon" rel="shortcut icon" />
</head>


<body>
    <form id="form1" runat="server">

        <uc1:HeaderUserControl runat="server" ID="HeaderUserControl" />
    
            <!-- ===== MODULES ===== -->
            <div class="container mt-5">
                <div class="row g-4">
                    <div class="col-md-12">
                        <h2 style="margin-top: 100px; color:white; margin-bottom: 20px;"><i class="bi bi-gear module-icon"></i>Settings</h2>
                    </div>
                    <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                        <a href="Master_Data.aspx" class="module-link">
                            <div class="module-card">
                                <i class="bi bi-database module-icon"></i>
                                <div class="module-name">MASTER DATA</div>
                            </div>
                        </a>
                    </div>

                    <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                        <a href="Suffix_Master.aspx" class="module-link">
                            <div class="module-card">
                                <i class="bi bi-tags module-icon"></i>
                                <div class="module-name">SUFFIX MASTER</div>
                            </div>
                        </a>
                    </div>

                    <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                        <a href="Suffix_Linking.aspx" class="module-link">
                            <div class="module-card">
                                <i class="bi bi-link-45deg module-icon"></i>
                                <div class="module-name">SUFFIX LINKING</div>
                            </div>
                        </a>
                    </div>

                    <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                        <a href="Dolly_Master.aspx" class="module-link">
                            <div class="module-card">
                                <i class="bi bi-box-seam module-icon"></i>
                                <div class="module-name">DOLLY MASTER</div>
                            </div>
                        </a>
                    </div>

                </div>
            </div>
 
    </form>
</body>
</html>
