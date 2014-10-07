<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ShopMasterPage.master"
    AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="BA.Web.Logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ShopMHead" runat="server">
    <script type="text/javascript">
        function ShowDiv(id) {
            var sign = document.getElementById("vSignIn");
            var recover = document.getElementById("vRecover");
            sign.style.display = "none";
            recover.style.display = "none";
            if (id == 1) {
                sign.style.display = "block";
            }
            else if (id == 3) {
                recover.style.display = "block";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ShopMBody" runat="server">
    <div style="width: 900px; margin-left: auto; margin-right: auto">
        <div id="vSignIn" style="margin: 20px; display: block;">
            <div style="float: left; background-color: #EFEFEF">
                <div style="float: left; margin: 20px;">
                    <div>
                        <asp:Label ID="lblSignin" runat="server" CssClass="TitleText">
                        </asp:Label>
                    </div>
                    <div style="margin: 10px 0px 0px 0px">
                        <asp:Label ID="lblTip" runat="server">
                        </asp:Label>
                    </div>
                    <div style="width: 300px">
                        <fieldset>
                            <p>
                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Email:*"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" Width="240px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="v1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required."
                                    CssClass="WarningText" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="v3" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Email format is invalid." CssClass="WarningText" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                    ValidationGroup="g1"></asp:RegularExpressionValidator>
                            </p>
                            <p>
                                <asp:Label ID="lblPasswd" runat="server" AssociatedControlID="txtPasswd"></asp:Label>
                                <asp:TextBox ID="txtPasswd" runat="server" Width="240px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="v2" runat="server" ControlToValidate="txtPasswd"
                                    ErrorMessage="Password is required." CssClass="WarningText" ValidationGroup="g1"></asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:CheckBox ID="cbxRemember" runat="server" Text="Remember me"></asp:CheckBox>
                            </p>
                        </fieldset>
                        <p style="text-align: right">
                            <asp:Button ID="btnSignin" runat="server" Text="Sign in" CssClass="clsButton" OnClick="btnSignin_Click"
                                ValidationGroup="g1"></asp:Button>
                        </p>
                    </div>
                    <div>
                        <asp:HyperLink ID="hlForget" runat="server" NavigateUrl="javascript: ShowDiv(3);"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div runat="server" id="divReg" style="float: left; margin: 0px 0px 0px 60px;">
                <div style="margin: 20px;">
                    <div>
                        <asp:Label ID="lblCreateA" runat="server" CssClass="SubTitleText">
                        </asp:Label>
                    </div>
                    <div style="margin: 10px 0px 0px 0px">
                        <asp:Label ID="lblCreateTip" runat="server">
                        </asp:Label>
                    </div>
                    <div style="width: 300px">
                        <fieldset>
                            <p>
                                <asp:Label ID="lblEmailCreate" runat="server" AssociatedControlID="txtEmail"></asp:Label>
                                <asp:TextBox ID="txtRegEmail" runat="server" Width="240px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="v5" runat="server" ControlToValidate="txtRegEmail"
                                    ValidationGroup="g2" ErrorMessage="Email is required." CssClass="WarningText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="v6" runat="server" ControlToValidate="txtRegEmail"
                                    ValidationGroup="g2" ErrorMessage="Email format is invalid." CssClass="WarningText"
                                    ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$">
                                </asp:RegularExpressionValidator>
                            </p>
                            <p>
                                <asp:Label ID="lblPasswdCreate" runat="server" AssociatedControlID="txtPasswd"></asp:Label>
                                <asp:TextBox ID="txtRegPasswd" runat="server" Width="240px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="v7" runat="server" ControlToValidate="txtRegPasswd"
                                    ValidationGroup="g2" ErrorMessage="Password is required." CssClass="WarningText"></asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername"></asp:Label>
                                <asp:TextBox ID="txtUsername" runat="server" Width="240px"></asp:TextBox>
                            </p>
                        </fieldset>
                        <p style="text-align: right">
                            <asp:Button ID="btnCreate" runat="server" CssClass="clsButton" OnClick="btnCreate_Click"
                                ValidationGroup="g2"></asp:Button>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div id="vRecover" style="margin: 10px; display: none;">
            <div>
                <asp:Label ID="lblRecover" runat="server" CssClass="TitleText">
                </asp:Label>
            </div>
            <div style="margin: 10px 0px 0px 0px">
                <asp:Label ID="lblRecoverTip" runat="server">
                </asp:Label>
            </div>
            <div style="width: 300px">
                <fieldset>
                    <p>
                        <asp:Label ID="lblEmailRecover" runat="server" AssociatedControlID="txtRecoverEmail"></asp:Label>
                        <asp:TextBox ID="txtRecoverEmail" runat="server" Width="240px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="v11" runat="server" ControlToValidate="txtRecoverEmail"
                            ErrorMessage="Email is required." CssClass="WarningText" ValidationGroup="g3"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="v12" runat="server" ControlToValidate="txtRecoverEmail"
                            ErrorMessage="Email format is invalid." CssClass="WarningText" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                            ValidationGroup="g3"></asp:RegularExpressionValidator>
                    </p>
                </fieldset>
                <p style="text-align: right">
                    <asp:HyperLink ID="hlCancel" runat="server" NavigateUrl="javascript: ShowDiv(1);"></asp:HyperLink>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="clsButton" OnClick="btnSubmit_Click"
                        ValidationGroup="g3"></asp:Button>
                </p>
            </div>
        </div>
        <div style="margin: 0px 0px 0px 20px;">
            <asp:Label ID="lbMsg" runat="server" CssClass="WarningText" Visible="false">
            </asp:Label>
        </div>
    </div>
</asp:Content>
