<%@ Page Title="User Roles Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRolesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.UserRolesManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New user role "<asp:Literal ID="AddedUserRoleNameLiteral" runat="server" />"
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="UserRolesGridView" runat="server" CssClass="administrationGridView"
        ItemType="Microsoft.AspNet.Identity.EntityFramework.IdentityRole"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="UserRolesGridView_GetData"
        UpdateMethod="UserRolesGridView_UpdateItem"
        DeleteMethod="UserRolesGridView_DeleteItem"
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

    <asp:CustomValidator ID="CustomValidator" Display="Dynamic" ForeColor="Red" runat="server" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new user role</h3>

        <asp:Label Text="Name: " runat="server" AssociatedControlID="UserRoleNameTextBox" />
        <asp:TextBox ID="UserRoleNameTextBox" required runat="server" />

        <p>
            <asp:Button ID="CreateUserRoleBtn" runat="server" Text="Create" CssClass="btn btn-info" 
                OnClick="CreateUserRoleBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
