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

public partial class Account_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string msg = string.Empty;
            if (Application["Result"] != null)
            {
                Application.Lock();
                msg = Application["Result"].ToString();
                Application.UnLock();
                Application.Remove("Result");
                lblMsg.Text = msg;
            }

            btnChangePwd.Visible = false;
            btnEditProfile.Visible = false;
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["UIdinProfile"] = Convert.ToInt32(Request.QueryString["Id"]);

            if (Session["UId"] != null && Session["AType"] != null)
            {

                if ((int)Session["AType"] == 0)
                {
                    btnChangePwd.Visible = true;
                    btnEditProfile.Visible = true;
                }
                else if ((int)Session["AType"] == 1 && (int)Session["UId"] == Id)
                {
                    btnChangePwd.Visible = true;
                    btnEditProfile.Visible = true;
                }
            }


            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
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
                            if (!reader.IsDBNull(0))
                            {
                                byte[] bytes = (byte[])reader.GetValue(reader.GetOrdinal("Avatar"));
                                string strBase64 = Convert.ToBase64String(bytes);
                                Debug.WriteLine("strBase64: " + strBase64);
                                Avatar.ImageUrl = "data:Image/png;base64," + strBase64;
                            }

                            if (reader.IsDBNull(1) && reader.IsDBNull(2))
                            {
                                lblFullname.Text = "My name is a mystery.";
                            }
                            else
                            {
                                lblFullname.Text = reader.GetString(1) + " " + reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                txtBio.Text = reader.GetString(3);
                            }
                            if (!reader.IsDBNull(5))
                            {
                                lblEmailAdd.Text = reader.GetString(5);
                            }

                            if (!reader.IsDBNull(4))
                            {
                                lblLastUpdateTime.Text = "Last time logged in at: " + reader.GetDateTime(4).ToShortDateString();
                            }
                            
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
    }

    protected void btnEditProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/ProfileEdit.aspx?Id=" + ViewState["UIdinProfile"].ToString());

    }

    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account/ChangePassword.aspx?Id=" + ViewState["UIdinProfile"].ToString());
    }
}