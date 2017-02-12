<%@ Page Title="Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="BalkanAir.Web.Account.Settings" %>

<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <div id="UserSettingsDiv">
        <div class="row">
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Email</strong>
                </div>
                <div class="col-xs-7">
                    <span class="text-white"><%: this.GetUserEmail() %></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Email confirmed?</strong>
                </div>
                <div class="col-xs-7">
                    <span class="text-white">
                        <asp:Literal ID="IsEmailConfirmedLiteral" runat="server" />
                        <asp:LinkButton ID="SendConfirmationEmailLinkButton" Visible="false" runat="server"
                            Text="Send me another confirmation email." />
                    </span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Password</strong>
                </div>
                <div class="col-xs-7">
                    <a href="<%= Page.ResolveUrl(Pages.CHANGE_PASSWORD) %>">Change your password</a>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Registration</strong>
                </div>
                <div class="col-xs-7">
                    <span class="text-white">aaaaaaaaaaaaa12.11.2011 22:00</span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Receive email when there is a new news</strong>
                </div>
                <div class="col-xs-7">
                    <input id="ReceiveEmailWhenNewNewsCheckBox" runat="server" type="checkbox" checked data-toggle="toggle" 
                        data-size="mini" data-style="ios" data-width="80" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Receive email when there is a new flight</strong>
                </div>
                <div class="col-xs-7">
                    <input id="ReceiveEmailWhenNewFlightCheckBox" runat="server" type="checkbox" checked data-toggle="toggle" 
                        data-size="mini" data-style="ios" data-width="80" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Receive notification when there is a new news</strong>
                </div>
                <div class="col-xs-7">
                    <input id="ReceiveNotificationWhenNewNewsCheckBox" runat="server" type="checkbox" checked data-toggle="toggle" 
                        data-size="mini" data-style="ios" data-width="80" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <strong class="text-primary">Receive notification when there is a new flight</strong>
                </div>
                <div class="col-xs-7">
                    <input id="ReceiveNotificationWhenNewFlightCheckBox" runat="server" type="checkbox" checked data-toggle="toggle" 
                        data-size="mini" data-style="ios" data-width="80" />
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 text-right">
                    <asp:Button ID="SaveSettingsBtn" runat="server" Text="Save" UseSubmitBehavior="true" CssClass="btn btn-primary" 
                        OnClick="SaveSettingsBtn_Click" />
                </div>
                <div class="col-md-7 text-left">
                    <a href="<%= Page.ResolveUrl(Pages.ACCOUNT) %>" class="btn btn-danger">Cancel</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
