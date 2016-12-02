<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Template-Main.Master" AutoEventWireup="true" CodeBehind="viewPosition.aspx.cs" Inherits="CareerWorkExperienceDatabase.viewPosition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <table cellpadding="0" cellspacing="0"  width="100%">
        <tr>
            <td width="66%" align="left" valign="top">
                <asp:Label ID="lblPositionName" runat="server" Text="Position Name" CssClass="mainPageTitle"></asp:Label>
                <asp:Table ID="tblPositionDetails" runat="server"></asp:Table>

                <asp:Literal ID="litPositionDescription" runat="server"></asp:Literal>
                <asp:Literal ID="litReviews" runat="server"></asp:Literal>

            </td>
            <td width="33%" align="left" valign="top"> 
                <asp:Label ID="lblBusinessName" runat="server" Text="Business Name" CssClass="mainPageTitle"></asp:Label>
                <asp:Table ID="tblBusinessDetails" runat="server"></asp:Table>
                
                <asp:Literal ID="litPositionFlags" runat="server"></asp:Literal>
                <asp:Literal ID="litCOPSBadges" runat="server"></asp:Literal>

            </td>
        </tr>
    </table>


</asp:Content>
