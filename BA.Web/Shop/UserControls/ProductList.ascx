<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.ProductList" %>
<div style="min-width: 220px;">
    <telerik:RadListView ID="lvProd" runat="server" ItemPlaceholderID="ItemContainer"
        OnNeedDataSource="lvProd_NeedDataSource" OnItemCreated="lvProd_ItemCreated">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <fieldset style="float: left; height: 220px; margin: 2px">
                <div style="margin: 10px;">
                    <div style="float: left; width: 120px;">
                        <div>
                            <asp:HyperLink ID="hlDetail" runat="server" NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>'
                                ToolTip="Click to show detail.">
                                <telerik:RadBinaryImage ID="Image1" runat="server" Width="120px" Height="120px" ResizeMode="Fit"
                                    BorderColor="Gray" BorderWidth="0px" DataValue='<%# Eval("Sketch") %>' />
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
                    <div style="float: left; margin-left: 20px; width: 240px;">
                        <div style="height: 24px">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>' CssClass="devTextBold"></asp:Label>
                        </div>
                        <div style="height: 20px">
                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                        </div>
                        <div style="height: 40px;">
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </div>
                        <div style="height: 20px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div style="margin: 10px; height: 30px; float: right;">
                    <asp:Button ID="btnAction" runat="server" CssClass="clsButton" OnCommand="Action_Command"
                        CommandArgument='<%# Eval("ProductId") %>' />
                </div>
            </fieldset>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:Label ID="lblEmpty" runat="server" Text="No results." />
        </EmptyDataTemplate>
    </telerik:RadListView>
</div>
