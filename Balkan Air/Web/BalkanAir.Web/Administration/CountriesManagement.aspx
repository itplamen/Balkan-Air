<%@ Page Title="Countries Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CountriesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.CountriesManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New country with ID <asp:Literal ID="AddedCountryIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="CountriesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Country"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="CountriesGridView_GetData"
        UpdateMethod="CountriesGridView_UpdateItem"
        DeleteMethod="CountriesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:BoundField DataField="Abbreviation" SortExpression="Abbreviation" HeaderText="Abbreviation" />
            <asp:TemplateField HeaderText="Number of airports">
                <ItemTemplate>
                    <%#: Item.Airports.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No countries found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Name length must be in the range [2 - 50]!" Type="String"
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{2,50}$" ValidationGroup="AddCountry" CssClass="validatorSpan"
        ControlToValidate="CountryNameTextBox" />

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Abbreviation length must be in the range [1 - 2]!" Type="String"
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{1,2}$" ValidationGroup="AddCountry" CssClass="validatorSpan"
        ControlToValidate="AbbreviationNameTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new country</h3>

        <asp:Label Text="Name:" runat="server" AssociatedControlID="CountryNameTextBox" />
        <asp:TextBox ID="CountryNameTextBox" required MaxLength="50" runat="server" />

        <asp:Label Text="Abbreviation:" runat="server" AssociatedControlID="AbbreviationNameTextBox" />
        <asp:TextBox ID="AbbreviationNameTextBox" required MaxLength="2" Style="text-transform: uppercase;" runat="server" />

        <p>
            <asp:Button ID="CreateCountrytBtn" runat="server" Text="Create" CssClass="btn btn-info" ValidationGroup="AddCountry"
                OnClick="CreateCountrytBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>