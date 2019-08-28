using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class Account_Add : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            btnSubmit.Enabled = false;
            if (Session["UId"] == null || (int)Session["AType"] != 0)
            {

                Application["errorMsg"] = "Unauthorised access. ";
                Response.Redirect("~/ErrorPage.aspx");

            }
            else
            {
                btnSubmit.Enabled = true;

            }
        }
    }

    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = SHA256.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    protected void AddAccount(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("AddAccount"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@PasswordHash", GetHashString(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@AccountType", selectAccountType.SelectedValue);

                    // cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Connection = con;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            string message = string.Empty;
            switch (userId)
            {
                case -1:
                    message = "Username already exists. Please choose a different username.";
                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                    lblAddAccountResult.Text = message;
                    break;
                //case -2:
                //    message = "Supplied email address has already been used.";
                //    break;
                default:
                    message = "Account created successfully.";
                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                    Application["Result"] = message;
                    Response.Redirect("~/Account/Profile.aspx?Id=" + userId.ToString());
                    // SendActivationEmail(userId);
                    break;
            }

        }
    }
}