<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeState.aspx.cs" Inherits="Account_ChangeState" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
      <div class="PageTitle">
            <asp:Label ID="PageTitle" runat="server" Text=""></asp:Label>
        </div>
    <br />
    User name:&nbsp;
    <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
    <br />
    <br />
     Full name:&nbsp;
    <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
    <br />
    <br />
     Email address:&nbsp;
    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
    <br />
    <br />
     Account create date:&nbsp;
    <asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label>
    <br />
    <br />
     Last time login:&nbsp;
    <asp:Label ID="lblLastTimeLogin" runat="server" Text=""></asp:Label>
    <br />
    <br />
       Account State:&nbsp;
    <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnChangeState" runat="server" Text="" OnClick="btnChangeState_Click" />
    &nbsp;
    <asp:Label ID="lblChangeMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
</asp:Content>

