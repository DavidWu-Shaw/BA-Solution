<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuoteView.ascx.cs" Inherits="BA.Web.UserControls.QuoteView" %>
<div id="divHeader" runat="server" style="height: 24px">
    <asp:Label ID="lbTitle" runat="server" CssClass="TitleText" />
</div>
<div>
    <div class="devContainerTable">
        <table cellpadding="2" cellspacing="0">
            <tr class="devSubTitle" style="height: 24px">
                <td colspan="5">
                    <asp:Label ID="lblSubTitle" runat="server" />
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lblQuoteNo" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vQuoteNo" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lblStatus" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lblTimeCreated" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vTimeCreated" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lblQuoteType" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vQuoteType" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 10px;" class="devContainerTable">
        <table cellpadding="2" cellspacing="0">
            <tr class="devSubTitle" style="height: 24px">
                <td colspan="5">
                    <asp:Label ID="lblSubTitle1" runat="server" />
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbEmail" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vEmail" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lbContact" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vContact" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbPhone" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vPhone" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lbZipCode" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vZipCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lbAddress" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vAddress" runat="server"></asp:Label>
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
    <div style="margin-top: 10px;" class="devContainerTable">
        <table cellpadding="2" cellspacing="0">
            <tr class="devSubTitle" style="height: 24px">
                <td colspan="5">
                    <asp:Label ID="lblSubTitle2" runat="server" />
                </td>
            </tr>
            <tr style="height: 24px">
                <td style="width: 120px">
                    <asp:Label ID="lblProdName" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vProdName" runat="server"></asp:Label>
                </td>
                <td style="width: 10px; white-space: nowrap">
                </td>
                <td style="width: 120px">
                    <asp:Label ID="lblAmount" runat="server" CssClass="devTextBold"></asp:Label>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="vAmount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</div>
