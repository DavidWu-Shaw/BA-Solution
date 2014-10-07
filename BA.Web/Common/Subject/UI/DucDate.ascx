<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucDate.ascx.cs" Inherits="BA.Web.Common.Subject.UI.DucDate" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadDatePicker ID="deDate" runat="server" Skin="Office2007">
                <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Office2007">
                </Calendar>
                <DateInput runat="server" DisplayDateFormat="MM/dd/yyyy" DateFormat="MM/dd/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
