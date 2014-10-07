<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="CategoryManager.aspx.cs" Inherits="BA.Web.WebPages.Catalog.CategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <div>
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
</asp:Content>
