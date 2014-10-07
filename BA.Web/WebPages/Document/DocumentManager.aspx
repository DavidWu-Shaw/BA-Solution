<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AppMasterPage.master" AutoEventWireup="true"
    CodeBehind="DocumentManager.aspx.cs" Inherits="BA.Web.WebPages.Document.DocumentManager" %>

<%@ Register Src="../../Common/Subject/UI/InstanceDetail.ascx" TagName="InstanceDetail"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AppMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppMBody" runat="server">
    <uc1:InstanceDetail ID="ucIDetail" runat="server" SubjectType="Document" />
</asp:Content>
