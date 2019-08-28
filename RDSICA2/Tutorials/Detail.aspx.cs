using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

public partial class Tutorials_Detail : System.Web.UI.Page
{
    public string path = VirtualPathUtility.ToAbsolute("~");
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        

            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["DetailId"] = Convert.ToInt32(Request.QueryString["Id"]);
            Debug.WriteLine("Id: " + Id);
            string videoPath = string.Empty;
            int permission = -1;
            int ownerId = -1;
            string lastUpdateDate = string.Empty;

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("TutorialDetail"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PageTitle.Text = reader.GetString(0);
                            Instructions.Text = reader.GetString(1);
                            videoPath = reader.GetString(2);
                            AuthorName.Text = reader.GetString(3);
                            lblCategory.Text = reader.GetString(4);
                            permission = reader.GetInt32(5);
                            ownerId = reader.GetInt32(6);
                            ViewState["ownerId"] = ownerId;
                            lblLastUpdateTime.Text = reader.GetDateTime(7).ToShortDateString();
                            //Debug.WriteLine("");
                        }

                        if (permission == 1)
                        {
                            if (Session["UId"] != null && Session["AType"] != null)
                            {

                                if ((int)Session["AType"] == 1 && (int)Session["UId"] != ownerId)
                                {
                                    Application["errorMsg"] = "This is a private tutorial of other user. Unable to access.";
                                    Response.Redirect("~/ErrorPage.aspx");
                                }
                            }
                            else
                            {
                                Application["errorMsg"] = "This Tutorial is not open to pubic.";
                                Response.Redirect("~/ErrorPage.aspx");
                            }
                        }


                    }
                    else
                    {
                        Debug.WriteLine("No rows found.");
                        Application["errorMsg"] = "Unavailable Tutorial.";
                        Response.Redirect("~/ErrorPage.aspx");
                    }
                    reader.Close();

                    con.Close();
                }

            }


            string src = Request.Url.GetLeftPart(UriPartial.Authority).ToString() + videoPath;
            string playThis = "<video width=550 controls><Source src=" + src + " type=video/mp4>Your browser does not support HTML5 video.</video>";
            Video.Text = playThis;
        
    }

    protected void AuthorProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/Profile.aspx?Id=" + ViewState["ownerId"].ToString());
    }

    protected void SubmitComments_Click(object sender, EventArgs e)
    {
        if (Session["UId"] != null)
        {
            if (txtComments.Text.Trim() == "")
            {
                lblSubmitMsg.Text = "Please don't submit space(s) as comments.";
            }
            else
            {


                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertComments"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TId", (int)ViewState["DetailId"]);
                            cmd.Parameters.AddWithValue("@UId", (int)Session["UId"]);
                            cmd.Parameters.AddWithValue("@Comments", txtComments.Text);

                            cmd.Connection = con;
                            con.Open();
                            if (cmd.ExecuteScalar() == DBNull.Value)
                            {
                                lblSubmitMsg.Text = "Failed to submit comments. Please try again later.";
                            }
                            else
                            {
                                lblSubmitMsg.Text = "Your comments are successfully submitted.";
                            }

                            con.Close();
                        }
                    }
                }

            }

        }
        else
        {
            lblSubmitMsg.Text = "Please login to leave your comments.";
        }
    }

    protected void DeleteComments_Click(object sender, EventArgs e)
    {
        LinkButton myButton = sender as LinkButton;
        if (myButton != null)
        {
            int cId = Convert.ToInt32(myButton.CommandArgument);
            Debug.WriteLine("cID: " + cId);

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteComments"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CId", cId);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteScalar();
                        
                    }
                }
            }

        }
    }

    protected void MyDataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (Session["UId"] != null && (int)Session["AType"] == 0)
        {
            LinkButton lbtn = (LinkButton)e.Item.FindControl("lbtnDelete");
            lbtn.Visible = true;


        }
    }
}