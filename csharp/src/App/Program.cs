using Microsoft.Extensions.Configuration;
using Signals.Library.Utility;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                Settings settings = LoadSettings();

                ISignalDemo demo = new SignalDemo(settings);
                await demo.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error-{ex.Message}");
            }
        }

        private static Settings LoadSettings()
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();


            IConfigurationSection section = Configuration.GetSection("Settings");

            Settings settings = new Settings();
            Configuration.GetSection("Settings").Bind(settings);
            settings.ApiPassword = Util.ToSecureString(section["ApiPassword"]);

            if (!Util.Validate(settings))
            {
                throw new Exception("App Settings file doesn't contain required fields");
            }

            return settings;
        }
    }
}