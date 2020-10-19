namespace Availability.Infrastructure.Data.Configuration
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class map registration
    /// </summary>
    public class ClassMapRegistration
    {
        /// <summary>
        /// Loads class map registration
        /// </summary>
        public void Load()
        {
            var classMaps = typeof(IClassMapRegistration).Assembly.GetTypes().Where(x => x.IsClass && x.GetInterface(nameof(IClassMapRegistration)) != null);
            foreach (var classMap in classMaps)
            {
                var instantiatedTypes = (IClassMapRegistration)Activator.CreateInstance(classMap);
                instantiatedTypes.Load();
            }
        }
    }
}
