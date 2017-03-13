<%@ Page Title="Users Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.UsersManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        <asp:Literal Id="SuccessPanelLiteral" runat="server" />
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

    <asp:Button ID="LogoffUsersBtn" Text="Logoff" UseSubmitBehavior="false" runat="server" CssClass="btn btn-info"
        OnClick="LogoffUsersBtn_Click" />

    <asp:Button ID="ChangeUsersRolesBtn" Text="Change roles" UseSubmitBehavior="false" runat="server" CssClass="btn btn-info"
        OnClick="ChangeUsersRolesBtn_Click" />

    <asp:Panel ID="LogoffUsersPanel" Visible="false" runat="server" CssClass="administrationAddEntityPanel">
        <h3>Logoff users</h3>

        <asp:Label Text="Users to logoff: " runat="server" AssociatedControlID="UsersListBox" />
        <asp:ListBox ID="UsersListBox" runat="server" SelectionMode="Multiple" required
            CssClass="bootstrapListBox"
            DataValueField="Id"
            DataTextField="UserInfo"
            SelectMethod="UsersListBox_GetData" />

        <p>
            <asp:Button ID="LogoffUsersButton" Text="Log off" runat="server" CssClass="btn btn-info"
                UseSubmitBehavior="true" ValidateRequestMode="Enabled" OnClick="LogoffUsersButton_Click" />

            <asp:Button ID="CancelLogoffUsersButton" runat="server" ValidateRequestMode="Disabled" UseSubmitBehavior="false"
                Text="Cancel" CssClass="btn btn-danger" OnClick="CancelLogoffUsersButton_Click" />
        </p>
    </asp:Panel>

    <asp:Panel ID="ChangeRolesPanel" Visible="false" runat="server" CssClass="administrationAddEntityPanel">
        <h3>Change roles</h3>

        <asp:Label Text="Users: " runat="server" AssociatedControlID="UsersToChangeRolesListBox" />
        <asp:ListBox ID="UsersToChangeRolesListBox" runat="server" SelectionMode="Single" required
            CssClass="bootstrapListBox"
            DataValueField="Id"
            DataTextField="UserInfo"
            AutoPostBack="true"
            SelectMethod="UsersListBox_GetData"
            OnSelectedIndexChanged="UsersToChangeRolesListBox_SelectedIndexChanged" />

        <asp:Label Text="Roles: " runat="server" AssociatedControlID="UsersToChangeRolesListBox" />
        <asp:ListBox ID="RolesOfSelectedUserListBox" runat="server" SelectionMode="Multiple" required
            ItemType="Microsoft.AspNet.Identity.EntityFramework.IdentityRole"
            CssClass="bootstrapListBox"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="RolesOfSelectedUserListBox_GetData" />

        <p>
            <asp:Button ID="AddToUserRolesButton" Text="Add" runat="server" UseSubmitBehavior="true" ValidateRequestMode="Enabled"
                CssClass="btn btn-info" OnClick="AddToUserRolesButton_Click" />

            <asp:Button ID="DeleteFromUserRolesButton" Text="Remove" CssClass="btn btn-warning" runat="server" 
                UseSubmitBehavior="true" ValidateRequestMode="Enabled" OnClick="DeleteFromUserRolesButton_Click" />

            <asp:Button ID="CancelChanginUserRolesButton" Text="Cancel" runat="server" ValidateRequestMode="Disabled" 
                UseSubmitBehavior="false" CssClass="btn btn-danger" OnClick="CancelChanginUserRolesButton_Click" />
        </p>
    </asp:Panel>
</asp:Content>
