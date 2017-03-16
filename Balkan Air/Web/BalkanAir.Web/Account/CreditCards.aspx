<%@ Page Title="Credit Cards" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditCards.aspx.cs" Inherits="BalkanAir.Web.Account.CreditCards" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="CreditCardsListView" runat="server"
        DataKeyNames="Id"
        ItemType="BalkanAir.Data.Models.CreditCard"
        SelectMethod="CreditCardsListView_GetData"
        DeleteMethod="CreditCardsListView_DeleteItem">
        <LayoutTemplate>
            <div runat="server" id="itemPlaceholder"></div>
            <asp:DataPager runat="server" PageSize="10">
                <Fields>
                    <asp:NextPreviousPagerField ShowPreviousPageButton="true" ShowNextPageButton="false"
                        ButtonCssClass="btn btn-success" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="false" ShowNextPageButton="true"
                        ButtonCssClass="btn btn-success" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="creditCardDiv">
                <p>Number: <span><%#: Item.Number %></span></p>
                <p>Name on card: <span><%#: Item.NameOnCard %></span></p>
                <p>Expiration: <span><%#: Item.ExpirationMonth + "/" + Item.ExpirationYear %></span></p>
                <p>CVV: <span><%#: Item.CvvNumber %></span></p>
                <asp:Button Text="Delete" CommandName="Delete" runat="server" CssClass="btn btn-danger" />
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:Panel ID="NoSavedCreditCardsPanel" runat="server" CssClass="warningPanel">
                <h5>YOU DON'T HAVE ANY SAVED CREDIT CARDS!</h5>
            </asp:Panel>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
