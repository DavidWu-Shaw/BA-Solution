<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierSearchCriteria.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.SupplierSearchCriteria" %>
<table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
    <tr style="height: 20px">
        <td align="left" style="width: 200px;">
            <asp:Label ID="lblName" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
    </tr>
    <tr style="height: 22px">
        <td>
            <telerik:RadTextBox ID="txtName" runat="server" Width="200px">
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr style="height: 20px">
        <td align="left" style="width: 200px;">
            <asp:Label ID="lblAddr" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
    </tr>
    <tr style="height: 22px">
        <td>
            <telerik:RadTextBox ID="txtAddress" runat="server" Width="200px">
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr style="height: 20px">
        <td align="left" style="width: 200px;">
            <asp:Label ID="lblPostCode" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
    </tr>
    <tr style="height: 22px">
        <td>
            <telerik:RadTextBox ID="txtPostalCode" runat="server" Width="200px">
            </telerik:RadTextBox>
        </td>
    </tr>
</table>
