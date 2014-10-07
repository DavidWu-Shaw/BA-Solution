<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderSearch.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.OrderSearch" %>
<%@ Register Src="../../UserControls/OrderBriefList.ascx" TagName="OrderBriefList"
    TagPrefix="uc1" %>
<div style="width: 1000px;">
    <div style="float: left; width: 320px;">
        <table cellspacing="0" cellpadding="0" border="0">
            <tr style="height: 30px">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="TitleText" />
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 120px;">
                    <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtRest" runat="server" Width="200px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 120px;">
                    <asp:Label ID="lblSupplier" runat="server"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="ddlRest" MarkFirstMatch="true" runat="server" Width="200px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 120px;">
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="ddlStatus" MarkFirstMatch="true" runat="server" Width="200px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 120px;">
                    <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="200px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr style="height: 40px">
                <td colspan="2" align="right">
                    <asp:Button ID="btnSearch" runat="server" CssClass="clsButton" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div style="float: left; margin-left: 20px; width: 640px;">
        <div style="height: 20px;">
            <asp:Label ID="lblList" runat="server" CssClass="TitleText" />
        </div>
        <div style="height: 250px; overflow: auto; border: 1px solid;">
            <uc1:OrderBriefList ID="ucOrderList" runat="server" />
        </div>
    </div>
</div>
