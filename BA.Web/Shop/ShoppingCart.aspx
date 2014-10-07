<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="BA.Web.Shop.ShoppingCart" %>

<%@ Register Src="UserControls/CartView.ascx" TagName="CartView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="width: 600px; margin-left: auto; margin-right: auto;">
        <div>
            <uc1:CartView ID="ucCart" runat="server" />
        </div>
        <div style="margin-top: 10px; float: right">
            <asp:Button ID="btnCheckout" runat="server" CssClass="clsButton" />
            <asp:Button ID="btnClearCart" runat="server" CssClass="clsButton" OnClick="btnClearCart_Click" />
        </div>
    </div>
</asp:Content>
