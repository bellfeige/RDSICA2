<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Account_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Create a new account"></asp:Label>
    </div>
    <br />
    Enter User Name :
            <asp:TextBox ID="txtUsername" MaxLength="50" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator runat="server" ID="txtUsernameRegularExpressionValidator"
        ControlToValidate="txtUsername" ValidationExpression="^[0-9A-Za-z]{3,50}$"
        ErrorMessage="Minimum 3 characters only letter or number allowed"
        ForeColor="Red" ValidationGroup='valGroup1' />
    <asp:RequiredFieldValidator ID="usernameReq"
        runat="server"
        ControlToValidate="txtUsername"
        ErrorMessage="Username is required!"
        ForeColor="Red"
        SetFocusOnError="True"
        ValidationGroup='valGroup1' />


    <br />
    Enter Password:
            <asp:TextBox ID="txtPassword" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="passwordReq"
        runat="server"
        ControlToValidate="txtPassword"
        ErrorMessage="Password is required!"
        ForeColor="Red"
        SetFocusOnError="True" Display="Dynamic" ValidationGroup='valGroup1' />
    <asp:RegularExpressionValidator runat="server" ID="rexNumber"
        ControlToValidate="txtPassword" ValidationExpression="^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,50}$"
        ErrorMessage="Minimum 6 characters containing at least one number and one letter!"
        ForeColor="Red" ValidationGroup='valGroup1' />
    <br />

    Confirm Password:
            <asp:TextBox ID="txtConfirmPassword" MaxLength="50" TextMode="Password" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="confirmPasswordReq"
        runat="server"
        ControlToValidate="txtConfirmPassword"
        ErrorMessage="Password confirmation is required!"
        ForeColor="Red"
        SetFocusOnError="True"
        Display="Dynamic" ValidationGroup='valGroup1' />
    <asp:CompareValidator ID="comparePasswords"
        runat="server"
        ControlToCompare="txtPassword"
        ControlToValidate="txtConfirmPassword"
        ForeColor="Red"
        ErrorMessage="Your passwords do not match up!"
        Display="Dynamic" />
    <br />
    <br />
    Account Type:
     &nbsp;<asp:DropDownList ID="selectAccountType" runat="server">
         <asp:ListItem Value="0" Text="Administrator"></asp:ListItem>
         <asp:ListItem Selected="True" Value="1" Text="User"></asp:ListItem>
     </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="btnSubmit" ValidationGroup='valGroup1' runat="server" Text="Submit" OnClick="AddAccount" />
    <asp:Label ID="lblAddAccountResult" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />


</asp:Content>

