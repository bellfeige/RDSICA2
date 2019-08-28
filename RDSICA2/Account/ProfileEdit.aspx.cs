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

public partial class Account_ProfileEdit : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["UIdinProfileEdit"] = Convert.ToInt32(Request.QueryString["Id"]);

            if (Session["UId"] != null && Session["AType"] != null)
            {

                if ((int)Session["AType"] == 1 && (int)Session["UId"] == Id || (int)Session["AType"] == 0)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("GetUserProfile"))
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
                                    if (!reader.IsDBNull(1)) { txtFirstName.Text = reader.GetString(1); }
                                    if (!reader.IsDBNull(2)) { txtLastName.Text = reader.GetString(2); }
                                    if (!reader.IsDBNull(3)) { txtBio.Text = reader.GetString(3); }
                                    if (!reader.IsDBNull(5)) { txtEmailAdd.Text = reader.GetString(5); }
                                    //Debug.WriteLine("");
                                }

                            }
                            else
                            {
                                Debug.WriteLine("No rows found.");
                                Application["errorMsg"] = "Unavailable User.";
                                Response.Redirect("~/ErrorPage.aspx");
                            }
                            reader.Close();

                            con.Close();
                        }

                    }
                }
                else
                {
                    Application["errorMsg"] = "Unauthorised access. You cannot change other user's profile.";
                    Response.Redirect("~/ErrorPage.aspx");
                }
            }
            else
            {
                Application["errorMsg"] = "Unauthorised access. Try again after login.";
                Response.Redirect("~/ErrorPage.aspx");
            }

        }
    }


    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        if (avatarUpload.HasFile)
        {
            HttpPostedFile postedFile = avatarUpload.PostedFile;
            string fileExt = Path.GetExtension(avatarUpload.FileName);
            Debug.WriteLine("fileExt: " + fileExt);

            if (fileExt.ToLower() != ".png" && fileExt.ToLower() != ".jpg")
            {
                uploadAvatarMsg.Text = "Sorry, only image with .png or .jpg extension supported!";
                uploadAvatarMsg.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                int fileSize = avatarUpload.PostedFile.ContentLength;
                if (fileSize >= 2097152)
                {
                    uploadAvatarMsg.Text = "Maximun video file size (50MB) exceeded. You video is " + fileSize / 1048576 + "MB.";
                    uploadAvatarMsg.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                    Debug.WriteLine("bytes: " + bytes.ToString());


                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("EditUserProfile"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Id", (int)ViewState["UIdinProfileEdit"]);
                                if (txtFirstName.Text.Trim() == string.Empty)
                                {
                                    cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                                }

                                if (txtLastName.Text.Trim() == string.Empty)
                                {
                                    cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                                }


                                cmd.Parameters.AddWithValue("@AvatarData", bytes);
                                cmd.Parameters.AddWithValue("@Bio", txtBio.Text);
                                if (txtEmailAdd.Text.Trim() == string.Empty)
                                {
                                    cmd.Parameters.AddWithValue("@EmailAdd", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@EmailAdd", txtEmailAdd.Text.Trim());
                                }



                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteScalar();
                                con.Close();
                                Response.Redirect("~/Account/Profile.aspx?Id=" + ViewState["UIdinProfileEdit"]);
                            }
                        }
                    }
                }
            }

        }
        else
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("EditUserProfile"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", (int)ViewState["UIdinProfileEdit"]);
                        if (txtFirstName.Text.Trim() == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                        }

                        if (txtLastName.Text.Trim() == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                        }
                        //cmd.Parameters.AddWithValue("@AvatarData", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Bio", txtBio.Text);
                        if (txtEmailAdd.Text.Trim() == string.Empty)
                        {
                            cmd.Parameters.AddWithValue("@EmailAdd", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EmailAdd", txtEmailAdd.Text.Trim());
                        }

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteScalar();
                        con.Close();
                        Response.Redirect("~/Account/Profile.aspx?Id=" + ViewState["UIdinProfileEdit"]);
                    }
                }
            }


        }



        // Response.Redirect("~/Account/Profile.aspx?Id=" + ViewState["UIdinProfileEdit"]);

    }
}