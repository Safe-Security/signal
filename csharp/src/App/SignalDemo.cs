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
                        if (file.EndsWith(".zip"))
                        {
                            await SubmitZipSignals(file);
                        }
                        else if(file.EndsWith(".json"))
                        {
                            await SubmitSignal(file);
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

        private async Task SubmitSignal(string file)
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


        private async Task SubmitZipSignals(string file)
        {
            SignalResponse resp = await Communication.SubmitSignalZip(file);
            if (bool.Parse(resp.Success))
            {
                Console.WriteLine($"Zip Signal-{file} submitted successfully");
            }
            else
            {
                Console.WriteLine($"Failed to submit Zip signal-{file} due to {resp.Message}");
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
