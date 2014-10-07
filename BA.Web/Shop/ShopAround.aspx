<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="ShopAround.aspx.cs" Inherits="BA.Web.Shop.ShopAround" %>

<%@ Register Src="UserControls/SupplierSearchCriteria.ascx" TagName="SupplierSearchCriteria"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/ReviewList.ascx" TagName="ReviewList" TagPrefix="uc2" %>
<%@ Register Src="UserControls/ProductList.ascx" TagName="ProductList" TagPrefix="uc3" %>
<%@ Register Src="UserControls/SupplierProfile.ascx" TagName="SupplierProfile" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <telerik:RadAjaxLoadingPanel ID="alp1" runat="server" />
    <div>
        <div style="width: 240px; float: left;">
            <div style="margin: 10px;">
                <telerik:RadListBox ID="lbSup" runat="server" Width="100%">
                    <HeaderTemplate>
                        <div class="LightTitle" style="height: 20px">
                            <asp:Label ID="Label5" Text="Supplier List" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="devTextBold">
                                    <%#Eval("Name")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <%#Eval("Address")%>
                                </td>
                            </tr>
                            <tr style="height: 5px">
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadListBox>
            </div>
            <div class="LightTitle" style="height: 18px; margin-top: 10px;">
                <asp:Label ID="Label1" Text="Refine List" runat="server" />
            </div>
            <div style="margin: 0px 10px 10px 10px;">
                <uc1:SupplierSearchCriteria ID="ucSupplierCriteria" runat="server" />
            </div>
            <div style="margin: 10px;">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="clsButton" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div style="width: 740px; min-width: 400px; float: left;">
            <div style="margin: 15px 10px 10px 10px;">
                <uc5:SupplierProfile ID="ucProfile" runat="server" />
            </div>
            <div style="margin: 10px;">
                <telerik:RadTabStrip ID="rTab" runat="server" Skin="Office2007" MultiPageID="rmp"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Products">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Reviews">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmp" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="rpv1" runat="server">
                        <uc3:ProductList ID="ucProductList" runat="server" />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpv2" runat="server">
                        <uc2:ReviewList ID="ucReviewList" runat="server" />
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
