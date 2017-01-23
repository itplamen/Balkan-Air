<%@ Page Title="News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="BalkanAir.Web.News" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ShowByCategoryDiv">
        <asp:Label Text="Category:" runat="server" AssociatedControlID="ShowNewsByCategoryDropDownList" />
        <asp:DropDownList ID="ShowNewsByCategoryDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="Name"
            AutoPostBack="true"
            ItemType="BalkanAir.Data.Models.Category"
            OnSelectedIndexChanged="ShowNewsByCategoryDropDownList_SelectedIndexChanged" />
    </div>

    <div id="NewsDiv">
        <asp:Repeater ID="NewsRepeater" runat="server"
            ItemType="BalkanAir.Data.Models.News">
            <ItemTemplate>
                <a href='<%= Page.ResolveUrl(Pages.VIEW_NEWS) + "?id=" %><%# Item.Id %>'>
                    <div class="news">
                        <h3><%#: Item.Title %></h3>
                        <p><h5>Category: <%#: Item.Category.Name %></h5></p>
                        <p>
                            <%# !string.IsNullOrEmpty(Item.Content) && Item.Content.Length > 300 ? 
                               Item.Content.Substring(0, 300) + "..." : Item.Content %>
                        </p>
                        <small><%#: Item.DateCreated.ToString("dd/MMMM/yyyy", CultureInfo.InvariantCulture) %></small>
                    </div>
                </a>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="NoNewsForCategoryLabel" ClientIDMode="Static" Text="There is no news for this category!"
                    Visible="<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>" runat="server" />
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
