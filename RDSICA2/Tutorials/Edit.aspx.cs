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

public partial class Tutorials_Edit : System.Web.UI.Page
{
    int tId = 0;
    string title = string.Empty;
    string instructions = string.Empty;
    string videoPath = string.Empty;
    string thumbnailPath = string.Empty;

    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Debug.WriteLine("IsPostBack: " + IsPostBack);
        if (!IsPostBack)
        {

            tId = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["tId"] = tId;
            Debug.WriteLine("tId: " + tId);
            SubmitTutorial.Enabled = false;
            if (Session["UId"] != null && Session["AType"] != null)
            {

                int myId = (int)Session["UId"];
                int uId = -1;

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTutorial"))
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
                                    SubmitTutorial.Enabled = true;
                                    inputTitle.Text = reader.GetString(1);
                                    selectCategory.DataSource = Categories;
                                    selectCategory.DataBind();
                                    selectCategory.ClearSelection();
                                    selectCategory.Items.FindByValue(reader.GetInt32(2).ToString()).Selected = true;
                                    videoPath = reader.GetString(3);
                                    ViewState["oldVideoPath"] = videoPath;
                                    Instructions.Text = reader.GetString(4);
                                    selectPermission.ClearSelection();
                                    selectPermission.Items.FindByValue(reader.GetInt32(5).ToString()).Selected = true;
                                    thumbnailPath = reader.GetString(6);
                                    ViewState["oldThumbnailPath"] = thumbnailPath;
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

    protected void SubmitTutorial_Click(object sender, EventArgs e)
    {

        if (uploadVideo.HasFile)
        {
            string fileExt = Path.GetExtension(uploadVideo.FileName);
            if (fileExt.ToLower() != ".mp4")
            {
                uploadVideoMsg.Text = "Sorry, only video with .mp4 extension supported!";
                uploadVideoMsg.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                int fileSize = uploadVideo.PostedFile.ContentLength;
                if (fileSize > 52428800)
                {
                    uploadVideoMsg.Text = "Maximun video file size (50MB) exceeded. You video is " + fileSize / 1048576 + "MB.";
                    uploadVideoMsg.ForeColor = System.Drawing.Color.Red;

                }
                else
                {

                    var folder = DateTime.Now.ToString("dd-MM-yyyy");
                    string fileName = DateTime.Now.ToString("hhmmss") + "_" + Session["UId"].ToString();
                    //Debug.WriteLine("fileName:" + fileName);
                    if (!Directory.Exists(Server.MapPath("~/UploadedFiles/" + folder)))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/" + folder));
                    }

                    uploadVideo.SaveAs(Server.MapPath("~/UploadedFiles/" + folder + "/" + fileName + fileExt));
                    //uploadVideoMsg.Text = "File uploaded";
                    videoPath = "/UploadedFiles/" + folder + "/" + fileName + fileExt;
                    thumbnailPath = "~/UploadedFiles/" + folder + "/" + fileName + ".jpg";
                    if (File.Exists(Server.MapPath("~/UploadedFiles/" + folder + "/" + fileName + fileExt)))
                    {
                        if (File.Exists(Server.MapPath("~" + ViewState["oldVideoPath"].ToString())))
                        {
                            Debug.WriteLine("Has video: " + ViewState["oldVideoPath"].ToString());
                            File.Delete(Server.MapPath("~" + ViewState["oldVideoPath"].ToString()));
                        }

                        if (File.Exists(Server.MapPath(ViewState["oldThumbnailPath"].ToString())))
                        {
                            Debug.WriteLine("Has thumbnail: " + ViewState["oldThumbnailPath"].ToString());
                            File.Delete(Server.MapPath(ViewState["oldThumbnailPath"].ToString()));
                        }

                        var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                        ffMpeg.GetVideoThumbnail(Server.MapPath("~" + videoPath), Server.MapPath(thumbnailPath));


                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand("EditTutorial"))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Id", (int)ViewState["tId"]);
                                    cmd.Parameters.AddWithValue("@Title", inputTitle.Text.Trim());
                                    cmd.Parameters.AddWithValue("@CategoryId", selectCategory.SelectedValue);
                                    cmd.Parameters.AddWithValue("@VideoPath", videoPath);
                                    cmd.Parameters.AddWithValue("@ThumbnailPath", thumbnailPath);
                                    cmd.Parameters.AddWithValue("@Instructions", Instructions.Text);
                                    cmd.Parameters.AddWithValue("@Permission", selectPermission.SelectedValue);

                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteScalar();
                                    con.Close();
                                }
                            }
                        }
                        Response.Redirect("Detail.aspx?Id=" + (int)ViewState["tId"]);

                    }
                    else
                    {
                        Application["errorMsg"] = "Video upload failed. Please try again.";
                        Response.Redirect("~/ErrorPage.aspx");
                    }




                }
            }

        }
        else
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("EditTutorial"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        Debug.WriteLine("tId: " + (int)ViewState["tId"]);
                        Debug.WriteLine("Title: " + inputTitle.Text.Trim());
                        Debug.WriteLine("CategoryId: " + selectCategory.SelectedValue);
                        Debug.WriteLine("VideoPath: " + videoPath);
                        Debug.WriteLine("ThumbnailPath: " + thumbnailPath);
                        Debug.WriteLine("Instructions: " + Instructions.Text);
                        Debug.WriteLine("Permission: " + selectPermission.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id", (int)ViewState["tId"]);
                        cmd.Parameters.AddWithValue("@Title", inputTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@CategoryId", selectCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@VideoPath", videoPath);
                        cmd.Parameters.AddWithValue("@ThumbnailPath", thumbnailPath);
                        cmd.Parameters.AddWithValue("@Instructions", Instructions.Text);
                        cmd.Parameters.AddWithValue("@Permission", selectPermission.SelectedValue);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteScalar();
                        con.Close();
                    }
                }
            }
            Response.Redirect("Detail.aspx?Id=" + (int)ViewState["tId"]);
        }
    }
}