using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;

/// <summary>
/// Summary description for DataManager
/// </summary>
public class DataManager
{
    //private string connString = "DATA SOURCE=oracle1.centennialcollege.ca:1521/SQLD;USER ID=COMP214F16_004_P_39;PASSWORD=password";
    private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["COMP229OracleCS"].ConnectionString;
    private OracleConnection conn;
    private OracleCommand cmd, ingCmd;
    private OracleDataReader reader;
    private string query = "";

    public DataManager()
    {
        conn = new OracleConnection(connString);
    }

    public User login(string username, string password)
    {
        User currUser = null;
        query = "select * from RB_USER where USERNAME=:UN AND PASSWORD=:PW";
        //query = "select * from RB_USER";

        conn.Open();

        cmd = new OracleCommand(query, conn);

        cmd.Parameters.Add("UN", username);
        cmd.Parameters.Add("PW", password);
        // Enclose database code in Try-Catch-Finally
        try
        {
            // Open the connection
            //conn.Open();
            // Execute the command
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                reader.Read();
                //showMessage(reader[0].ToString() + " " + reader[1].ToString());

                currUser = new User();
                currUser.Username = reader["USERNAME"].ToString();                
                
            }

            reader.Close();
        }
        catch(Exception e)
        {

        }
        finally
        {
            // Close the connection
            conn.Close();
        }

