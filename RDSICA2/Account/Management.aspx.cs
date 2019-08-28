using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Application["changeAccountState"] != null)
            {
                Application.Lock();
                lblMsg.Text = Application["changeAccountState"].ToString();
                Application.UnLock();
                Application.Remove("changeAccountState");

            }

            if (Session["UId"] != null && Session["AType"] != null)
            {
                if ((int)Session["AType"]!=0)
                {
                    Application["errorMsg"] = "Unauthorised access. Only administrator have access on this page.";
                    Response.Redirect("~/ErrorPage.aspx");
                }

               

            }
            else {
                Application["errorMsg"] = "Unauthorised access. Please try after login.";
                Response.Redirect("~/ErrorPage.aspx");
            }

        }
    }

    protected void btnAddAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Add.aspx");
    }
}