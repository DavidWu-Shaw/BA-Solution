<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="GlobalProductExplorer.aspx.cs" Inherits="BA.Web.Shop.GlobalProductExplorer" %>

<%@ Register Src="UserControls/MonoProductExplorer.ascx" TagName="MonoProductExplorer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="margin-top: 10px;">
        <uc1:MonoProductExplorer ID="ucProductExplorer" runat="server" />
    </div>
</asp:Content>
