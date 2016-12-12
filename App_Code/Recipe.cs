using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Recipe
/// </summary>
public class Recipe
{
    private int id;
    private string name;
    private string submittedBy;
    private string category;
    private int cookingTime;
    private int servings;
    private string description;
    List<Ingredient> ingredientList;

    
    

    public Recipe()
    {
    }

    // Getters/Setters START //

    public int Id { get; set; }
    public string Name { get; set; }
    public string SubmittedBy { get; set; }
    public string Category { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public string Description { get; set; }
    public List<Ingredient> IngredientList { get; set; }
    public string ImgPath { get; set; }


    // Getters/Setters END //
}