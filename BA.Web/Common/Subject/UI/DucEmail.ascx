<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DucEmail.ascx.cs" Inherits="BA.Web.Common.Subject.UI.DucEmail" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="lblLabel" runat="server" CssClass="devTextBold"></asp:Label>
        </td>
        <td style="width: 20px; white-space: nowrap" align="right">
            <asp:Label ID="lblMark" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        </td>
        <td runat="server" id="tdInput" visible="false">
            <telerik:RadTextBox ID="txtEmail" runat="server">
            </telerik:RadTextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Invalid Email format." ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$">
            </asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:Label ID="lblEmail" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
