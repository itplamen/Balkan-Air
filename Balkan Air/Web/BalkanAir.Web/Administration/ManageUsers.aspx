<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageUsers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageUsersGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.User"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageUsersGridView_GetData"
        UpdateMethod="ManageUsersGridView_UpdateItem"
        DeleteMethod="ManageUsersGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="UserSettings.FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="UserSettings.LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="UserSettings.DateOfBirth" HeaderText="Date Of Birth" />
            <asp:BoundField DataField="UserSettings.Gender" HeaderText="Gender" />
            <asp:BoundField DataField="UserSettings.IdentityDocumentNumber" HeaderText="Identity Document Number" />
            <asp:BoundField DataField="UserSettings.Nationality" HeaderText="Nationality" />
            <asp:BoundField DataField="UserSettings.FullAddress" HeaderText="Address" />
            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" />
            <asp:BoundField DataField="DeletedOn" HeaderText="Deleted On" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No users found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

     
    <%--<asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new user</h3>

        <asp:Label Text="First name:" runat="server" />
        <asp:TextBox ID="FirstNameTextBox" runat="server" required />

        <asp:Label Text="Last name:" runat="server" />
        <asp:TextBox ID="LastNameTextBox" runat="server" required />

        <asp:Label Text="Date of birth:" runat="server" />
        <asp:TextBox runat="server" ID="DateOfBirthTextBox" ClientIDMode="Static" />

      



        <asp:Button ID="CreateAircraftBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateAircraftBtn_Click" />
    </asp:Panel>--%>

    <%--<script>
        $(function () {
            $('#DateOfBirthTextBox').datepicker({ dateFormat: "yy/mm/dd" });
        });
    </script>--%>
</asp:Content>
