<%@ Page Title="Users Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.UsersManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New travel class with ID <asp:Literal ID="AddedTravelClassIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="UsersGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.User"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="UsersGridView_GetData"
        UpdateMethod="UsersGridView_UpdateItem"
        DeleteMethod="UsersGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                <ItemTemplate>
                    <%#: Item.Email %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Role">
                <ItemTemplate>
                    <%#: this.GetUserRole(Item.Roles) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserSettings.FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="UserSettings.LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="UserSettings.DateOfBirth" HeaderText="Date Of Birth" />
            <asp:BoundField DataField="UserSettings.Gender" HeaderText="Gender" />
            <asp:BoundField DataField="UserSettings.IdentityDocumentNumber" HeaderText="Identity Document Number" />
            <asp:BoundField DataField="UserSettings.Nationality" HeaderText="Nationality" />
            <asp:BoundField DataField="UserSettings.FullAddress" HeaderText="Address" />
            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" />
            <asp:BoundField DataField="DeletedOn" HeaderText="Deleted On" />
            <asp:TemplateField HeaderText="Last Login">
                <ItemTemplate>
                    <%#: Item.UserSettings.LastLogin %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Logout">
                <ItemTemplate>
                    <%#: Item.UserSettings.LastLogout %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No users found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
