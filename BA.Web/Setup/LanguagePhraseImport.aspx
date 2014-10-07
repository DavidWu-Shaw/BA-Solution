<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="LanguagePhraseImport.aspx.cs" Inherits="BA.Web.Setup.LanguagePhraseImport" %>

<%@ Register Src="../Common/UserControls/ExcelImport.ascx" TagName="ExcelImport"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
    <link href="../Styles/WizardHeader.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%;" class="clsContainerTable">
        <tr>
            <td class="DarkTitle">
                <asp:Label ID="lblTitle" runat="server">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="width: 150px">
                            <asp:Label ID="lblLang" runat="server" AssociatedControlID="ddlLanguage" CssClass="devTextBold"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlLanguage" MarkFirstMatch="true" runat="server" Width="200px">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:RadioButton ID="rbData" runat="server" Checked="true" GroupName="PhraseType" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbSystem" runat="server" GroupName="PhraseType" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="vertical-align: top;">
                <uc1:ExcelImport ID="ucExcelImport" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
