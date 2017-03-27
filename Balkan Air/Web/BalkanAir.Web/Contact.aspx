<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BalkanAir.Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <address>
        If you find a bug, or you have idea for new feature, contact me.
    </address>

    <address>
        <strong>Developer profile:</strong> <a target="_blank" href="https://github.com/itplamen">GitHub</a><br />
        <strong>Email:</strong> <a href="mailto:itplamen@gmail.com">itplamen@gmail.com</a>
    </address>
</asp:Content>
