// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment1
{
    public partial class Recipes : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = Application["CurrentTheme"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // Al Roben Adriane Goh - 300910584
            List<Recipe> RecipeList = new DataManager().getRecipeList();

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
    }
}

// Al Roben Adriane Goh - 300910584