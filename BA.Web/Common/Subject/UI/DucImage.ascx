<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucImage.ascx.cs" Inherits="BA.Web.Common.Subject.UI.DucImage" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadUpload ID="uploadImage" runat="server" ControlObjectsVisibility="None"
                AllowedFileExtensions=".jpg, .jpeg" MaxFileSize="10240000">
            </telerik:RadUpload>
        </td>
        <td>
            <telerik:RadBinaryImage runat="server" ID="radImage" AutoAdjustImageControlSize="false"
                Width="90px" Height="110px" Visible="false" />
        </td>
        <td>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
