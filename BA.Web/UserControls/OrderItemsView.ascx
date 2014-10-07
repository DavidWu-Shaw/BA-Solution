<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderItemsView.ascx.cs"
    Inherits="BA.Web.UserControls.OrderItemsView" %>
<table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
    <tr class="devSubTitle" style="height: 24px">
        <td>
            <asp:Label ID="lblTitle" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadListView ID="lvItems" runat="server" ItemPlaceholderID="ItemsContainer">
                <LayoutTemplate>
                    <table cellspacing="0" cellpadding="2" border="1" style="width: 100%">
                        <thead>
                            <tr style="height: 24px">
                                <th style="width: 240px">
                                    <asp:Label ID="Label9" runat="server" Text='<%# GetHeaderOfItem() %>'></asp:Label>
                                </th>
                                <th style="width: 120px">
                                    <asp:Label ID="Label3" runat="server" Text='<%# GetHeaderOfPrice() %>'></asp:Label>
                                </th>
                                <th style="width: 120px">
                                    <asp:Label ID="Label8" runat="server" Text='<%# GetHeaderOfQuantity() %>'></asp:Label>
                                </th>
                                <th style="width: 120px">
                                    <asp:Label ID="Label13" runat="server" Text='<%# GetHeaderOfAmount() %>'></asp:Label>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style="height: 20px">
                        <td align="left" style="width: 240px" class="devTextBold">
                            <asp:Label ID="lblName" runat="server" Text='<%# GetItemName(Container.DataItem) %>' />
                        </td>
                        <td align="right" style="width: 120px">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("UnitPrice") %>' />
                        </td>
                        <td align="right" style="width: 120px">
                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("QtyOrdered") %>' />
                        </td>
                        <td align="right" style="width: 120px">
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Amount") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </telerik:RadListView>
        </td>
    </tr>
    <tr>
        <td>
            <table cellspacing="0" cellpadding="2" border="1" style="width: 100%">
                <tr class="devSubTitle" style="height: 24px">
                    <td align="left" style="width: 360px">
                        <asp:Label ID="lblSubTotal" runat="server" />
                    </td>
                    <td align="right" style="width: 120px">
                        <asp:Label ID="lblQtyOrderedTotal" runat="server" />
                    </td>
                    <td align="right" style="width: 120px">
                        <asp:Label ID="lblTotalAmount" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
