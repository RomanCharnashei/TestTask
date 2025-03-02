using Ocelot.DependencyInjection;
using Ocelot.Errors;
using Ocelot.Errors.QoS;
using Ocelot.Middleware;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TestTask.Proxy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json")
                .AddEnvironmentVariables();

            builder.Services.AddOcelot(builder.Configuration)
                .AddDelegatingHandler<RetryHandler>();

            var app = builder.Build();

            await app.UseOcelot();

            if (app.Environment.IsDevelopment())
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                StartAngularProject(logger);
            }

            app.Run();
        }

        private static void StartAngularProject(ILogger logger)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            if (tcpConnInfoArray.Any(x => x.LocalEndPoint.Port == 4200))
            {
                return;
            }
            else
            {
                try
                {
                    string angularProjectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../TestTask.Client"));

                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c npm start",
                        UseShellExecute = true,
                        CreateNoWindow = false,
                        WorkingDirectory = angularProjectPath
                    };

                    var process = new Process
                    {
                        StartInfo = processStartInfo
                    };

                    process.Start();

                    logger.LogInformation("Angular project has started.");
                }
                catch (Exception ex)
                {
                    logger.LogError("Failed to start Angular project: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
