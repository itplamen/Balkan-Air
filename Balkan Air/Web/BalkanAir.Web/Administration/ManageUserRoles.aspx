<%@ Page Title="Manage User Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUserRoles.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageUserRoles" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageUserRolesGridView" runat="server" CssClass="administrationGridView"
        ItemType="Microsoft.AspNet.Identity.EntityFramework.IdentityRole"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageUserRolesGridView_GetData"
        UpdateMethod="ManageUserRolesGridView_UpdateItem"
        DeleteMethod="ManageUserRolesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No user roles found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new user role</h3>

        <asp:Label Text="Name: " runat="server" />
        <asp:TextBox ID="UserRoleNameTextBox" required runat="server" />

        <asp:Button ID="CreateUserRoleBtn" runat="server" Text="Create" CssClass="btn btn-info" 
            OnClick="CreateUserRoleBtn_Click" />
    </asp:Panel>
</asp:Content>
