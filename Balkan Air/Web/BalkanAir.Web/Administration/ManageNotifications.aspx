<%@ Page Title="Manage Notifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageNotifications.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageNotifications" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageNotificationsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Notification"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageNotificationsGridView_GetData"
        UpdateMethod="ManageNotificationsGridView_UpdateItem"
        DeleteMethod="ManageNotificationsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Content" SortExpression="Content">
                <ItemTemplate>
                    <%#: Item.Content.Length > 30 ? Item.Content.Substring(0, 40) + "..." : Item.Content %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="EditContentTextBox" runat="server" Text="<%# BindItem.Content %>" TextMode="MultiLine" Rows="10" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created on:" SortExpression="DateCreated">
                <ItemTemplate>
                    <%#: Item.DateCreated.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" />
            <asp:TemplateField HeaderText="Sent to users:">
                <ItemTemplate>
                    <%#: Item.UserNotification.Count %>
                </ItemTemplate>
            </asp:TemplateField>
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
        <br />

        <p>
            <asp:Label Text="Content: " runat="server" AssociatedControlID="ContentHtmlEditor" />
            <ajaxToolkit:HtmlEditor.Editor ID="ContentHtmlEditor" Height="300px" Width="100%" AutoFocus="true" runat="server" required />
        </p>

        <p>
            <asp:Label Text="Type: " runat="server" AssociatedControlID="TypeDropDownList" />
            <asp:DropDownList ID="TypeDropDownList" runat="server" />
        </p>

        <p>
            <asp:Button ID="CreateNotificationtBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateNotificationtBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
