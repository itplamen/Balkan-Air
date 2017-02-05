<%@ Page Title="Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="BalkanAir.Web.Booking.Payment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="FilledProfileDetailsRequiredPanel" CssClass="warningPanel" Visible="false" runat="server">
        <h5>YOU NEED TO FILL ALL PROFILE DETAILS TO CONTINUE.</h5>

        <asp:Button ID="GoToProfileBtn" ClientIDMode="Static" Text="GO TO PROFILE" runat="server" UseSubmitBehavior="false"
            PostBackUrl="~/Account/Profile" />
    </asp:Panel>

    <asp:Panel ID="PaymentDonePanel" ClientIDMode="Static" Visible="false" runat="server">
        <h5>THE PAYMENT WAS SUCCESSFUL!!! YOU WILL BE REDIRECTED TO YOUR PROFILE!!!</h5>
        <asp:Image ImageUrl="~/Content/Images/splash.gif" runat="server" />
    </asp:Panel>

    <asp:Panel ID="PaymentDetailsPanel" Visible="false" runat="server">
        <div id="BillingDetailsDiv">
            <h3>Billing details</h3>

            <div class="fancyTextBox">
                <asp:Label CssClass="label" runat="server" Text="First name" AssociatedControlID="FirstNamePaymentTextBox" />
                <asp:TextBox ID="FirstNamePaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
            </div>

            <div class="fancyTextBox">
                <asp:Label CssClass="label" runat="server" Text="Last name" AssociatedControlID="LastNamePaymentTextBox" />
                <asp:TextBox ID="LastNamePaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
            </div>
            <br />

            <div class="fancyTextBox">
                <asp:Label CssClass="label" runat="server" Text="E-mail" AssociatedControlID="EmailPaymentTextBox" />
                <asp:TextBox ID="EmailPaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
            </div>
            <br />

            <div class="fancyTextBox">
                <asp:Label CssClass="label" runat="server" Text="Address" AssociatedControlID="AddressPaymentTextBox" />
                <asp:TextBox ID="AddressPaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
            </div>
            <br />

            <div class="fancyTextBox">
                <asp:Label CssClass="label" runat="server" Text="Phone number" AssociatedControlID="PhoneNumberPaymentTextBox" />
                <asp:TextBox ID="PhoneNumberPaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
            </div>
            <br />

            <div class="fancyTextBox">
                <asp:Label CssClass="label" runat="server" Text="Nationality" AssociatedControlID="NationalityDropDownList" />
                <asp:DropDownList ID="NationalityDropDownList" runat="server"
                    ItemType="BalkanAir.Data.Models.Country"
                    DataValueField="Id"
                    DataTextField="Name" />

            </div>
        </div>

        <div id="PaymentOptionsDiv">
            <h3>Payment options</h3>

            <div id="AcceptedCardsDiv">
                <span id="AcceptedCardsSpan">ACCEPTED CARDS:</span>
                <img src="../Content/Images/acceted-cards-image.png" id="AcceptedCardsImage" alt="Accepted Cards Image" />
            </div>

            <div id="CreditCardDetailsDiv">
                <div class="fancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="Card number" AssociatedControlID="CardNumberPaymentTextBox" />
                    <asp:TextBox ID="CardNumberPaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
                </div>
                <br />

                <div class="fancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="Name on card" AssociatedControlID="NameOnCardPaymentTextBox" />
                    <asp:TextBox ID="NameOnCardPaymentTextBox" runat="server" ClientIDMode="Static" required="required" />
                </div>
                <br />

                <div id="CardDateExpirationDiv" class="fancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="MM/" AssociatedControlID="MonthsPaymentDropDown" />
                    <asp:Label CssClass="label" runat="server" Text="YY" AssociatedControlID="YearsPaymentDropDown" />
                    <br />

                    <asp:DropDownList ID="MonthsPaymentDropDown" ClientIDMode="Static" runat="server" />
                    <asp:DropDownList ID="YearsPaymentDropDown" ClientIDMode="Static" runat="server" />
                </div>

                <div class="fancyTextBox">
                    <asp:Label CssClass="label" runat="server" Text="CVC2/CVV" AssociatedControlID="CvvPaymentTextBox" />
                    <asp:TextBox ID="CvvPaymentTextBox" TextMode="Password" runat="server" ClientIDMode="Static" required="required"
                        MaxLength="3" pattern=".{3,}" title="Enter exactly 3 numbers!" />
                </div>
                <br />

                <div id="TotalPriceDiv">
                    <asp:Label ID="TotalPriceLabel" Text="text" runat="server" />
                </div>

                <div id="SaveCreditCardDiv">
                    <asp:CheckBox ID="SaveCreditCardCheckBox" ClientIDMode="Static" runat="server" />
                    <asp:Label runat="server" AssociatedControlID="SaveCreditCardCheckBox" Text="Save credit card" />
                </div>
            </div>
        </div>

        <div id="PayAndBookNowDiv">
            <asp:Button ID="PayAndBookNowBtn" Text="PAY AND BOOK NOW" runat="server" ClientIDMode="Static" 
                OnClick="PayAndBookNowBtn_Click" />
        </div>
    </asp:Panel>
</asp:Content>
