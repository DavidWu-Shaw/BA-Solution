<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostList.ascx.cs" Inherits="BA.Web.Social.UserControls.PostList" %>
<div style="margin: 10px;">
    <asp:Repeater ID="rptItems" runat="server">
        <HeaderTemplate>
            <hr />
        </HeaderTemplate>
        <ItemTemplate>
            <table style="width: 100%;" cellpadding="2" cellspacing="0">
                <tr style="height: 24px">
                    <td style="width: 100">
                        <asp:Label ID="hlItem" runat="server" Text='<%# Eval("IssuedBy") %>' CssClass="devTextBold">
                        </asp:Label>
                    </td>
                    <td style="width: 700px">
                        <asp:Label ID="Label1" runat="server" Text='<%# GetPostedTime(Container.DataItem) %>'></asp:Label>
                    </td>
                    <td style="width: 120px">
                    </td>
                </tr>
                <tr style="height: 24px">
                    <td style="width: 100">
                    </td>
                    <td style="width: 700px">
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Content") %>' CssClass="ContentText"></asp:Label>
                    </td>
                    <td style="width: 120px">
                    </td>
                </tr>
                <tr style="height: 24px">
                    <td style="width: 100">
                    </td>
                    <td style="width: 700px">
                    </td>
                    <td align="center" style="width: 120px">
                        <asp:Button ID="btnReply" runat="server" Text='<%# GetTextOfReply() %>' Width="60px"
                            Enabled='<%# GetReplyEnable(Container.DataItem) %>' Visible='<%# GetReplyVisibility(Container.DataItem) %>'
                            PostBackUrl='<%# GetPostBackUrl(Container.DataItem) %>' CssClass="clsButton" />
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
