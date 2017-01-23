<%@ Page Title="View news" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewNews.aspx.cs" Inherits="BalkanAir.Web.ViewNews" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="InvalidNewsPanel" Visible="false" runat="server" CssClass="warningPanel">
        <h5>COULD NOT FIND SUCH NEWS!</h5>
    </asp:Panel>

    <asp:FormView ID="ViewNewsFormView" runat="server"
        ItemType="BalkanAir.Data.Models.News"
        SelectMethod="ViewNewsFormView_GetItem">
        <ItemTemplate>
            <h2><%#: Item.Title %> <small>Category: <%#: Item.Category.Name %></small></h2>
            <p><%# Item.Content %></p>
            <p><small>Created on: <%#: Item.DateCreated.ToString("dd/MMMM/yyyy", CultureInfo.InvariantCulture) %></small></p>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
