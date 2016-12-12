// Al Roben Adriane Goh - 300910584

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment1
{
    public partial class AddRecipe : System.Web.UI.Page
    {
        private List<Control> ingList;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = Application["CurrentTheme"].ToString();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //ingList = new List<Control>();
            //initiateIngredientInputs(3);
            //showIngredientInputs();
        }

        DataManager dm;

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckLogin("AddRecipe.aspx");

            if (Session["User"] == null)
            {
                Session["RedirectPage"] = "AddRecipe.aspx";
                Response.Redirect("Login.aspx");
            }

            dm = new DataManager();

            txtSubmitted.Text = ((User)Session["User"]).Username;
            txtSubmitted.Attributes["readonly"] = "readonly";

            if (!Page.IsPostBack)
            {
                bindItems();
                ingList = new List<Control>();
                initiateIngredientInputs(3);
                //showIngredientInputs();
            }

            showIngredientInputs();

            //Response.Write("<script type='text/javascript'> alert('page load!'); </script>");

            //setTestValues();
        }

        private void bindItems()
        {
            ddlCategories.DataSource = dm.getCategories();
            ddlCategories.DataBind();
        }

        private void setTestValues()
        {
            txtName.Text = "Adobo Test";
            txtDescription.Text = "Sample desc test";
            txtCookingTime.Text = "24";
            //txtCategory.Text = "Filipino foods";
            txtServings.Text = "8";
            txtSubmitted.Text = "Al Test";
        }

        private void initiateIngredientInputs(int num)
        {
            for (int x = 1; x <= num; x++)
            {
                Control tempControl = LoadControl("WUCIngredients.ascx");
                ((WUCIngredients)tempControl).ID = "WUCIngredient" + x;
                ((WUCIngredients)tempControl)._txtName.ID = "ingName" + x;
                ((WUCIngredients)tempControl)._txtQuantity.ID = "ingQuantity" + x;
                ((WUCIngredients)tempControl)._txtUnit.ID = "ingUnit" + x;
                ingList.Add(tempControl);
            }

            Session["ingList"] = ingList;
        }

        // Al Roben Adriane Goh - 300910584

        private List<string> UWCIngIdCollection
        {
            get
            {
                var collection = ViewState["UWCIngIdCollection"] as List<string>;
                return collection ?? new List<string>();
            }
            set { ViewState["UWCIngIdCollection"] = value; }
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

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtAddCategory.Text.Trim() == "")
            {
                Response.Write("<script> alert('Please enter category'); </script>");
            }
            else
            {
                DataManager dm = new DataManager();
                dm.addCategory(txtAddCategory.Text);
                ddlCategories.DataSource = dm.getCategories();
                ddlCategories.DataBind();
            }
            showIngredientInputs();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Recipe newRecipe = new Recipe()
                {
                    Name = txtName.Text,
                    SubmittedBy = txtSubmitted.Text,
                    Category = ddlCategories.SelectedItem.Text,
                    //Category = txtCategory.Text,
                    Servings = int.Parse(txtServings.Text),
                    CookingTime = int.Parse(txtCookingTime.Text),
                    Description = txtDescription.Text,
                    IngredientList = getIngredients()
                };

                string imgPath = "";
                if (imgUpload.PostedFile.ContentLength > 0)
                {
                    string fileName = imgUpload.PostedFile.FileName;
                    imgPath = "/images/" + fileName;
                    imgUpload.PostedFile.SaveAs(Server.MapPath("/images/") + fileName);//Or code to save in the DataBase.
                }
                //if (fuImage.HasFile)
                //{
                //    try
                //    {
                //        string filename = Path.GetFileName(fuImage.FileName);
                //        imgPath = Server.MapPath("~/images/") + filename;
                //        fuImage.SaveAs(imgPath);
                //    }
                //    catch
                //    {

                //    }
                //}
                // add to db..
                newRecipe.ImgPath = imgPath;
                new DataManager().insertNewRecipe(newRecipe);

                //((List<Recipe>)Application["RecipeList"]).Add(newRecipe);

                Response.Redirect("Success.aspx?type=add");
            }
        }


        private List<Ingredient> getIngredients()
        {
            List<Ingredient> IngredientList = new List<Ingredient>();

            ControlCollection cList = phIngredients.Controls;

            List<Control> iList = (List<Control>)Session["ingList"];

            //foreach(Control tempControl in iList)
            foreach (Control tempControl in phIngredients.Controls)
            {
                if (!((WUCIngredients)tempControl)._txtName.Text.Trim().Equals("")
                 && !((WUCIngredients)tempControl)._txtQuantity.Text.Trim().Equals("")
                 && !((WUCIngredients)tempControl)._txtUnit.Text.Trim().Equals(""))
                {
                    IngredientList.Add(new Ingredient() { Name = ((WUCIngredients)tempControl).Name, Quantity = ((WUCIngredients)tempControl).Quantity, Unit = ((WUCIngredients)tempControl).Unit });
                }
            }

            /*
            //IngredientList.Add(new Ingredient() { Name = wucIngredient1.Name, Quantity = wucIngredient1.Quantity, Unit = wucIngredient1.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient2.Name, Quantity = wucIngredient2.Quantity, Unit = wucIngredient2.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient3.Name, Quantity = wucIngredient3.Quantity, Unit = wucIngredient3.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient4.Name, Quantity = wucIngredient4.Quantity, Unit = wucIngredient4.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient5.Name, Quantity = wucIngredient5.Quantity, Unit = wucIngredient5.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient6.Name, Quantity = wucIngredient6.Quantity, Unit = wucIngredient6.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient7.Name, Quantity = wucIngredient7.Quantity, Unit = wucIngredient7.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient8.Name, Quantity = wucIngredient8.Quantity, Unit = wucIngredient8.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient9.Name, Quantity = wucIngredient9.Quantity, Unit = wucIngredient9.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient10.Name, Quantity = wucIngredient10.Quantity, Unit = wucIngredient10.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient11.Name, Quantity = wucIngredient11.Quantity, Unit = wucIngredient11.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient12.Name, Quantity = wucIngredient12.Quantity, Unit = wucIngredient12.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient13.Name, Quantity = wucIngredient13.Quantity, Unit = wucIngredient13.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient14.Name, Quantity = wucIngredient14.Quantity, Unit = wucIngredient14.Unit });
            //IngredientList.Add(new Ingredient() { Name = wucIngredient15.Name, Quantity = wucIngredient15.Quantity, Unit = wucIngredient15.Unit });
            */

            return IngredientList;
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            //CheckLogin("Login.aspx");
            if (Session["User"] == null)
            {
                Session["RedirectPage"] = "AddRecipe.aspx";
                Response.Redirect("Login.aspx");
            }


            int ctr = ((List<Control>)Session["ingList"]).Count + 1;

            Control tempControl = LoadControl("WUCIngredients.ascx");
            ((WUCIngredients)tempControl).ID = "WUCIngredient" + ctr;
            ((WUCIngredients)tempControl)._txtName.ID = "ingName" + ctr;
            ((WUCIngredients)tempControl)._txtQuantity.ID = "ingQuantity" + ctr;
            ((WUCIngredients)tempControl)._txtUnit.ID = "ingUnit" + ctr;

            ((List<Control>)Session["ingList"]).Add(tempControl);

            showIngredientInputs();
        }
    }
}

// Al Roben Adriane Goh - 300910584