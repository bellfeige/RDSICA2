<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
     <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Change Password"></asp:Label></div>
    <br />
    <br />
    Old Password:
    <asp:TextBox ID="txtOldPwd" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="oldpasswordReq"
            runat="server"
            ControlToValidate="txtOldPwd"
            ErrorMessage="Old Password is required!"
            ForeColor="Red"
            SetFocusOnError="True" Display="Dynamic" ValidationGroup='valGroup1' />
    <br />
    <br />
    New Password:
    <asp:TextBox ID="txtNewPwd" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
     <asp:RequiredFieldValidator ID="passwordReq"
            runat="server"
            ControlToValidate="txtNewPwd"
            ErrorMessage="New Password is required!"
            ForeColor="Red"
            SetFocusOnError="True" Display="Dynamic" ValidationGroup='valGroup1' />
        <asp:RegularExpressionValidator runat="server" ID="rexNumber"
            ControlToValidate="txtNewPwd" ValidationExpression="^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,50}$"
            ErrorMessage="Minimum 6 characters containing at least one number and one letter!"
            ForeColor="Red" ValidationGroup='valGroup1' />
    <br />

    Confirm New Password:
    <asp:TextBox ID="txtNewPwdConfirm" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator ID="confirmPasswordReq"
            runat="server"
            ControlToValidate="txtNewPwdConfirm"
            ErrorMessage="Password confirmation is required!"
            ForeColor="Red"
            SetFocusOnError="True"
            Display="Dynamic" ValidationGroup='valGroup1' />
        <asp:CompareValidator ID="comparePasswords"
            runat="server"
            ControlToCompare="txtNewPwd"
            ControlToValidate="txtNewPwdConfirm"
            ForeColor="Red"
            ErrorMessage="Your passwords do not match up!"
            Display="Dynamic" />
    <br />
    <br />
    <asp:Button ID="btnSubmit" ValidationGroup='valGroup1' runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    &nbsp;
    <asp:Label ID="lblChangeResult" runat="server" Text=""></asp:Label>

</asp:Content>

