namespace BalkanAir.Web.Infrastructure
{
    using Ninject;

    /// <summary>
    /// Use when cannot make dependency injection and you need instance from certain type.
    /// </summary>
    /// <example>
    /// ObjectFactory.Get<IRepository>();
    /// ObjectFactory.Get<INewsServices>();
    /// </example>
    public static class ObjectFactory
    {
        private static IKernel savedKernel;

        public static void Initialize(IKernel kernel)
        {
            savedKernel = kernel;
        }

        public static T Get<T>()
        {
            return savedKernel.Get<T>();
        }
    } 
}