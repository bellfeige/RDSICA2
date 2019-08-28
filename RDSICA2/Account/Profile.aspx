<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Account_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="User Profile"></asp:Label></div>
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
   
    <div class="userProfile">
        <div class="userProfile-avatar">
            <asp:Image ID="Avatar" runat="server" Width="200px" ImageUrl="../Sources/Images/avatar.png" />
        </div>
        <br />
        <div>
            <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
            <br />
            <br />
            Email:&nbsp; <asp:Label ID="lblEmailAdd" runat="server" Text=""></asp:Label>
            <br />
            <br />
        </div>
        <div>
            Bio:<br />
            <asp:TextBox ID="txtBio" MaxLength="1000" TextMode="MultiLine" Rows="8" Width="350px" runat="server"></asp:TextBox>
            <br />
            <br />
        </div>
        <asp:Label ID="lblLastUpdateTime" runat="server" Text=""></asp:Label>
        <br />
            <br />

        <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />

        &nbsp;&nbsp;

        <asp:Button ID="btnChangePwd" runat="server" Text="Change Password" OnClick="btnChangePwd_Click" />
    </div>


    <br />


</asp:Content>

