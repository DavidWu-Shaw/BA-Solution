<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductExplorer.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.ProductExplorer" %>
<%@ Register Src="ProductListGrid.ascx" TagName="ProductListGrid" TagPrefix="uc1" %>
<%@ Register Src="CategoryTree.ascx" TagName="CategoryTree" TagPrefix="uc2" %>
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
        <div style="margin: 0px 0px 0px 12px;">
            <uc2:CategoryTree ID="ucCategoryTree" runat="server" OnSelectedCategoryChanged="ucCategoryTree_SelectedCategoryChanged" />
        </div>
    </div>
    <div style="margin: 4px; width: 740px; min-width: 400px; float: left;">
        <div id="dvRotator" runat="server">
            <telerik:RadRotator runat="server" ID="rotProd" ScrollDirection="Up" BorderStyle="Solid"
                RotatorType="AutomaticAdvance" BorderWidth="1px" FrameDuration="5000" Width="99%"
                ItemWidth="600px" Height="160px" ItemHeight="160px">
                <ItemTemplate>
                    <div style="margin: 10px;">
                        <div style="float: left; width: 120px;">
                            <div>
                                <asp:HyperLink ID="hlDetail" runat="server" NavigateUrl='<%# GetProductUrl(Container.DataItem) %>'
                                    ToolTip="Click to show detail.">
                                    <telerik:RadBinaryImage ID="Image1" runat="server" Width="120px" Height="120px" ResizeMode="Fit"
                                        BorderColor="Gray" BorderWidth="0px" DataValue='<%# Eval("Sketch") %>' />
                                </asp:HyperLink>
                            </div>
                        </div>
                        <div style="float: left; margin-left: 20px; width: 240px;">
                            <div style="height: 24px">
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Name") %>' CssClass="devTextBold"></asp:Label>
                            </div>
                            <div style="height: 20px">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                            </div>
                            <div style="height: 20px">
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Packaging") %>'></asp:Label>
                            </div>
                            <div style="height: 40px;">
                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </div>
                            <div style="height: 20px">
                                <asp:HyperLink ID="hlSupplier" runat="server" Text='<%# Eval("SupplierName") %>'
                                    NavigateUrl='<%# GetSupplierUrl(Container.DataItem) %>' ToolTip="Click to show supplier detail.">
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </ItemTemplate>
            </telerik:RadRotator>
        </div>
        <div id="dvList" runat="server" visible="false">
            <uc1:ProductListGrid ID="ucProductList" runat="server" />
        </div>
    </div>
    <div class="clear">
    </div>
</div>
