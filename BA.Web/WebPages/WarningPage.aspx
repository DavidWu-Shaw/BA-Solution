<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="WarningPage.aspx.cs" Inherits="BA.Web.WebPages.WarningPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainC" runat="server">
    <div style="padding: 5px;">
        <asp:Label ID="lblWarning" runat="server" CssClass="clsTextBold">
        </asp:Label>
    </div>
</asp:Content>
