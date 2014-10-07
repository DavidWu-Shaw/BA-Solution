<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AppMasterPage.master"
    AutoEventWireup="true" CodeBehind="OrderProcess.aspx.cs" Inherits="BA.Web.WebPages.Order.OrderProcess" %>

<%@ Register Src="../../UserControls/OrderBriefList.ascx" TagName="OrderBriefList"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/OrderProcessor.ascx" TagName="OrderProcessor"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AppMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppMBody" runat="server">
    <div style="width: 1080px; margin-left: auto; margin-right: auto;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="vertical-align: top;">
                    <div style="width: 360px; margin: 10px;">
                        <div>
                            <div style="margin-top: 4px; height: 24px;">
                                <asp:Label ID="lblTitle" runat="server" Text="New coming orders" CssClass="TitleText" />
                            </div>
                            <div style="height: 250px; overflow: auto; border: 1px solid;">
                                <uc1:OrderBriefList ID="oNew" runat="server" />
                            </div>
                        </div>
                        <div style="margin-top: 10px;">
                            <div style="margin-top: 4px; height: 24px;">
                                <asp:Label ID="Label1" runat="server" Text="Orders in process" CssClass="TitleText" />
                            </div>
                            <div style="height: 250px; overflow: auto; border: 1px solid;">
                                <uc1:OrderBriefList ID="oInProc" runat="server" />
                            </div>
                        </div>
                    </div>
                </td>
                <td style="vertical-align: top;">
                    <div style="margin: 10px;">
                        <uc2:OrderProcessor ID="ucOrder" runat="server" Visible="false" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
