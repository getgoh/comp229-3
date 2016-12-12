// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment1
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = Application["CurrentTheme"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populateLists();
                performSearch();
            }
        }

        private void populateLists()
        {
            DataManager dm = new DataManager();
            List<Recipe> RecipeList = dm.getRecipeList();

            IEnumerable<string> authorList = RecipeList.Select(x => x.SubmittedBy).Distinct().OrderBy(a => a);

            IEnumerable<string> categoryList = RecipeList.Select(x => x.Category).Distinct().OrderBy(a => a);

            ddlSubmittedBy.DataSource = authorList;
            ddlSubmittedBy.DataBind();
            ddlSubmittedBy.Items.Insert(0, "All");

            ddlCategory.DataSource = categoryList;
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "All");

            ddlIngredientName.DataSource = dm.getIngrendientsNameList().OrderBy(a => a);
            ddlIngredientName.DataBind();
            ddlIngredientName.Items.Insert(0, "All");
        }

        // Al Roben Adriane Goh - 300910584

        private void performSearch()
        {
            string ingredient = ddlIngredientName.SelectedValue;
            string category = ddlCategory.SelectedValue;
            string submittedBy = ddlSubmittedBy.SelectedValue;

            List<Recipe> RecipeList = new DataManager().getRecipeBySearchParams(submittedBy, category, ingredient);

            foreach (Recipe recipe in RecipeList)
            {
                Control tempControl = LoadControl("WUCRecipe.ascx");
                ((WUCRecipe)tempControl).Name = recipe.Name;
                ((WUCRecipe)tempControl).SubmittedBy = recipe.SubmittedBy;
                ((WUCRecipe)tempControl).PrepareTime = recipe.CookingTime;
                ((WUCRecipe)tempControl)._linkRecipeName.HRef = "../RecipeDetails.aspx?recipeID=" + recipe.Id;
                ((WUCRecipe)tempControl)._img.ImageUrl = recipe.ImgPath;
                phRecipes.Controls.Add(tempControl);
            }
        }

        protected void ddlIngredientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            performSearch();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            performSearch();
        }

        protected void ddlSubmittedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            performSearch();
        }
    }
}

// Al Roben Adriane Goh - 300910584