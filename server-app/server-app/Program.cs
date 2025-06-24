using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using System.Diagnostics;

namespace server_app
{
    internal class Program : Hub
    {
        static void Main(string[] args)
        {
            startNginx();
            //configServer(args).Run();
            //CreateHostBuilder(args).Build().Run();
            hostBuilder(args).Build().Run();
            database.toggleConnection(true);
        }
        private static WebApplication configServer(string[] args)  // DOESNT WORK BUT NO CHATGPT SO MAYBE LIKE GET IT WORKING PERHAPS
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
            database.toggleConnection(false);
            var processes = Process.GetProcessesByName("nginx.exe");
            foreach (var process in processes)
            {
                process.Kill();
            }
        }


        private static IHostBuilder hostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            host.ConfigureWebHostDefaults(config =>
            {
                config.ConfigureServices(services =>
                {
                    services.AddSignalR();
                    services.AddCors(setup =>
                    {
                        setup.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin());
                    });
                });
                config.Configure(setup =>
                {
                    setup.UseRouting();
                    setup.Use(async (context, next) =>
                    {
                        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                        await next.Invoke();
                    });
                    setup.UseEndpoints(endpoints =>
                    {
                        endpoints.MapHub<connections.connection>("/cs-nea/connections");
                        endpoints.MapHub<accounts>("/cs-nea/accounts");
                    });
                });
                config.UseUrls("http://0.0.0.0:3900");
            });

            return host;
        }

        static IHostBuilder CreateHostBuilder(string[] args) => // WORKS YAYAYAYAYA BUT CHATGPT
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSignalR();
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();

                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", policy =>
                            {
                                policy.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                            });
                        });
                    })


                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseCors("AllowAll");
                        app.Use(async (context, next) =>
                        {
                            //Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                            await next.Invoke();
                        });
                        //app.UseAuthentication();
                        //app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            // subdomain specifies the application required for the request (automatically handled by NGINX)
                            // class specifies the type of request to the application
                            // subclass specifies any further details

                            endpoints.MapHub<connections.connection>("/cs-nea/connections");
                            endpoints.MapHub<accounts>("/cs-nea/accounts");

                        });
                    })

                    // ensure port is not used elsewhere
                    // port number is linked to the subdomain such that only requests to :5252/subdomain are forwarded to :5200

                    .UseUrls("http://0.0.0.0:3900");

                });
    }
}