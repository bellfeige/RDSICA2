using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string path = VirtualPathUtility.ToAbsolute("~");
    protected void Page_Load(object sender, EventArgs e)
    {

  
            

        if (!IsPostBack)
        {
            
            lblUserName.Visible = false;
            MyProfile.Visible = false;
            Logout.Visible = false;
            TutorialsMgr.Visible = false;
            rightContentTitle.Visible = false;
            CreateTutorial.Visible = false;
            AccountsMgr.Visible = false;

            if (Session["UId"] != null && Session["AType"] != null)
            {
                //UserId.Text = Session["UId"].ToString();
                lblUserName.Visible = true;
                lblUserName.Text = "Hello, " + Session["UName"].ToString();
                LinkButtonRegister.Visible = false;
                LinkButtonLogin.Visible = false;
                MyProfile.Visible = true;
                Logout.Visible = true;
                TutorialsMgr.Visible = true;
                rightContentTitle.Visible = true;
                CreateTutorial.Visible = true;

                //LastLoginDate.Text = Session["LLoginDate"].ToString();
                if ((int)Session["AType"] == 0)
                {
                    TutorialsMgr.Text = "Manage All Tutorials";
                    AccountsMgr.Visible = true;
                }
                else if ((int)Session["AType"] == 1)
                {
                    TutorialsMgr.Text = "Manage My Tutorials";
                }

            }
        }




    }


    protected void Logout_Click(object sender, EventArgs e)
    {
        if (Session["UName"] != null)
        {
            Session.RemoveAll();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }

    protected void About_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/About.aspx");
    }

    protected void Home_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
 
    }

    protected void Register_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Register.aspx");
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Login.aspx");
    }

    protected void MyProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Profile.aspx?Id="+ Session["UId"].ToString());
    }

    protected void TutorialsMgr_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Tutorials/ListManagement.aspx");
    }

    protected void CreateTutorial_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Tutorials/Insert.aspx");
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Application["searchTerm"] = TxtSearch.Text.Trim();
        Response.Redirect("~/Tutorials/SearchResults.aspx");
    }

    //public Panel SearchPanel
    //{
    //    get {
    //        return this.PanelSearch;
    //    }
    //}

    //public Button SearchButton
    //{
    //    get
    //    {
    //        return this.BtnSearch;
    //    }
    //}

    //public string SearchTerm
    //{
    //    get
    //    {
    //        return this.TxtSearch.Text;
    //    }
    //}


    protected void AccountsMgr_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Management.aspx");
    }



}
