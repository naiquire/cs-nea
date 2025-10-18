using Spectre.Console;
using SQLitePCL;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace server_app
{
	public static class Logger
	{
		private static readonly Layout layout = new();
		private static readonly Table loggerTable = new();
		public static void Log(string code, ConsoleColor codeColor, string message)
		{
			Console.ResetColor();
			Console.Write($"[ ");
			Console.ForegroundColor = codeColor;
			Console.Write($"{code}");
			Console.ResetColor();
			Console.WriteLine($" ] {message}");
		}

		private static readonly Channel<string> _channel = Channel.CreateUnbounded<string>();

		public static async Task Log(string message)
		{
			await _channel.Writer.WriteAsync(message);
		}

		public static async Task SetupAsync()
		{
			loggerTable.Border(TableBorder.Rounded);
			loggerTable.Expand();
			loggerTable.AddColumn("items");

			var loggerPanel = new Panel(loggerTable).Header("Logger", Justify.Center).Expand();

			// elsewhere in your layout setup
			var layout = new Layout("Root");


			var topLeft = new Layout("topLeft");
			var topRight = new Layout("topRight");

			var top = new Layout("top");
			var bottom = new Layout("bottom");

			topLeft.Update(new Panel("topleft").Expand().Header("topleft"));
			topRight.Update(loggerPanel);
			bottom.Update(new Panel("bottom").Expand().Header("bottom"));

			top.SplitColumns(topLeft, topRight);

			layout.SplitRows(top, bottom);


			var tcs = new TaskCompletionSource();

			await AnsiConsole.Live(layout)
				.StartAsync(ctx =>
				{
					ctx.Refresh();
					_ = Task.Run(async () =>
					{
						while (await _channel.Reader.WaitToReadAsync())
						{
							while (_channel.Reader.TryRead(out var item))
							{
								loggerTable.AddRow(item);
								ctx.Refresh();
							}
						}
					});

					return tcs.Task;
				});



			//// Live update only the loggerPanel
			//await AnsiConsole.Live(layout)
			//	.StartAsync(async ctx =>
			//	{
			//		loggerTable.AddRow("aaaaa");
			//		ctx.Refresh();

			//		// Simulate external producer
			//		_ = Task.Run(async () =>
			//		{
			//			await Task.Delay(1000);
			//			await _channel.Writer.WriteAsync("First item");
			//			await Task.Delay(1000);
			//			await _channel.Writer.WriteAsync("Second item");
			//		});



			//		var reader = _channel.Reader;
			//		while (await reader.WaitToReadAsync())
			//		{
			//			string item = await reader.ReadAsync();
			//			loggerTable.AddRow(item);
			//			ctx.Refresh();
			//		}
			//	});

		}


		private static async Task function(LiveDisplayContext ctx)
		{
			
		}


		public static Layout createLayout()
		{

			//layout.SplitRows(
			//	new Layout("Top")
			//		.SplitColumns(
			//			new Layout("Left")
			//				.SplitRows(
			//					new Layout("LeftTop"),
			//					new Layout("LeftBottom")),
			//			new Layout("Right").Ratio(2),
			//			new Layout("RightRight").Ratio(3)),
			//	new Layout("Bottom"));

			//layout["LeftBottom"].Update(
			//	new Panel("[blink]CTRL+C to kill processes[/]")
			//		.Expand()
			//		.BorderColor(Color.Yellow)
			//		.Padding(0, 0));

			//layout["Right"].Update(
			//	new Panel(
			//		new Table()
			//			.AddColumns("[blue]Qux[/]", "[green]Corgi[/]")
			//			.AddRow("9", "8")
			//			.AddRow("7", "6")
			//			.Expand())
			//	.Header("connected users")
			//	.Expand());

			
				




			//layout["RightRight"].Update(new Panel(logger).Expand().Header("logger"));


			//layout["Bottom"].Update(
			//new Panel("")
			//	.Header("errors")
			//	.Expand());

			return layout;
		}
	}
}
