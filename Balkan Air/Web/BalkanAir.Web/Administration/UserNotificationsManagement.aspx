<%@ Page Title="User Notifications Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserNotificationsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.UserNotificationsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="UserNotificationsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.UserNotification"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="UserNotificationsGridView_GetData"
        UpdateMethod="UserNotificationsGridView_UpdateItem"
        DeleteMethod="UserNotificationsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Received on" SortExpression="DateReceived">
                <ItemTemplate>
                    <%#: Item.DateReceived %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsRead" HeaderText="IsRead" />
            <asp:TemplateField HeaderText="Read on" SortExpression="DateRead">
                <ItemTemplate>
                    <%#: Item.DateRead %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserId" SortExpression="UserId" HeaderText="User Id" />
            <asp:TemplateField HeaderText="User">
                <ItemTemplate>
                    <%#: Item.User.UserSettings.FirstName + " " + Item.User.UserSettings.LastName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NotificationId" SortExpression="NotificationId" HeaderText="Notification Id" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Set as read" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No user notifications found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        Notification sent successfully!
    </asp:Panel>

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Send notification to users</h3>

        <asp:Label Text="Send to: " runat="server" AssociatedControlID="UsersListBox" />
        <asp:ListBox ID="UsersListBox" runat="server" SelectionMode="Multiple" required
            DataValueField="Id"
            DataTextField="UserInfo"
            SelectMethod="UsersListBox_GetData" />

        <asp:Label Text="Notification: " runat="server" AssociatedControlID="NotificationsDropDownList" />
        <asp:DropDownList ID="NotificationsDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="NotificationInfo"
            SelectMethod="NotificationsDropDownList_GetData" />

        <p>
            <asp:Button ID="SendNotificationButton" runat="server" UseSubmitBehavior="true" Text="Send" CssClass="btn btn-info"
                OnClick="SendNotificationButton_Click" />
            <asp:Button ID="CancelButton" runat="server" UseSubmitBehavior="false" Text="Cancel" CssClass="btn btn-danger"
                OnClick="CancelButton_Click" />
        </p>
    </asp:Panel>

    <script type="text/javascript">
        $(function () {
            $('[id*=UsersListBox]').multiselect({
                enableFiltering: true,
                includeSelectAllOption: true,
                enableCollapsibleOptGroups: true,
                maxHeight: 300,
                buttonWidth: '350px'
            });
            if ($('#SuccessPanel').is(':visible')) {
                setTimeout(function () {
                    $('#SuccessPanel').fadeOut('slow');
                }, 3000);
            }
        });
    </script>
</asp:Content>
