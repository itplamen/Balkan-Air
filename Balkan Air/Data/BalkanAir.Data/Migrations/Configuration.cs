namespace BalkanAir.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BalkanAirDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BalkanAirDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.SeedRoles(context);
            }

            var seedData = new SeedData(context);
              

            if (!context.AircraftManufacturers.Any())
            {
                context.AircraftManufacturers.AddOrUpdate(seedData.AircraftManufacturers.ToArray());
            }

            if (!context.Aircraft.Any())
            {
                context.Aircraft.AddOrUpdate(seedData.Aircrafts.ToArray());
            }

            if (!context.Countries.Any())
            {
                context.Countries.AddOrUpdate(seedData.Countries.ToArray());
            }

            if (!context.Airports.Any())
            {
                context.Airports.AddOrUpdate(seedData.Airports.ToArray());
            }

            if (!context.FlightStatuses.Any())
            {
                context.FlightStatuses.AddOrUpdate(seedData.FlightStatuses.ToArray());
            }

            if (!context.Flights.Any())
            {
                context.Flights.AddOrUpdate(seedData.Flights.ToArray());
                context.SaveChanges();
            }

            if (!context.TravelClasses.Any())
            {
                context.TravelClasses.AddOrUpdate(seedData.TravelClasses.ToArray());
                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                seedData.SeedSeats();
                context.Seats.AddOrUpdate(seedData.Seats.ToArray());
                context.SaveChanges();

            }

            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(seedData.Categories.ToArray());
            }

            if (!context.Articles.Any())
            {
                context.Articles.AddOrUpdate(seedData.Articles.ToArray());
            }

            if (!context.Notifications.Any())
            {
                context.Notifications.AddOrUpdate(seedData.Notifications.ToArray());
            }

            context.SaveChanges();
        }

        private void SeedRoles(BalkanAirDbContext context)
        {
            foreach (var entity in context.Roles)
            {
                context.Roles.Remove(entity);
            }

            context.Roles.AddOrUpdate(new IdentityRole(ValidationConstants.ADMINISTRATOR_ROLE));
        }
    }
}
