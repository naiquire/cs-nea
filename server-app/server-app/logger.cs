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
		public static void Log(string code, ConsoleColor codeColor, string message)
		{
			Console.ResetColor();
			Console.Write($"[ ");
			Console.ForegroundColor = codeColor;
			Console.Write($"{code}");
			Console.ResetColor();
			Console.WriteLine($" ] {message}");
		}

		private static readonly Channel<(string code, string colour, string message)> _channel = Channel.CreateUnbounded<(string, string, string)>();

		public static async void Log(string code, string colour, string message)
		{
			await _channel.Writer.WriteAsync((code, colour, message));
		}

		public static async void SetupAsync()
		{
			Table loggerTable = new Table()
				.NoBorder()
				.Expand()
				.AddColumn("")
				.AddColumn("");

			var loggerPanel = new Panel(loggerTable).Header("Logger", Justify.Left).Expand();

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
			await AnsiConsole.Live(layout).StartAsync(ctx =>
			{
				ctx.Refresh();
				Task.Run(() => readChannel());
				return tcs.Task;

				async void readChannel()
				{
					while (await _channel.Reader.WaitToReadAsync())
					{
						while (_channel.Reader.TryRead(out var item))
						{
							loggerTable.AddRow($@"[{item.colour}]{item.code}[/]", item.message);
							ctx.Refresh();
						}
					}
				}
			});
		}
	}
}
