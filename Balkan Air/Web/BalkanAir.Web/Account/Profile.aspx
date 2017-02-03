﻿<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="BalkanAir.Web.Account.Profile" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <div class="errorMessages">
        <div>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="First name is required!"
                ControlToValidate="FirstNameTextBox" Display="Dynamic" />
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Last name is required!"
                ControlToValidate="LastNameTextBox" Display="Dynamic" />
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Date of birth is required!"
                ControlToValidate="DatepickerTextBox" Display="Dynamic" />
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Identity document number is required!"
                ControlToValidate="IdentityDocumentNumberTextBox" Display="Dynamic" />
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Phone number is required!"
                ControlToValidate="PhoneNumberTextBox" Display="Dynamic" />
        </div>

        <div>
            <asp:RegularExpressionValidator runat="server" ErrorMessage="Invalid phone number!" ValidationExpression="^\d+$"
                ControlToValidate="PhoneNumberTextBox" Display="Dynamic" />
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ErrorMessage="Address is required!"
                ControlToValidate="FullAddressTextBox" Display="Dynamic" />
        </div>
    </div>

    <div id="PersonalDetailsDiv">
        <h4>PERSONAL DETAILS</h4>

        <div class="fancyTextBox">
            <asp:Label Text="FIRST NAME" runat="server" AssociatedControlID="FirstNameTextBox" />
            <asp:TextBox runat="server" ID="FirstNameTextBox" ClientIDMode="Static" />
        </div>

        <div class="fancyTextBox">
            <asp:Label Text="LAST NAME" runat="server" AssociatedControlID="LastNameTextBox" />
            <asp:TextBox runat="server" ID="LastNameTextBox" ClientIDMode="Static" />
        </div>

        <div class=" fancyTextBox">
            <asp:Label Text="DATE OF BIRTH" runat="server" AssociatedControlID="DatepickerTextBox" />
            <asp:TextBox runat="server" ID="DatepickerTextBox" ClientIDMode="Static" />
        </div>

        <div class="fancyTextBox">
            <asp:Label Text="GENDER" runat="server" AssociatedControlID="GenderDropDownList" />
            <asp:DropDownList runat="server" ID="GenderDropDownList" />
        </div>

        <div class="fancyTextBox">
            <asp:Label Text="NATIONALITY" runat="server" AssociatedControlID="NationalityDropDownList" />
            <asp:DropDownList runat="server" ID="NationalityDropDownList" />
        </div>

        <div class="fancyTextBox">
            <asp:Label Text="IDENTITY DOCUMENT NUMBER" runat="server" AssociatedControlID="IdentityDocumentNumberTextBox" />
            <asp:TextBox runat="server" ID="IdentityDocumentNumberTextBox" ClientIDMode="Static" />
        </div>
    </div>

    <div id="ContactDetailsDiv">
        <h4>CONTACT DETAILS</h4>

        <div class="fancyTextBox">
            <asp:Label Text="PHONE NUMBER" runat="server" AssociatedControlID="PhoneNumberTextBox" />
            <asp:TextBox runat="server" ID="PhoneNumberTextBox" ClientIDMode="Static" />
        </div>

        <div class="fancyTextBox">
            <asp:Label Text="FULL ADDRESS" runat="server" AssociatedControlID="FullAddressTextBox" />
            <asp:TextBox runat="server" ID="FullAddressTextBox" />
        </div>
    </div>

    <div class="buttonBox">
        <asp:Button ID="SavePersonalInfoDataBtn" ClientIDMode="Static" Text="SAVE" runat="server"
            OnClick="SavePersonalInfoDataBtn_Click" />
    </div>
</asp:Content>


