<%-- Al Roben Adriane Goh - 300910584--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="Setup.aspx.cs" Inherits="Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%-- Al Roben Adriane Goh - 300910584--%>

    <h3>
        Select a colour scheme for the application
    </h3>
    <asp:Button ID="btnLight" Width="150" Height="40" Font-Size="Medium" Text="Light" runat="server" OnClick="btnLight_Click" />
    <br />
    <asp:Button ID="btnDark" Width="150" Height="40" Font-Size="Medium" Text="Dark" runat="server" OnClick="btnDark_Click" />
</asp:Content>

<%-- Al Roben Adriane Goh - 300910584--%>