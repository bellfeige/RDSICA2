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

public partial class Account_ChangeState : System.Web.UI.Page
{
    int uId = 0;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            uId = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["changeAccountId"] = Convert.ToInt32(Request.QueryString["Id"]);
            Debug.WriteLine("uId: " + uId);

            if (Session["UId"] != null && (int)Session["AType"] == 0)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserProfile"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", uId);
                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(6)) { lblUsername.Text = reader.GetString(6); }
                                if (!reader.IsDBNull(1) || !reader.IsDBNull(2)) { lblFullname.Text = reader.GetString(1) + " " + reader.GetString(2); }
                                if (!reader.IsDBNull(5)) { lblEmail.Text = reader.GetString(5); }
                                if (!reader.IsDBNull(7)) { lblCreateDate.Text = reader.GetDateTime(7).ToShortDateString(); }
                                if (!reader.IsDBNull(4)) { lblLastTimeLogin.Text = reader.GetDateTime(4).ToShortDateString(); }
                                if (!reader.IsDBNull(8))
                                {
                                    if (reader.GetInt32(8) == 0)
                                    {
                                        lblState.Text = "Enabled";
                                        btnChangeState.Text = "Disable";
                                        PageTitle.Text = "Are you sure to disable this account?";
                                    }
                                    else if (reader.GetInt32(8) == 1)
                                    {
                                        lblState.Text = "Disabled";
                                        btnChangeState.Text = "Enable";
                                        PageTitle.Text = "Are you sure to enable this account?";
                                    }
                                }


                            }

                        }
                        else
                        {
                            Debug.WriteLine("No rows found.");
                            Application["errorMsg"] = "Unavailable Account.";
                            Response.Redirect("~/ErrorPage.aspx");
                        }
                        reader.Close();
                        con.Close();

                    }
                }




            }
            else
            {
                Application["errorMsg"] = "Unauthorised access. Only administrators have access on this page.";
                Response.Redirect("~/ErrorPage.aspx");
            }

        }


    }

    protected void btnChangeState_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("ChangeAccountState"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ViewState["changeAccountId"]);
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == -1)
                        {
                            lblChangeMsg.Text = "Administrator is system build-in account that cannot be disabled!";
                        }
                        else
                        {

                            Application["changeAccountState"] = "Account state changed successfully.";
                            Response.Redirect("~/Account/Management.aspx");

                        }



                    }

                }
                else
                {
                    Debug.WriteLine("No rows found.");
                    Application["errorMsg"] = "Unavailable Account.";
                    Response.Redirect("~/ErrorPage.aspx");
                }
                reader.Close();
                con.Close();

            }
        }
    }
}
