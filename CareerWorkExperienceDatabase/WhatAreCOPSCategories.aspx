<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/Template-Main.Master" AutoEventWireup="true" CodeBehind="WhatAreCOPSCategories.aspx.cs" Inherits="CareerWorkExperienceDatabase.WhatAreCOPSCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <p>
        COPS Categories is a Career Guide that is used by many highschool career and guidance counsellours. 
        Your teacher may have given you a test, that will match your interests, to occupations or careers that best suit your 
        interests.
    </p>
    <p>
        You then can look at the Comprehension Career Guide, to find the 14 Career Clusters.
        The Career Clusters contain information about that specific career, including example occupations, skills needed in
        those occupations, and related fields of study.
    </p>
    <p>
        We have categorized each available Career Work Experience position to its proper Career Cluster, for ease of finding
        job shadow locations best suited to you!
    </p>
    <p>
        Each Career Cluster below contains a small description and a few examples, to help you familiarize with them. To see 
        the full information packets, contact your guidance/career counsellour, or visit <a href="http://www.edits.net/products/career-guidance/cops.html">http://www.edits.net/products/career-guidance/cops.html</a>
    </p>
    <asp:Table ID="tblCategories" runat="server" Width="100%"></asp:Table>
</asp:Content>
