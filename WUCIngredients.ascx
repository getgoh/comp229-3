<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCIngredients.ascx.cs" Inherits="WUCIngredients" %>


<div class="IngredientsHolder">
    <asp:TextBox ID="txtName" placeholder="name" CssClass="" runat="server" Width="150px"></asp:TextBox>
    <asp:TextBox ID="txtQuantity" TextMode="Number" placeholder="quantity" runat="server" Width="50px"></asp:TextBox>
    <asp:TextBox ID="txtUnit" placeholder="unit (g, l, etc)" runat="server" Width="80px"></asp:TextBox>

    <%--<asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please enter name" ControlToValidate="txtName" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>--%>
    <asp:Label ID="lblError" Text="" runat="server" />
</div>