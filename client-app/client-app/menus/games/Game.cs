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
		public gameStats(object arg) // requires argument for some reason
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
		Knockout,
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

		private string _gameID;
		private readonly Games _type;
		protected List<friendData> _users;

		private bool _started;
		private int _rounds;
		private readonly int _maxPlayers;

		protected gameStats _stats;
		protected readonly List<char> _letters;

		public Guna2Panel panel_results;
		public Guna2Shapes panel_outline;
		public Guna2HtmlLabel lbl_letter;
		public Guna2GradientButton btn_submit;
		public Guna2GradientButton btn_clear;

		public Panel panel_stats;
		public Guna2HtmlLabel lbl_rounds;
		public Guna2HtmlLabel lbl_countdown;
		public Guna2HtmlLabel lbl_status;

		private input _drawingPanel;

		protected Game(Main main, Games type, int maxPlayers)
		{
			this.main = main;
			_type = type;
			_stats = new gameStats("");
			_started = false;
			_rounds = 0;
			_letters = new List<char>();
			_maxPlayers = maxPlayers;
		}
		public virtual void UpdateUsers(List<friendData> users)
		{
			this._users = users;

			if (!_started)
			{
				UXelements.configLobbyPanel(this, users);
			}

			UXelements.configLeftGamePanel(this, users);
			main.panel_left.Controls.Add(main.btn_home);

		}
		public async virtual void QueueGame()
		{
			if (Main.connection.State != HubConnectionState.Connected)
			{
				main.btn_home.PerformClick();
				return;
			}

			_gameID = await Main.connection.InvokeAsync<string>("queueGame", _type, Main.userData.userID);

			if (string.IsNullOrEmpty(_gameID))
			{
				Main.LoadAlert(languages.localisation["An error occured. Please wait and try again"][Main.userData.localisation]);
				main.btn_home.PerformClick();
			}
			else
			{
				await JoinGameLobby();
			}
		}
		public virtual async Task JoinGameLobby()
		{
			if (Main.connection.State != HubConnectionState.Connected)
			{
				main.btn_home.PerformClick();
			}

			UXelements.ResetLayout(main);
			if (!await Main.connection.InvokeAsync<bool>("userJoined", _gameID))
			{
				Main.LoadAlert(languages.localisation["An error occured. Please wait and try again"][Main.userData.localisation]);
				main.btn_home.PerformClick();
			}
		}
		public virtual async void AwaitStart()
		{
			_started = true;
			await UXelements.Countdown(lbl_countdown, 5, lbl_status, languages.localisation["Starting in"][Main.userData.localisation]);
		}
		public virtual void StartGame()
		{
			UXelements.configRightGamePanel(this);
		}
		public async void AwaitRound()
		{
			_drawingPanel = UXelements.ConfigGamePanel(this);

			btn_clear.Click += (sender, e) => _drawingPanel.ClearPanel();
			btn_submit.Click += async (sender, e) =>
			{
				btn_submit.Enabled = false;
				btn_clear.Enabled = false;
				_drawingPanel.disablePanel();

				var submission = _drawingPanel.GetDrawing();

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

				await Main.connection.InvokeAsync("receiveSubmission", _gameID, Main.userData.userID, data);

				_drawingPanel.ClearPanel();
			};

			await UXelements.Countdown(lbl_countdown, 3, lbl_status, languages.localisation["Next letter in"][Main.userData.localisation]);
		}
		public void SubmissionPhase(char letter)
		{
			_rounds++;
			lbl_rounds.Text = $"{languages.localisation["Round"][Main.userData.localisation]} {_rounds}";

			_letters.Add(letter);
			lbl_letter.Text = letter.ToString();

			_drawingPanel.EnablePanel();
			btn_submit.Enabled = true;
			btn_clear.Enabled = true;
		}
		public virtual void EvaluationPhase(bool correct, double accuracy, TimeSpan time)
		{
			_stats.UpdateStats(correct, accuracy, time);
			panel_results = UXelements.ConfigResultsPanel(this, _letters[_letters.Count - 1], _stats);

			UXelements.ConfigRightGamePanelStats(panel_stats, _letters, _stats.accuracy);
		}

		public virtual void EndGame()
		{
			UXelements.ConfigEndGamePanel(this, _letters, _stats);
		}

		public int GetMaxPlayers() => _maxPlayers;
		public string GetGameID() => _gameID;
		public Games GetGameType() => _type;
		public bool HasStarted() => _started;
		public int GetRounds() => _rounds;

	}
}
