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
		string getGameID();
	}

	public abstract class abstractGame : Form
	{
		public main main;

		public string gameID;
		protected readonly string type;
		public List<friendData> users;

		protected bool started;
		private int rounds;
		protected readonly int maxPlayers;

		public stats stats;
		public List<char> letters;

		public Guna.UI2.WinForms.Guna2Shapes panel_outline;
		public Guna.UI2.WinForms.Guna2TextBox lbl_letter;
		public Guna.UI2.WinForms.Guna2GradientButton btn_submit;
		public Guna.UI2.WinForms.Guna2GradientButton btn_clear;
		public Guna.UI2.WinForms.Guna2GradientButton btn_continue;
		public Panel panel_stats;
		public Guna.UI2.WinForms.Guna2TextBox lbl_rounds;
		public Guna.UI2.WinForms.Guna2TextBox lbl_countdown;

		private input drawingPanel;

		protected abstractGame(main main, string type, int maxPlayers)
		{
			this.main = main;
			this.type = type;
			stats = new stats("");
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
				interfaces.configLobbyPanel(this, users);
			}

			interfaces.configLeftGamePanel(main.panel_left, users);
			main.panel_left.Controls.Add(main.btn_home);

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
			interfaces.resetLayout(main);
			interfaces.initialiseLobby(main);
			if (!await main.connection.InvokeAsync<bool>("userJoined", gameID))
			{
				new alert("An error occured joining the game. Please try again.");
				main.btn_home.PerformClick();
			}
		}
		public virtual void awaitStart()
		{
			started = true;
			// label not present on lobby screen yet
			// also need to add a progress textbox at bottom for number of queues required
			interfaces.countdown(lbl_countdown, 5);
		}
		public virtual void startGame()
		{
			interfaces.configRightGamePanel(this);
		}
		public void awaitRound()
		{
			interfaces.countdown(lbl_countdown, 3);

			drawingPanel = interfaces.configGamePanel(this);

			btn_clear.Click += (sender, e) => drawingPanel.clearPanel();
			btn_submit.Click += async (sender, e) =>
			{
				btn_submit.Enabled = false;
				drawingPanel.disablePanel();

				var submission = drawingPanel.ImageToArray();

				await main.connection.InvokeAsync("receiveSubmission", gameID, main.userData.userID, submission);

				drawingPanel.clearPanel();
			};			
		}
		public virtual void submissionPhase(char letter)
		{
			rounds++;
			lbl_rounds.Text = $"Round {rounds}";

			letters.Add(letter);
			lbl_letter.Text = letter.ToString();

			drawingPanel.enablePanel();
			btn_submit.Enabled = true;
			
		}
		public virtual void evaluationPhase(bool correct, double accuracy, TimeSpan time)
		{
			stats.updateStats(correct, accuracy, time);
			interfaces.configResultsPanel(this, letters[letters.Count - 1], stats);

			interfaces.configRightGamePanelStats(panel_stats, letters, stats.accuracy, getType()); // idk
		}

		public virtual void endGame()
		{
			interfaces.configEndGamePanel(this);
		}

		public int getMaxPlayers() => maxPlayers;
		public string getGameID() => gameID;
		public string getType() => type;
		public int getRounds() => rounds;
	}
}
