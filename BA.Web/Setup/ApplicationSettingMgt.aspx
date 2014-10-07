<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="ApplicationSettingMgt.aspx.cs" Inherits="BA.Web.Setup.ApplicationSettingMgt" %>

<%@ Register Src="../Common/Subject/UI/InstanceList.ascx" TagName="InstanceList"
    TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:InstanceList ID="ucIList" runat="server" SubjectType="ApplicationSetting" />
</asp:Content>
