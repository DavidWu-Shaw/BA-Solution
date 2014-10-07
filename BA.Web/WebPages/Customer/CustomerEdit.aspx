<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AppMasterPage.master" AutoEventWireup="true"
    CodeBehind="CustomerEdit.aspx.cs" Inherits="BA.Web.WebPages.Customer.CustomerEdit" %>

<%@ Register Src="../../Common/Subject/UI/InstanceEdit.ascx" TagName="InstanceEdit"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AppMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppMBody" runat="server">
    <uc1:InstanceEdit ID="ucIEdit" runat="server" SubjectType="Customer" />
</asp:Content>
