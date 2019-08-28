<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div style="text-align:center;margin:20px">
        <asp:Image ID="Image1" runat="server" ImageUrl="Sources/Images/errorOops.png" Height="200px" />
        <div style="font-size:20px">
            <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label></div>
    </div>


</asp:Content>

