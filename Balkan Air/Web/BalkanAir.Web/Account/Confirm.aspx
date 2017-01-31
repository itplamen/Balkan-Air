<%@ Page Title="Account Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="BalkanAir.Web.Account.Confirm" Async="true" %>

<%@ Import Namespace="BalkanAir.Web.Common" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: this.Page.Title %></h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
            <p>
                Thank you for confirming your account. Click <a href="<%= Page.ResolveUrl(Pages.ACCOUNT) %>">here</a> to check your profile.             
            </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="text-danger">
                An error has occurred.
            </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
