
using client_app.menus;
using System.Linq.Expressions;
using System.Drawing;
using System.Windows.Forms;
using client_app.menus.games;

namespace client_app.games
{
	partial class accuracy : abstractGame
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		
		public void InitializeComponent()
		{
			interfaces.resetLayout(main);

			interfaces.configGamePanel(this);
		}

		#endregion
	}
}