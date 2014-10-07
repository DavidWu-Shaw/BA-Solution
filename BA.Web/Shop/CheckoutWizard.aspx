<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="CheckoutWizard.aspx.cs" Inherits="BA.Web.Shop.CheckoutWizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
    <link href="../Styles/WizardHeader.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="width: 660px; margin-left: auto; margin-right: auto">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
            <tr style="height: 40px">
                <td style="width: 40px">
                    <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Width="30px" Height="30px"
                        ImageUrl="~/Images/CartLarge.png" />
                </td>
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
                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" CssClass="clsButton" />
            </FinishNavigationTemplate>
            <WizardSteps>
                <asp:WizardStep runat="server" StepType="Start" Title="Shipping Information" ID="s0">
                    <div style="margin: 0px 0px 10px 0px">
                        <table cellpadding="2" cellspacing="0" border="0" class="devContainerTable">
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblContact" runat="server" CssClass="devTextBold"></asp:Label>
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
                                <td>
                                    <telerik:RadTextBox ID="txtPhone" runat="server" Width="300" MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                    <asp:RequiredFieldValidator ID="valid1" runat="server" ControlToValidate="txtPhone"
                                        ErrorMessage="* Required field." ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblAddress" runat="server" CssClass="devTextBold"></asp:Label>
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
                                <td>
                                    <telerik:RadTextBox ID="txtZipCode" runat="server" Width="300" MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblShipBy" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker ID="deShipByTime" runat="server" Skin="Office2007" Width="300">
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblNotes" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNotes" runat="server" MaxLength="500" TextMode="MultiLine"
                                        Wrap="true" Rows="2" Width="300">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 120px">
                                    <asp:Label ID="lblEmail" runat="server" CssClass="devTextBold"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtEmail" runat="server" Width="300" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 120px; white-space: nowrap">
                                    <asp:RegularExpressionValidator ID="regValid" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="* Invalid Email format." ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep runat="server" StepType="Step" Title="Preview" ID="s1">
                    <div style="margin: 0px 0px 10px 0px">
                        <asp:PlaceHolder ID="phPreview" runat="server"></asp:PlaceHolder>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep runat="server" StepType="Finish" Title="Payment and Submit" ID="s2">
                    <div style="margin: 0px 0px 10px 0px">
                        <asp:Label ID="lblPayment" Font-Size="Medium" runat="server" />
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
                            <tr style="height: 20px;">
                                <td>
                                    <asp:Label ID="lblHint" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="phConfirm" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</asp:Content>
