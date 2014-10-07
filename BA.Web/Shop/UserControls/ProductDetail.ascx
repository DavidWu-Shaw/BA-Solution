<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetail.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.ProductDetail" %>
<%@ Register Src="ProductProfile.ascx" TagName="ProductProfile" TagPrefix="uc1" %>
<%@ Register Src="ReviewList.ascx" TagName="ReviewList" TagPrefix="uc2" %>
<div>
    <div style="margin: 0px 10px 10px 10px; height: 24px;">
        <div style="float: right;">
            <asp:HyperLink ID="hlQuote" runat="server" Font-Size="1.2em" />
            <asp:Button ID="btnAdd" runat="server" CssClass="clsButton" OnClick="btnAdd_Click" />
        </div>
    </div>
    <div style="margin: 10px;">
        <uc1:ProductProfile ID="ucProfile" runat="server" />
    </div>
    <div style="margin: 10px;">
        <uc2:ReviewList ID="ucReviewList" runat="server" />
    </div>
</div>
