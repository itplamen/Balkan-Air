<%@ Page Title="Seats Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeatsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.SeatsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:GridView ID="SeatsGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Seat"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="SeatsGridView_GetData"
        UpdateMethod="SeatsGridView_UpdateItem"
        DeleteMethod="SeatsGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="Number" />
            <asp:BoundField DataField="Row" SortExpression="Row" HeaderText="Row" />
            <asp:CheckBoxField DataField="IsReserved" HeaderText="Is Reserved" />
            <asp:TemplateField HeaderText="Travel Class" SortExpression="TravelClassId">
                <ItemTemplate>
                    <%#: this.GetTravelClass(Item.TravelClassId) %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditTravelClassDropDownList" runat="server"
                        DataValueField="Id"
                        DataTextField="TravelClassInfo"
                        SelectedValue="<%#: BindItem.TravelClassId %>"
                        SelectMethod="TravelClassDropDownList_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Leg Instance" SortExpression="LegInstanceId">
                <ItemTemplate>
                    <%#: "Id:" + Item.LegInstanceId + ", " + Item.LegInstance.DepartureDateTime + " -> " + Item.LegInstance.ArrivalDateTime %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="EditLegInstanceDropDown" runat="server"
                        DataValueField="Id"
                        DataTextField="LegInstanceInfo"
                        SelectedValue="<%#: BindItem.LegInstanceId %>"
                        SelectMethod="LegInstanceDropDown_GetData" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No seats found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RangeValidator ErrorMessage="Row must be in the range [1 - 30]!" ControlToValidate="RowTextBox" runat="server"
        ForeColor="Red" MinimumValue="1" MaximumValue="30" Type="Integer" CssClass="validatorSpan" ValidationGroup="CreateNewSeat" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new seat</h3>

        <asp:Label Text="Row:" runat="server" AssociatedControlID="RowTextBox" />
        <asp:TextBox ID="RowTextBox" required runat="server" TextMode="Number" />

        <asp:Label Text="Number:" runat="server" AssociatedControlID="SeatNumberTextBox" />
        <asp:TextBox ID="SeatNumberTextBox" required MaxLength="1" Style="text-transform: uppercase;" runat="server" />

        <asp:Label Text="Is Reserved:" runat="server" AssociatedControlID="IsSeatReservedCheckBox" />
        <asp:CheckBox ID="IsSeatReservedCheckBox" runat="server" />

        <asp:Label Text="Travel Class:" runat="server" AssociatedControlID="AddTravelClassDropDownList" />
        <asp:DropDownList ID="AddTravelClassDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="TravelClassInfo"
            SelectMethod="TravelClassDropDownList_GetData" />

        <asp:Label Text="Leg Instance:" runat="server" AssociatedControlID="AddLegInstanceDropDown" />
        <asp:DropDownList ID="AddLegInstanceDropDown" runat="server"
            DataValueField="Id"
            DataTextField="LegInstanceInfo"
            SelectMethod="LegInstanceDropDown_GetData" />

        <p>
            <asp:Button ID="CreateAirportBtn" runat="server" Text="Create" CssClass="btn btn-info" ValidationGroup="CreateNewSeat"
                OnClick="CreateAirportBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
