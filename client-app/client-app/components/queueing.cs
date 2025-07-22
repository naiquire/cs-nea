using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.components
{
	public class queueing : Form
	{
		public queueing(string text)
		{

			lbl_header = new Label();
			pic_loading = new PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pic_loading)).BeginInit();
			this.SuspendLayout();
			///
			/// lbl_header
			///
			this.lbl_header.BackColor = this.BackColor;
			this.lbl_header.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_header.Location = new System.Drawing.Point(10, 10);
			this.lbl_header.Name = "lbl_header";
			this.lbl_header.Size = new System.Drawing.Size(172, 30);
			this.lbl_header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_header.TabIndex = 0;
			this.lbl_header.Text = text;
			///
			/// pic_loading
			///
			this.pic_loading.Image = global::client_app.Properties.Resources.loading;
			this.pic_loading.Location = new System.Drawing.Point(200, 20);
			this.pic_loading.Name = "loading";
			this.pic_loading.Size = new System.Drawing.Size(100, 100);
			this.pic_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pic_loading.TabIndex = 0;
			this.pic_loading.TabStop = false;
			///
			/// queueing
			/// 
			this.BackColor = System.Drawing.Color.Gray;
			this.ClientSize = new System.Drawing.Size(384, 216);
			this.Controls.Add(this.lbl_header);
			this.Controls.Add(this.pic_loading);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "abstractMenu";
			this.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pic_loading)).EndInit();

			this.CenterToScreen();
			this.ShowInTaskbar = false;
			this.ShowDialog();
		}

		public void close()
		{
			this.Hide();
			this.Close();
		}

		private Label lbl_header;
		private PictureBox pic_loading;
	}
}
