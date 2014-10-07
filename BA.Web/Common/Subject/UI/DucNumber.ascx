<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucNumber.ascx.cs" Inherits="BA.Web.Common.Subject.UI.DucNumber" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadNumericTextBox ID="txtNumber" runat="server" Type="Number" />
        </td>
        <td>
            <asp:Label ID="lblNumber" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
