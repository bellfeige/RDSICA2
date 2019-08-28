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

public partial class Tutorials_SearchResults : System.Web.UI.Page
{

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    Master.SearchButton.Click += new EventHandler(SearchButton_Click);
    //}

    //protected void SearchButton_Click(object sender, EventArgs e)
    //{
    //    //throw new NotImplementedException();
    //    GetData(Master.SearchTerm);

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string searchTerm = string.Empty;
            if (Application["searchTerm"] != null)
            {
                Application.Lock();
                searchTerm = Application["searchTerm"].ToString();
                Application.UnLock();
                if (searchTerm == "")
                {
                    Application["errorMsg"] = "Please enter a valid keyword for searching!";
                    Response.Redirect("~/ErrorPage.aspx");
                }
                else
                {
                    GetData(searchTerm);
                }
            }

        }
    }

    private void GetData(string searchTerm)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SearchTutorials"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Connection = con;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataList1.DataSource = reader;
                    DataList1.DataBind();
                    lblResultCount.Text = "There are " + DataList1.Items.Count + " matched result(s).";
                }
                else
                {
                    lblResultCount.Text = "There is no matched result.";
                }
          



            }
        }
    }
}