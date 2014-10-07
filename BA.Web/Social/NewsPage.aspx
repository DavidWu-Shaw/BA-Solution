<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="NewsPage.aspx.cs" Inherits="BA.Web.Social.NewsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="margin-top: 20px;">
        <div style="margin: 10px">
            <asp:Label ID="lblTitle" runat="server" CssClass="TitleText"></asp:Label>
        </div>
        <div style="margin: 10px">
            <asp:Label ID="lblContent" runat="server" CssClass="ContentText"></asp:Label>
        </div>
    </div>
</asp:Content>
