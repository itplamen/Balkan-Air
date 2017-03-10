<%@ Page Title="Categories Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoriesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.CategoriesManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New category with ID <asp:Literal ID="AddedCategoryIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="CategoriesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Category"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="CategoriesGridView_GetData"
        UpdateMethod="CategoriesGridView_UpdateItem"
        DeleteMethod="CategoriesGridView_DeleteItem"
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
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{1,50}$" ValidationGroup="AddCategory" CssClass="validatorSpan"
        ControlToValidate="CategoryNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new category</h3>

        <asp:Label Text="Name:" runat="server" AssociatedControlID="CategoryNameTextBox" />
        <asp:TextBox ID="CategoryNameTextBox" required MaxLength="50" runat="server" />

        <p>
            <asp:Button ID="CreateCategoryBtn" runat="server" Text="Create" CssClass="btn btn-info" ValidationGroup="AddCategory"
                OnClick="CreateCategoryBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
