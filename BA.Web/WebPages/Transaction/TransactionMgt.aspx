﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SetupMasterPage.master" AutoEventWireup="true"
    CodeBehind="TransactionMgt.aspx.cs" Inherits="BA.Web.WebPages.Transaction.TransactionMgt" %>

<%@ Register Src="../../Common/Subject/UI/InstanceList.ascx" TagName="InstanceList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SetMHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SetMBody" runat="server">
    <uc1:InstanceList ID="ucIList" runat="server" SubjectType="Transaction" />
</asp:Content>
