<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Template-Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CareerWorkExperienceDatabase.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <table cellspacing="0" cellpadding+"0">
        <tr>
            <td width="800" valign="top" align="left">
                <!-- list of categories -->
                 <div class="mainPageTitle">Job Categories</div>
                <asp:Literal ID="litCategories" runat="server"></asp:Literal>
            </td>
            <td width="200" rowspan="2" valign="top" align="left">
                <!-- list of cities -->
                <asp:Literal ID="litCities" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left"> 
                <div class="mainPageTitle">Recently Updated</div>
                <!-- list of recently updated positions -->
                <asp:Literal ID="litUpdatedPositions" runat="server"></asp:Literal>

            </td>
        </tr>
    </table>
    
    
 

   
    
   

</asp:Content>
