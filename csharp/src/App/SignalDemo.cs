using Newtonsoft.Json.Linq;
using Signals.Library.Communication;
using Signals.Library.Communication.Models;
using Signals.Library.Models;
using Signals.Library.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace App
{
    class SignalDemo : ISignalDemo
    {
        private ICommunication Communication { get; }

        private Settings Settings { get; } 
        public SignalDemo(Settings settings)
            
        {
            Settings = settings;
            Communication = new Communication(Settings.SafeUrl, Settings.ApiUsername, Settings.ApiPassword);

        }
        public async Task Run()
        {
            try
            {
                string path = string.IsNullOrWhiteSpace(Settings.ExamplesDirectoryPath) ? GetDefaultSampleExamplesPath() : Settings.ExamplesDirectoryPath;
                List<string> filePath = Util.GetAllFilesFromDirectory(path);
                foreach (string file in filePath)
                {
                    try
                    {
                        JObject rawSignal = JObject.Parse(await File.ReadAllTextAsync(file));
                        Signal signal = rawSignal.ToObject<Signal>();

                        SignalResponse resp = await Communication.SubmitSignal(signal);
                        if (bool.Parse(resp.Success))
                        {
                            Console.WriteLine($"Signal-{signal.Name} submitted successfully");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to submit signal-{signal.Name} due to {resp.Message}");
                        }

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error-{ex.Message}");
                    }

                }

            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Error-{ex.Message}");
                throw;
            }

        }

        private static string GetDefaultSampleExamplesPath()
        {
            int index = (Environment.CurrentDirectory).IndexOf("csharp", StringComparison.Ordinal);
            string dirname = (Environment.CurrentDirectory)[..index];
            string defaultPath = Path.Combine(dirname, @"examples\samples");
            return defaultPath;
        }

    }
}
