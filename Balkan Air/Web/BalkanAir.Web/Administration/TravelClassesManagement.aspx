<%@ Page Title="Travel Classes Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelClassesManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.TravelClassesManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New travel class with ID <asp:Literal ID="AddedTravelClassIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="TravelClassesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.TravelClass"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="TravelClassesGridView_GetData"
        UpdateMethod="TravelClassesGridView_UpdateItem"
        DeleteMethod="TravelClassesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" />
            <asp:BoundField DataField="Meal" SortExpression="Meal" HeaderText="Meal" />
            <asp:CheckBoxField DataField="PriorityBoarding" HeaderText="Priority Boarding" />
            <asp:CheckBoxField DataField="ReservedSeat" HeaderText="Reserved Seat" />
            <asp:CheckBoxField DataField="EarnMiles" HeaderText="Earn Miles" />
            <asp:BoundField DataField="NumberOfRows" SortExpression="NumberOfRows" HeaderText="Number Of Rows" />
            <asp:BoundField DataField="NumberOfSeats" SortExpression="NumberOfSeats" HeaderText="Number Of Seats" />
            <asp:BoundField DataField="Price" SortExpression="Price" HeaderText="Price (&#8364;)" />
            <asp:TemplateField HeaderText="Aircraft" SortExpression="AircraftId">
                <ItemTemplate>
                    <%#: "Id: " + Item.AircraftId + ", " + Item.Aircraft.AircraftManufacturer.Name + " " + Item.Aircraft.Model %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditAircraftsDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="AircraftInfo"
                        SelectedValue="<%#: BindItem.AircraftId %>"
                        SelectMethod="AircraftsDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Available Seats" SortExpression="NumberOfAvailableSeats">
                <ItemTemplate>
                    <%#: Item.NumberOfAvailableSeats %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No travel classes found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Meal length must be in the range [2 - 50]!" Type="String"
        ForeColor="Red" runat="server" ValidationExpression="^[\s\S]{2,50}$" ControlToValidate="MealTextBox"
        CssClass="validatorSpan" ValidationGroup="CreateNewTravelClass" />

    <asp:RangeValidator ErrorMessage="Number of rows must be in the range [2 - 26]!" ControlToValidate="NumberOfRowsTextBox"
        runat="server" ForeColor="Red" MinimumValue="2" MaximumValue="26" Type="Integer" CssClass="validatorSpan"
        ValidationGroup="CreateNewTravelClass" />

    <asp:RangeValidator ErrorMessage="Number of seats must be in the range [12 - 156]!" ControlToValidate="NumberOfSeatsTextBox"
        runat="server" ForeColor="Red" MinimumValue="12" MaximumValue="156" Type="Integer" CssClass="validatorSpan"
        ValidationGroup="CreateNewTravelClass" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new travel class</h3>

        <asp:Label Text="Type: " runat="server" AssociatedControlID="TravelClassTypeDropDownList" />
        <asp:DropDownList runat="server" ID="TravelClassTypeDropDownList" />

        <asp:Label Text="Meal: " runat="server" AssociatedControlID="MealTextBox" />
        <asp:TextBox ID="MealTextBox" required runat="server" MaxLength="50" />

        <asp:Label Text="Priority Boarding: " runat="server" AssociatedControlID="PriorityBoardingCheckBox" />
        <asp:CheckBox ID="PriorityBoardingCheckBox" runat="server" />

        <asp:Label Text="Reserved Seat: " runat="server" AssociatedControlID="ReservedSeatCheckBox" />
        <asp:CheckBox ID="ReservedSeatCheckBox" Checked="true" runat="server" />

        <asp:Label Text="Earn Miles: " runat="server" AssociatedControlID="EarnMilesCheckBox" />
        <asp:CheckBox ID="EarnMilesCheckBox" runat="server" />

        <asp:Label Text="Number Of Rows: " runat="server" AssociatedControlID="NumberOfRowsTextBox" />
        <asp:TextBox ID="NumberOfRowsTextBox" required runat="server" TextMode="Number" />

        <asp:Label Text="Number Of Seats: " runat="server" AssociatedControlID="NumberOfSeatsTextBox" />
        <asp:TextBox ID="NumberOfSeatsTextBox" required runat="server" TextMode="Number" />

        <asp:Label Text="Price: " runat="server" AssociatedControlID="PriceTextBox" />
        <asp:TextBox ID="PriceTextBox" required runat="server" />

        <asp:Label Text="Aircraft: " runat="server" AssociatedControlID="AddAircraftsDropDownList" />
        <asp:DropDownList ID="AddAircraftsDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="AircraftInfo"
            SelectMethod="AircraftsDropDownList_GetData" />

        <p>
            <asp:Button ID="CreatTravelClassBtn" runat="server" Text="Create" CssClass="btn btn-info" 
                ValidationGroup="CreateNewTravelClass" OnClick="CreatTravelClassBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
