<%@ Page Title="Notifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="BalkanAir.Web.Account.Notifications" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <div id="AllNotificationsDiv">
        <asp:ListView ID="NotificationsListView" runat="server"
            DataKeyNames="Id"
            ItemType="BalkanAir.Data.Models.UserNotification"
            SelectMethod="NotificationsListView_GetData"
            ViewStateMode="Disabled">
            <ItemTemplate>
                <p class="notificationsP <%# Item.IsRead ? "read" : "unread" %>">
                    <a href="News.aspx">
                        <span class="notificationContentSpan">
                            <%#: Item.Notification.Content %>
                            <br />
                        </span>
                        <small class="notificationDateSmall">Received on <%#: Item.DateReceived.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                        at <%#: Item.DateReceived.ToString("HH:mm", CultureInfo.InvariantCulture) %>
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
    </div>
</asp:Content>
