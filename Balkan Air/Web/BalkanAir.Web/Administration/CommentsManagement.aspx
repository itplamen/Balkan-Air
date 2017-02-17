<%@ Page Title="Comments Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommentsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.CommentsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="CommentsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Comment"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="CommentsGridView_GetData"
        UpdateMethod="CommentsGridView_UpdateItem"
        DeleteMethod="CommentsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Content" SortExpression="Content">
                <ItemTemplate>
                    <%#: Item.Content.Length > 30 ? Item.Content.Substring(0, 30) + "..." : Item.Content  %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="EditContentTextBox" runat="server" Text="<%#: BindItem.Content %>" TextMode="MultiLine" Rows="10" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Of Comment" SortExpression="DateOfComment">
                <ItemTemplate>
                    <%#: Item.DateOfComment %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserId" SortExpression="UserId" HeaderText="User Id" />
            <asp:TemplateField HeaderText="User">
                <ItemTemplate>
                    <%#: Item.User != null ? Item.User.UserSettings.FirstName + " " 
                            + Item.User.UserSettings.LastName : "Anonymous"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NewsId" SortExpression="NewsId" HeaderText="News Id" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No comments found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
