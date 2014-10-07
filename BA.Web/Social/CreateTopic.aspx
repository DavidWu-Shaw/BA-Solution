<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AppMasterPage.master"
    AutoEventWireup="true" CodeBehind="CreateTopic.aspx.cs" Inherits="BA.Web.Social.CreateTopic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AppMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppMBody" runat="server">
    <div style="margin-top: 10px;">
        <div style="height: 30px; margin: 10px;">
            <asp:Label ID="lblTitle" runat="server" Text="New Topic" CssClass="TitleText" />
        </div>
        <hr />
        <div style="margin: 10px;">
            <telerik:RadTextBox ID="txtTitle" runat="server" Width="300" Height="20" MaxLength="200">
            </telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="valid1" runat="server" ControlToValidate="txtTitle"
                ErrorMessage="* Please input title." ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div style="margin: 10px;">
            <telerik:RadEditor ID="edContent" runat="server" Width="100%">
            </telerik:RadEditor>
            <asp:RequiredFieldValidator ID="valid2" runat="server" ControlToValidate="edContent"
                ErrorMessage="* Please input content." ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div style="margin: 10px;">
            <asp:Button ID="btnPost" runat="server" CssClass="clsButton" OnClick="btnPost_Click" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="clsButton" />
        </div>
    </div>
</asp:Content>
