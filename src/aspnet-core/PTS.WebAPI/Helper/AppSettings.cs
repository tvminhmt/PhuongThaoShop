using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace App.Helper
{
    public class AppSettings
    {
        private static AppSettings _appSettings;
        public string AppSettingValue { get; set; }
        public static string AppSetting(string key)
        {
            _appSettings = GetCurrentSettings(key);
            return _appSettings.AppSettingValue;
        }

        public AppSettings(IConfiguration config, string key)
        {
            this.AppSettingValue = config.GetValue<string>(key);
        }

        public static AppSettings GetCurrentSettings(string key)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new AppSettings(configuration.GetSection("AppSettings"), key);

            return settings;
        }
        public static string RootPath = !string.Equals(AppSetting("ROOT_PATH"), null, StringComparison.Ordinal) ? AppSetting("ROOT_PATH") : "";
        public static string ApiRootPath = !string.Equals(AppSetting("API_ROOT_PATH"), null, StringComparison.Ordinal) ? AppSetting("API_ROOT_PATH") : "";
        public static string Domain = !string.Equals(AppSetting("Domain"), null, StringComparison.Ordinal) ? AppSetting("Domain") : "";
        public static string EmailHost = !string.Equals(AppSetting("EmailHost"), null, StringComparison.Ordinal) ? AppSetting("EmailHost") : "smtp.gmail.com";
        public static string EmailUserName = !string.Equals(AppSetting("EmailUserName"), null, StringComparison.Ordinal) ? AppSetting("EmailUserName") : "theuniverse500.company@gmail.com";
        public static string EmailPassword = !string.Equals(AppSetting("EmailPassword"), null, StringComparison.Ordinal) ? AppSetting("EmailPassword") : "nlfclqbagoqsqosn";
        public static string DomainAdmin = !string.Equals(AppSetting("Domain"), null, StringComparison.Ordinal) ? AppSetting("Domain") : "";
        public static string EmailHostAdmin = !string.Equals(AppSetting("EmailHost"), null, StringComparison.Ordinal) ? AppSetting("EmailHost") : "smtp.gmail.com";
        public static string EmailUserNameAdmin = !string.Equals(AppSetting("EmailUserName"), null, StringComparison.Ordinal) ? AppSetting("EmailUserName") : "theuniverse500.company@gmail.com";
        public static string EmailPasswordAdmin = !string.Equals(AppSetting("EmailPassword"), null, StringComparison.Ordinal) ? AppSetting("EmailPassword") : "nlfclqbagoqsqosn";
    }
}
