<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucCheckbox.ascx.cs"
    Inherits="BA.Web.Common.Subject.UI.DucCheckbox" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput">
            <asp:CheckBox ID="chkBool" runat="server" Enabled="false" />
        </td>
        <td>
            <asp:Label ID="lblBool" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
