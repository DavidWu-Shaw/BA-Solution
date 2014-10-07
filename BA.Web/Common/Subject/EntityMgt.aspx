<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="EntityMgt.aspx.cs" Inherits="BA.Web.Common.Subject.EntityMgt" %>

<%@ Register Src="../UserControls/ListManager.ascx" TagName="ListManager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
    <title>Entity List</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:ListManager ID="ucListManager" runat="server" />
</asp:Content>
