<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" CodeFile="Default.aspx.cs" Inherits="Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="Latest Released Tutorials"></asp:Label>
    </div>
    <br />
    <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" RepeatColumns="3" RepeatDirection="Horizontal" ShowFooter="False">
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>

            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <%--<asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>'></asp:Label>--%>
                        <div><a href="<%# String.Format("Tutorials/Detail.aspx?Id={0}", Eval("ID")) %>"><%# Eval("Title") %></a></div>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <%--<asp:ImageButton ID="ImageButton1" runat="server" Height="270px" Width="300px" ImageUrl='<%# Eval("ThumbnailPath") %>' />--%>
                        <a href="<%# String.Format("Tutorials/Detail.aspx?Id={0}", Eval("ID")) %>">
                            <asp:Image ID="Image1" runat="server" Height="220px" Width="250px" ImageUrl='<%# Eval("ThumbnailPath") %>' /></a>

                    </asp:TableCell>
                </asp:TableRow>
               
            </asp:Table>


        </ItemTemplate>
        <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    </asp:DataList>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>" SelectCommand="SELECT Id, Title, ThumbnailPath FROM Tutorials WHERE (Permission = @Permission) ORDER BY CreateDate DESC">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="Permission" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
