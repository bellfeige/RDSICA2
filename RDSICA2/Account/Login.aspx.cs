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
using System.Security.Cryptography;
using System.Text;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsCookieDisabled())
        {
            lblCookie.Visible = true;
            hylCookie.Visible = true;

            Debug.WriteLine("cookie is: " + IsCookieDisabled());
        }

        if (!IsPostBack)
        {
         
            //string msg = string.Empty;
            if (Application["registerSucc"] != null)
            {
                Application.Lock();
                lblRegSucc.Text = Application["registerSucc"].ToString();
                Application.UnLock();
                Application.Remove("registerSucc");
     
            }

        }

    }


    protected void ValidateUser(object sender, EventArgs e)
    {

        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Validate_User"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Login1.UserName);
                cmd.Parameters.AddWithValue("@PasswordHash", GetHashString(Login1.Password));


                cmd.Connection = con;
                con.Open();
                // userId = Convert.ToInt32(cmd.ExecuteScalar());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("reader(0): "+reader.GetInt32(0));
                        if (reader.GetInt32(0) == -3)
                        {
                            Login1.FailureText = "This account is disabled.";
                        }
                        else if (reader.GetInt32(0) == -2)
                        {
                            Login1.FailureText = "Account not exist.";
                        }
                        else if (reader.GetInt32(0) == -1) {
                            Login1.FailureText = "Incorrect password.";
                        }
                        else
                        {
                      
                            Session.Add("UId", reader.GetInt32(0));
                            Session.Add("UName", reader.GetString(1));
                            Session.Add("AType", reader.GetInt32(2));
                            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                            Response.Redirect("~/default.aspx");


                        }


                       
                    }
                    

                }
                else
                {
                    Debug.WriteLine("No rows found.");
                    Login1.FailureText = "Failed to login.";

                }
                reader.Close();

                con.Close();
            }
            //switch (userId)
            //{
            //    case -1:
            //        Login1.FailureText = "Incorrect PWD.";
            //        break;
            //    case -2:
            //        Login1.FailureText = "User invalid.";
            //        break;
            //    default:
            //        //Session["UName"] = Login1.UserName;
            //        //Debug.WriteLine("username: " + username);
            //        Session.Add("UId", userId);
            //        Session.Add("UName", userName);
            //        //Session.Add("LLoginDate", lastLoginDate);
            //        Session.Add("AType", accountType);
            //        System.Web.Security.FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
            //        break;
            //}
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

    protected bool IsCookieDisabled()
    {
        string currentUrl = Request.RawUrl;

        if (Request.QueryString["cookieCheck"] == null)
        {
            try
            {
                HttpCookie c = new HttpCookie("SupportCookies", "true");
                Response.Cookies.Add(c);

                if (currentUrl.IndexOf("?") > 0)
                    currentUrl = currentUrl + "&cookieCheck=true";
                else
                    currentUrl = currentUrl + "?cookieCheck=true";

                Response.Redirect(currentUrl);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        if (!Request.Browser.Cookies || Request.Cookies["SupportCookies"] == null)
            return true;

        return false;
    }
}