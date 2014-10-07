<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderProcessor.ascx.cs"
    Inherits="BA.Web.UserControls.OrderProcessor" %>
<%@ Register Src="OrderView.ascx" TagName="OrderView" TagPrefix="uc1" %>
<div>
    <uc1:OrderView ID="ucOrder" runat="server" Title="Selected order" />
    <div style="height: 10px">
    </div>
    <div runat="server" id="divButtons" style="float: right;">
    </div>
</div>
