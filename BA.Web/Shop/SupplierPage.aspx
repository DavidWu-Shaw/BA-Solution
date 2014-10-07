<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="SupplierPage.aspx.cs" Inherits="BA.Web.Shop.SupplierPage" %>

<%@ Register Src="UserControls/SupplierProfile.ascx" TagName="SupplierProfile" TagPrefix="uc1" %>
<%@ Register Src="UserControls/ReviewList.ascx" TagName="ReviewList" TagPrefix="uc2" %>
<%@ Register Src="UserControls/CartBrief.ascx" TagName="CartBrief" TagPrefix="uc4" %>
<%@ Register Src="UserControls/SupProductExplorer.ascx" TagName="SupProductExplorer"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <telerik:RadAjaxManager ID="amProd" runat="server">
    </telerik:RadAjaxManager>
    <div style="margin-top: 10px;">
        <div style="margin: 10px;">
            <div style="float: left; width: 700px; margin-top: 5px;">
                <uc1:SupplierProfile ID="ucProfile" runat="server" />
            </div>
            <div style="float: left">
                <uc4:CartBrief ID="ucCartBrief" runat="server" Visible="false" />
            </div>
        </div>
        <div class="clear">
        </div>
        <div style="margin: 10px 0px 10px 0px;">
            <telerik:RadTabStrip ID="rTab" runat="server" Skin="Office2007" MultiPageID="rmp"
                SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Value="tabProduct">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Value="tabReview">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="rmp" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="rpv1" runat="server">
                    <uc3:SupProductExplorer ID="ucProductExplorer" runat="server" />
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpv2" runat="server">
                    <uc2:ReviewList ID="ucReviewList" runat="server" />
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>
