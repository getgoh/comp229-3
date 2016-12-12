using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        Page.Theme = Application["CurrentTheme"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["User"] != null)
        {
            Response.Redirect("Home.aspx");
        }
    }

    private void login()
    {
        string username = txtUser.Text.Trim();
        string password = txtPassword.Text;

        User currUser = new DataManager().login(username, password);

        if (currUser != null)
        {
            Session["User"] = currUser;
            Response.Redirect(Session["RedirectPage"].ToString());
        }
        else
        {
            Response.Write("<script type='text/javascript'> alert('Invalid username/password.'); </script>");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        login();
    }
}