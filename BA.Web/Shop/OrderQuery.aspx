<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="OrderQuery.aspx.cs" Inherits="BA.Web.Shop.OrderQuery" %>

<%@ Register Src="../UserControls/OrderView.ascx" TagName="OrderView" TagPrefix="uc2" %>
<%@ Register Src="UserControls/OrderSearch.ascx" TagName="OrderSearch" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div>
        <div style="margin-top: 4px;">
            <uc1:OrderSearch ID="ucSearch" runat="server" />
        </div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="vertical-align: top;">
                </td>
            </tr>
            <tr style="height: 10px;">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:OrderView ID="ucOrder" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
