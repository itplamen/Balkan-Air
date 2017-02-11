<%@ Page Title="View news" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewNews.aspx.cs" Inherits="BalkanAir.Web.ViewNews" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="InvalidNewsPanel" Visible="false" runat="server" CssClass="warningPanel">
        <h5>COULD NOT FIND SUCH NEWS!</h5>
    </asp:Panel>

    <asp:FormView ID="ViewNewsFormView" runat="server"
        ItemType="BalkanAir.Data.Models.News"
        SelectMethod="ViewNewsFormView_GetItem">
        <ItemTemplate>
            <h2><%#: Item.Title %> <small>Category: <%#: Item.Category.Name %></small></h2>

            <p>
                <img src='<%# Item.HeaderImage == null ? null : "data:image/jpeg;base64," + Convert.ToBase64String(Item.HeaderImage) %>'
                    runat="server" visible="<%# Item.HeaderImage != null ? true : false %>" />
            </p>

            <p><%# Item.Content %></p>

            <p>
                <span class="glyphicon glyphicon-calendar"></span>
                <small><%#: Item.DateCreated.ToString("dd.MMMM.yyyy hh:mm", CultureInfo.InvariantCulture) %></small>
            </p>
        </ItemTemplate>
    </asp:FormView>

    <asp:ListView ID="CommentsListView" runat="server"
        ItemType="BalkanAir.Data.Models.Comment"
        DataKeyNames="Id"
        SelectMethod="CommentsListView_GetData"
        InsertMethod="CommentsListView_InsertItem"
        InsertItemPosition="FirstItem">
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
            <div class="comment">
                <p>
                    <span id="CommentPostedFromSpan">
                        <img runat="server" src="<%#: this.GetProfileIconSrc(Item.UserId) %>" 
                            visible="<%# this.GetProfileIconSrc(Item.UserId) == string.Empty ? false : true %>"  />
                        <%#: this.GetAuthorOfTheComment(Item.UserId) %>
                    </span> <br />

                    <small id="DateOfCommentSmall">
                        <span class="glyphicon glyphicon-calendar"></span>
                        <%#: Item.DateOfComment.ToString("dd.MMMM.yyyy hh:mm", CultureInfo.InvariantCulture) %>
                    </small>
                </p>
                <p>
                    <%#: Item.Content %>
                </p>
            </div>
        </ItemTemplate>
        <InsertItemTemplate>
            <div>
                <h3><%# this.NumberOfComments %> Comments:</h3>

                <asp:TextBox ID="CommentTextBox" Width="500" Height="100" runat="server" Text="<%#: BindItem.Content %>"
                    TextMode="MultiLine" placeholder="Add a comment..." />
                <br />

                <asp:Button ID="PostCommentBtn" ClientIDMode="Static" Text="Comment" CommandName="Insert" runat="server"
                    CssClass="btn btn-primary" />
            </div>
        </InsertItemTemplate>
        <EmptyDataTemplate>
            <h4>No news found!</h4>
        </EmptyDataTemplate>
    </asp:ListView>

    <asp:HiddenField ID="NewsIdHiddenField" runat="server" />
</asp:Content>
