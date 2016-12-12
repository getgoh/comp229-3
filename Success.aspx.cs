// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Success : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        Page.Theme = Application["CurrentTheme"].ToString();
    }

    // Al Roben Adriane Goh - 300910584

    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        string msg = "";
        switch (type)
        {
            case "add":
                msg = "Recipe successfully added!";
                break;
            case "delete":
                msg = "Recipe successfully deleted!";
                break;
            case "update":
                msg = "Recipe successfully updated!";
                break;
            default:
                break;
        }

        txtMessage.InnerText = msg;
    }
}

// Al Roben Adriane Goh - 300910584