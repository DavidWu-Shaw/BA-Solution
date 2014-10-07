<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="BrowseSupplier.aspx.cs" Inherits="BA.Web.Shop.BrowseSupplier" %>

<%@ Register Src="UserControls/SupplierSearchCriteria.ascx" TagName="SupplierSearchCriteria"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/CategoryTree.ascx" TagName="CategoryTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div>
        <div class="LightBlueBar">
            <div style="margin: 4px 0px 0px 4px; width: 240px; float: left; vertical-align: middle">
                <asp:Label ID="lblFilter" runat="server" CssClass="devTextBold" />
            </div>
            <div style="margin: 4px 0px 0px 0px; width: 740px; float: left;">
                <asp:Label ID="lblTitle" runat="server" CssClass="devTextBold" />
            </div>
        </div>
        <div style="width: 240px; float: left; background: LightGrey;">
            <div style="margin: 10px 0px 10px 20px">
                <asp:LinkButton ID="btnPreset" runat="server" Font-Size="10pt" OnClick="btnPreset_Click"></asp:LinkButton>
            </div>
            <div style="margin: 6px 0px 0px 6px">
                <asp:Label ID="lblByCat" runat="server" CssClass="devTextBold" />
            </div>
            <div style="margin: 0px 0px 0px 12px">
                <uc2:CategoryTree ID="ucCategoryTree" runat="server" OnSelectedCategoryChanged="ucCategoryTree_SelectedCategoryChanged" />
            </div>
            <div style="margin: 30px 0px 0px 6px;">
                <asp:Label ID="lblByCriteria" runat="server" CssClass="devTextBold" />
            </div>
            <div style="margin: 10px">
                <uc1:SupplierSearchCriteria ID="ucSupplierCriteria" runat="server" />
            </div>
            <div style="margin: 10px;">
                <asp:Button ID="btnSearch" runat="server" CssClass="clsButton" />
            </div>
        </div>
        <div style="margin: 4px; width: 740px; min-width: 400px; float: left;">
            <div id="dvRotator" runat="server">
                <telerik:RadRotator runat="server" ID="rotProd" ScrollDirection="Up" BorderStyle="Solid"
                    RotatorType="AutomaticAdvance" BorderWidth="1px" FrameDuration="5000" Width="99%"
                    ItemWidth="600px" Height="120px" ItemHeight="120px">
                    <ItemTemplate>
                        <div style="margin: 10px;">
                            <div style="float: left; width: 120px;">
                                <asp:HyperLink ID="hlDetail" runat="server" NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>'
                                    ToolTip="Click to show detail.">
                                    <telerik:RadBinaryImage ID="Image1" runat="server" Width="120px" Height="100px" ResizeMode="Fit"
                                        BorderColor="Gray" BorderWidth="0px" DataValue='<%# Eval("Logo") %>' />
                                </asp:HyperLink>
                            </div>
                            <div style="float: left; margin-left: 20px; width: 160px;">
                                <div style="height: 24px">
                                    <asp:HyperLink ID="hlItem" runat="server" Text='<%# Eval("Name") %>' CssClass="devTextBold"
                                        NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>'>
                                    </asp:HyperLink>
                                </div>
                                <div>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CategoryText") %>'></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ZipCode") %>'></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ContactPhone") %>'></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="Label4" runat="server" Text='<%# GetOpeningHour(Container.DataItem) %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </ItemTemplate>
                </telerik:RadRotator>
            </div>
            <div id="dvList" runat="server" visible="false">
                <telerik:RadListView ID="lvProd" runat="server" ItemPlaceholderID="ItemContainer">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemContainer" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <fieldset style="float: left; width: 320px; height: 160px; margin: 2px">
                            <div style="margin: 10px;">
                                <div style="float: left; width: 120px;">
                                    <div>
                                        <asp:HyperLink ID="hlDetail" runat="server" NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>'
                                            ToolTip="Click to show detail.">
                                            <telerik:RadBinaryImage ID="Image1" runat="server" Width="120px" Height="100px" ResizeMode="Fit"
                                                BorderColor="Gray" BorderWidth="0px" DataValue='<%# Eval("Logo") %>' />
                                        </asp:HyperLink>
                                    </div>
                                    <div style="margin-top: 10px; height: 20px;">
                                        <telerik:RadRating ID="Rating1" runat="server" ItemCount="5" Value='<%# Eval("Rating") %>'
                                            ReadOnly="true" SelectionMode="Continuous" Precision="Half" Orientation="Horizontal" />
                                    </div>
                                    <div style="margin-top: 5px; height: 20px">
                                        <asp:Label ID="Label6" runat="server" Text='<%# GetNumberOfRatingsDisplay(Container.DataItem) %>'></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; margin-left: 20px; width: 160px;">
                                    <div style="height: 24px">
                                        <asp:HyperLink ID="hlItem" runat="server" Text='<%# Eval("Name") %>' CssClass="devTextBold"
                                            NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>'>
                                        </asp:HyperLink>
                                    </div>
                                    <div>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CategoryText") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("ZipCode") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ContactPhone") %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="Label4" runat="server" Text='<%# GetOpeningHour(Container.DataItem) %>'></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </fieldset>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmpty" runat="server" Text="No results." />
                    </EmptyDataTemplate>
                </telerik:RadListView>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
