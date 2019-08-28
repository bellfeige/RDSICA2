<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <div>
        <div class="PageTitle">
            <asp:Label ID="PageTitle" runat="server" Text="Login"></asp:Label>
        </div>
        <asp:Label ID="lblRegSucc" runat="server" Text="" ForeColor="Red"></asp:Label><br />
         <asp:Label ID="lblCookie" Visible="false" runat="server" Text="Your web browser has disabled cookies so that login function cannnot work correctly. Click the link below to learn about how to enable cookies." ForeColor="Red"></asp:Label><br />
        <asp:HyperLink ID="hylCookie" Visible="false" runat="server" href="https://www.whatismybrowser.com/guides/how-to-enable-cookies">https://www.whatismybrowser.com/guides/how-to-enable-cookies</asp:HyperLink>
     


        <asp:Login ID="Login1" runat="server" OnAuthenticate="ValidateUser" Height="167px" Width="377px">
            <LayoutTemplate>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="LoginButton">
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0" style="height: 167px; width: 377px;">
                                    <tr>
                                        <td align="center" colspan="2">Log In</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </LayoutTemplate>
        </asp:Login>




    </div>


</asp:Content>

