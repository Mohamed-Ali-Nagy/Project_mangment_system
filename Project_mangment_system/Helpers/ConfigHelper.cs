using Microsoft.Extensions.Configuration;

namespace Project_management_system.Helpers
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetAppSetting(string key)
        {
            return _configuration?[key];
        }
    }
}
