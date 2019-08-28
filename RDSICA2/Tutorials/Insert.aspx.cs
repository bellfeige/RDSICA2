using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

public partial class Tutorials_Insert : System.Web.UI.Page
{


    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UId"] == null)
        {

            Application["errorMsg"] = "Unauthorised access. Try again after login.";
            Response.Redirect("~/ErrorPage.aspx");

        }

    }



    protected void SubmitTutorial_Click(object sender, EventArgs e)
    {
        string videoPath = string.Empty;
        string thumbnailPath = string.Empty;
        int Id = -1;

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
                if (fileSize >= 52428800)
                {
                    uploadVideoMsg.Text = "Maximun video file size (50MB) exceeded. You video is " + fileSize / 1048576 + "MB.";
                    uploadVideoMsg.ForeColor = System.Drawing.Color.Red;

                }
                else
                {

                    var folder = DateTime.Now.ToString("dd-MM-yyyy");
                    string fileName = DateTime.Now.ToString("hhmmss")+"_"+ Session["UId"].ToString();
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

                        var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                        ffMpeg.GetVideoThumbnail(Server.MapPath("~" + videoPath), Server.MapPath(thumbnailPath));

                        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand("Insert_Tutorial"))
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Title", inputTitle.Text.Trim());
                                    cmd.Parameters.AddWithValue("@CategoryId", selectCategory.SelectedValue);
                                    cmd.Parameters.AddWithValue("@VideoPath", videoPath);
                                    cmd.Parameters.AddWithValue("@ThumbnailPath", thumbnailPath);
                                    cmd.Parameters.AddWithValue("@OwnerId", (int)Session["UId"]);
                                    cmd.Parameters.AddWithValue("@Instructions", Instructions.Text);
                                    cmd.Parameters.AddWithValue("@Permission", selectPermission.SelectedValue);

                                    cmd.Connection = con;
                                    con.Open();
                                    Id = Convert.ToInt32(cmd.ExecuteScalar());
                                    Debug.WriteLine("Id: " + Id);
                                    con.Close();
                                }
                            }
                        }
                        Response.Redirect("Detail.aspx?Id=" + Id);

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
            uploadVideoMsg.Text = "Please select a video to upload!";
            uploadVideoMsg.ForeColor = System.Drawing.Color.Red;

        }




        //Debug.WriteLine("Title: " + inputTitle.Text);
        //Debug.WriteLine("CategoryId: " + selectCategory.SelectedValue);
        //Debug.WriteLine("videoPath: " + videoPath);
        //Debug.WriteLine("Instructions: " + Request.Form["editInstructions"]);
        //Debug.WriteLine("Permission: " + selectPermission.SelectedValue);



    }


}
