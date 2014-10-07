<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartView.ascx.cs" Inherits="BA.Web.Shop.UserControls.CartView" %>
<div style="width: 600px">
    <div>
        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
            <tr>
                <td style="width: 40px">
                    <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Width="30px" Height="30px"
                        ImageUrl="~/Images/CartLarge.png" />
                </td>
                <td align="left">
                    <asp:Label ID="lblTitle" Font-Size="1.4em" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadListView ID="lvItem" runat="server" ItemPlaceholderID="Cont1">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="2" border="1">
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
                                <asp:Label ID="Label2" runat="server" Text='<%# GetHeaderOfAmount() %>'></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder ID="Cont1" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="height: 20px">
                    <td style="width: 240px" class="devTextBold">
                        <asp:Label ID="lblProduct" runat="server" Text='<%# GetItemName(Container.DataItem) %>' />
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
    </div>
    <div>
        <table cellspacing="0" cellpadding="2" border="1" style="width: 100%;">
            <tr class="devSubTitle" style="height: 30px">
                <td style="width: 360px" class="devTextBold">
                    <asp:Label ID="lblTotal" runat="server" Text="Sub Total" />
                </td>
                <td align="right" style="width: 120px">
                    <asp:Label ID="lblQtyTotal" runat="server" />
                </td>
                <td align="right" style="width: 120px">
                    <asp:Label ID="lblTotalAmount" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</div>
