<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierProfile.ascx.cs"
    Inherits="BA.Web.Shop.UserControls.SupplierProfile" %>
<div>
    <div style="height: 20px;">
        <asp:Label ID="lblName" runat="server" CssClass="devTextBold" />
    </div>
    <div style="margin-top: 10px;">
        <div style="float: left; height: 130px;">
            <telerik:RadBinaryImage ID="imgSupplier" runat="server" Width="200" Height="120"
                ResizeMode="Fit" />
        </div>
        <div style="float: left; margin-left: 40px; width: 200px; height: 120px;">
            <div style="height: 20px">
                <asp:Label ID="lblAddress" runat="server" />
            </div>
            <div style="height: 20px">
                <asp:Label ID="lblCity" runat="server" />
            </div>
            <div style="height: 20px">
                <asp:Label ID="lblZipCode" runat="server" />
            </div>
            <div style="height: 20px">
                <asp:Label ID="lblPhone" runat="server" />
            </div>
            <div style="height: 40px">
                <asp:Label ID="lblWebsite" runat="server" />
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
