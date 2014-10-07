<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonoProductExplorer.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.MonoProductExplorer" %>
<%@ Register Src="CategoryTree.ascx" TagName="CategoryTree" TagPrefix="uc1" %>
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
            <uc1:CategoryTree ID="ucCategoryTree" runat="server" OnSelectedCategoryChanged="ucCategoryTree_SelectedCategoryChanged" />
        </div>
    </div>
    <div style="margin: 4px; width: 740px; min-width: 400px; float: left;">
        <telerik:RadListView ID="lvProd" runat="server" ItemPlaceholderID="ItemContainer"
            OnItemCreated="lvProd_ItemCreated">
            <LayoutTemplate>
                <asp:PlaceHolder ID="ItemContainer" runat="server" />
            </LayoutTemplate>
            <ItemTemplate>
                <fieldset style="float: left; width: 700px; height: 160px; margin: 2px">
                    <div style="margin: 10px;">
                        <div style="float: left; width: 120px;">
                            <asp:HyperLink ID="hlDetail" runat="server" NavigateUrl='<%# GetProductUrl(Container.DataItem) %>'
                                ToolTip="Click to show detail.">
                                <telerik:RadBinaryImage ID="Image1" runat="server" Width="120px" Height="120px" ResizeMode="Fit"
                                    BorderColor="Gray" BorderWidth="0px" DataValue='<%# Eval("Sketch") %>' />
                            </asp:HyperLink>
                        </div>
                        <div style="float: left; margin-left: 20px; width: 540px;">
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
                        <asp:HyperLink ID="hlQuote" runat="server" Font-Size="1.2em" NavigateUrl='<%# GetQuoteUrl(Container.DataItem) %>' />
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
