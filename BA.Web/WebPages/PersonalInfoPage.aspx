<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="PersonalInfoPage.aspx.cs" Inherits="BA.Web.WebPages.PersonalInfoPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%;" class="devContainerTable">
        <tr>
            <td class="DarkTitle">
                <asp:Label ID="lblTitle" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table cellspacing="2" cellpadding="0" border="0">
                    <tr style="height: 10px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                            <asp:Label ID="lblEmail" runat="server" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="240px" Enabled="false">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                            <asp:Label ID="lblUsername" runat="server" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtUsername" runat="server" Width="240px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                            <asp:Label ID="lblLanguage" runat="server" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlLanguage" runat="server" Width="240px" MarkFirstMatch="true">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                            <asp:Label ID="lblDomain" runat="server" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlDomain" runat="server" Width="240px" Enabled="false">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                            <asp:Label ID="lblMatch" runat="server" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtMatch" runat="server" Width="240px" Enabled="false">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                            <asp:Label ID="lblLastTime" runat="server" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLastTime" runat="server" Width="240px" Enabled="false">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 140px">
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSave" runat="server" Text="Save changes" CssClass="clsButton"
                                Width="110" OnClick="btnSave_Click"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel changes" CssClass="clsButton"
                                Width="110" OnClick="btnCancel_Click"></asp:Button>
                        </td>
                    </tr>
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
</asp:Content>
