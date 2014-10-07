<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AppMasterPage.master"
    AutoEventWireup="true" CodeBehind="ContactMgt.aspx.cs" Inherits="BA.Web.WebPages.Contact.ContactMgt" %>

<%@ MasterType VirtualPath="~/MasterPages/AppMasterPage.master" %>
<%@ Register Src="../../Common/Subject/UI/InstanceList.ascx" TagName="InstanceList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AppMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppMBody" runat="server">
    <uc1:InstanceList ID="ucIList" runat="server" SubjectType="Contact" />
</asp:Content>
