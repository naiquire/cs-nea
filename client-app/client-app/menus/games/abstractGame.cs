using client_app.components;
using Guna.UI2.WinForms.Suite;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
	}
		public interface IPlayable
	{
		void queueGame();
		Task joinGame();
		void awaitStart();
		void startGame();
		void awaitRound();
		void submissionPhase(char letter);
		void evaluationPhase();
		void updateUsers(List<friendData> users);
		void updateStats(bool correct, double accuracy, TimeSpan time);
	}

	public abstract class abstractGame : Form
	{
		public main main;

		public string gameID;
		protected readonly string type;
		public List<friendData> users;

		private bool started;

		protected stats stats;
		

		public Guna.UI2.WinForms.Guna2Shapes panel_outline;
		public Guna.UI2.WinForms.Guna2TextBox lbl_letter;
		public Guna.UI2.WinForms.Guna2GradientButton btn_submit;
		public Guna.UI2.WinForms.Guna2GradientButton btn_clear;

		private input drawingPanel;

		protected abstractGame(main main, string type)
		{
			this.main = main;
			this.type = type;
			stats = new stats();
			started = false;
		}
		public void updateUsers(List<friendData> users)
		{
			this.users = users;

			if (!started)
			{
				// this is hit before the gameID is returned and the base lobby initialised, need to delay updateUsers to AFTER the join confirmation has been sent
				abstractMenu.configLobby(main.panel_main, users);
			}
			else
			{

			}
		}
		public void updateStats(bool correct, double accuracy, TimeSpan time)
		{
			stats.correct.Add(correct);
			stats.accuracy.Add(accuracy);
			stats.time.Add(time);
		}
		public async virtual void queueGame()
		{
			gameID = await main.connection.InvokeAsync<string>("queueGame", type, main.userData.userID);
			if (!string.IsNullOrEmpty(gameID))
			{
				await joinGame();
			}
		}
		public virtual async Task joinGame()
		{
			abstractMenu.initialiseLobby(main);
			await main.connection.InvokeAsync("checkStart", gameID);
		}
		public virtual void awaitStart()
		{
			started = true;
			// display countdown to game start
		}
		public virtual void startGame()
		{
			drawingPanel = abstractMenu.configGamePanel(this);
		}
		public void awaitRound()
		{
			// display countdown to next round
		}
		public virtual void submissionPhase(char letter)
		{
			lbl_letter.Text = letter.ToString();

			drawingPanel.enablePanel();

			btn_submit.Click += async (sender, e) =>
			{
				Bitmap drawing = drawingPanel.disablePanel();
				var submission = convertBitmap(drawing);

				await main.connection.InvokeAsync("receiveSubmission", gameID, main.userData.userID, submission);
			};

			double[] convertBitmap(Bitmap bitmap)
			{
				Bitmap resize = new Bitmap(bitmap, new Size(28, 28));

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
		public virtual void evaluationPhase()
		{

		}
	}
}
