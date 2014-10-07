<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="EntityEdit.aspx.cs" Inherits="BA.Web.Common.Subject.EntityEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
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
                            <asp:Label ID="lblText" runat="server" CssClass="devTextBold" Text="Edit" Width="300"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="60" OnClick="btnSave_Click" />
                        </td>
                        <td style="white-space: nowrap; width: 20px;">
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Close" Width="60" />
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table cellpadding="1" cellspacing="0" border="0" width="100%">
                    <tr style="height: 22px">
                        <td align="left" valign="middle">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblLabel" runat="server" Text="Entity Code" Width="180" CssClass="devTextBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px; white-space: nowrap" align="right">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="middle">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Description" Width="180" CssClass="devTextBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px; white-space: nowrap" align="right">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbResultMsg" runat="server" EnableViewState="false" BackColor="Red">
                </asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
