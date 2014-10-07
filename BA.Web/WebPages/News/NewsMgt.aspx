<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="NewsMgt.aspx.cs" Inherits="BA.Web.WebPages.News.NewsMgt" %>

<%@ Register Src="../../Common/Subject/UI/InstanceList.ascx" TagName="InstanceList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:InstanceList ID="ucIList" runat="server" SubjectType="News" />
</asp:Content>
