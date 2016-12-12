<%-- Al Roben Adriane Goh - 300910584--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="Assignment1.Recipes" %>

<%@ Register Src="~/WUCRecipe.ascx" TagPrefix="uc1" TagName="WUCRecipe" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Recipes Page</h1>
    <h3>In this page, you will find all the recipes</h3>

    <%-- Al Roben Adriane Goh - 300910584--%>

    <div class="dvFormHolder">
        <div id="dvRecipeListHolder">
            <asp:PlaceHolder ID="phRecipes" runat="server" />
        </div>
    </div>

</asp:Content>


<%-- Al Roben Adriane Goh - 300910584--%>