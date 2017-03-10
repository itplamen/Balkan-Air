<%@ Page Title="Flight Statuses Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FlightStatusesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.FlightStatusesManagement" %>

<asp:Content ID="BoydContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New flight status with ID <asp:Literal ID="AddedFLightStatusIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="FlightStatusesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.FlightStatus"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="FlightStatusesGridView_GetData"
        UpdateMethod="FlightStatusesGridView_UpdateItem"
        DeleteMethod="FlightStatusesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:TemplateField HeaderText="Leg Instances">
                <ItemTemplate>
                    <%#: Item.LegInstances.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No flight statuses found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Name length must be in the range [2 - 15]!" Type="String"
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{2,15}$" ValidationGroup="CreateNewFlightStatus" 
        CssClass="validatorSpan" ControlToValidate="FlightStatusNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new flight status</h3>

        <asp:Label Text="Name:" runat="server" />
        <asp:TextBox ID="FlightStatusNameTextBox" required MaxLength="15" runat="server" />

        <p>
            <asp:Button ID="CreateFlightStatustBtn" runat="server" Text="Create" CssClass="btn btn-info"
                ValidationGroup="CreateNewFlightStatus" OnClick="CreateFlightStatustBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
