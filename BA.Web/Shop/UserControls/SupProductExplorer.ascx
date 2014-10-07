<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupProductExplorer.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.SupProductExplorer" %>
<%@ Register Src="CategoryTree.ascx" TagName="CategoryTree" TagPrefix="uc2" %>
<div>
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
    <div style="margin: 4px; width: 720px; min-width: 400px; float: left;">
        <telerik:RadListView ID="lvProd" runat="server" ItemPlaceholderID="ItemContainer"
            OnItemCreated="lvProd_ItemCreated">
            <LayoutTemplate>
                <asp:PlaceHolder ID="ItemContainer" runat="server" />
            </LayoutTemplate>
            <ItemTemplate>
                <fieldset style="float: left; height: 220px; margin: 2px">
                    <div style="margin: 10px;">
                        <div style="float: left; width: 120px;">
                            <div>
                                <asp:HyperLink ID="hlDetail" runat="server" NavigateUrl='<%# GetProductUrl(Container.DataItem) %>'
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
                        <div style="float: left; margin-left: 20px; width: 180px;">
                            <div style="height: 24px">
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>' CssClass="devTextBold"></asp:Label>
                            </div>
                            <div style="height: 20px">
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CategoryText") %>'></asp:Label>
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
                    <div style="margin: 10px; float: right;">
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
    <div class="clear">
    </div>
</div>
