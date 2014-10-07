<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucAttachment.ascx.cs"
    Inherits="BA.Web.Common.Subject.UI.DucAttachment" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadUpload ID="uploadAttachment" runat="server" MaxFileSize="1000000" ControlObjectsVisibility="None">
            </telerik:RadUpload>
        </td>
        <td>
            <asp:HyperLink ID="hlAttachment" runat="server" Target="_blank" Visible="false"></asp:HyperLink>
        </td>
    </tr>
</table>
