<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="ContactUsPage.aspx.cs" Inherits="BA.Web.Shop.ContactUsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="width: 660px; margin-left: auto; margin-right: auto">
        <table cellpadding="2" cellspacing="0" style="width: 100%;" class="devContainerTable">
            <tr class="devSubTitle" style="height: 24px">
                <td>
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
                            <td align="right" style="width: 120px">
                                <asp:Label ID="lblEmail" runat="server" CssClass="devTextBold"></asp:Label>
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                                <asp:Label ID="lm1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtEmail" runat="server" Width="400px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="v1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required."
                                    CssClass="WarningText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Invalid Email format." CssClass="WarningText" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 120px">
                                <asp:Label ID="lblName" runat="server" CssClass="devTextBold"></asp:Label>
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtName" runat="server" Width="400px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 120px">
                                <asp:Label ID="lblPhone" runat="server" CssClass="devTextBold"></asp:Label>
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtPhone" runat="server" Width="400px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 120px; vertical-align: top">
                                <asp:Label ID="lblAddress" runat="server" CssClass="devTextBold"></asp:Label>
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtAddress" runat="server" Width="400px" TextMode="MultiLine"
                                    Wrap="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 120px">
                                <asp:Label ID="lblSubject" runat="server" CssClass="devTextBold"></asp:Label>
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                                <asp:Label ID="lm3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtSubject" runat="server" Width="400px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="v2" runat="server" ControlToValidate="txtSubject"
                                    ErrorMessage="Subject is required." CssClass="WarningText"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 5px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 120px; vertical-align: top">
                                <asp:Label ID="lblMessage" runat="server" CssClass="devTextBold"></asp:Label>
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtMessage" runat="server" Width="400px" TextMode="MultiLine"
                                    Wrap="true" Rows="10">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 120px">
                            </td>
                            <td style="width: 20px; white-space: nowrap" align="right">
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="clsButton" OnClick="btnSubmit_Click">
                                </asp:Button>
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
    </div>
</asp:Content>
