namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Common;

    [ComplexType]
    public class UserSettings
    {
        public UserSettings()
        {
            this.DateOfBirth = null;
            this.ReceiveEmailWhenNewNews = true;
            this.ReceiveEmailWhenNewFlight = true;
            this.ReceiveNotificationWhenNewNews = true;
            this.ReceiveNotificationWhenNewFlight = true;
            this.Bookings = new HashSet<Booking>();
        }

        [Column("ProfilePicture")]
        public byte[] ProfilePicture { get; set; }

        [Column("FirstName")]
        [MinLength(ValidationConstants.PASSENGER_NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.PASSENGER_NAME_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [MinLength(ValidationConstants.PASSENGER_NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.PASSENGER_NAME_MAX_LENGTH)]
        public string LastName { get; set; }

        [Column("DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Column("Gender")]
        public Gender Gender { get; set; }

        [Column("IdentityDocumentNumber")]
        [MinLength(ValidationConstants.PASSENGER_IDENTITY_DOCUMENT_ID_MIN_LENGTH)]
        [MaxLength(ValidationConstants.PASSENGER_IDENTITY_DOCUMENT_ID_MAX_LENGTH)]
        public string IdentityDocumentNumber { get; set; }

        [Column("Nationality")]
        public string Nationality { get; set; }

        [Column("FullAddress")]
        public string FullAddress { get; set; }

        [Column("ReceiveEmailWhenNewNews")]
        public bool ReceiveEmailWhenNewNews { get; set; }

        [Column("ReceiveEmailWhenNewFlight")]
        public bool ReceiveEmailWhenNewFlight { get; set; }

        [Column("ReceiveNotificationWhenNewNews")]
        public bool ReceiveNotificationWhenNewNews { get; set; }

        [Column("ReceiveNotificationWhenNewFlight")]
        public bool ReceiveNotificationWhenNewFlight { get; set; }

        [Column("LastLogin", TypeName = "datetime2")]
        public DateTime LastLogin { get; set; }

        [Column("LastLogout", TypeName = "datetime2")]
        public DateTime LastLogout { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
