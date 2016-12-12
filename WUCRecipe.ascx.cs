using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WUCRecipe : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public HtmlAnchor _linkRecipeName
    {
        get { return this.linkRecipeName; }
    }

    public string Name
    {
        set { this.txtName.Text = "Name: " + value; }
    }

    public string SubmittedBy
    {
        set { this.txtSubmitted.Text = "Submitted by: " + value; }
    }

    public int PrepareTime
    {
        set { this.txtPrepareTime.Text = "Prepare time: " + value.ToString(); }
    }

    public Image _img { get { return this.img; } }

}