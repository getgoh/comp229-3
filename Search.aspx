<%-- Al Roben Adriane Goh - 300910584--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Assignment1.Search" %>

<%@ Register Src="~/WUCRecipe.ascx" TagPrefix="uc1" TagName="WUCRecipe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Search Page</h1>
    <h3>In this page, you can search for a recipe</h3>

    <br /><br />

    <asp:Label Text="Submitted by: " runat="server" />
    <br />
    <asp:DropDownList ID="ddlSubmittedBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubmittedBy_SelectedIndexChanged">
    </asp:DropDownList>

    <%-- Al Roben Adriane Goh - 300910584--%>

    <br /><br />
    <asp:Label Text="Category: " runat="server" />
    <br />
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
    </asp:DropDownList>

    <br /><br />
    <asp:Label Text="Name of ingredient: " runat="server" />
    <br />
    <asp:DropDownList ID="ddlIngredientName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIngredientName_SelectedIndexChanged">
    </asp:DropDownList>

    <br /><br />
    <div class="dvFormHolder">
        <div id="dvRecipeListHolder">
            <asp:PlaceHolder ID="phRecipes" runat="server" />
        </div>
    </div>

</asp:Content>


<%-- Al Roben Adriane Goh - 300910584--%>