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
			Console.Write($"[");
			if (Enum.TryParse(colour[0].ToString().ToUpper() + colour[1..], out ConsoleColor c))
			{
				// capitalise the first letter of the colour string to match the enum type
				Console.ForegroundColor = c;
			}

			Console.Write($"{code}");
			Console.ResetColor();
			Console.WriteLine($"] {message}");

			//await _logChannel.Writer.WriteAsync((code, colour, message));
		}
		public static async void ErrorLog(string message)
		{
			Console.Write($"["); Console.ForegroundColor = ConsoleColor.Red; Console.Write($"ERROR"); Console.ResetColor(); Console.WriteLine($"] {message}");
			//await _errorChannel.Writer.WriteAsync(message);
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
			Table infoTable = new Table()
				.NoBorder()
				.Expand()
				.AddColumn("")
				.AddColumn("")
				.AddRow("[palegreen1]Info[/]", "server application for cs-nea\n")
				.AddRow("[palegreen1]Libraries[/]", "Microsoft.AspNetCore.SignalR\nMicrosoft.Data.Sqlite\nSpectre.Console\nMathNet.Numerics\n")
				.AddRow("[palegreen1]Statistics[/]", "~6000 lines of code\n14 server classes\n15 client classes\n")
				.AddRow("", "Running on [link]https://localhost:3900[/]");

			var loggerPanel = new Panel(loggerTable).Header("[palegreen3_1]Logger[/]", Justify.Right).Expand().BorderColor(Color.DarkSeaGreen1_1).RoundedBorder();
			var errorPanel = new Panel(errorTable).Header("[hotpink2]Errors[/]", Justify.Right).Expand().BorderColor(Color.Plum3).RoundedBorder();

			var layout = new Layout("layout");
				var top = new Layout("top");
					var topLeft = new Layout("topLeft");
						topLeft.Update(new Panel(infoTable).Expand().Header("server-app").RoundedBorder());
					var topRight = new Layout("topRight");
						topRight.Update(loggerPanel);
				top.SplitColumns(topLeft, topRight);
				var bottom = new Layout("bottom");
					var bottomLeft = new Layout("bottomLeft").Ratio(1);
						bottomLeft.Update(new Panel("[bold][paleturquoise1]Information[/][/]\n\nTesting for Objective 4\n\n(i) [dim]Sending a friend request[/]\n(ii) [dim]Sending duplicate friend requests[/]\n(iii) [dim]Online receiving[/]\n(iv) [dim]Offline receiving[/]\n(v) [dim]Removing a friend[/]\n(vi) [dim]UI is updated[/]\n").Expand().RoundedBorder());
					var bottomRight = new Layout("bottomRight").Ratio(2);
						bottomRight.Update(errorPanel);
				bottom.SplitColumns(bottomLeft, bottomRight);
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
