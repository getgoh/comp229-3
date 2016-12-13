// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment1
{
    public partial class Master : System.Web.UI.MasterPage
    {

        // Al Roben Adriane Goh - 300910584

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                btnLogin.Text = "Logout";
                btnLogin.PostBackUrl = "..\\Logout.aspx";
            }
            else
            {
                btnLogin.Text = "Login";
                btnLogin.PostBackUrl = "..\\Login.aspx";
            }

        }
        
    }
}

// Al Roben Adriane Goh - 300910584