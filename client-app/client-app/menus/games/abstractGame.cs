using client_app.components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus.games
{
	public struct gameStats
	{
		public gameStats(object arg) // requires argument for some reason
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
		string getGameID();
	}

	public abstract class abstractGame : Form
	{
		public readonly main main;

		private string gameID;
		private readonly string type;
		protected List<friendData> users;

		private bool started;
		private int rounds;
		private readonly int maxPlayers;

		private gameStats stats;
		private readonly List<char> letters;

		public Guna.UI2.WinForms.Guna2Shapes panel_outline;
		public Guna.UI2.WinForms.Guna2HtmlLabel lbl_letter;
		public Guna.UI2.WinForms.Guna2GradientButton btn_submit;
		public Guna.UI2.WinForms.Guna2GradientButton btn_clear;
		public Guna.UI2.WinForms.Guna2GradientButton btn_continue;
		public Panel panel_stats;
		public Guna.UI2.WinForms.Guna2HtmlLabel lbl_rounds;
		public Guna.UI2.WinForms.Guna2HtmlLabel lbl_countdown;
		public Guna.UI2.WinForms.Guna2HtmlLabel lbl_status;

		private input drawingPanel;

		protected abstractGame(main main, string type, int maxPlayers)
		{
			this.main = main;
			this.type = type;
			stats = new gameStats("");
			started = false;
			rounds = 0;
			letters = new List<char>();
			this.maxPlayers = maxPlayers;
		}
		public virtual void updateUsers(List<friendData> users)
		{
			this.users = users;

			if (!started)
			{
				UXelements.configLobbyPanel(this, users);
			}

			UXelements.configLeftGamePanel(this, users);			
			main.panel_left.Controls.Add(main.btn_home);

		}
		public async virtual void queueGame()
		{
			gameID = await main.connection.InvokeAsync<string>("queueGame", type, main.userData.userID);

			if (string.IsNullOrEmpty(gameID))
			{
				new alert("An error occured queueing for a game. Please try again.");
				main.btn_home.PerformClick();
			}
			else
			{
				await joinGameLobby();
			}
		}
		public virtual async Task joinGameLobby()
		{
			UXelements.resetLayout(main);
			UXelements.initialiseLobby(main);
			if (!await main.connection.InvokeAsync<bool>("userJoined", gameID))
			{
				new alert("An error occured joining the game. Please try again.");
				main.btn_home.PerformClick();
			}
		}
		public virtual async void awaitStart()
		{
			started = true;
			await UXelements.countdown(lbl_countdown, 5, lbl_status, "Starting in");
		}
		public virtual void startGame()
		{
			UXelements.configRightGamePanel(this);
		}
		public async void awaitRound()
		{
			drawingPanel = UXelements.configGamePanel(this);

			btn_clear.Click += (sender, e) => drawingPanel.clearPanel();
			btn_submit.Click += async (sender, e) =>
			{
				btn_submit.Enabled = false;
				btn_clear.Enabled = false;
				drawingPanel.disablePanel();

				var submission = drawingPanel.getDrawing();

				byte[] data;
				using (var ms = new MemoryStream())
				{
					submission.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
					data = ms.ToArray();
				}

				await main.connection.InvokeAsync("receiveSubmission", gameID, main.userData.userID, data);

				drawingPanel.clearPanel();
			};

			await UXelements.countdown(lbl_countdown, 3, lbl_status, "Next letter in");
		}
		public virtual void submissionPhase(char letter)
		{
			rounds++;
			lbl_rounds.Text = $"Round {rounds}";

			letters.Add(letter);
			lbl_letter.Text = letter.ToString();

			drawingPanel.enablePanel();
			btn_submit.Enabled = true;
			btn_clear.Enabled = true;
			
		}
		public virtual void evaluationPhase(bool correct, double accuracy, TimeSpan time)
		{
			stats.updateStats(correct, accuracy, time);
			UXelements.configResultsPanel(this, letters[letters.Count - 1], stats);

			UXelements.configRightGamePanelStats(panel_stats, letters, stats.accuracy);
		}

		public virtual void endGame()
		{
			UXelements.configEndGamePanel(this, letters, stats);
		}

		public int getMaxPlayers() => maxPlayers;
		public string getGameID() => gameID;
		public string getType() => type;
		public bool hasStarted() => started;
		public int getRounds() => rounds;

	}
}
