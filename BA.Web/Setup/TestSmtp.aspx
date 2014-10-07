<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="TestSmtp.aspx.cs" Inherits="BA.Web.Setup.TestSmtp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <table cellspacing="0" cellpadding="0" border="0">
        <tr style="height: 30px">
            <td style="width: 120px;">
                <asp:Label ID="Label5" runat="server" Text="Mail To:"></asp:Label>
                <asp:RequiredFieldValidator ID="v1" runat="server" ControlToValidate="txtMailTo"
                    ErrorMessage="*" ToolTip="Required field." CssClass="WarningText"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtMailTo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 120px;">
                <asp:Label ID="Label3" runat="server" Text="SMTP Server:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSmtpServer" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 120px;">
                <asp:Label ID="Label1" runat="server" Text="SMTP Port:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPort" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 120px;">
                <asp:Label ID="Label2" runat="server" Text="SMTP Username:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUser" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 120px;">
                <asp:Label ID="Label4" runat="server" Text="SMTP Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPasswd" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 40px">
            <td colspan="2" align="right">
                <asp:Button ID="btnSend" runat="server" Text="Send Test" CssClass="clsButton" OnClick="btnSend_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
