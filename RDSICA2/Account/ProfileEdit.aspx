<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProfileEdit.aspx.cs" Inherits="Account_ProfileEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Edit Profile"></asp:Label>
    </div>
    <br /><br />
    <div>
        First Name:
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <br />
        <br />
    Last Name:
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
         <br />
        <br />
        Email address:
        <asp:TextBox ID="txtEmailAdd" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator runat="server" ID="txtUsernameRegularExpressionValidator"
            ControlToValidate="txtEmailAdd" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ErrorMessage="Not a email address format!"
            ForeColor="Red" ValidationGroup='valGroup1' />
    </div>
    <br />
    <asp:FileUpload ID="avatarUpload" runat="server" /> 
    <br />
    <asp:Label ID="uploadAvatarMsg" runat="server" Text=""></asp:Label>
    <br />
    <br />
     Bio:<br />
            <asp:TextBox ID="txtBio" placeholder="Edit biography here...." MaxLength="1000" TextMode="MultiLine" Rows="8" Width="350px" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="BtnUpdate" ValidationGroup='valGroup1' runat="server" Text="Update" OnClick="BtnUpdate_Click" />

</asp:Content>

