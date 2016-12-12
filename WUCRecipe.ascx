<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCRecipe.ascx.cs" Inherits="WUCRecipe" %>

<div style="display:inline-block; width:100%">
    <div style="float: left; margin-right: 8px">
        <asp:Image ID="img" Width="100" Height="100" runat="server" />
    </div>

    <a id="linkRecipeName" runat="server"><asp:Label ID="txtName" Text="text" runat="server" /></a>
    <br /><br />
    <asp:Label ID="txtSubmitted" Text="text" runat="server" />
    <br /><br />
    <asp:Label ID="txtPrepareTime" Text ="text" runat="server" />
</div>
<hr />
<br />