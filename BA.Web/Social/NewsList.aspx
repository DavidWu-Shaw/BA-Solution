<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="BA.Web.Social.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="margin-top: 20px;">
        <div style="margin: 10px">
            <asp:Repeater ID="rptNews" runat="server">
                <HeaderTemplate>
                    <hr />
                </HeaderTemplate>
                <ItemTemplate>
                    <table style="width: 100%;" cellpadding="2" cellspacing="0">
                        <tr style="height: 24px">
                            <td style="width: 700px">
                                <asp:Label ID="lblNews" runat="server" Text='<%# Eval("Title") %>' CssClass="TitleText">
                                </asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label1" runat="server" Text='<%# GetPostedTime(Container.DataItem) %>'></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 24px">
                            <td colspan="2">
                                <asp:Label ID="Label3" runat="server" Text='<%# GetContent(Container.DataItem) %>'
                                    CssClass="ContentText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:HyperLink ID="hlMore" runat="server" Text='<%# GetTextOfLink() %>' Visible='<%# GetButtonVisibility(Container.DataItem) %>'
                                    NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>' Target="_blank">
                                </asp:HyperLink>
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
    </div>
</asp:Content>
