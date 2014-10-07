<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductListGrid.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.ProductListGrid" %>
<telerik:RadGrid ID="gvProd" runat="server" Skin="Windows7" AutoGenerateColumns="false"
    AllowSorting="true" OnNeedDataSource="gvProd_NeedDataSource">
    <MasterTableView AllowSorting="true">
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="true">
        <Scrolling AllowScroll="true" ScrollHeight="600" />
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
</telerik:RadGrid>
