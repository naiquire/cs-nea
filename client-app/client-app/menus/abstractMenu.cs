using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus
{
    public abstract class abstractMenu : Form
    {
        protected Panel panel_topBorder;
        protected Button btn_close;
        protected Label lbl_appName;
        protected Panel panel_left;
        protected Panel panel_topLeft;
        protected Panel panel_main;
        protected Panel panel_right;

        protected virtual void InitializeComponent()
        {
            Controls.Clear();

            panel_topBorder = new Panel();
            lbl_appName = new Label();
            btn_close = new Button();
            panel_left = new Panel();
            panel_topLeft = new Panel();
            panel_main = new Panel();
            panel_right = new Panel();
            
            panel_topBorder.SuspendLayout();
            panel_main.SuspendLayout();
            SuspendLayout();
            // 
            // panel_topBorder
            // 
            panel_topBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
            panel_topBorder.Controls.Add(lbl_appName);
            panel_topBorder.Controls.Add(btn_close);
            panel_topBorder.Location = new System.Drawing.Point(0, 0);
            panel_topBorder.Name = "panel_topBorder";
            panel_topBorder.Size = new System.Drawing.Size(1920, 30);
            panel_topBorder.TabIndex = 0;
            // 
            // lbl_appName
            // 
            lbl_appName.BackColor = panel_topBorder.BackColor;
            lbl_appName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbl_appName.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_appName.Location = new System.Drawing.Point(10, 7);
            lbl_appName.Name = "lbl_appName";
            lbl_appName.Size = new System.Drawing.Size(100, 16);
            lbl_appName.TabIndex = 0;
            lbl_appName.Text = "appName";
            // 
            // btn_close
            // 
            btn_close.Location = new System.Drawing.Point(1890, 0);
            btn_close.Name = "btn_close";
            btn_close.Size = new System.Drawing.Size(30, 30);
            btn_close.TabIndex = 0;
            btn_close.Text = "X";
            btn_close.UseVisualStyleBackColor = true;
            // 
            // panel_left
            // 
            panel_left.AutoScroll = true;
            panel_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
            panel_left.Location = new System.Drawing.Point(0, 130);
            panel_left.Name = "panel_left";
            panel_left.Size = new System.Drawing.Size(300, 950);
            panel_left.TabIndex = 2;
            // 
            // panel_topLeft
            // 
            panel_topLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            panel_topLeft.Location = new System.Drawing.Point(0, 30);
            panel_topLeft.Name = "panel_topLeft";
            panel_topLeft.Size = new System.Drawing.Size(300, 100);
            panel_topLeft.TabIndex = 1;
            // 
            // panel_main
            // 
            panel_main.BackColor = System.Drawing.Color.Transparent;
            panel_main.Location = new System.Drawing.Point(300, 30);
            panel_main.Name = "panel_main";
            panel_main.Size = new System.Drawing.Size(1120, 1050);
            panel_main.TabIndex = 4;
            // 
            // panel_right
            // 
            panel_right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
            panel_right.Location = new System.Drawing.Point(1420, 30);
            panel_right.Name = "panel_right";
            panel_right.Size = new System.Drawing.Size(500, 1050);
            panel_right.TabIndex = 3;
            // 
            // main
            // 
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1920, 1080);
            Controls.Add(panel_topLeft);
            Controls.Add(panel_topBorder);
            Controls.Add(panel_left);
            Controls.Add(panel_main);
            Controls.Add(panel_right);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "main";
        }
    }
}
