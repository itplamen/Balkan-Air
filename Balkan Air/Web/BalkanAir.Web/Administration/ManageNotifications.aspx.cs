namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using BalkanAir.Services.Data.Contracts;
    using Data.Models;

    public partial class ManageNotifications : Page
    {
        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Notification> ManageNotificationsGridView_GetData()
        {
            return this.NotificationsServices.GetAll();
        }

        public void ManageNotificationsGridView_UpdateItem(int id)
        {
            var notification = this.NotificationsServices.GetNotification(id);
            if (notification == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(notification);
            if (ModelState.IsValid)
            {
                this.NotificationsServices.UpdateNotification(id, notification);
            }
        }

        public void ManageNotificationsGridView_DeleteItem(int id)
        {
            this.NotificationsServices.DeleteNotification(id);
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

        protected void CreateNotificationtBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var notification = new Notification()
                {
                    Content = this.ContentTextBox.Text,
                    DateCreated = DateTime.Now,
                    Url = this.UrlTextBox.Text
                };

                int notificationId = this.NotificationsServices.AddNotification(notification);

                if (this.UsersListBox.SelectedIndex != -1)
                {
                    var usersId = this.GetSelectedUsers();

                    if (usersId != null && usersId.Count > 0)
                    {
                        this.UserNotificationsServices.SendNotification(notificationId, usersId);
                    }
                }
            }
        }

        private ICollection<string> GetSelectedUsers()
        {
            var usersId = new List<string>();

            foreach (ListItem item in this.UsersListBox.Items)
            {
                if (item.Selected)
                {
                    usersId.Add(item.Value);
                }
            }

            if (usersId.Count == 0)
            {
                return null;
            }

            return this.UsersServices.GetAll()
                .Where(u => usersId.Contains(u.Id))
                .Select(u => u.Id)
                .ToList();
        }
    }
}