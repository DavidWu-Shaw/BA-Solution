<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListManager.ascx.cs"
    Inherits="BA.Web.Common.UserControls.ListManager" %>
<script type="text/javascript">

    function AddNewItem(gridId) {
        var masterTable = $find(gridId).get_masterTableView();
        masterTable.showInsertItem();
    }

</script>
<table cellpadding="0" cellspacing="0" style="width: 100%;" class="devContainerTable">
    <tr>
        <td runat="server" id="tdTitle" style="height: 20px">
            <table cellspacing="0" cellpadding="0" style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" />
                        <asp:Label ID="lblRowCount" runat="server" />
                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td runat="server" id="tdEditAll" visible="false">
                                    <asp:ImageButton ID="btnEditAll" runat="server" ImageUrl="~/Images/EditAll.png" ImageAlign="Middle"
                                        AlternateText="Edit All" />
                                    <asp:Label ID="lblEditAll" runat="server" />
                                </td>
                                <td runat="server" id="tdSaveAll" visible="false">
                                    <asp:ImageButton ID="btnSaveAll" runat="server" ImageUrl="~/Images/SaveAll.png" ImageAlign="Middle"
                                        AlternateText="Save All" />
                                    <asp:Label ID="lblSaveAll" runat="server" />
                                </td>
                                <td runat="server" id="tdCancelAll" visible="false">
                                    <asp:ImageButton ID="btnCancelAll" runat="server" ImageUrl="~/Images/CancelAll.png"
                                        ImageAlign="Middle" AlternateText="Cancel All" />
                                    <asp:Label ID="lblCancelAll" runat="server" />
                                </td>
                                <td runat="server" id="tdAddRecord" visible="false">
                                    <asp:ImageButton ID="btnAddRecord" runat="server" ImageUrl="~/Images/add.png" ImageAlign="Middle"
                                        AlternateText="Add one record" />
                                    <asp:Label ID="lblAdd" runat="server" />
                                </td>
                                <td runat="server" id="tdImport" visible="false">
                                    <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/import1.png" ImageAlign="Middle"
                                        AlternateText="Import data for excel file" />
                                    <asp:Label ID="lblImport" runat="server" />
                                </td>
                                <td runat="server" id="tdExport">
                                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/export1.png" ImageAlign="Middle"
                                        AlternateText="Export all data to excel file" OnClick="btnExport_Click" />
                                    <asp:Label ID="lblExport" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 5px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>
