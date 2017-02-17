<%@ Page Title="News Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.NewsManagement" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <div>
        <asp:CustomValidator ErrorMessage="" ID="ImageValidator" runat="server" ValidationGroup="image" Display="None" />
    </div>

    <asp:ListView ID="NewsListView" runat="server"
        ItemType="BalkanAir.Data.Models.News"
        DataKeyNames="Id"
        SelectMethod="NewsListView_GetData"
        UpdateMethod="NewsListView_UpdateItem"
        DeleteMethod="NewsListView_DeleteItem"
        InsertMethod="NewsListView_InsertItem"
        InsertItemPosition="None">
        <LayoutTemplate>
            <asp:HyperLink NavigateUrl="?orderBy=title" Text="Sort by Title" runat="server" CssClass="btn btn-md-2 btn-default" />
            <asp:HyperLink NavigateUrl="?orderBy=date" Text="Sort by Date" runat="server" CssClass="btn btn-md-2 btn-default" />
            <asp:HyperLink NavigateUrl="?orderBy=category" Text="Sort by Category" runat="server" CssClass="btn btn-md-2 btn-default" />

            <div runat="server" id="itemPlaceholder"></div>
            <asp:Button ID="AddNewsButton" Text="Add news" runat="server" CssClass="btn btn-info pull-right"
                OnClick="AddNewsButton_Click" />
            <br />
            <br />

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
            <div class="newsItemTemplate">
                <h2>
                    <%#: Item.Title %> <small>Category: <%#: Item.Category != null ? Item.Category.Name : "" %></small>
                    <asp:Button Text="Edit" CommandName="Edit" runat="server" CssClass="btn btn-info" />
                    <asp:Button Text="Delete" CommandName="Delete" runat="server" CssClass="btn btn-danger" />
                </h2>

                <img src='<%# Item.HeaderImage == null ? null : "data:image/jpeg;base64," + Convert.ToBase64String(Item.HeaderImage) %>'
                    alt="NO NEWS IMAGE!" />
                <p><%# Item.Content %></p>
                <p><strong>Is Deteled: <%#: Item.IsDeleted %></strong></p>
                <small>Created on: <%#: Item.DateCreated.ToString("dd.MMM.yyyy", CultureInfo.InvariantCulture) %></small>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <h2>
                <asp:TextBox runat="server" ID="TitleTextBox" Text="<%#: BindItem.Title %>" />
                <asp:Button Text="Save" CommandName="Update" runat="server" CssClass="btn btn-success" />
                <asp:Button Text="Cancel" CommandName="Cancel" runat="server" CssClass="btn btn-danger" />
            </h2>
            <p>
                <asp:DropDownList ID="CategoriesDropDownList" runat="server"
                    ItemType="BalkanAir.Data.Models.Category"
                    DataValueField="Id"
                    DataTextField="Name"
                    SelectedValue="<%#: BindItem.CategoryId %>"
                    SelectMethod="CategoriesDropDownList_GetData" />
            </p>
            <p>
                <asp:FileUpload ID="UploadImageEdit" runat="server" />
            </p>
            <p>
                <asp:TextBox runat="server" ID="ContentTextBox" Text="<%# BindItem.Content %>" TextMode="MultiLine" Rows="10" Width="100%" />
            </p>
            <p>
                <asp:CheckBox ID="IsDeletedCheckBox" Text="Is Deleted" Checked="<%# BindItem.IsDeleted  %>" runat="server" />
            </p>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div>
                <asp:RequiredFieldValidator ErrorMessage="Title is required!" ControlToValidate="TitleInsertTextBox" runat="server"
                    ForeColor="Red" Display="Dynamic" ValidationGroup="AddNews" />
            </div>

            <div>
                <asp:RequiredFieldValidator ErrorMessage="Content is required!" ControlToValidate="ContentInsertEditor" runat="server"
                    ForeColor="Red" Display="Dynamic" ValidationGroup="AddNews" />
            </div>

            <p>
                <h3>Title:</h3>
                <asp:TextBox ID="TitleInsertTextBox" Text="<%#: BindItem.Title %>" runat="server" />
            </p>

            <div id="UploadPictureDivBox">
                <h3>Image:</h3>
                <p id="MaximumFilzeSizeAndType">
                    <span>Maximum file size: 1 MB</span>
                    <br />
                    <span>Allowed file types: jpg, jpeg, png, gif</span>
                </p>
                <asp:FileUpload ID="UploadImage" runat="server" />
            </div>

            <p>
                <h3>Category:</h3>
                <asp:DropDownList ID="CategoriesInsertDropDownList" runat="server"
                    ItemType="BalkanAir.Data.Models.Category"
                    DataValueField="Id"
                    DataTextField="Name"
                    SelectedValue="<%#: BindItem.CategoryId %>"
                    SelectMethod="CategoriesDropDownList_GetData" />
            </p>
            <p>
                <h3>Content:</h3>
                <ajaxToolkit:HtmlEditor.Editor ID="ContentInsertEditor" Height="300px" Width="100%"
                    AutoFocus="true" runat="server" Content="<%#: BindItem.Content %>" />
            </p>

            <asp:Button Text="Insert" CommandName="Insert" runat="server" CssClass="btn btn-success" />
            <asp:Button ID="CancelButton" OnClick="CancelButton_Click" Text="Cancel" runat="server" CssClass="btn btn-danger" />
        </InsertItemTemplate>
        <EmptyDataTemplate>
            <h4>No news found!</h4>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
