using client_app.components;
using Guna.UI2.WinForms;
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
		public gameStats(object _)
		{
			correct = new List<bool>();
			accuracy = new List<double>();
			time = new List<TimeSpan>();
		}

		public List<bool> correct;
		public List<double> accuracy;
		public List<TimeSpan> time;

		public void UpdateStats(bool correct, double accuracy, TimeSpan time)
		{
			this.correct.Add(correct);
			this.accuracy.Add(accuracy);
			this.time.Add(time);
		}
	}
	public enum Games
	{
		Accuracy,
		Versus,
		Elimination,
	}

	public interface IPlayable
	{
		void QueueGame();
		Task JoinGameLobby();
		void AwaitStart();
		void StartGame();
		void AwaitRound();
		void SubmissionPhase(char letter);
		void EvaluationPhase(bool correct, double accuracy, TimeSpan time);
		void EndGame();
		void UpdateUsers(List<friendData> users);
		Games GetGameType();
		string GetGameID();
	}

	public abstract class Game : Form
	{
		public readonly Main main;

		private string gameID;
		private readonly Games type;
		protected List<friendData> users;

		private bool started;
		private int rounds;
		private readonly int maxPlayers;

		protected gameStats gameStats;
		protected readonly List<char> letters;

		protected Guna2Panel panel_results;
		public Guna2Panel panel_stats;
		public Guna2HtmlLabel lbl_letter;
		public Guna2GradientButton btn_submit;
		public Guna2GradientButton btn_clear;
		public Guna2HtmlLabel lbl_rounds;

		private InputPanel drawingPanel;

		public int GetMaxPlayers() => maxPlayers;
		public string GetGameID() => gameID;
		public Games GetGameType() => type;
		public bool HasStarted() => started;
		public int GetRounds() => rounds;

		public Game(Main main, Games type, int maxPlayers)
		{
			this.main = main;
			this.type = type;
			gameStats = new gameStats(null);
			started = false;
			rounds = 0;
			letters = new List<char>();
			this.maxPlayers = maxPlayers;
		}
		public virtual void UpdateUsers(List<friendData> users)
		{
			this.users = users;

			if (!started)
			{
				UXelements.configLobbyPanel(this, users);
			}

			UXelements.configLeftGamePanel(this, users);
			main.panel_left.Controls.Add(main.btn_home);

		}
		public async void QueueGame()
		{
			if (Main.connection.State != HubConnectionState.Connected)
			{
				main.btn_home.PerformClick();
				return;
			}

			gameID = await Main.connection.InvokeAsync<string>("queueGame", type, Main.userData.userID);

			if (string.IsNullOrEmpty(gameID))
			{
				Main.LoadAlert(Languages.localisation["An error occured. Please wait and try again"][Main.userData.localisation]);
				main.btn_home.PerformClick();
			}
			else
			{
				await JoinGameLobby();
			}
		}
		public async Task JoinGameLobby()
		{
			if (Main.connection.State != HubConnectionState.Connected)
			{
				main.btn_home.PerformClick();
			}

			UXelements.ResetLayout(main);
			if (!await Main.connection.InvokeAsync<bool>("userJoined", gameID))
			{
				Main.LoadAlert(Languages.localisation["An error occured. Please wait and try again"][Main.userData.localisation]);
				main.btn_home.PerformClick();
			}
		}
		public async virtual void AwaitStart()
		{
			started = true;
			await UXelements.Countdown(main.panel_left, 5, Languages.localisation["Starting in"][Main.userData.localisation]);
		}
		public void StartGame()
		{
			UXelements.configRightGamePanel(this);
		}
		public async void AwaitRound()
		{
			drawingPanel = UXelements.ConfigGamePanel(this);

			btn_clear.Click += (sender, e) => drawingPanel.ClearPanel();
			btn_submit.Click += async (sender, e) =>
			{
				btn_submit.Enabled = false;
				btn_clear.Enabled = false;
				drawingPanel.DisablePanel();

				var submission = drawingPanel.GetDrawing();

				byte[] data;
				using (var ms = new MemoryStream())
				{
					submission.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
					data = ms.ToArray();
				}

				if (Main.connection.State != HubConnectionState.Connected)
				{
					main.btn_home.PerformClick();
				}

				await Main.connection.InvokeAsync("receiveSubmission", gameID, Main.userData.userID, data);

				drawingPanel.ClearPanel();
			};

			UXelements.configLeftGamePanel(this, users);
			main.panel_left.Controls.Add(main.btn_home);
			await UXelements.Countdown(main.panel_left, 3, Languages.localisation["Next letter in"][Main.userData.localisation]);
		}
		public void SubmissionPhase(char letter)
		{
			rounds++;
			lbl_rounds.Text = $"{Languages.localisation["Round"][Main.userData.localisation]} {rounds}";

			letters.Add(letter);
			lbl_letter.Text = letter.ToString();

			drawingPanel.EnablePanel();
			btn_submit.Enabled = true;
			btn_clear.Enabled = true;
		}
		public void EvaluationPhase(bool correct, double accuracy, TimeSpan time)
		{
			gameStats.UpdateStats(correct, accuracy, time);
			panel_results = UXelements.ConfigResultsPanel(this, letters[letters.Count - 1], gameStats);

			UXelements.ConfigRightGamePanelStats(panel_stats, letters, gameStats.accuracy);
		}
		public virtual void EndGame()
		{
			UXelements.ConfigEndGamePanel(this, letters, gameStats);
		}
	}
}
