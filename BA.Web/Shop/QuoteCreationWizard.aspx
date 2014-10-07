<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="QuoteCreationWizard.aspx.cs" Inherits="BA.Web.Shop.QuoteCreationWizard" %>

<%@ Register Src="../UserControls/QuoteView.ascx" TagName="QuoteView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
    <link href="../Styles/WizardHeader.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="width: 660px; margin-left: auto; margin-right: auto">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
            <tr style="height: 40px">
                <td align="left">
                    <asp:Label ID="lblTitle" Font-Size="Medium" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="false" OnPreRender="Wizard1_PreRender"
            OnFinishButtonClick="Wizard1_FinishButtonClick" OnNextButtonClick="Wizard1_NextButtonClick"
            OnPreviousButtonClick="Wizard1_PreviousButtonClick">
            <HeaderTemplate>
                <div style="margin: 0px 0px 10px 0px">
                    <span style="font-weight: bold">
                        <%= GetHeaderText() %>
                    </span>
                </div>
                <div style="margin: 0px 0px 10px 0px">
                    <ul id="wizHeader">
                        <asp:Repeater ID="rptSteps" runat="server">
                            <ItemTemplate>
                                <li><a class="<%# GetItemStyle(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                    <%# Eval("Name")%></a> </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </HeaderTemplate>
            <StartNavigationTemplate>
                <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" CssClass="clsButton" />
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" CssClass="clsButton" />
                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" CssClass="clsButton" />
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <asp:Button ID="FinishPreviousButton" runat="server" CommandName="MovePrevious" CssClass="clsButton" />
                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" CssClass="clsButton"
                    Width="160px" />
            </FinishNavigationTemplate>
            <WizardSteps>
                <asp:WizardStep runat="server" StepType="Start" Title="Contact Information" ID="s0">
                    <div style="margin: 0px 0px 10px 0px">
                        <table cellpadding="2" cellspacing="0" border="0" class="devContainerTable">
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblEmail" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td style="width: 20px; white-space: nowrap" align="right">
                                    <asp:Label ID="lm1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtEmail" runat="server" Width="300" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                    <asp:RequiredFieldValidator ID="valid1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="* Required." ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regValid" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="* Invalid Email format." ForeColor="Red" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblContact" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td style="width: 20px; white-space: nowrap" align="right">
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtContact" runat="server" Width="300" MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblPhone" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td style="width: 20px; white-space: nowrap" align="right">
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtPhone" runat="server" Width="300" MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblAddress" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td style="width: 20px; white-space: nowrap" align="right">
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtAddress" runat="server" MaxLength="100" TextMode="MultiLine"
                                        Wrap="true" Rows="2" Width="300">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblPostCode" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td style="width: 20px; white-space: nowrap" align="right">
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtZipCode" runat="server" Width="300" MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblNotes" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td style="width: 20px; white-space: nowrap" align="right">
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNotes" runat="server" MaxLength="500" TextMode="MultiLine"
                                        Wrap="true" Rows="2" Width="300">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep runat="server" StepType="Finish" Title="Confirm and Submit" ID="s2">
                    <div style="margin: 0px 0px 10px 0px">
                        <uc1:QuoteView ID="ucQuote" runat="server" />
                    </div>
                </asp:WizardStep>
                <asp:WizardStep runat="server" StepType="Complete" Title="Complete" ID="s3">
                    <div style="margin: 0px 0px 10px 0px">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr style="height: 60px">
                                <td>
                                    <asp:Label ID="lblComplete" Font-Size="Medium" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:QuoteView ID="ucSavedQuote" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</asp:Content>
