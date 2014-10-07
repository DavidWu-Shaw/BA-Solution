<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="TopicPage.aspx.cs" Inherits="BA.Web.Social.TopicPage" %>

<%@ Register Src="UserControls/PostList.ascx" TagName="PostList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="margin-top: 20px;">
        <div style="height: 30px; margin: 10px;">
            <asp:Label ID="lblTitle" runat="server" CssClass="TitleText" />
        </div>
        <div>
            <uc1:PostList ID="ucPostList" runat="server" />
        </div>
    </div>
</asp:Content>
