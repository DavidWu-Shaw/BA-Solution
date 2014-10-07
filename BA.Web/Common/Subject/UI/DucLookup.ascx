<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucLookup.ascx.cs" Inherits="BA.Web.Common.Subject.UI.DucLookup" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadComboBox ID="ddlLookup" MarkFirstMatch="true" runat="server">
            </telerik:RadComboBox>
        </td>
        <td>
            <asp:HyperLink ID="hlLookup" runat="server" Visible="false"></asp:HyperLink>
        </td>
    </tr>
</table>
