namespace BalkanAir.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Common;

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

            if (!context.TravelClasses.Any())
            {
                context.TravelClasses.AddOrUpdate(seedData.TravelClasses.ToArray());
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

            if (!context.Routes.Any())
            {
                context.Routes.AddOrUpdate(seedData.Routes.ToArray());
                context.SaveChanges();
            }

            if (!context.Fares.Any())
            {
                context.Fares.AddOrUpdate(seedData.Fares.ToArray());
                context.SaveChanges();
            }

            if (!context.FlightLegs.Any())
            {
                context.FlightLegs.AddOrUpdate(seedData.FlightLegs.ToArray());
                context.SaveChanges();
            }

            if (!context.LegInstances.Any())
            {
                context.LegInstances.AddOrUpdate(seedData.LegInstances.ToArray());
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(seedData.Categories.ToArray());
            }

            if (!context.Notifications.Any())
            {
                context.Notifications.AddOrUpdate(seedData.Notifications.ToArray());
            }

            if (!context.News.Any())
            {
                context.News.AddOrUpdate(seedData.News.ToArray());
            }

            context.SaveChanges();
        }

        private void SeedRoles(BalkanAirDbContext context)
        {
            foreach (var entity in context.Roles)
            {
                context.Roles.Remove(entity);
            }

            context.Roles.AddOrUpdate(new IdentityRole(UserRolesConstants.ADMINISTRATOR_ROLE));
            context.Roles.AddOrUpdate(new IdentityRole(UserRolesConstants.AUTHENTICATED_USER_ROLE));
        }
    }
}
