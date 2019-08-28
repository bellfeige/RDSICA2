<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Management.aspx.cs" Inherits="Account_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Account Management"></asp:Label>
    </div>
    <br />

    <asp:Button ID="btnAddAccount" runat="server" Text="Create a new account" OnClick="btnAddAccount_Click" />
    <br />
     <br />
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
   
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Profile.aspx?Id={0}" HeaderText="View" Text="View" />
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ProfileEdit.aspx?Id={0}" HeaderText="Edit" Text="Edit" />
 
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ChangeState.aspx?Id={0}" HeaderText="Change State" Text="Change State" />
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Account Type" HeaderText="Account Type" ReadOnly="True" SortExpression="Account Type" />
                <asp:BoundField DataField="Account State" HeaderText="Account State" ReadOnly="True" SortExpression="Account State" />
                <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name" />
                <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name" />
                <asp:BoundField DataField="Create Date" HeaderText="Create Date" SortExpression="Create Date" />
                <asp:BoundField DataField="Last Login Date" HeaderText="Last Login Date" SortExpression="Last Login Date" />
            </Columns>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>" SelectCommand="SELECT Id, Username, Email, CASE WHEN AccountType = 0 THEN 'Administrator' WHEN AccountType = 1 THEN 'User' END AS 'Account Type', FirstName AS 'First Name', LastName AS 'Last Name', CreateDate AS 'Create Date', LastLoginDate AS 'Last Login Date', CASE WHEN AccountDisabled = 0 THEN 'Enabled' WHEN AccountDisabled = 1 THEN 'Disabled' END AS 'Account State' FROM Accounts"></asp:SqlDataSource>

    </div>
    <br />

</asp:Content>

