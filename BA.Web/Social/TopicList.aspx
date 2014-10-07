<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="BA.Web.Social.TopicList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="margin-top: 20px;">
        <div style="margin: 10px;">
            <telerik:RadTextBox ID="txtKey" runat="server" Width="500" MaxLength="200">
            </telerik:RadTextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="clsButton" OnClick="btnSearch_Click" />
        </div>
        <div style="margin: 10px;">
            <asp:Button ID="btnNew" runat="server" CssClass="clsButton" />
        </div>
        <div style="margin: 10px;">
            <asp:Repeater ID="rptItems" runat="server">
                <HeaderTemplate>
                    <table cellspacing="0" cellpadding="2" border="0" style="width: 100%">
                        <tr style="height: 24px">
                            <th style="width: 600px">
                            </th>
                            <th style="width: 80px">
                                <asp:Label ID="lblPost" runat="server" Text='<%# GetHeaderOfPost() %>'></asp:Label>
                            </th>
                            <th style="width: 80px">
                                <asp:Label ID="lblVisit" runat="server" Text='<%# GetHeaderOfVisit() %>'></asp:Label>
                            </th>
                            <th style="width: 160px">
                                <asp:Label ID="lblLastPost" runat="server" Text='<%# GetHeaderOfLastPost() %>'></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table style="width: 100%;" cellpadding="2" cellspacing="0">
                        <tr style="height: 24px">
                            <td style="width: 600px">
                                <asp:Image ID="img" runat="server" ImageUrl="~/Images/Sticky.png" Visible='<%# GetImgVisibility(Container.DataItem) %>' />
                                <asp:HyperLink ID="hlItem" runat="server" Text='<%# Eval("Title") %>' CssClass="TitleText"
                                    NavigateUrl='<%# GetNavigateUrl(Container.DataItem) %>'>
                                </asp:HyperLink>
                            </td>
                            <td align="center" style="width: 80px">
                                <asp:Label ID="lblPosts" runat="server" Text='<%# Eval("NumberOfPosts") %>'></asp:Label>
                            </td>
                            <td align="center" style="width: 80px">
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("NumberOfVisits") %>'></asp:Label>
                            </td>
                            <td align="center" style="width: 160px">
                                <asp:Label ID="Label1" runat="server" Text='<%# GetLastPostBy(Container.DataItem) %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblStartBy" runat="server" Text='<%# GetStartedBy(Container.DataItem) %>'></asp:Label>
                            </td>
                            <td align="center" style="width: 80px">
                            </td>
                            <td align="center" style="width: 80px">
                            </td>
                            <td align="center" style="width: 160px">
                                <asp:Label ID="Label8" runat="server" Text='<%# GetLastPostTime(Container.DataItem) %>'></asp:Label>
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
