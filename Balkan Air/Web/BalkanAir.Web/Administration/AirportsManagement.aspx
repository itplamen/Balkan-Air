<%@ Page Title="Airports Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AirportsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.AirportsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New airport with ID <asp:Literal ID="AddedAirportIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="AirportsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Airport"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="AirportsGridView_GetData"
        UpdateMethod="AirportsGridView_UpdateItem"
        DeleteMethod="AirportsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" />
            <asp:BoundField DataField="Abbreviation" SortExpression="Abbreviation" HeaderText="Abbreviation" />
            <asp:TemplateField HeaderText="Country">
                <ItemTemplate>
                    <%#: Item.Country.Name %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="CountriesDropDownList" runat="server"
                        ItemType="BalkanAir.Data.Models.Country"
                        DataValueField="Id"
                        DataTextField="Name"
                        SelectedValue="<%#: BindItem.CountryId %>"
                        SelectMethod="CountryDropDownList_GetData"
                        OnDataBinding="CountriesDropDownList_DataBinding" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Origins">
                <ItemTemplate>
                    <%#: Item.Origins.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Destinations">
                <ItemTemplate>
                    <%#: Item.Destinations.Count %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No airports found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Name length must be in the range [2 - 50]!" Type="String"
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{2,50}$" ValidationGroup="CreateAirport"
        CssClass="validatorSpan" ControlToValidate="AirportNameTextBox" />

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Abbreviation length must be in the range [1 - 3]!"
        Type="String" ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{1,3}$" ValidationGroup="CreateAirport"
        CssClass="validatorSpan" ControlToValidate="AbbreviationTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new airport</h3>

        <asp:Label Text="Name:" runat="server" AssociatedControlID="AirportNameTextBox" />
        <asp:TextBox ID="AirportNameTextBox" required MaxLength="50" runat="server" />

        <asp:Label Text="Abbreviation:" runat="server" AssociatedControlID="AbbreviationTextBox" />
        <asp:TextBox ID="AbbreviationTextBox" required MaxLength="3" Style="text-transform: uppercase;" runat="server" />

        <asp:Label Text="Country:" runat="server" AssociatedControlID="CountryDropDownList" />
        <asp:DropDownList ID="CountryDropDownList" runat="server"
            ItemType="BalkanAir.Data.Models.Country"
            DataValueField="Id"
            DataTextField="Name"
            SelectMethod="CountryDropDownList_GetData" />

        <p>
            <asp:Button ID="CreateAirportBtn" runat="server" Text="Create" CssClass="btn btn-info"
                ValidationGroup="CreateAirport" OnClick="CreateAirportBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
