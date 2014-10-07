<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="ShipToEdit.aspx.cs" Inherits="BA.Web.WebPages.ShipTo.ShipToEdit" %>

<%@ Register Src="../../Common/Subject/UI/InstanceEdit.ascx" TagName="InstanceEdit"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:InstanceEdit ID="ucIEdit" runat="server" SubjectType="ShipTo" />
</asp:Content>
