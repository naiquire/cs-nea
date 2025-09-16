using Microsoft.AspNetCore.SignalR;
using server_app.connections;
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
						hubContext = context.RequestServices.GetRequiredService<IHubContext<connection>>();
						await next.Invoke();
					});
					setup.UseEndpoints(endpoints =>
					{
						endpoints.MapHub<connection>("/cs-nea/connections");
						endpoints.MapHub<accounts>("/cs-nea/accounts");
					});
				});
				config.UseUrls("http://0.0.0.0:3900");
			});

			return host;
		}
	}
}