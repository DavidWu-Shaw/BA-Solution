<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="BA.Web.Shop.ProductPage" %>

<%@ Register Src="UserControls/ProductDetail.ascx" TagName="ProductDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <uc1:ProductDetail ID="ucProduct" runat="server" />
</asp:Content>
