using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {        
        base.OnPreInit(e);
       // CheckLogin("Home.aspx");

        Page.Theme = Application["CurrentTheme"].ToString();
    }

    public void CheckLogin(string redirect)
    {
        if (Session["User"] == null)
        {
            Session["RedirectPage"] = redirect;
            Response.Redirect("Login.aspx");
        }
    }
}