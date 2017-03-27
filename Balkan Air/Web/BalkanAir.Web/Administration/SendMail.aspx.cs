namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Common;
    using Services.Data.Contracts;

    public partial class SendMail : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected string SuccessMessage { get; private set; }

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

        protected void SendMailButton_Click(object sender, EventArgs e)
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

                var recipients = this.UsersServices.GetAll()
                    .Where(u => usersId.Contains(u.Id))
                    .Select(u => u.Email)
                    .ToList();

                var mailSender = MailSender.Instance;
                var recipient = string.Empty;

                if (recipients.Count > 1)
                {
                    recipient = "itplamen@gmail.com";
                }
                else
                {
                    recipient = recipients[0];
                    recipients = null;
                }

                mailSender.SendMail(recipient, this.SubjectTextBox.Text, this.ContentHtmlEditor.Content, recipients);
                this.SuccessPanel.Visible = true;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.UsersListBox.ClearSelection();
            this.SubjectTextBox.Text = string.Empty;
            this.ContentHtmlEditor.Content = string.Empty;
        }
    }
}