using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace server_app
{
    internal class Program : Hub
    {
        static void Main(string[] args)
        {
            //startNginx();
            configServer(args).Run();
        }
        private static WebApplication configServer(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseRouting();
            app.UseCors("AllowAll");
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                await next.Invoke();
            });

            app.MapHub<connections.connection>("/cs-nea/connections");
            app.MapHub<accounts>("/cs-nea/accounts");


            // binds to all address on port 3900
            app.Urls.Add("http://0.0.0.0:3900");

            return app;
        }
        private static void startNginx()
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = "nginx.exe",
                WorkingDirectory = @"C:\Users\boyss\Documents\General\Relay\nginx-1.26.2",
                UseShellExecute = true
            };
            Process.Start(startInfo);

            // kill nginx binded to server app close
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                foreach (var process in Process.GetProcessesByName("nginx"))
                {
                    try
                    {
                        process.Kill();
                        Console.WriteLine($"Killed process: {process.ProcessName} (ID: {process.Id})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error killing process: {ex.Message}");
                    }
                }
                Thread.Sleep(1000);
            };
        }

        private static void killNginx(object? sender, EventArgs e)
        {
            var processes = Process.GetProcessesByName("nginx.exe");
            foreach (var process in processes)
            {
                process.Kill();
            }
        }
    }
}