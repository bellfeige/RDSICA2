<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" CodeFile="Insert.aspx.cs" Inherits="Tutorials_Insert" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headNotice" runat="Server">
    <style type="text/css">
        #editInstructions {
            height: 279px;
            width: 591px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="Server">
    <link href="~/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <div class="PageTitle">
    <asp:Label ID="PageTitle" runat="server" Text="Create a new tutorial"></asp:Label></div>

    <div>

        <br />
        <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
        &nbsp;<asp:TextBox MaxLength="50" ID="inputTitle" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="titleInput"
            runat="server"
            ControlToValidate="inputTitle"
            ErrorMessage="Title is required!"
            ForeColor="Red"
            SetFocusOnError="True" 
             ValidationGroup='valGroup1'/>
        <br />
        <br />
        <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
        &nbsp;<asp:DropDownList ID="selectCategory" runat="server" DataSourceID="Categories" DataTextField="Name" DataValueField="Id">
        </asp:DropDownList>
        <asp:SqlDataSource ID="Categories" runat="server" ConnectionString="<%$ ConnectionStrings:constr %>" SelectCommand="SELECT [Id], [Name] FROM [Categories]"></asp:SqlDataSource>
        <br />
        <br />
        <asp:Label ID="lblUploadVideo" runat="server" Text="Upload tutorial video"></asp:Label>
        <br />
        <asp:FileUpload ID="uploadVideo" runat="server" />
        <br />
        <asp:Label ID="uploadVideoMsg" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblInstructions" runat="server" Text="Edit instructions"></asp:Label>
        <br />
        <asp:TextBox ID="Instructions" MaxLength="5000" TextMode="MultiLine" Rows="10" Width="600px" runat="server"></asp:TextBox>
        <%--<textarea id="editInstructions" name="editInstructions" cols="80" rows="8"></textarea>--%><br />
        <br />
        <asp:Label ID="lblPermission" runat="server" Text="Permission"></asp:Label>
        &nbsp;<asp:DropDownList ID="selectPermission" runat="server">
            <asp:ListItem Selected="True" Value="0" Text="Public"></asp:ListItem>
            <asp:ListItem Value="1" Text="Private"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="SubmitTutorial" ValidationGroup='valGroup1' runat="server" Text="Submit" OnClick="SubmitTutorial_Click" />
        
        <br />
    </div>
  


</asp:Content>

