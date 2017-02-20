namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class UserNotificationsManagement : Page
    {
        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected string SuccessMessage { get; private set; }

        public IQueryable<UserNotification> UserNotificationsGridView_GetData()
        {
            return this.UserNotificationsServices.GetAll()
                .OrderBy(un => un.UserId)
                .ThenBy(un => un.Id);
        }

        public void UserNotificationsGridView_UpdateItem(int id)
        {
            var userNotification = this.UserNotificationsServices.GetUserNotification(id);
            
            if (userNotification == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(userNotification);

            if (ModelState.IsValid)
            {
                this.UserNotificationsServices.UpdateUserNotification(id, userNotification);
            }
        }

        // Set notification as read instead of deleting it.
        public void UserNotificationsGridView_DeleteItem(int id, string userId)
        {
            this.UserNotificationsServices.SetNotificationAsRead(id, userId);
        }

        public IQueryable<object> UsersListBox_GetData()
        {
            var users = this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted)
                .OrderBy(u => u.Email)
                .Select(u => new
                {
                    Id = u.Id,
                    UserInfo = string.IsNullOrEmpty(u.UserSettings.FirstName) && string.IsNullOrEmpty(u.UserSettings.LastName) ?
                        u.Email + ", (Name not set)" : u.Email + ", (" + u.UserSettings.FirstName + " " + u.UserSettings.LastName + ")"
                });

            return users;
        }

        public IQueryable<object> NotificationsDropDownList_GetData()
        {
            var notifications = this.NotificationsServices.GetAll()
                .Where(n => !n.IsDeleted)
                .Select(n => new
                {
                    Id = n.Id,
                    NotificationInfo = "Id: " + n.Id + ", Type: " + n.Type + ", Content: " + n.Content.Substring(0, 40) + "..."
                });

            return notifications;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SendNotificationButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var usersId = new List<string>();

                foreach (ListItem item in this.UsersListBox.Items)
                {
                    if (item.Selected)
                    {
                        usersId.Add(item.Value);
                    }
                }

                int notificationId = int.Parse(this.NotificationsDropDownList.SelectedItem.Value);

                this.UserNotificationsServices.SendNotification(notificationId, usersId);
                this.SuccessPanel.Visible = true;

                this.ClearFields();
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.UsersListBox.ClearSelection();
            this.NotificationsDropDownList.SelectedIndex = 0;
        }
    }
}