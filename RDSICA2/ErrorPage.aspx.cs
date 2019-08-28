﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string errorMsg=string.Empty;
            if (Application["errorMsg"] != null)
            {
                Application.Lock();
                errorMsg = Application["errorMsg"].ToString();
                Application.UnLock();
                Application.Remove("errorMsg");
                lblErrorMsg.Text = errorMsg;
            }
        }
   
    }
}