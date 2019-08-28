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

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            ViewState["UIdinChangePwd"] = Convert.ToInt32(Request.QueryString["Id"]);
            btnSubmit.Enabled = false;

            if (Session["UId"] != null && Session["AType"] != null)
            {

                if ((int)Session["AType"] == 1 && (int)Session["UId"] == Id || (int)Session["AType"] == 0)
                {
                    btnSubmit.Enabled = true;
                }
                else
                {
                    Application["errorMsg"] = "Unauthorised access. You cannot change other user's password.";
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int changeResult = 0;
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("ChangePassword"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", (int)ViewState["UIdinChangePwd"]);
                    cmd.Parameters.AddWithValue("@OldPwdHash", GetHashString(txtOldPwd.Text));
                    cmd.Parameters.AddWithValue("@NewPwdHash", GetHashString(txtNewPwd.Text));
                    
                    cmd.Connection = con;
                    con.Open();
                    changeResult=(int)cmd.ExecuteScalar();
                    con.Close();
                    
                }
            }
        }
        string message=string.Empty;
        switch (changeResult) {
            case -1:
                message= "Your old password is incorrect.";
                lblChangeResult.Text = message;
                break;
            case 1:
                message = "Your password has been successfully changed.";
                Application["Result"] = message;
                Response.Redirect("~/Account/Profile.aspx?Id=" + ViewState["UIdinChangePwd"]);
                break;

        }


    }
}

