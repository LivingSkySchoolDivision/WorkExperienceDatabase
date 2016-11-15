<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Template-Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CareerWorkExperienceDatabase.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- list of cities -->
    <asp:Literal ID="litCities" runat="server"></asp:Literal>

    <!-- list of categories -->
    <asp:Literal ID="litCategories" runat="server"></asp:Literal>
    

</asp:Content>
