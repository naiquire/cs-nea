using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace server_app
{
    internal class Program : Hub
    {
        static void Main(string[] args)
        {
       
            neuralNetwork.training training = new neuralNetwork.training();

            //startNginx();
            //configServer(args).Run();
        }
        private static WebApplication configServer(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            builder.Services.AddCors();

            var app = builder.Build();

            app.MapHub<connections.connection>("/cs-nea/connections");
            //app.MapHub<connections.queueing>("/cs-nea/queueing");
            //app.MapHub<connections.social>("/cs-nea/social");

            // binds to all address on port 3900
            app.Urls.Add("http://0.0.0.0:3900");

            return app;
        }
        private static void startNginx()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "nginx.exe",
                WorkingDirectory = @"C:\Users\boyss\Documents\General\Relay\nginx-1.26.2",
            };
            Process.Start(startInfo);

            // kill nginx binded to server app close
            AppDomain.CurrentDomain.ProcessExit += killNginx;
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