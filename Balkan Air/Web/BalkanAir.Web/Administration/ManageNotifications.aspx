<%@ Page Title="Manage Notifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageNotifications.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageNotifications" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageNotificationsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Notification"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        PageSize="50"
        SelectMethod="ManageNotificationsGridView_GetData"
        UpdateMethod="ManageNotificationsGridView_UpdateItem"
        DeleteMethod="ManageNotificationsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Content" SortExpression="Content" HeaderText="Content" />
            <asp:BoundField DataField="DateCreated" SortExpression="DateCreated" HeaderText="Date Created" />
            <asp:BoundField DataField="Url" SortExpression="Url" HeaderText="Url" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No notifications found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new notification</h3>

        <asp:Label Text="Content: " runat="server" />
        <asp:TextBox ID="ContentTextBox" required runat="server" />

        <asp:Label Text="Url:" runat="server" />
        <asp:TextBox ID="UrlTextBox" required runat="server" />

        <asp:Label Text="Send to:" runat="server" />
        <asp:ListBox ID="UsersListBox" runat="server" SelectionMode="Multiple" required
            DataValueField="Id"
            DataTextField="UserInfo"
            SelectMethod="UsersListBox_GetData" />

        <asp:Button ID="CreateNotificationtBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateNotificationtBtn_Click" />
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
        });
    </script>
</asp:Content>
