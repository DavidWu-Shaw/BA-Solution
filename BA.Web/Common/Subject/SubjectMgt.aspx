<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="SubjectMgt.aspx.cs" Inherits="BA.Web.Common.Subject.SubjectMgt" %>

<%@ Register Src="../UserControls/ListManager.ascx" TagName="ListManager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
    <title>Subject List</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:ListManager ID="ucListManager" runat="server" />
</asp:Content>
