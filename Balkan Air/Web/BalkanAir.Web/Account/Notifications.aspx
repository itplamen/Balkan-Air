<%@ Page Title="Notifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="BalkanAir.Web.Account.Notifications" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="AllNotificationsPanel" runat="server">
        <asp:ListView ID="NotificationsListView" runat="server"
            DataKeyNames="Id"
            ItemType="BalkanAir.Data.Models.UserNotification"
            SelectMethod="NotificationsListView_GetData"
            ViewStateMode="Disabled">
            <ItemTemplate>
                <p class="notificationsP <%# Item.IsRead ? "read" : "unread" %>">
                    <a href="<%= Page.ResolveUrl(Pages.NOTIFICATIONS) + "?id=" %><%# Item.NotificationId %>">
                        <span class="notificationContentSpan">
                            <%#: Item.Notification.Content %>
                            <br />
                        </span>
                        <small class="notificationDateSmall">
                            Received on <%#: Item.DateReceived.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                            at <%#: Item.DateReceived.ToString("HH:mm", CultureInfo.InvariantCulture) %>

                            <span>
                                <%# Item.IsRead ? "Read on " + Item.DateRead.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + 
                                        "at " + Item.DateRead.ToString("HH:mm", CultureInfo.InvariantCulture) : null %>
                            </span>
                        </small>
                    </a>
                </p>
            </ItemTemplate>
            <ItemSeparatorTemplate>
                <hr />
            </ItemSeparatorTemplate>
            <LayoutTemplate>
                <div runat="server" id="itemPlaceholder"></div>
                <br />
                <br />

                <asp:DataPager runat="server" PageSize="5">
                    <Fields>
                        <asp:NextPreviousPagerField ShowPreviousPageButton="true" ShowNextPageButton="false"
                            ButtonCssClass="pagerBtns" PreviousPageText="«" />
                        <asp:NumericPagerField ButtonCount="5" CurrentPageLabelCssClass="currentPageBtn" NumericButtonCssClass="pagerBtns" />
                        <asp:NextPreviousPagerField ShowPreviousPageButton="false" ShowNextPageButton="true"
                            ButtonCssClass="pagerBtns" NextPageText="»" />
                    </Fields>
                </asp:DataPager>
                <br />
                <br />

                <asp:Button ID="MarkAllNotificationsAsReadBtn" runat="server" Text="Mark all as read"
                    UseSubmitBehavior="false" CssClass="btn btn-default" OnClick="MarkAllNotificationsAsReadBtn_Click" />
            </LayoutTemplate>
        </asp:ListView>
    </asp:Panel>

    <asp:Panel ID="ConcreteNotificationPanel" runat="server">
        <asp:FormView ID="ViewNotificationFormView" runat="server"
            ItemType="BalkanAir.Data.Models.UserNotification"
            SelectMethod="ViewNotificationFormView_GetItem">
            <ItemTemplate>
                <span class="notificationContentSpan">
                    <%#: Item.Notification.Content %>
                    <br />
                </span>
                <small class="notificationDateSmall">Received on <%#: Item.DateReceived.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                    at <%#: Item.DateReceived.ToString("HH:mm", CultureInfo.InvariantCulture) %>

                    <span>
                        Read on <%#: Item.DateRead.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                        at <%#: Item.DateRead.ToString("HH:mm", CultureInfo.InvariantCulture) %>
                    </span>
                </small>
            </ItemTemplate>
        </asp:FormView>
    </asp:Panel>
</asp:Content>
