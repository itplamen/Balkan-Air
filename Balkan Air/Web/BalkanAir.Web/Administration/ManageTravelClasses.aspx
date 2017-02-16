<%@ Page Title="Manage travel classes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageTravelClasses.aspx.cs" Inherits="BalkanAir.Web.Administration.ManageTravelClasses" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="ManageTravelClassesGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.TravelClass"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="ManageTravelClassesGridView_GetData"
        UpdateMethod="ManageTravelClassesGridView_UpdateItem"
        DeleteMethod="ManageTravelClassesGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" />
            <asp:BoundField DataField="Meal" SortExpression="Meal" HeaderText="Meal" />
            <asp:CheckBoxField DataField="PriorityBoarding" HeaderText="Priority Boarding" />
            <asp:CheckBoxField DataField="ReservedSeat" HeaderText="Reserved Seat" />
            <asp:CheckBoxField DataField="EarnMiles" HeaderText="Earn Miles" />
            <asp:BoundField DataField="Price" SortExpression="Price" HeaderText="Price" />
            <asp:BoundField DataField="FlightId" SortExpression="FlightId" HeaderText="Flight Id" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No travel classes found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Invalid meal length!" Type="String" ForeColor="Red" runat="server"
        ValidationExpression="^[\s\S]{2,50}$" ControlToValidate="MealTextBox" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new travel class</h3>

        <p>
            <asp:Label Text="Type: " runat="server" AssociatedControlID="TravelClassTypeDropDownList" />
            <asp:DropDownList runat="server" ID="TravelClassTypeDropDownList" />
        </p>

        <p>
            <asp:Label Text="Meal: " runat="server" AssociatedControlID="MealTextBox" />
            <asp:TextBox ID="MealTextBox" required runat="server" MaxLength="50" />
        </p>

        <p>
            <asp:Label Text="Priority Boarding: " runat="server" AssociatedControlID="PriorityBoardingCheckBox" />
            <asp:CheckBox ID="PriorityBoardingCheckBox" runat="server" />

            <asp:Label Text="Reserved Seat: " runat="server" AssociatedControlID="ReservedSeatCheckBox" />
            <asp:CheckBox ID="ReservedSeatCheckBox" Checked="true" runat="server" />

            <asp:Label Text="Earn Miles: " runat="server" AssociatedControlID="EarnMilesCheckBox" />
            <asp:CheckBox ID="EarnMilesCheckBox" runat="server" />
        </p>

        <p>
            <asp:Label Text="Price: " runat="server" AssociatedControlID="PriceTextBox" />
            <asp:TextBox ID="PriceTextBox" required runat="server" />
        </p>

        <p>
            <asp:Label Text="Flight: " runat="server" AssociatedControlID="FlightsDropDownList" />
            <asp:DropDownList ID="FlightsDropDownList" runat="server"
                DataValueField="Id"
                DataTextField="FlightInfo"
                SelectMethod="FlightsDropDownList_GetData" />
        </p>

        <asp:Button ID="CreatTravelClassBtn" runat="server" Text="Create" CssClass="btn btn-info" OnClick="CreatTravelClassBtn_Click" />
    </asp:Panel>
</asp:Content>
