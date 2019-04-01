using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MVCalculator
{
    public class Program
    {

        public static class Journal
        {
            public static Dictionary<int, List<string>> journal;
        }

        public static void Main(string[] args)
        {
            Program.Journal.journal = new Dictionary<int, List<string>> { };

            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = "log.txt",
                ArchiveEvery = NLog.Targets.FileArchivePeriod.Day, 
                Layout = "${longdate} | ${level:uppercase=true} | ${message}"
            };

            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);
            
            NLog.LogManager.Configuration = config;


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
