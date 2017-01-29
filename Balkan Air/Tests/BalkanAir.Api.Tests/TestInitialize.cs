namespace BalkanAir.Api.Tests
{
    using System.Reflection;
    using System.Web.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Common;
    using Web.App_Start;

    [TestClass]
    public class TestInitialize
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.BALKAN_AIR_WEB));

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
