﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="ProductManager.aspx.cs" Inherits="BA.Web.WebPages.Product.ProductManager" %>

<%@ Register Src="../../Common/Subject/UI/InstanceDetail.ascx" TagName="InstanceDetail"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:InstanceDetail ID="ucIDetail" runat="server" SubjectType="Product" />
</asp:Content>
