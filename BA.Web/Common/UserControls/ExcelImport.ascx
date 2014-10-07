<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExcelImport.ascx.cs"
    Inherits="BA.Web.Common.UserControls.ExcelImport" %>
<%@ Register Src="SuperGridView.ascx" TagName="SuperGridView" TagPrefix="uc1" %>
<script type="text/javascript">
    function RowSelected(sender, args) {
    }
</script>
<asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="false" OnPreRender="Wizard1_PreRender"
    OnFinishButtonClick="Wizard1_FinishButtonClick" OnNextButtonClick="Wizard1_NextButtonClick"
    OnPreviousButtonClick="Wizard1_PreviousButtonClick">
    <HeaderTemplate>
        <div style="margin: 4px">
            <span style="font-weight: bold">
                <%= GetHeaderText() %>
            </span>
        </div>
        <div style="margin: 4px 4px 10px 4px">
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
    <NavigationStyle HorizontalAlign="Left" Height="40px" />
    <StartNavigationTemplate>
        <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next"
            CssClass="clsButton" />
    </StartNavigationTemplate>
    <StepNavigationTemplate>
        <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" Text="Previous"
            CssClass="clsButton" />
        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Next"
            CssClass="clsButton" />
    </StepNavigationTemplate>
    <FinishNavigationTemplate>
        <asp:Button ID="FinishPreviousButton" runat="server" CommandName="MovePrevious" Text="Previous"
            CssClass="clsButton" />
        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="Submit"
            CssClass="clsButton" />
    </FinishNavigationTemplate>
    <WizardSteps>
        <asp:WizardStep ID="s0" runat="server" StepType="Start" Title="Upload File">
            <div>
                <asp:Label ID="lblSelectFile" runat="server" CssClass="clsTextBold" Text="Select the excel file: "></asp:Label>
            </div>
            <div>
                <asp:FileUpload ID="excelFileUpload" runat="server" Width="300px" />
            </div>
        </asp:WizardStep>
        <asp:WizardStep ID="s1" runat="server" StepType="Step" Title="Pickup Sheet">
            <div>
                <asp:Label ID="lblSelectSheet" runat="server" CssClass="clsTextBold" Text="Select the excel sheet: "></asp:Label>
            </div>
            <div>
                <asp:DropDownList ID="ddlSheetName" runat="server" Width="300px">
                </asp:DropDownList>
            </div>
        </asp:WizardStep>
        <asp:WizardStep ID="s2" runat="server" StepType="Step" Title="Set Header Row">
            <div>
                <telerik:RadGrid ID="gvRaw" runat="server" Skin="Office2007" AllowPaging="false">
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowSelected="RowSelected" />
                        <Scrolling AllowScroll="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </asp:WizardStep>
        <asp:WizardStep ID="s3" runat="server" StepType="Step" Title="Map Fields">
            <div>
                <telerik:RadGrid ID="gvSample" runat="server" Skin="Office2007" AllowPaging="false">
                </telerik:RadGrid>
            </div>
            <div>
                <asp:Repeater ID="rptItems" runat="server" OnItemCreated="rptItems_ItemCreated">
                    <HeaderTemplate>
                        <hr />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="2">
                            <tr>
                                <td style="width: 200px">
                                    <asp:Label ID="hlItem" runat="server" Text='<%# Eval("RequestFieldDisplay") %>'>
                                    </asp:Label>
                                </td>
                                <td style="width: 240px">
                                    <asp:DropDownList ID="ddlColumns" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        <hr />
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:WizardStep>
        <asp:WizardStep ID="s4" runat="server" StepType="Finish" Title="Preview Data">
            <div>
                <uc1:SuperGridView ID="gvPreview" runat="server" AllowScroll="true" />
            </div>
        </asp:WizardStep>
        <asp:WizardStep ID="s5" runat="server" StepType="Complete" Title="Complete">
            <div>
                <asp:Label ID="lblComplete" runat="server" CssClass="devTextBold">
                </asp:Label>
            </div>
        </asp:WizardStep>
    </WizardSteps>
</asp:Wizard>
