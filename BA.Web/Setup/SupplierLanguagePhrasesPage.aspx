﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master"
    AutoEventWireup="true" CodeBehind="SupplierLanguagePhrasesPage.aspx.cs" Inherits="BA.Web.Setup.SupplierLanguagePhrasesPage" %>

<%@ Register Src="../Common/UserControls/SuperGridView.ascx" TagName="SuperGridView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <div>
        <uc1:SuperGridView ID="ucGrid" runat="server" IsAutoGenerateColumns="true" PageSize="20" />
    </div>
</asp:Content>
