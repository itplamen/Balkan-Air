<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FlightInfo.aspx.cs" Inherits="BalkanAir.Web.FlightInfo" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="SearchConcreteFlightPanel" ClientIDMode="Static" runat="server">
        <div class="fancyTextBox" id="DepartureFancyTextBox">
            <asp:Label Text="Flight number:" CssClass="label" runat="server" AssociatedControlID="SearchedFlightNumberTextBox" />
            <asp:TextBox ID="SearchedFlightNumberTextBox" ClientIDMode="Static" runat="server" placeholder="E.G. 2DA5" />
        </div>

        <asp:Button ID="SearchFligthNumberBtn" ClientIDMode="Static" OnClick="SearchFligthNumberBtn_Click" Text="Search" runat="server" />
    </asp:Panel>

    <asp:Panel ID="SortFlightsPanel" ClientIDMode="Static" runat="server">
        <div>
            <span>Sort by:</span>
            <asp:HyperLink NavigateUrl="?orderBy=number" Text="Flight No." runat="server" />
            <asp:HyperLink NavigateUrl="?orderBy=fromAirport" Text="From Airport" runat="server" />
            <asp:HyperLink NavigateUrl="?orderBy=toAirport" Text="To Airport" runat="server" />
        </div>
    </asp:Panel>

    <asp:GridView ID="FlightInfoGridView" runat="server" ClientIDMode="Static"
        ItemType="BalkanAir.Data.Models.Flight"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="20"
        OnPageIndexChanging="FlightInfoGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="Number" HeaderText="Flight No." />
            <asp:TemplateField HeaderText="From">
                <ItemTemplate>
                    <%#: Item.FromAirport.Name %> (<%#: Item.FromAirport.Abbreviation %>)
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To">
                <ItemTemplate>
                    <%#: Item.ToAirport.Name %> (<%#: Item.ToAirport.Abbreviation %>)
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Departure">
                <ItemTemplate>
                    <%#: Item.Departure.ToString("dd/MMMM/yy -> H:mm", CultureInfo.InvariantCulture) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arrival">
                <ItemTemplate>
                    <%#: Item.Arrival.ToString("dd/MMMM/yy -> H:mm", CultureInfo.InvariantCulture) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Duration" HeaderText="Duration" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%#: Item.FlightStatus.Name %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <h4>Sorry, no flights found for this number. Have another look and try again.</h4>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