        return currUser;
    }

    public int insertNewRecipe(Recipe newRecipe)
    {
        query = "RB_ADDRECIPE";

        conn.Open();

        cmd = new OracleCommand(query, conn);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("NAME", OracleDbType.Varchar2).Value = newRecipe.Name;
        cmd.Parameters.Add("SUBMITTEDBY", OracleDbType.Varchar2).Value = newRecipe.SubmittedBy;
        cmd.Parameters.Add("CATEGORY", OracleDbType.Varchar2).Value = newRecipe.Category;
        cmd.Parameters.Add("COOKINGTIME", OracleDbType.Int32).Value = newRecipe.CookingTime;
        cmd.Parameters.Add("SERVINGS", OracleDbType.Int32).Value = newRecipe.Servings;
        cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = newRecipe.Description;
        cmd.Parameters.Add("IMAGEPATH", OracleDbType.Varchar2).Value = newRecipe.ImgPath;

        cmd.Parameters.Add("NEWID", OracleDbType.Int32).Direction = ParameterDirection.Output;

        cmd.ExecuteNonQuery();

        int newId = newRecipe.Id = int.Parse(cmd.Parameters["NEWID"].Value.ToString());
        
        

        //cmd.ExecuteNonQuery();

        insertIngredients(newRecipe);

        conn.Close();

        return newId;
    }

    private int insertIngredients(Recipe newRecipe)
    {
        query = "RB_ADDINGREDIENT";
        
        foreach (Ingredient i in newRecipe.IngredientList)
        {
            cmd = new OracleCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("RECIPEID", OracleDbType.Int32).Value = newRecipe.Id;
            cmd.Parameters.Add("NAME", OracleDbType.Varchar2).Value = i.Name;
            cmd.Parameters.Add("QUANTITY", OracleDbType.Int32).Value = i.Quantity;
            cmd.Parameters.Add("MEASUREUNIT", OracleDbType.Varchar2).Value = i.Unit;

            cmd.ExecuteNonQuery();
        }

        return 0;
    }

    public List<Recipe> getRecipeList()
    {
        List<Recipe> RecipeList = new List<Recipe>();

        query = "select * from RB_RECIPE";

        conn.Open();

        cmd = new OracleCommand(query, conn);

        reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            Recipe r = new Recipe();
            r.Id = Convert.ToInt32(reader["RECIPEID"].ToString());
            r.Name = reader["NAME"].ToString();
            r.SubmittedBy = reader["SUBMITTEDBY"].ToString();
            r.Category = reader["CATEGORY"].ToString();
            r.CookingTime = Convert.ToInt32(reader["COOKINGTIME"].ToString());
            r.Servings = Convert.ToInt32(reader["SERVINGS"].ToString());
            r.Description = reader["DESCRIPTION"].ToString();
            r.ImgPath = reader["IMAGEPATH"].ToString();

            r.IngredientList = getIngredientsByRecipeID(r.Id);

            RecipeList.Add(r);
        }

        conn.Close();

        return RecipeList;
    }

    public void addCategory(string category)
    {
        query = "RB_ADDCATEGORY";

        conn.Open();

        cmd = new OracleCommand(query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("LV_NAME", OracleDbType.Varchar2).Value = category;

        cmd.ExecuteNonQuery();

        conn.Close();
    }

    public List<string> getCategories()
    {
        List<string> categoryList = new List<string>();

        query = "select * from RB_CATEGORY";

        conn.Open();

        cmd = new OracleCommand(query, conn);

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            categoryList.Add(reader["NAME"].ToString());
        }

        conn.Close();

        return categoryList;
    }

    public List<Recipe> getRecipeBySearchParams(string submittedBy, string category, string ingredient)
    {
        List<Recipe> RecipeList = new List<Recipe>();

        conn.Open();

        query = "select * from RB_RECIPE";
        bool hasFirst = false;

        cmd = new OracleCommand();
        cmd.Connection = conn;

        if (submittedBy != "All" || category != "All")
        {
            query += " where ";

            if(submittedBy != "All")
            {
                query += "SUBMITTEDBY= :SB";
                cmd.Parameters.Add(new OracleParameter("SB", submittedBy));
                hasFirst = true;
            }
            if(category != "All")
            {
                query += hasFirst ? " and CATEGORY= :CAT" : "CATEGORY= :CAT";
                cmd.Parameters.Add(new OracleParameter("CAT", category));
            }
        }

        cmd.CommandText = query;

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Recipe r = new Recipe();
            r.Id = Convert.ToInt32(reader["RECIPEID"].ToString());
            r.Name = reader["NAME"].ToString();
            r.SubmittedBy = reader["SUBMITTEDBY"].ToString();
            r.Category = reader["CATEGORY"].ToString();
            r.CookingTime = Convert.ToInt32(reader["COOKINGTIME"].ToString());
            r.Servings = Convert.ToInt32(reader["SERVINGS"].ToString());
            r.Description = reader["DESCRIPTION"].ToString();
            r.ImgPath = reader["IMAGEPATH"].ToString();


            r.IngredientList = getIngredientsByRecipeID(r.Id);

            RecipeList.Add(r);
        }

        conn.Close();

        if(ingredient != "All")
        {
            RecipeList = RecipeList.Where(a => a.IngredientList.Where(b => b.Name.Equals(ingredient)).Count() > 0).ToList();
        }

        return RecipeList;
    }

    public List<string> getIngrendientsNameList()
    {
        List<string> IngredientList = new List<string>();

        query = "select distinct NAME from RB_INGREDIENT";
        
        conn.Open();

        ingCmd = new OracleCommand(query, conn);

        OracleDataReader ingReader = ingCmd.ExecuteReader();

        if (ingReader == null || !ingReader.HasRows)
            return IngredientList;

        while (ingReader.Read())
        {
            //Ingredient i = new Ingredient();
            //i.Id = Convert.ToInt32(ingReader["INGREDIENTID"].ToString());
            //i.Name = ingReader["NAME"].ToString();
            //i.Quantity = Convert.ToInt32(ingReader["QUANTITY"].ToString());
            //i.Unit = ingReader["MEASUREUNIT"].ToString();

            IngredientList.Add(ingReader["NAME"].ToString());
        }

        conn.Close();

        return IngredientList;
    }

    public Recipe getRecipeById(int recipeId)
    {
        Recipe r = null;

        query = "select * from RB_RECIPE where RECIPEID= :recipeId";

        conn.Open();

        cmd = new OracleCommand(query, conn);
        cmd.Parameters.Add(new OracleParameter("recipeId", recipeId));
        cmd.CommandTimeout = 30;
        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            r = new Recipe();
            r.Id = Convert.ToInt32(reader["RECIPEID"].ToString());
            r.Name = reader["NAME"].ToString();
            r.SubmittedBy = reader["SUBMITTEDBY"].ToString();
            r.Category = reader["CATEGORY"].ToString();
            r.CookingTime = Convert.ToInt32(reader["COOKINGTIME"].ToString());
            r.Servings = Convert.ToInt32(reader["SERVINGS"].ToString());
            r.Description = reader["DESCRIPTION"].ToString();
            r.ImgPath = reader["IMAGEPATH"].ToString();

            r.IngredientList = getIngredientsByRecipeID(r.Id);            
        }

        conn.Close();

        return r;
    }

    private List<Ingredient> getIngredientsByRecipeID(int recipeId)
    {
        List<Ingredient> IngredientList = new List<Ingredient>();

        query = "select * from RB_INGREDIENT where RECIPEID=" + recipeId;

        // conn is already open at this point
        //conn.Open();

        ingCmd = new OracleCommand(query, conn);

        OracleDataReader ingReader = ingCmd.ExecuteReader();

        if (ingReader == null || !ingReader.HasRows)
            return IngredientList;

        while(ingReader.Read())
        {
            Ingredient i = new Ingredient();
            i.Id = Convert.ToInt32(ingReader["INGREDIENTID"].ToString());
            i.Name = ingReader["NAME"].ToString();
            i.Quantity = Convert.ToInt32(ingReader["QUANTITY"].ToString());
            i.Unit = ingReader["MEASUREUNIT"].ToString();

            IngredientList.Add(i);
        }

        return IngredientList;
    }

   public void updateRecipe(Recipe recipe)
    {
        query = "RB_UPDATERECIPE";

        conn.Open();

        cmd = new OracleCommand(query, conn);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("LV_ID", OracleDbType.Int32).Value = recipe.Id;
        cmd.Parameters.Add("LV_NAME", OracleDbType.Varchar2).Value = recipe.Name;
        cmd.Parameters.Add("LV_CATEGORY", OracleDbType.Varchar2).Value = recipe.Category;
        cmd.Parameters.Add("LV_COOKINGTIME", OracleDbType.Int32).Value = recipe.CookingTime;
        cmd.Parameters.Add("LV_SERVINGS", OracleDbType.Int32).Value = recipe.Servings;
        cmd.Parameters.Add("LV_DESCRIPTION", OracleDbType.Varchar2).Value = recipe.Description;
        cmd.Parameters.Add("LV_IMAGEPATH", OracleDbType.Varchar2).Value = recipe.ImgPath;

        cmd.ExecuteNonQuery();

        insertIngredients(recipe);

        //cmd.ExecuteNonQuery();        

        conn.Close();
    }

    public void deleteRecipeById(int recipeId)
    {
        // delete recipe, then delete ingredients
        query = "RB_DELETERECIPE";

        conn.Open();
        
        cmd = new OracleCommand(query, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        
        cmd.Parameters.Add("LV_RECIPEID", OracleDbType.Int32).Value = recipeId;
        cmd.ExecuteNonQuery();
        
        conn.Close();
    }

    public static string SafeGetString(OracleDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
            return reader.GetString(colIndex);
        return string.Empty;
    }

    private void testOracle()
    {
        string cityListQuery = "select * from bb_basket";

        conn.Open();

        cmd = new OracleCommand(cityListQuery, conn);

        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            //Response.Write(oreader[0].ToString() + "<br />");
            //Response.Write(oreader[1].ToString() + "<br />");
            //Response.Write(oreader[2].ToString() + "<br />");
            //Response.Write(oreader[0].ToString() + "<br />");
        }
        conn.Close();
    }
}