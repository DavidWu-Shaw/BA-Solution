<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="SubjectManager.aspx.cs" Inherits="BA.Web.Common.Subject.SubjectManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
    <title>Subject Manager</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%;" class="devContainerTable">
        <tr>
            <td class="DarkTitle">
                <asp:Label ID="lblTitle" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="devSubTitle" style="height: 18px">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblText" runat="server" CssClass="devTextBold" Text="Detail" Width="300" />
                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="60" Visible="false" />
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table cellpadding="1" cellspacing="0" border="0" width="100%" runat="server">
                    <tr style="height: 22px">
                        <td align="left" valign="middle">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblLabel" runat="server" Text="Subject Type" Width="180" CssClass="devTextBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px; white-space: nowrap" align="right">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubjectType" Width="300" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="middle">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Subject Label" Width="180" CssClass="devTextBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px; white-space: nowrap" align="right">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubjectLabel" Width="300" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 22px">
                        <td align="left" valign="middle">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label3" runat="server" Text="SubjectId Field" Width="180" CssClass="devTextBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px; white-space: nowrap" align="right">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubjectIdField" Width="300" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="middle">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label6" runat="server" Text="MasterSubjectId Field" Width="180" CssClass="devTextBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px; white-space: nowrap" align="right">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMasterSubjectIdField" Width="300" runat="server"></asp:Label>
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
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
