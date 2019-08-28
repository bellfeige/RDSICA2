<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headNotice" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="PageTitle">
        <asp:Label ID="PageTitle" runat="server" Text="About"></asp:Label>
    </div>
    <br />
    <div>
    This website is designed to be a platform that everyone could access
cooking tutorials in a range of topics from the right panel. A tutorial is mainly consisted of title, video, category and
instructions. Besides that, visitors and authors can interact with each other by leaving
comments on tutorials.
Every registered user is allowed to create tutorials, and the tutorials can be managed by
themselves or administrators. The website also provides portals for administrators to manage
all users and tutorials.
</div>
     <br />
     <br />
         <div >
        <asp:Label ID="lblCookie" style="font-size:18px;font-weight:bold;" runat="server" Text="Browser Cookies"></asp:Label>
    </div>
    <br />
    <div>
        Please allow sites to save and read cookie data in browser so that login function can work correctly. <a href="https://www.whatismybrowser.com/guides/how-to-enable-cookies">Click here</a> to learn about how to enable cookies for your browser.


    </div>
    <br />
     <br />
     <div >
        <asp:Label ID="lbl3Party" style="font-size:18px;font-weight:bold;" runat="server" Text="Third-party component"></asp:Label>
    </div>
     <br />
    <div>
        The feature that thumbnail images generated from the first frame of uploaded video rely on an open source third-party component FFmpeg to function properly. This application will follow the LICENSE protocol on <a href="https://github.com/FFmpeg/FFmpeg">github.com/FFmpeg/FFmpeg</a>.

        

    </div>

</asp:Content>

