<%@ Page Title="Send Mail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="BalkanAir.Web.Administration.SendMail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert" 
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        Message sent successfully!
    </asp:Panel>

    <div id="SendMailDiv">
        <h4>Recipients:</h4>
        <asp:ListBox ID="UsersListBox" runat="server" SelectionMode="Multiple" required
            DataValueField="Id"
            DataTextField="UserInfo"
            SelectMethod="UsersListBox_GetData" />

        <h4>Subject:</h4>
        <asp:TextBox ID="SubjectTextBox" runat="server" CssClass="form-control" ClientIDMode="Static" required />

        <h4>Content:</h4>
        <ajaxToolkit:HtmlEditor.Editor ID="ContentHtmlEditor" Height="300px" Width="100%" AutoFocus="true" runat="server" required />

        <div id="MailButtonsDiv">
            <asp:Button ID="SendMailButton" runat="server" UseSubmitBehavior="true" Text="Send" CssClass="btn btn-info" OnClick="SendMailButton_Click" />
            <asp:Button ID="CancelButton" runat="server" UseSubmitBehavior="false" Text="Cancel" CssClass="btn btn-danger" OnClick="CancelButton_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $('[id*=UsersListBox]').multiselect({
                enableFiltering: true,
                includeSelectAllOption: true,
                enableCollapsibleOptGroups: true,
                maxHeight: 300,
                buttonWidth: '350px'
            });
        });
    </script>
</asp:Content>
