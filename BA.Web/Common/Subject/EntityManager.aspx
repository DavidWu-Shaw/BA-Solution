<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="EntityManager.aspx.cs" Inherits="BA.Web.Common.Subject.EntityManager" %>

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
            <td class="devSubTitle" style="height: 18px">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblText" runat="server" CssClass="devTextBold" Text="Detail" Width="300" />
                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="60" />
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table cellpadding="1" cellspacing="0" border="0" style="width: 100%;">
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
                                        <asp:Label ID="lblCode" Width="300" runat="server"></asp:Label>
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
                                        <asp:Label ID="lblDescription" Width="300" runat="server"></asp:Label>
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
