using client_app.components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus.games
{
	public struct @stats
	{
		public @stats(object arg) // requires argument for some reason
		{
			correct = new List<bool>();
			accuracy = new List<double>();
			time = new List<TimeSpan>();
		}

		public List<bool> correct;
		public List<double> accuracy;
		public List<TimeSpan> time;

		public void updateStats(bool correct, double accuracy, TimeSpan time)
		{
			this.correct.Add(correct);
			this.accuracy.Add(accuracy);
			this.time.Add(time);
		}
	}
	public interface IPlayable
	{
		void queueGame();
		Task joinGameLobby();
		void awaitStart();
		void startGame();
		void awaitRound();
		void submissionPhase(char letter);
		void evaluationPhase(bool correct, double accuracy, TimeSpan time);
		void endGame();
		void updateUsers(List<friendData> users);
		string getType();
	}

	public abstract class abstractGame : Form
	{
		public main main;

		public string gameID;
		protected readonly string type;
		public List<friendData> users;

		protected bool started;
		private int rounds;

		protected stats stats;
		protected char letter;

		public Guna.UI2.WinForms.Guna2Shapes panel_outline;
		public Guna.UI2.WinForms.Guna2TextBox lbl_letter;
		public Guna.UI2.WinForms.Guna2GradientButton btn_submit;
		public Guna.UI2.WinForms.Guna2GradientButton btn_clear;
		public Guna.UI2.WinForms.Guna2GradientButton btn_continue;

		private input drawingPanel;

		protected abstractGame(main main, string type)
		{
			this.main = main;
			this.type = type;
			stats = new stats("");
			started = false;
			rounds = 0;
		}
		public virtual void updateUsers(List<friendData> users)
		{
			this.users = users;

			if (!started)
			{
				interfaces.configLobbyPanel(main.panel_main, users);
			}

			interfaces.configLeftGamePanel(main.panel_left, users);
		}
		public async virtual void queueGame()
		{
			gameID = await main.connection.InvokeAsync<string>("queueGame", type, main.userData.userID);
			
			if (!string.IsNullOrEmpty(gameID))
			{
				await joinGameLobby();
			}
		}
		public virtual async Task joinGameLobby()
		{
			interfaces.initialiseLobby(main);
			if (!await main.connection.InvokeAsync<bool>("userJoined", gameID))
			{
				// gameID couldn't be found, quit to menu
			}
		}
		public virtual void awaitStart()
		{
			started = true;
			// display countdown to game start
		}
		public virtual void startGame()
		{
			interfaces.configRightGamePanel(this);
		}
		public void awaitRound()
		{
			// countdown timer of 5 sec

			interfaces.resetLayout(main);
			drawingPanel = interfaces.configGamePanel(this);

			btn_clear.Click += (sender, e) => drawingPanel.clearPanel();
			btn_submit.Click += async (sender, e) =>
			{
				btn_submit.Enabled = false;
				Bitmap drawing = drawingPanel.disablePanel();

				var submission = convertBitmap(drawing);

				

				await main.connection.InvokeAsync("receiveSubmission", gameID, main.userData.userID, submission);

				drawingPanel.clearPanel();
			};

			double[] convertBitmap(Bitmap bitmap)
			{
				Bitmap resize = new Bitmap(bitmap, new Size(28, 28));
				bitmap.Save("raw.png");
				resize.Save("resize.png");
				int width = resize.Width;
				int height = resize.Height;
				double[] pixels = new double[width * height];

				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						Color c = resize.GetPixel(x, y);
						double gray = (0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
						pixels[y * width + x] = 1.0 - (gray / 255.0);
					}
				}

				return pixels;
			}
		}
		public virtual void submissionPhase(char letter)
		{
			rounds++;

			this.letter = letter;
			lbl_letter.Text = letter.ToString();

			drawingPanel.enablePanel();
			btn_submit.Enabled = true;
		}
		public virtual void evaluationPhase(bool correct, double accuracy, TimeSpan time)
		{
			interfaces.resetLayout(main);
			stats.updateStats(correct, accuracy, time);
			interfaces.configResultsPanel(this, letter, stats);
		}

		public virtual void endGame()
		{

		}

		public string getType() => type;
	}
}
