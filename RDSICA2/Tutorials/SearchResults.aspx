<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchResults.aspx.cs" Inherits="Tutorials_SearchResults" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Searching Results"></asp:Label>
    </div>
    <br />
    <asp:Label ID="lblResultCount" runat="server" Text=""></asp:Label>
    <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="" ForeColor="Black" GridLines="Horizontal" RepeatColumns="3" RepeatDirection="Horizontal" ShowFooter="False">
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>

            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <%--<asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>'></asp:Label>--%>
                        <a href="<%# String.Format("Detail.aspx?ID={0}", Eval("ID")) %>"><%# Eval("Title") %></a>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <%--<asp:ImageButton ID="ImageButton1" runat="server" Height="270px" Width="300px" ImageUrl='<%# Eval("ThumbnailPath") %>' />--%>
                        <a href="<%# String.Format("Detail.aspx?ID={0}", Eval("ID")) %>">
                            <asp:Image ID="Image1" runat="server" Height="220px" Width="250px" ImageUrl='<%# Eval("ThumbnailPath") %>' /></a>

                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>


        </ItemTemplate>
        <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    </asp:DataList>

</asp:Content>

