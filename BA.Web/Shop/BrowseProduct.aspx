<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="BrowseProduct.aspx.cs" Inherits="BA.Web.Shop.BrowseProduct" %>

<%@ Register Src="UserControls/ProductExplorer.ascx" TagName="ProductExplorer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <uc1:ProductExplorer ID="ucProductExplorer" runat="server" />
</asp:Content>
