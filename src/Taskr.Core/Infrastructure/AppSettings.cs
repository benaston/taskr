using System.Configuration;

namespace Taskr.Core.Infrastructure
{
    public class AppSettings : IAppSettings
    {
        public virtual string this[string key]
        {
            get
            {
                var value = ConfigurationManager.AppSettings[key];
                if (value == null)
                {
                    throw new ConfigurationErrorsException("Configuration value not found for appsetting key \"" + key
                                                           + "\".");
                }

                return value;
            }
        }
    }
}