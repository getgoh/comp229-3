// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment1
{
    public partial class Home : System.Web.UI.Page
    {

        // Al Roben Adriane Goh - 300910584

        protected override void OnPreInit(EventArgs e)
        {
            Page.Theme = Application["CurrentTheme"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}

// Al Roben Adriane Goh - 300910584