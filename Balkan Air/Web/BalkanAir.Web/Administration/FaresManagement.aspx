<%@ Page Title="Fares Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FaresManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.FaresManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New fare with ID <asp:Literal ID="AddedFareIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="FaresGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Fare"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="FaresGridView_GetData"
        UpdateMethod="FaresGridView_UpdateItem"
        DeleteMethod="FaresGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Price" SortExpression="Price" HeaderText="Price (&#8364;)" />
            <asp:TemplateField HeaderText="Route">
                <ItemTemplate>
                    <%#: "Id:" + Item.RouteId + ", " + Item.Route.Origin.Name + " -> " + Item.Route.Destination.Name %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditRoutesDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="RouteInfo"
                        SelectedValue="<%#: BindItem.RouteId %>"
                        SelectMethod="RoutesDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No fares found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new fare</h3>

        <asp:Label Text="Price:" runat="server" AssociatedControlID="PriceTextBox" />
        <asp:TextBox ID="PriceTextBox" required runat="server" />

        <asp:Label Text="Route:" runat="server" AssociatedControlID="AddRoutesDropDownList" />
        <asp:DropDownList ID="AddRoutesDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="RouteInfo"
            SelectMethod="RoutesDropDownList_GetData" />

        <p>
            <asp:Button ID="CreateFaretBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreateFaretBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
