﻿using System.Drawing;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            pbAvatar = new PictureBox();
            lblUserName = new Label();
            lblUserStatus = new Label();
            btn_createroom = new Btn_Pixel();
            btn_joinroom = new Btn_Pixel();
            btnLogout = new Btn_Pixel();
            panelMainContent = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox7 = new PictureBox();
            pictureBox9 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            tbQuestLog = new RichTextBox();
            lblWelcome = new Label();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            panelMainContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(180, 83, 9);
            panelSidebar.Controls.Add(pbAvatar);
            panelSidebar.Controls.Add(lblUserName);
            panelSidebar.Controls.Add(lblUserStatus);
            panelSidebar.Controls.Add(btn_createroom);
            panelSidebar.Controls.Add(btn_joinroom);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(4, 5, 4, 5);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(335, 789);
            panelSidebar.TabIndex = 1;
            // 
            // pbAvatar
            // 
            pbAvatar.BackColor = Color.FromArgb(255, 192, 128);
            pbAvatar.BorderStyle = BorderStyle.FixedSingle;
            pbAvatar.Location = new Point(78, 65);
            pbAvatar.Margin = new Padding(4, 5, 4, 5);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.Size = new Size(169, 165);
            pbAvatar.TabIndex = 3;
            pbAvatar.TabStop = false;
            pbAvatar.Tag = "Placeholder for Hero Avatar";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.BackColor = Color.Transparent;
            lblUserName.Font = new Font("Courier New", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUserName.ForeColor = Color.FromArgb(64, 0, 0);
            lblUserName.Location = new Point(7, 262);
            lblUserName.Margin = new Padding(4, 0, 4, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(326, 41);
            lblUserName.TabIndex = 2;
            lblUserName.Text = "HERO NAME HERE";
            // 
            // lblUserStatus
            // 
            lblUserStatus.AutoSize = true;
            lblUserStatus.ForeColor = Color.Silver;
            lblUserStatus.Location = new Point(29, 323);
            lblUserStatus.Margin = new Padding(4, 0, 4, 0);
            lblUserStatus.Name = "lblUserStatus";
            lblUserStatus.Size = new Size(236, 20);
            lblUserStatus.TabIndex = 1;
            lblUserStatus.Text = "Status: Connected to Server Realm";
            // 
            // btn_createroom
            // 
            btn_createroom.BtnColor = Color.FromArgb(34, 139, 34);
            btn_createroom.FlatStyle = FlatStyle.Flat;
            btn_createroom.Font = new Font("Courier New", 10F, FontStyle.Bold);
            btn_createroom.ForeColor = Color.White;
            btn_createroom.Location = new Point(12, 410);
            btn_createroom.Name = "btn_createroom";
            btn_createroom.Size = new Size(314, 45);
            btn_createroom.TabIndex = 7;
            btn_createroom.Text = "▶ CREATR ROOM ◀";
            // 
            // btn_joinroom
            // 
            btn_joinroom.BtnColor = Color.FromArgb(34, 139, 34);
            btn_joinroom.FlatStyle = FlatStyle.Flat;
            btn_joinroom.Font = new Font("Courier New", 10F, FontStyle.Bold);
            btn_joinroom.ForeColor = Color.White;
            btn_joinroom.Location = new Point(12, 487);
            btn_joinroom.Name = "btn_joinroom";
            btn_joinroom.Size = new Size(314, 45);
            btn_joinroom.TabIndex = 8;
            btn_joinroom.Text = "▶ JOIN ROOM ◀";
            // 
            // btnLogout
            // 
            btnLogout.BtnColor = Color.FromArgb(194, 24, 91);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(29, 620);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(267, 69);
            btnLogout.TabIndex = 9;
            btnLogout.Text = "LOGOUT";
            btnLogout.Click += btnLogout_Click;
            // 
            // panelMainContent
            // 
            panelMainContent.BackgroundImage = Properties.Resources.background2;
            panelMainContent.Controls.Add(label5);
            panelMainContent.Controls.Add(label4);
            panelMainContent.Controls.Add(label3);
            panelMainContent.Controls.Add(label2);
            panelMainContent.Controls.Add(label1);
            panelMainContent.Controls.Add(pictureBox7);
            panelMainContent.Controls.Add(pictureBox9);
            panelMainContent.Controls.Add(pictureBox8);
            panelMainContent.Controls.Add(pictureBox6);
            panelMainContent.Controls.Add(pictureBox5);
            panelMainContent.Controls.Add(pictureBox4);
            panelMainContent.Controls.Add(pictureBox3);
            panelMainContent.Controls.Add(pictureBox2);
            panelMainContent.Controls.Add(pictureBox1);
            panelMainContent.Controls.Add(tbQuestLog);
            panelMainContent.Controls.Add(lblWelcome);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(335, 0);
            panelMainContent.Margin = new Padding(4, 5, 4, 5);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(977, 789);
            panelMainContent.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(180, 83, 9);
            label5.Font = new Font("Courier New", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(64, 0, 0);
            label5.Location = new Point(61, 620);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(194, 27);
            label5.TabIndex = 16;
            label5.Text = "Longnor Blaze";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(180, 83, 9);
            label4.Font = new Font("Courier New", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(64, 0, 0);
            label4.Location = new Point(299, 620);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(166, 27);
            label4.TabIndex = 15;
            label4.Text = "Linvay Gray";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(180, 83, 9);
            label3.Font = new Font("Courier New", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(64, 0, 0);
            label3.Location = new Point(719, 620);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(208, 27);
            label3.TabIndex = 14;
            label3.Text = "Scarlet Hunter";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(180, 83, 9);
            label2.Font = new Font("Courier New", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(64, 0, 0);
            label2.Location = new Point(530, 620);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(138, 27);
            label2.TabIndex = 13;
            label2.Text = "Bruise Li";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(180, 83, 9);
            label1.Font = new Font("Courier New", 40F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(64, 0, 0);
            label1.Location = new Point(157, 283);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(672, 76);
            label1.TabIndex = 12;
            label1.Text = "CHOOSE YOUR HERO";
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox7.Image = Properties.Resources.thanhspeed;
            pictureBox7.Location = new Point(746, 416);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(148, 54);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 9;
            pictureBox7.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox9.Image = Properties.Resources.balanceskill;
            pictureBox9.Location = new Point(504, 391);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(186, 79);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 11;
            pictureBox9.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox8.Image = Properties.Resources.thanhdamage;
            pictureBox8.Location = new Point(69, 416);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(173, 54);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 10;
            pictureBox8.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox6.Image = Properties.Resources.thanh_hp;
            pictureBox6.Location = new Point(306, 397);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(145, 64);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 8;
            pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.logogame;
            pictureBox5.Location = new Point(130, 0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(724, 269);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 7;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox4.Image = Properties.Resources.boy3;
            pictureBox4.Location = new Point(296, 467);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(164, 140);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox3.Image = Properties.Resources.boy2;
            pictureBox3.Location = new Point(516, 467);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(164, 140);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox2.Image = Properties.Resources.boy1;
            pictureBox2.Location = new Point(69, 467);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(164, 140);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(180, 83, 9);
            pictureBox1.Image = Properties.Resources.girlwithgun;
            pictureBox1.Location = new Point(737, 467);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(164, 140);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // tbQuestLog
            // 
            tbQuestLog.BackColor = Color.FromArgb(180, 83, 9);
            tbQuestLog.BorderStyle = BorderStyle.None;
            tbQuestLog.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbQuestLog.ForeColor = Color.WhiteSmoke;
            tbQuestLog.Location = new Point(20, 274);
            tbQuestLog.Margin = new Padding(4, 5, 4, 5);
            tbQuestLog.Name = "tbQuestLog";
            tbQuestLog.ReadOnly = true;
            tbQuestLog.Size = new Size(933, 501);
            tbQuestLog.TabIndex = 1;
            tbQuestLog.Text = "";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.BackColor = Color.SaddleBrown;
            lblWelcome.Font = new Font("Courier New", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = Color.SandyBrown;
            lblWelcome.Location = new Point(157, 9);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(0, 53);
            lblWelcome.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 15, 8);
            ClientSize = new Size(1312, 789);
            Controls.Add(panelMainContent);
            Controls.Add(panelSidebar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trang Chủ - Socket Client";
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            panelMainContent.ResumeLayout(false);
            panelMainContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.PictureBox pbAvatar;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.RichTextBox tbQuestLog;
        private Btn_Pixel btn_createroom;
        private Btn_Pixel btn_joinroom;
        private Btn_Pixel btnLogout;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label5;
        private Label label4;
    }
}
