<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Tutorials_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
    <link href="~/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <div class="PageTitle"><asp:Label ID="PageTitle" runat="server" Text="Are you sure to delete this tutorial?"></asp:Label></div>
      
    <br />
    <asp:Label ID="warning" runat="server" Text="Warning: The deletion cannot be rolled back!" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
    <br />
  
    <asp:Image ID="ImgThum" runat="server" Height="270px" Width="300px"/>
    <br />
    <br />
    <asp:Button ID="DeleteTut" runat="server" Text="Delete" OnClick="DeleteTut_Click" />
</asp:Content>

