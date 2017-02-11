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

                        <h5><strong>Category: <%#: Item.Category.Name %></strong></h5>

                        <p>
                            <img src='<%# Item.HeaderImage == null ? null : "data:image/jpeg;base64," + Convert.ToBase64String(Item.HeaderImage) %>' 
                               runat="server" visible="<%# Item.HeaderImage != null ? true : false %>" />
                        </p>

                        <p>
                            <%# !string.IsNullOrEmpty(Item.Content) && Item.Content.Length > 190 ? 
                               Item.Content.Substring(0, 190) + "..." : Item.Content %>
                        </p>
                        
                        <p>
                            <span class="glyphicon glyphicon-calendar"></span>
                            <small><%#: Item.DateCreated.ToString("dd.MMMM.yyyy hh:mm", CultureInfo.InvariantCulture) %></small>

                            <span class="glyphicon glyphicon-comment"></span>
                            <%#: Item.Comments.Count %>
                        </p>
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
