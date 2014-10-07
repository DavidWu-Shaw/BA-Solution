<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="CatalogManager.aspx.cs" Inherits="BA.Web.WebPages.Catalog.CatalogManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <div style="margin-top: 20px; margin-left: 12px;">
        <div>
            <asp:Label ID="lblCatalog" runat="server" CssClass="devTextBold"></asp:Label>
            <asp:Label ID="lblName" runat="server"></asp:Label>
        </div>
        <div style="margin-top: 10px;">
            <telerik:RadTreeView ID="tvCat" runat="server" AllowNodeEditing="true" DataFieldID="Id"
                DataFieldParentID="ParentId" DataTextField="Name" OnContextMenuItemClick="tvCat_ContextMenuItemClick"
                OnNodeDataBound="tvCat_NodeDataBound">
                <DataBindings>
                    <telerik:RadTreeNodeBinding Expanded="True" />
                </DataBindings>
                <ContextMenus>
                    <telerik:RadTreeViewContextMenu ID="ConMenu" runat="server">
                    </telerik:RadTreeViewContextMenu>
                </ContextMenus>
            </telerik:RadTreeView>
        </div>
    </div>
</asp:Content>
