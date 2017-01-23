namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class User : IdentityUser
    {
        public User()
        {
            this.CreatedOn = DateTime.Now;
            this.UserSettings = new UserSettings();
            this.CreditCards = new HashSet<CreditCard>();
            this.UserNotification = new HashSet<UserNotification>();
        }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public UserSettings UserSettings { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<CreditCard> CreditCards { get; set; }

        public virtual ICollection<UserNotification> UserNotification { get; set; }

        [NotMapped]
        public bool AreProfileDetailsFilled
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.UserSettings.FirstName) || string.IsNullOrWhiteSpace(this.UserSettings.LastName) ||
                    this.UserSettings.DateOfBirth == null || string.IsNullOrWhiteSpace(this.UserSettings.IdentityDocumentNumber) ||
                    string.IsNullOrWhiteSpace(this.PhoneNumber) || string.IsNullOrWhiteSpace(this.UserSettings.FullAddress))
                {
                    return false;
                }

                return true;
            }
        }

        [NotMapped]
        public int NumberOfUnreadNotifications
        {
            get
            {
                return this.UserNotification
                    .Count(n => !n.IsRead);
            }
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }
}
