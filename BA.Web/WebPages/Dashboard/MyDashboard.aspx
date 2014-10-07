<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AppMasterPage.master" AutoEventWireup="true" CodeBehind="MyDashboard.aspx.cs" Inherits="BA.Web.WebPages.Dashboard.MyDashboard" %>
<%@ Register src="../../Common/UserControls/SchedulerControl.ascx" tagname="SchedulerControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AppMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppMBody" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="devContainerTable">
        <tr>
            <td runat="server" id="tdTitle" class="DarkTitle">
                <asp:Label ID="lbTitle" runat="server" Text="My Dashboard"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:SchedulerControl ID="ucSchedulerControl" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
