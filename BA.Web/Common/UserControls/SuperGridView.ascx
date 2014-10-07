<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperGridView.ascx.cs"
    Inherits="BA.Web.Common.UserControls.SuperGridView" %>
<table cellspacing="0" cellpadding="0" style="width: 100%; height: 100%">
    <tr>
        <td runat="server" id="tdTitle" style="height: 20px" class="devSubTitle">
            <table cellspacing="0" cellpadding="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/export1.png" ImageAlign="Middle"
                            AlternateText="Export" onclick="btnExport_Click" />
                        <asp:Label ID="lblExport" runat="server" Text="Export" />
                    </td>
                    <td style="width: 5px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 100%; vertical-align: top">
            <telerik:RadGrid ID="gvList" runat="server">
            </telerik:RadGrid>
        </td>
    </tr>
</table>
