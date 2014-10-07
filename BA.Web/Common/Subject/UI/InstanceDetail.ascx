<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstanceDetail.ascx.cs"
    Inherits="BA.Web.Common.Subject.UI.InstanceDetail" %>
<table cellpadding="1" cellspacing="0" style="width: 100%;" class="devContainerTable">
    <tr>
        <td class="DarkTitle">
            <asp:Label ID="lblTitle" runat="server">
            </asp:Label>
        </td>
    </tr>
    <tr>
        <td class="devSubTitle" style="height: 18px">
            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblText" runat="server" CssClass="devTextBold" Width="300" />
                    </td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Width="60" Visible="false" />
                    </td>
                    <td style="width: 100%">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table cellpadding="1" cellspacing="0" border="0" width="100%" runat="server" id="tblInfo"
                class="devContainerTable">
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>
