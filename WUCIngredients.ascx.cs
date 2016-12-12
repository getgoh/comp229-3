using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class WUCIngredients : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    public TextBox _txtName { get { return this.txtName; } }
    public TextBox _txtQuantity { get { return this.txtQuantity; } }
    public TextBox _txtUnit { get { return this.txtUnit; } }
    

    

    public string Name {
        get
        {
            return this.txtName.Text;
        }
        set
        {
            this.txtName.Text = value;
        }
    }
    public int Quantity
    {
        get
        {
            if(this.txtQuantity.Text.Trim() != "") { return int.Parse(this.txtQuantity.Text); }
            else { return 0; }
        }
        set
        {
            this.txtQuantity.Text = value.ToString().Trim();
        }
    }
    public string Unit
    {
        get
        {
            return this.txtUnit.Text;
        }
        set
        {
            this.txtUnit.Text = value;
        }
    }
}