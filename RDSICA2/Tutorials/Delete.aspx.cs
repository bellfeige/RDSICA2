using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

public partial class Tutorials_Delete : System.Web.UI.Page
{
    
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int tId = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["deleteTId"] = Convert.ToInt32(Request.QueryString["Id"]);
            Debug.WriteLine("tId: " + tId);

            DeleteTut.Enabled = false;
            if (Session["UId"] != null && Session["AType"] != null)
            {



                int myId = (int)Session["UId"];
                int uId = -1;

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("TutorialOwnerId"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", tId);
                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                uId = reader.GetInt32(0);
                                //Debug.WriteLine("");

                                if (myId == uId || (int)Session["AType"] == 0)
                                {
                                    DeleteTut.Enabled = true;
                                    lblTitle.Text = reader.GetString(1); ;
                                    ImgThum.ImageUrl = reader.GetString(2);
                                }
                                else
                                {


                                    Application["errorMsg"] = "Unauthorised access. You are not the author of this tutorial.";
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

            }
            else
            {
                Application["errorMsg"] = "Unauthorised access. Try again after login.";
                Response.Redirect("~/ErrorPage.aspx");
            }
        }
    }

    protected void DeleteTut_Click(object sender, EventArgs e)
    {
        string vPath = string.Empty;
        string tPath = string.Empty;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteTutorial"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", (int)ViewState["deleteTId"]);
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        vPath = reader.GetString(0);
                        tPath = reader.GetString(1);
                        //Debug.WriteLine("");
                    }

                }
                else
                {
                    Debug.WriteLine("No rows found.");
                    Application["errorMsg"] = "Failed to delete. Please try again";
                    Response.Redirect("~/ErrorPage.aspx");
                }
                reader.Close();
                con.Close();
            }
            if (File.Exists(Server.MapPath("~" + vPath)))
            {
                Debug.WriteLine("Has video: " + vPath);
                File.Delete(Server.MapPath("~" + vPath));
            }

            if (File.Exists(Server.MapPath(tPath)))
            {
                Debug.WriteLine("Has thumbnail: " + vPath);
                File.Delete(Server.MapPath(tPath));
            }

        }

        Application["deleteTutorialMsg"] = "Tutorial is deleted successfully.";
        Response.Redirect("~/Tutorials/ListManagement.aspx");

    }
}
