<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchedulerControl.ascx.cs"
    Inherits="BA.Web.Common.UserControls.SchedulerControl" %>
<table style="width: 100%">
    <tr>
        <td valign="top" style="width: 230px">
            <table style="width: 100%">
                <tr>
                    <td>
                        <telerik:RadCalendar ID="RadCalendar1" runat="server" EnableNavigation="true" AutoPostBack="true"
                            EnableMultiSelect="false" Skin="Windows7">
                        </telerik:RadCalendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadCalendar ID="RadCalendar2" runat="server" EnableNavigation="false" AutoPostBack="true"
                            EnableMultiSelect="false" Skin="Windows7">
                        </telerik:RadCalendar>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <telerik:RadScheduler ID="RadScheduler1" runat="server" Height="100%" Skin="Windows7"
                ReadOnly="true">
                <TimelineView UserSelectable="false" />
            </telerik:RadScheduler>
        </td>
    </tr>
</table>
