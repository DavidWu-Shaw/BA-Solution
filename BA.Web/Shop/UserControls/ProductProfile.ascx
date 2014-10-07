<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductProfile.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.ProductProfile" %>
<div class="devContainerTable">
    <div style="margin: 10px;">
        <div style="float: left; height: 200px;">
            <telerik:RadBinaryImage ID="imgProduct" runat="server" Width="200" Height="180" ResizeMode="Fit" />
        </div>
        <div style="float: left; margin-left: 40px; width: 400px; height: 180px;">
            <div style="height: 20px">
                <asp:Label ID="lblName" runat="server" CssClass="devTextBold" />
            </div>
            <div style="height: 20px">
                <asp:Label ID="lblSupplierName" runat="server" />
            </div>
            <div style="height: 80px">
                <asp:Label ID="lblDescription" runat="server" />
            </div>
            <div style="height: 20px">
                <asp:Label ID="lblPrice" runat="server" />
            </div>
            <div style="height: 20px;">
                <telerik:RadRating ID="Rating1" runat="server" ItemCount="5" SelectionMode="Continuous"
                    ReadOnly="true" Precision="Half" Orientation="Horizontal" />
            </div>
            <div style="height: 20px;">
                <div style="float: left;">
                    <asp:Label ID="lblCount" runat="server" />
                </div>
                <div style="float: left; margin-left: 10px;">
                    <asp:Label ID="lblNumberOfRatings" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
