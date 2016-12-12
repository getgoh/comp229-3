using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ingredient
/// </summary>
public class Ingredient
{
    private int id;
    private string name;
    private int quantity;
    private string measureUnit; // kg, g, l, cups, pieces

    public Ingredient()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Id { get; set; }

    public string Name
    {
        get { return name; }
        set { this.name = value; }
    }
    
    public int Quantity
    {
        get
        {
            return this.quantity;
        }
        set
        {
            this.quantity = value;
        }
    }

    public string Unit
    {
        get
        {
            return this.measureUnit;
        }
        set
        {
            this.measureUnit = value;
        }
    }



}