<%@ Page Title="Developers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Developers.aspx.cs" Inherits="BalkanAir.Web.Developers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome!</h2>

    <p>You are a pioneer of our new API. If you find a mistake, or can't find what you're looking for, let us know.</p>

    <p>
        The documentation is in two sections. 'Getting Started' tells you everything you need to know to make a request via a REST 
        client or our API Playground. It gives an overview of how our requests are structured and what you can expect as a response. 
        If you've used a REST API before then you'll know most of this already. The 'Method Details' section describes the possible 
        input and output fields for every service.
    </p>

    <p>
        Subscribing to our Public Plan will allow you to make a limited number of requests to the API. This should be sufficient to 
        explore the API and its contents. Once you develop an application for serious use, contact us so that we can raise your call 
        quota.
    </p>

    <p>
        We also offer a range of additional services to our commercial partners via an extended Partner Plan. Please contact us for 
        further information on available services.
    </p>
</asp:Content>
