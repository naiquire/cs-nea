
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
            this.btn_close = new System.Windows.Forms.Button();
            this.panel_topBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_topBorder
            // 
            this.panel_topBorder.Controls.Add(this.btn_close);
            this.panel_topBorder.Location = new System.Drawing.Point(0, 0);
            this.panel_topBorder.Name = "panel_topBorder";
            this.panel_topBorder.Size = new System.Drawing.Size(1920, 30);
            this.panel_topBorder.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(1890, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(30, 30);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = true;
            //this.btn_close.Click += new System.EventHandler(main.closeApp); // works when classes merged
            // 
            // accuracy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panel_topBorder);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "accuracy";
            this.Text = "accuracy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel_topBorder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_topBorder;
        private System.Windows.Forms.Button btn_close;
    }
}