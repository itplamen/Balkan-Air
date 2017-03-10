<%@ Page Title="Notifications Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotificationsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.NotificationsManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New notification with ID <asp:Literal ID="AddedNotificationIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="NotificationsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Notification"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="NotificationsGridView_GetData"
        UpdateMethod="NotificationsGridView_UpdateItem"
        DeleteMethod="NotificationsGridView_DeleteItem"
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

    <asp:CustomValidator ID="InvalidTypeCustomValidatior" ErrorMessage="" Display="Dynamic" ForeColor="Red" runat="server" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new notification</h3>

            <asp:Label Text="Content:" runat="server" AssociatedControlID="ContentAjaxHtmlEditor" />
            <ajaxToolkit:HtmlEditor.Editor ID="ContentAjaxHtmlEditor" Height="300px" Width="100%" AutoFocus="true" runat="server" required />
 
            <asp:Label Text="Type:" runat="server" AssociatedControlID="TypeDropDownList" />
            <asp:DropDownList ID="TypeDropDownList" runat="server" />

        <p>
            <asp:Button ID="CreateNotificationtBtn" runat="server" Text="Create" CssClass="btn btn-info" 
                OnClick="CreateNotificationtBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
