// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Setup : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = Application["CurrentTheme"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // Al Roben Adriane Goh - 300910584

    protected void btnLight_Click(object sender, EventArgs e)
    {
        Application["CurrentTheme"] = "Light";
        Response.Redirect("Setup.aspx");
    }

    protected void btnDark_Click(object sender, EventArgs e)
    {
        Application["CurrentTheme"] = "Dark";
        Response.Redirect("Setup.aspx");
    }
}

// Al Roben Adriane Goh - 300910584