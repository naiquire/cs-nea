using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Diagnostics;

namespace server_app
{
	internal class Program : Hub
	{
		public static IHubContext<connection>? hubContext;
		static void Main(string[] args)
		{
			startNginx();
			hostBuilder(args).Build().Run();
			data.preloadParameters();
		}
		private static void startNginx()
		{
			ProcessStartInfo startInfo = new()
			{
				FileName = "nginx.exe",
				WorkingDirectory = @"C:\Users\naiquire\Documents\General\Relay\nginx-1.26.2",
				UseShellExecute = true
			};
			Process.Start(startInfo);
			Logger.Log("NGINX", ConsoleColor.White, $"Started process");

			// kill nginx binded to server app close
			AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
			{
				foreach (var process in Process.GetProcessesByName("nginx"))
				{
					try
					{
						process.Kill();
						Logger.Log("NGINX", ConsoleColor.White, $"Killed process <{process.Id}>");
					}
					catch (Exception ex)
					{
						database.outputException(ex);
					}
				}
				Thread.Sleep(1000);
			};
		}
		private static IHostBuilder hostBuilder(string[] args)
		{
			IHostBuilder host = Host.CreateDefaultBuilder(args);
			host.ConfigureWebHostDefaults(config =>
			{
				config.ConfigureServices(services =>
				{
					// add required services
					services.AddSignalR();
					services.AddCors(setup =>
					{
						// allow any device to connect
						setup.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin());
					});
				});
				config.Configure(setup =>
				{
					setup.UseRouting();
					setup.Use(async (context, next) =>
					{
						// load the hubContext
						hubContext = context.RequestServices.GetRequiredService<IHubContext<connection>>();
						await next.Invoke();
					});
					setup.UseEndpoints(endpoints =>
					{
						// map endpoints to a class
						endpoints.MapHub<connection>("/cs-nea/connections");
						endpoints.MapHub<accounts>("/cs-nea/accounts");
					});
				});

				// all ip addresses using port 3900
				config.UseUrls("http://0.0.0.0:3900");
			});

			return host;
		}
	}
}



















		