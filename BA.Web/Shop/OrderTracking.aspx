<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="OrderTracking.aspx.cs" Inherits="BA.Web.Shop.OrderTracking" %>

<%@ Register Src="../UserControls/OrderView.ascx" TagName="OrderView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="width: 660px; margin-left: auto; margin-right: auto">
        <div style="margin: 10px">
            <telerik:RadTextBox ID="txtNo" runat="server" Width="300" MaxLength="50">
            </telerik:RadTextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="clsButton" OnClick="btnSearch_Click" />
        </div>
        <div>
            <asp:RequiredFieldValidator ID="valid1" runat="server" ControlToValidate="txtNo"
                ErrorMessage="* Please input order number." ForeColor="Red">
            </asp:RequiredFieldValidator>
        </div>
        <div style="margin: 10px">
            <uc1:OrderView ID="ucOrder" runat="server" Visible="false" />
            <asp:Label ID="lblMsg" runat="server" Text="Order not found. Please verify the order number."
                CssClass="devTextBold" Visible="false" ForeColor="Red">
            </asp:Label>
        </div>
    </div>
</asp:Content>
