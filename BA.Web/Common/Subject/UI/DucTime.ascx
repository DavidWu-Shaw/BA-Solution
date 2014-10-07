<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucTime.ascx.cs" Inherits="BA.Web.Common.Subject.UI.DucTime" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadTimePicker ID="deTime" runat="server" Skin="Office2007">
            </telerik:RadTimePicker>
        </td>
        <td>
            <asp:Label ID="lblTime" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
