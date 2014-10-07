<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderView.ascx.cs" Inherits="BA.Web.UserControls.OrderView" %>
<%@ Register Src="OrderItemsView.ascx" TagName="OrderItemsView" TagPrefix="uc1" %>
<div id="divHeader" runat="server" style="height: 24px">
    <asp:Label ID="lbTitle" runat="server" CssClass="TitleText" />
</div>
<div class="devContainerTable">
    <div class="LightTitle">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr style="height: 24px">
                <td style="width: 100px">
                    <asp:Label ID="lbOrderNo" runat="server"></asp:Label>
                </td>
                <td style="width: 400px">
                    <asp:Label ID="vOrderNo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table cellpadding="2" cellspacing="0">
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbStatus" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vStatus" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lbSupplier" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vSupplier" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbShipToContact" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vShipToContact" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lbShipToPhone" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vShipToPhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbShipToAddress" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vShipToAddress" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lbShipToZipCode" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vShipToZipCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbShipBy" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vShipBy" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lbNotes" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vNotes" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 10px;">
        <uc1:OrderItemsView ID="ucItems" runat="server" />
    </div>
</div>
