<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryTree.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.CategoryTree" %>
<asp:TreeView ID="tvCat" runat="server" ImageSet="Simple" OnSelectedNodeChanged="tvCat_SelectedNodeChanged">
    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
        NodeSpacing="0px" VerticalPadding="0px" />
    <ParentNodeStyle Font-Bold="False" />
    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
        VerticalPadding="0px" />
</asp:TreeView>
