<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="ContactImport.aspx.cs" Inherits="BA.Web.WebPages.Contact.ContactImport" %>

<%@ Register Src="../../Common/UserControls/ExcelImport.ascx" TagName="ExcelImport"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
    <link href="../../Styles/WizardHeader.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <table cellpadding="1" cellspacing="0" style="width: 100%;" class="clsContainerTable">
        <tr>
            <td class="DarkTitle">
                <asp:Label ID="lblTitle" runat="server" Text="Import Contacts">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="vertical-align: top;">
                <table cellpadding="1" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <uc1:ExcelImport ID="ucExcelImport" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
