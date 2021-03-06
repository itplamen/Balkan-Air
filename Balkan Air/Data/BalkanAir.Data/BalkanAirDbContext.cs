﻿namespace BalkanAir.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class BalkanAirDbContext : IdentityDbContext<User>, IBalkanAirDbContext
    {
        public BalkanAirDbContext()
            : this("DefaultConnection")
        {
        }

        public BalkanAirDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual IDbSet<Aircraft> Aircraft { get; set; }

        public virtual IDbSet<AircraftManufacturer> AircraftManufacturers { get; set; }

        public virtual IDbSet<Airport> Airports { get; set; }

        public virtual IDbSet<News> News { get; set; }

        public virtual IDbSet<Baggage> Baggages { get; set; }

        public virtual IDbSet<Booking> Booking { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<CreditCard> CreditCards { get; set; }

        public virtual IDbSet<Fare> Fares { get; set; }

        public virtual IDbSet<Flight> Flights { get; set; }

        public virtual IDbSet<FlightLeg> FlightLegs { get; set; }

        public virtual IDbSet<LegInstance> LegInstances { get; set; }

        public virtual IDbSet<FlightStatus> FlightStatuses { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public virtual IDbSet<Route> Routes { get; set; }

        public virtual IDbSet<Seat> Seats { get; set; }

        public virtual IDbSet<TravelClass> TravelClasses { get; set; }

        public virtual IDbSet<UserNotification> UserNotifications { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public static BalkanAirDbContext Create()
        {
            return new BalkanAirDbContext();
        }

        // Use to solve the error "FOREIGN KEY constraint may cause cycles or multiple cascade paths..." 
        // about Route and Airport relationship.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use fluent api to specify the actions the error message suggests.
            modelBuilder
                .Entity<Airport>()
                .HasMany(i => i.Origins)
                .WithRequired()
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<Airport>()
                .HasMany(i => i.Destinations)
                .WithRequired()
                .WillCascadeOnDelete(false);

            // Another option is to remove all CASCADE DELETES.
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
