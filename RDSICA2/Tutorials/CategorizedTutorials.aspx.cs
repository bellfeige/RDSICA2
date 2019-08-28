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

public partial class Tutorials_CategorizedTutorials : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            Debug.WriteLine("Id: " + Id);
            string command = "SELECT T.[Id], T.[Title], T.[ThumbnailPath], C.[Name] as CName FROM [Tutorials] as T inner join [Categories] as C on C.[Id] = T.[CategoryId] WHERE[Permission] = 0 and [CategoryId]=" + Id.ToString();
            SqlDataSource1.SelectCommand = command;
            Debug.WriteLine("command: " + command);

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string ccmd = "select [name] from [Categories] where Id=" + Id.ToString();
                using (SqlCommand cmd = new SqlCommand(ccmd))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
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
                                PageTitle.Text = "Tutorials for " + reader.GetString(0);

                            }
                      

                        }
                    }
                    //else
                    //{
                    //    lblMsg.Text = "There is no tutorials under this gategory.";
                    //}

                }
            }
        }
    }

    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {

 
        if (e.AffectedRows < 1)


        {


            lblMsg.Text = "There is no tutorials under this gategory.";

        }

    }
}