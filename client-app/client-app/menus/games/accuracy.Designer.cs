
using client_app.menus;
using System.Linq.Expressions;

namespace client_app.games
{
    partial class accuracy : abstractMenu
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
        
        public override void InitializeComponent()
        {
            resetLayout(main);

			this.txt_letter = new System.Windows.Forms.Label();
            // 
            // txt_letter
            // 
            this.txt_letter.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_letter.Location = new System.Drawing.Point(550, 115);
            this.txt_letter.Name = "txt_letter";
            this.txt_letter.Size = new System.Drawing.Size(92, 86);
            this.txt_letter.TabIndex = 0;
            this.txt_letter.Text = "K";
            this.txt_letter.TextAlign = (System.Drawing.ContentAlignment)System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// accuracy
			// 
			main.panel_main.Controls.Add(this.txt_letter);
		}

        #endregion

        private System.Windows.Forms.Label txt_letter;
    }
}