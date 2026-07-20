using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using server_app.neuralNetwork.structures;
using System.Diagnostics;

namespace server_app
{
	internal class Program : Hub
	{
		public static IHubContext<Connection>? hubContext;

		static void Main(string[] args)
		{
			Data.BuildMatrices(Data.LoadWeights(), Data.LoadBiases());

			// startNginx();
			hostBuilder(args).Build().Run();
			//Logger.SetupAsync();
			Logger.Log("SERVER", "white", "Application started");
		}

		private static void startNginx()
		{
			ProcessStartInfo startInfo = new()
			{
				FileName = "nginx.exe",
				WorkingDirectory = @"[INSERT_NGINX_DIRECTORY]",
				UseShellExecute = true
			};
			Process.Start(startInfo);

			Logger.Log("NGINX", "white", $"Started process");

			// kill nginx binded to server app close
			AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
			{
				foreach (var process in Process.GetProcessesByName("nginx"))
				{
					try
					{
						process.Kill();
						Logger.Log("NGINX", "white", $"Killed process <{process.Id}>");
					}
					catch (Exception ex)
					{
						Database.outputException(ex);
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
					// add signalR service
					services.AddSignalR();
				});
				config.Configure(setup =>
				{
					setup.UseRouting();
					setup.Use((context, next) =>
					{
						// load the hubContext
						hubContext = context.RequestServices.GetRequiredService<IHubContext<Connection>>();
						return next(context);
					});
					setup.UseEndpoints(endpoints =>
					{
						// map endpoints to a class
						endpoints.MapHub<Connection>("/cs-nea/connections");
						endpoints.MapHub<Accounts>("/cs-nea/accounts");
					});
				});
				config.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.None));

				// bind to localhost on port 3900
				config.UseUrls("http://0.0.0.0:3900");
			});

			return host;
		}
	}
}
