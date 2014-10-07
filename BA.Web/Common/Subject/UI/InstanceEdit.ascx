<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstanceEdit.ascx.cs"
    Inherits="BA.Web.Common.Subject.UI.InstanceEdit" %>
<table cellpadding="1" cellspacing="0" style="width: 100%;" class="devContainerTable">
    <tr>
        <td class="DarkTitle">
            <asp:Label ID="lblTitle" runat="server">
            </asp:Label>
        </td>
    </tr>
    <tr>
        <td class="devSubTitle">
            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblText" runat="server" CssClass="devTextBold" Width="300"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Width="60" OnClick="btnSave_Click" />
                    </td>
                    <td style="white-space: nowrap; width: 20px;">
                    </td>
                    <td>
                        <asp:Button ID="btnSaveAndAnother" runat="server" Visible="false" Width="100" OnClick="btnSaveAndAnother_Click" />
                    </td>
                    <td style="white-space: nowrap; width: 20px;">
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Width="60" />
                    </td>
                    <td style="width: 100%">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table cellpadding="1" cellspacing="0" border="0" width="100%" runat="server" id="tblEdit"
                class="devContainerTable">
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbResultMsg" runat="server" EnableViewState="false" CssClass="WarningText">
            </asp:Label>
        </td>
    </tr>
</table>
