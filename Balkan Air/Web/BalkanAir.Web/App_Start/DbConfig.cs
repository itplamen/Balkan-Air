namespace BalkanAir.Web.App_Start
{
    using System.Data.Entity;

    using BalkanAir.Data;
    using BalkanAir.Data.Migrations;

    public class DbConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BalkanAirDbContext, Configuration>());
            BalkanAirDbContext.Create().Database.Initialize(true);
        }
    }
}