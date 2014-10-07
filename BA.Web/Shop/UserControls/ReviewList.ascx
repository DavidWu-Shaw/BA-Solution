<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewList.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.ReviewList" %>
<div>
    <div style="height: 20px; margin: 10px;">
        <asp:Label ID="lblTitle" runat="server" CssClass="SubTitleText" />
    </div>
    <div style="margin: 10px;">
        <asp:Repeater ID="rptItems" runat="server">
            <HeaderTemplate>
                <hr />
            </HeaderTemplate>
            <ItemTemplate>
                <table style="width: 100%;" cellpadding="2" cellspacing="0">
                    <tr style="height: 24px">
                        <td style="width: 100px">
                            <asp:Label ID="hlItem" runat="server" Text='<%# Eval("IssuedBy") %>' CssClass="devTextBold">
                            </asp:Label>
                        </td>
                        <td style="width: 700px">
                            <asp:Label ID="Label1" runat="server" Text='<%# GetPostedTime(Container.DataItem) %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <telerik:RadRating ID="Rating1" runat="server" ItemCount="5" Value='<%# Eval("Rating") %>'
                                ReadOnly="true" Orientation="Horizontal" />
                        </td>
                    </tr>
                    <tr style="height: 24px">
                        <td style="width: 100px">
                        </td>
                        <td style="width: 700px">
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Content") %>'></asp:Label>
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
            <FooterTemplate>
                <hr />
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div runat="server" id="divAdd" style="margin: 30px 10px 10px 10px;">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 120px">
                    <asp:Label ID="lblTip2" runat="server" CssClass="devTextBold" />
                </td>
                <td>
                    <telerik:RadRating ID="Rating1" runat="server" ItemCount="5" Value="4" SelectionMode="Continuous"
                        Precision="Half" Orientation="Horizontal" />
                </td>
            </tr>
            <tr style="height: 10px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; vertical-align: top">
                    <asp:Label ID="lblTip1" runat="server" CssClass="devTextBold" />
                </td>
                <td>
                    <telerik:RadEditor ID="edContent" runat="server" Width="100%" Height="200px">
                    </telerik:RadEditor>
                </td>
            </tr>
            <tr style="height: 10px">
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="clsButton" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
</div>
