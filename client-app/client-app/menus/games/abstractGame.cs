using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus.games
{
	public interface IPlayable
	{
		void queueGame(main main);
		void joinGame();
		void startGame();
	}

	public abstract class abstractGame : Form
	{
		public main main;

		public Guna.UI2.WinForms.Guna2Shapes panel_outline;
		public Guna.UI2.WinForms.Guna2TextBox lbl_letter;
		public Guna.UI2.WinForms.Guna2GradientButton btn_submit;
		public Guna.UI2.WinForms.Guna2GradientButton btn_clear;
	}
}
