<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Login</h1>

    <div class="dvFormHolder">
        <ul>
            <li class="style-1">
                <span class="widelabel2">User Name:</span>
                <asp:TextBox ID="txtUser" runat="server" />
            </li>
            <li class="style-1">
                <span class="widelabel2">Password:</span>
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
            </li>
            <li>
                <span class="widelabel2"></span>
                <asp:Button Width="150" Height="40" Font-Size="Medium" Text="Login" ID="btnLogin" runat="server" OnClick="btnLogin_Click" />
            </li>
        </ul>
    </div>
</asp:Content>

