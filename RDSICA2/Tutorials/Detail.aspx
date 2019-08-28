<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Tutorials_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <link href="~/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <div>
        <div class="PageTitle">
            <asp:Label ID="PageTitle" runat="server" Text="Title"></asp:Label>
        </div>
        <br />
        <asp:Literal ID="Video" runat="server"></asp:Literal>
        <br />
        Category:&nbsp;
         <asp:Label ID="lblCategory" runat="server" Text="categoryName"></asp:Label>
        &nbsp;|&nbsp;
        Author:&nbsp;
        <%--<asp:Label ID="lblAuthorName" runat="server" Text="autherName"></asp:Label>--%>
        <asp:LinkButton ID="AuthorName" runat="server" OnClick="AuthorProfile_Click" Text=""></asp:LinkButton>

        &nbsp;|&nbsp
        Last Update at:&nbsp;
        <asp:Label ID="lblLastUpdateTime" runat="server" Text="lastUpdateTime"></asp:Label>
        <br />
        <%--<asp:ImageButton ID="Like" runat="server" />
        <asp:Label ID="LikeQty" runat="server" Text="10"></asp:Label>
        &nbsp;|&nbsp;
        <asp:ImageButton ID="Dislike" runat="server" />
        <asp:Label ID="DislikeQty" runat="server" Text="5"></asp:Label>
        &nbsp;|&nbsp;
        <asp:ImageButton ID="AddFav" runat="server" />
        <br />--%>
        <br />
        Instructions:
            <br />
        <asp:TextBox ID="Instructions" TextMode="MultiLine" Rows="10" Width="500px" runat="server"></asp:TextBox>
        <br />
        <br />
        Submit comments:<br />
        <asp:TextBox ID="txtComments" placeholder="Leave your comments here...." MaxLength="1000" TextMode="MultiLine" Rows="8" Width="400px" runat="server"></asp:TextBox><br />
        &nbsp;<asp:Button ID="btnSubmitComments" runat="server" Text="Submit" OnClick="SubmitComments_Click" />&nbsp;<asp:Label ID="lblSubmitMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <br />
        Comments:
        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" DataKeyField="CId" OnItemDataBound="MyDataList_ItemDataBound">
            <ItemTemplate>
                
                <hr />
                <div>

                    <a href="<%= path%><%# String.Format("Account/Profile.aspx?ID={0}", Eval("UID")) %>"><%# Eval("Username") %></a>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Time") %>' />
                    <asp:LinkButton ID="lbtnDelete" runat="server" Visible="false" Text="Delete" OnClientClick="return confirm('Do you really want to delete this comment?')" OnClick="DeleteComments_Click" CommandArgument='<%# Eval("CId") %>' />
                    <%--<asp:button id="btndelete" text="Delete" commandname="Delete" runat="server"></asp:button>--%>
                </div>
                <br />
                <div>
                    <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                </div>


                <br />
            </ItemTemplate>
        </asp:DataList>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>" SelectCommand="SELECT C.Id AS CId, C.AccountId AS UId, A.Username, C.Comments, C.CreateDatetime AS Time FROM Accounts AS A INNER JOIN Comments AS C ON A.Id = C.AccountId INNER JOIN Tutorials AS T ON C.TutorialId = T.Id WHERE (C.TutorialId = @TId) ORDER BY Time DESC">
            <SelectParameters>
                <asp:QueryStringParameter Name="TId" QueryStringField="Id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
</asp:Content>

