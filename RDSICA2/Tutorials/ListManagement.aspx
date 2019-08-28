<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListManagement.aspx.cs" Inherits="Tutorials_ListManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <link href="~/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Tutorials Management"></asp:Label>
    </div>
    <br />
    
    <asp:Label ID="lblDeleteTutorialMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
     <br />
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
        <Columns>

              <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Detail.aspx?Id={0}" HeaderText="View" Text="View" />
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Edit.aspx?Id={0}" HeaderText="Edit" Text="Edit" />
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Delete.aspx?Id={0}" HeaderText="Delete" Text="Delete" />
            
        </Columns>
    </asp:GridView>
    <br />


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>" SelectCommand="SELECT T.Id, T.Title,C.Name AS Category, T.ownerId as AuthorId ,A.Username AS Author,  CASE WHEN T.Permission = 0 THEN 'Public' WHEN T.Permission = 1 THEN 'Private' END AS Permission, T.CreateDate FROM Tutorials AS T INNER JOIN Accounts AS A ON A.Id = T.OwnerId INNER JOIN Categories AS C ON C.Id = T.CategoryId"></asp:SqlDataSource>

</asp:Content>

