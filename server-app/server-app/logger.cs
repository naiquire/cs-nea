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
		public static async void ErrorLog(Exception ex)
		{
			await _errorChannel.Writer.WriteAsync(ex.Message);
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

			// elsewhere in your layout setup
			var layout = new Layout("Root");


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
                            errorTable.AddRow($@"[red]ERROR[/]", item);
                            ctx.Refresh();
                        }
                    }
                }
            });
		}
	}
}
