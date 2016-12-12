// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecipeDetails : System.Web.UI.Page
{
    int currRecipeId;
    Recipe currRecipe;
    private List<Control> ingList;

    DataManager dm;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = Application["CurrentTheme"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["recipeID"] == null)
            return;

        currRecipeId = int.Parse(Request.QueryString["recipeID"]);

        dm = new DataManager();

        currRecipe = dm.getRecipeById(currRecipeId);

        if (currRecipe == null)
            return;

        if(Session["User"] != null)
        {
            if(((User)Session["User"]).Username != currRecipe.SubmittedBy)
            {
                setEditable(false);
            }
        }
        else
        {
            setEditable(false);
        }

        if(!IsPostBack)
        {
            bindItems();
        }

        showIngredientInputs();
        

        // Al Roben Adriane Goh - 300910584
        

        

        //Session["ingList"] = ingList;
    }

    private void setEditable(bool canEdit)
    {
        if(!canEdit)        
        {
            btnSave.Visible = false;
            btnAddCategory.Visible = false;
            btnAddIngredient.Visible = false;
            btnDelete.Visible = false;
        }
    }

    private void bindItems()
    {
        ddlCategories.DataSource = dm.getCategories();
        ddlCategories.DataBind();

        ddlCategories.SelectedValue = currRecipe.Category;

        txtName.Text = currRecipe.Name;
        txtSubmitted.Text = currRecipe.SubmittedBy;

        //txtCategory.Text = currRecipe.Category;
        txtCookingTime.Text = currRecipe.CookingTime.ToString();
        txtDescription.Text = currRecipe.Description;
        txtServings.Text = currRecipe.Servings.ToString();
        imgLink.HRef = currRecipe.ImgPath;
        img.ImageUrl = currRecipe.ImgPath;

        txtSubmitted.Attributes.Add("readonly", "readonly");

        //ingList = new List<Control>();
        Session["ingList"] = new List<Control>();

        foreach (Ingredient i in currRecipe.IngredientList)
        {
            Control tempControl = LoadControl("WUCIngredients.ascx");
            ((WUCIngredients)tempControl)._txtName.ID = "ingName" + i.Id;
            ((WUCIngredients)tempControl)._txtName.Text = i.Name;

            ((WUCIngredients)tempControl)._txtQuantity.ID = "ingQuantity" + i.Id;
            ((WUCIngredients)tempControl)._txtQuantity.Text = i.Quantity.ToString();

            ((WUCIngredients)tempControl)._txtUnit.ID = "ingUnit" + i.Id;
            ((WUCIngredients)tempControl)._txtUnit.Text = i.Unit;

            ((List<Control>)Session["ingList"]).Add(tempControl);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try {
            new DataManager().deleteRecipeById(currRecipeId);
            Response.Redirect("Success.aspx?type=delete");
        }
        catch
        {
            Response.Write("<script> alert('An error has occured. Please try again.'); </script>");
        }
    }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        if (txtAddCategory.Text.Trim() == "")
        {
            Response.Write("<script> alert('Please enter category'); </script>");
        }
        else
        {
            new DataManager().addCategory(txtAddCategory.Text);
            //bindItems();            
            ddlCategories.DataSource = dm.getCategories();
            ddlCategories.DataBind();

            ddlCategories.SelectedValue = currRecipe.Category;
        }
        showIngredientInputs();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string imgPath = "";

        if (imgUpload.PostedFile.ContentLength > 0)
        {
            string fileName = imgUpload.PostedFile.FileName;
            imgPath = "/images/" + fileName;
            imgUpload.PostedFile.SaveAs(Server.MapPath("/images/") + fileName);

            currRecipe.ImgPath = imgPath;
        }

        currRecipe.Name = txtName.Text;
        currRecipe.Category = ddlCategories.SelectedValue;
        currRecipe.CookingTime = int.Parse(txtCookingTime.Text);
        currRecipe.Servings = int.Parse(txtServings.Text);
        currRecipe.Description = txtDescription.Text;
        currRecipe.IngredientList = getIngredients();

        try
        {
            new DataManager().updateRecipe(currRecipe);
            Response.Redirect("Success.aspx?type=update");
        }
        catch
        {
            Response.Write("<script> alert('An error has occured. Please try again.'); </script>");
        }
    }

    private List<Ingredient> getIngredients()
    {
        List<Ingredient> IngredientList = new List<Ingredient>();                
        
        foreach (Control tempControl in ((List<Control>)Session["ingList"]))
        {
            if (!((WUCIngredients)tempControl)._txtName.Text.Trim().Equals("")
             && !((WUCIngredients)tempControl)._txtQuantity.Text.Trim().Equals("")
             && !((WUCIngredients)tempControl)._txtUnit.Text.Trim().Equals(""))
            {
                IngredientList.Add(new Ingredient() { Name = ((WUCIngredients)tempControl).Name, Quantity = ((WUCIngredients)tempControl).Quantity, Unit = ((WUCIngredients)tempControl).Unit });
            }
        }

        return IngredientList;
    }

    protected void btnAddIngredient_Click(object sender, EventArgs e)
    {
        int ctr = ((List<Control>)Session["ingList"]).Count + 1;

        Control tempControl = LoadControl("WUCIngredients.ascx");
        ((WUCIngredients)tempControl).ID = "WUCIngredient" + ctr;
        ((WUCIngredients)tempControl)._txtName.ID = "ingName" + ctr;
        ((WUCIngredients)tempControl)._txtQuantity.ID = "ingQuantity" + ctr;
        ((WUCIngredients)tempControl)._txtUnit.ID = "ingUnit" + ctr;

        ((List<Control>)Session["ingList"]).Add(tempControl);

        showIngredientInputs();
    }

    private void showIngredientInputs()
    {
        phIngredients.Controls.Clear();
        int x = 1;
        foreach (Control tempControl in ((List<Control>)Session["ingList"]))
        {
            ((WUCIngredients)tempControl).ID = "WUCIngredient" + x;
            ((WUCIngredients)tempControl)._txtName.ID = "ingName" + x;
            ((WUCIngredients)tempControl)._txtQuantity.ID = "ingQuantity" + x;
            ((WUCIngredients)tempControl)._txtUnit.ID = "ingUnit" + x;

            phIngredients.Controls.Add(tempControl);

            x++;
        }
    }
}

// Al Roben Adriane Goh - 300910584