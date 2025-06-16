
using System.Linq.Expressions;

namespace client_app.games
{
    partial class accuracy // change to main once completed
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_topBorder = new System.Windows.Forms.Panel();
            this.txt_appName = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel_topLeft = new System.Windows.Forms.Panel();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_right = new System.Windows.Forms.Panel();
            this.txt_letter = new System.Windows.Forms.TextBox();
            this.panel_topBorder.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_topBorder
            // 
            this.panel_topBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
            this.panel_topBorder.Controls.Add(this.txt_appName);
            this.panel_topBorder.Controls.Add(this.btn_close);
            this.panel_topBorder.Location = new System.Drawing.Point(0, 0);
            this.panel_topBorder.Name = "panel_topBorder";
            this.panel_topBorder.Size = new System.Drawing.Size(1920, 30);
            this.panel_topBorder.TabIndex = 0;
            // 
            // txt_appName
            // 
            this.txt_appName.BackColor = this.panel_topBorder.BackColor;
            this.txt_appName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_appName.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_appName.Location = new System.Drawing.Point(10, 7);
            this.txt_appName.Name = "txt_appName";
            this.txt_appName.Size = new System.Drawing.Size(100, 16);
            this.txt_appName.TabIndex = 0;
            this.txt_appName.Text = "appName";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(1890, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(30, 30);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = true;
            // 
            // panel_left
            // 
            this.panel_left.AutoScroll = true;
            this.panel_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
            this.panel_left.Location = new System.Drawing.Point(0, 130);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(300, 950);
            this.panel_left.TabIndex = 2;
            // 
            // panel_topLeft
            // 
            this.panel_topLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panel_topLeft.Location = new System.Drawing.Point(0, 30);
            this.panel_topLeft.Name = "panel_topLeft";
            this.panel_topLeft.Size = new System.Drawing.Size(300, 100);
            this.panel_topLeft.TabIndex = 1;
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.Transparent;
            this.panel_main.Controls.Add(this.txt_letter);
            this.panel_main.Location = new System.Drawing.Point(300, 30);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1120, 1050);
            this.panel_main.TabIndex = 4;
            // 
            // txt_letter
            // 
            this.txt_letter.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_letter.Location = new System.Drawing.Point(550, 115);
            this.txt_letter.Name = "txt_letter";
            this.txt_letter.ReadOnly = true;
            this.txt_letter.Size = new System.Drawing.Size(92, 86);
            this.txt_letter.TabIndex = 0;
            this.txt_letter.Text = "K";
            this.txt_letter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel_right
            // 
            this.panel_right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
            this.panel_right.Location = new System.Drawing.Point(1420, 30);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(500, 1050);
            this.panel_right.TabIndex = 3;
            // 
            // accuracy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panel_topBorder);
            this.Controls.Add(this.panel_left);
            this.Controls.Add(this.panel_topLeft);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_right);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "accuracy";
            this.Text = "accuracy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel_topBorder.ResumeLayout(false);
            this.panel_topBorder.PerformLayout();
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_topBorder;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox txt_appName;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.Panel panel_topLeft;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.TextBox txt_letter;
        private System.Windows.Forms.Panel panel_right;
    }
}