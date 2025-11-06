using Spectre.Console;
using System.Threading.Channels;

namespace server_app
{
	public static class Logger
	{
		private static readonly Channel<(string code, string colour, string message)> _logChannel = Channel.CreateUnbounded<(string, string, string)>();
		private static readonly Channel<string> _errorChannel = Channel.CreateUnbounded<string>();

		public static async void Log(string code, string colour, string message)
		{
			await _logChannel.Writer.WriteAsync((code, colour, message));
		}
		public static async void ErrorLog(string message)
		{
			await _errorChannel.Writer.WriteAsync(message);
		}

		public static void SetupAsync()
		{
			Table loggerTable = new Table()
				.NoBorder()
				.Expand()
				.AddColumn("")
				.AddColumn("");
			Table errorTable = new Table()
				.NoBorder()
				.Expand()
				.AddColumn("")
				.AddColumn("");

			var loggerPanel = new Panel(loggerTable).Header("Logger", Justify.Left).Expand();
			var errorPanel = new Panel(errorTable).Header("Errors", Justify.Left).Expand();

			var layout = new Layout("layout");

			var topLeft = new Layout("topLeft");
			var topRight = new Layout("topRight");

			var top = new Layout("top");
			var bottom = new Layout("bottom");

			topLeft.Update(new Panel("topleft").Expand().Header("topleft"));
			topRight.Update(loggerPanel);
			bottom.Update(errorPanel);

			top.SplitColumns(topLeft, topRight);

			layout.SplitRows(top, bottom);


			var tcs = new TaskCompletionSource();
			_ = AnsiConsole.Live(layout).StartAsync(ctx =>
			{
				ctx.Refresh();
				Task.Run(() => readLogChannel());
				Task.Run(() => readErrorChannel());
				return tcs.Task;

				async void readLogChannel()
				{
					while (await _logChannel.Reader.WaitToReadAsync())
					{
						while (_logChannel.Reader.TryRead(out var item))
						{
							if (loggerTable.Rows.Count > 10)
							{
								loggerTable.RemoveRow(0);
							}

							loggerTable.AddRow($@"[{item.colour}]{item.code}[/]", item.message);
							ctx.Refresh();
						}
					}
				}
				async void readErrorChannel()
				{
					while (await _errorChannel.Reader.WaitToReadAsync())
					{
						while (_errorChannel.Reader.TryRead(out var item))
						{
							if (errorTable.Rows.Count > 10)
							{
								errorTable.RemoveRow(0);
							}

							errorTable.AddRow($@"[red]ERROR[/]", item);
							ctx.Refresh();
						}
					}
				}
			});
		}
	}
}
