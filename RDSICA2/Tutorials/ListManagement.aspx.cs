using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tutorials_ListManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["deleteTutorialMsg"] != null)
        {
            Application.Lock();
            string deleteMsg = Application["deleteTutorialMsg"].ToString();
            Application.UnLock();
            Application.Remove("errorMsg");
            lblDeleteTutorialMsg.Text = deleteMsg;
        }

        string ownerId = "";

        if (Session["UId"] != null && Session["AType"] != null)
        {

            if ((int)Session["AType"] == 1)
            {
                
                ownerId = Session["UId"].ToString();
                if (!IsPostBack)
                {
                    string command = "SELECT T.Id, T.Title,C.Name AS Category, A.Username AS Author,  CASE WHEN T.Permission = 0 THEN 'Public' WHEN T.Permission = 1 THEN 'Private' END AS Permission, T.CreateDate FROM Tutorials AS T INNER JOIN Accounts AS A ON A.Id = T.OwnerId INNER JOIN Categories AS C ON C.Id = T.CategoryId where T.ownerId=" + ownerId;
                    SqlDataSource1.SelectCommand = command;

                }
            }

        }
        else
        {
            Application["errorMsg"] = "Unauthorised access. Try again after login.";
            Response.Redirect("~/ErrorPage.aspx");
        }
    
    }
}