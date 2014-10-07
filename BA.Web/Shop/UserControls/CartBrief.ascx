<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartBrief.ascx.cs" Inherits="BA.Web.Shop.UserControls.CartBrief" %>
<div>
    <div>
        <table cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td style="width: 30px">
                    <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Width="30px" Height="30px"
                        ImageUrl="~/Images/CartLarge.png" />
                </td>
                <td align="left">
                    <asp:Label ID="lblTitle" Font-Size="Medium" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadListView ID="lvItem" runat="server" ItemPlaceholderID="Cont1">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="2" border="1">
                    <thead>
                        <tr style="height: 20px">
                            <th style="width: 160px">
                                <asp:Label ID="Label4" runat="server" Text='<%# GetHeaderOfItem() %>'></asp:Label>
                            </th>
                            <th style="width: 40px">
                                <asp:Label ID="Label2" runat="server" Text='<%# GetHeaderOfPrice() %>'></asp:Label>
                            </th>
                            <th style="width: 40px">
                                <asp:Label ID="Label5" runat="server" Text='<%# GetHeaderOfQuantity() %>'></asp:Label>
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
                    <td style="width: 160px" class="devTextBold">
                        <asp:Label ID="lblProduct" runat="server" Text='<%# GetItemName(Container.DataItem) %>' />
                    </td>
                    <td align="right" style="width: 40px">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("UnitPrice") %>' />
                    </td>
                    <td align="right" style="width: 40px">
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("QtyOrdered") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </telerik:RadListView>
    </div>
</div>
