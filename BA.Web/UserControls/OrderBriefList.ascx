<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderBriefList.ascx.cs"
    Inherits="BA.Web.UserControls.OrderBriefList" %>
<div>
    <div id="divHeader" runat="server" style="height: 24px">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleText" />
    </div>
    <telerik:RadListBox ID="lbOrders" runat="server" Width="100%" OnSelectedIndexChanged="lbOrders_SelectedIndexChanged"
        AutoPostBack="True" EmptyMessage="No data.">
        <HeaderTemplate>
            <table class="LightTitle" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                <tr style="height: 24px">
                    <th style="width: 220px">
                        <asp:Label ID="lblNo" runat="server" Text='<%# GetHeaderOfOrderNumber() %>'></asp:Label>
                    </th>
                    <th style="width: 70px">
                        <asp:Label ID="lblStatus" runat="server" Text='<%# GetHeaderOfStatus() %>'></asp:Label>
                    </th>
                    <th style="width: 50px">
                        <asp:Label ID="lblAmount" runat="server" Text='<%# GetHeaderOfAmount() %>'></asp:Label>
                    </th>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                <tr style="height: 24px">
                    <td align="left" style="width: 220px" class="devTextBold">
                        <asp:Label ID="lblOrder" runat="server" Text='<%# Eval("OrderNumber") %>' />
                    </td>
                    <td align="center" style="width: 70px">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("StatusText") %>' />
                    </td>
                    <td align="right" style="width: 50px">
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Amount") %>' />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </telerik:RadListBox>
</div>
