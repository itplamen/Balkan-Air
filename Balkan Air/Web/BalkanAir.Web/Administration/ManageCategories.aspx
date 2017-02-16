<%@ Page Title="Manage Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageCategories" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageCategoriesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Category"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageCategoriesGridView_GetData"
        UpdateMethod="ManageCategoriesGridView_UpdateItem"
        DeleteMethod="ManageCategoriesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:TemplateField HeaderText="Number of news">
                <ItemTemplate>
                    <%#: Item.News.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No categories found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Name length must be in the range [1 - 50]!" Type="String" 
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{1,50}$" ControlToValidate="CategoryNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new category</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="CategoryNameTextBox" required MaxLength="50" runat="server" />

        <asp:Button ID="CreateCategoryBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateCategoryBtn_Click" />
    </asp:Panel>
</asp:Content>
