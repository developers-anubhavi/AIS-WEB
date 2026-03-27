<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderUserControl.ascx.cs" Inherits="AIS_WEB_APPLICATION.HeaderUserControl" %>
<!-- ===== HEADER ===== -->


<link href="stylesheet/header.css" rel="stylesheet" />

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

<!-- FIXED SIDE NAV -->
<div class="side-nav fixed-sidenav">
    <a href="Suffix_Linking.aspx"><i class="bi bi-link-45deg module-icon moduleiconside"></i><span>Suffix Linking</span></a>
    <a href="Suffix_Master.aspx"><i class="bi bi-tags module-icon moduleiconside"></i> <span>Suffix Master</span></a>
    <a href="Dolly_Master.aspx"><i class="bi bi-box-seam module-icon moduleiconside"></i> <span>Dolly Master</span></a>
    <a href="Master_Data.aspx"><i class="bi bi-database module-icon moduleiconside"></i><span>Master Data</span></a>
    <a href="Landing.aspx"><i class="bi bi-gear module-icon moduleiconside"></i> <span>Settings</span></a>
   <%-- <a href="Start_ASN_Set.aspx"><i class="bi bi-gear module-icon moduleiconside"></i> <span>Start ASN Set</span></a>--%>
    <asp:PlaceHolder ID="phASN" runat="server">
    <a href="Start_ASN_Set.aspx">
        <i class="bi bi-gear module-icon moduleiconside"></i>
        <span>Start ASN Set</span>
    </a>
</asp:PlaceHolder>
     <!-- LOGOUT -->
    <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">
        <i class="bi bi-box-arrow-right module-icon moduleiconside"></i>
        <span>Logout</span>
    </asp:LinkButton>


     
     
    

    
    <!-- USER INFO -->
<div class="user-info">
    <i class="bi bi-person-circle module-icon moduleiconside"></i>
    <asp:Label CssClass="lbluser" ID="lblUserName" runat="server"></asp:Label>
</div>

</div>


