using System.Drawing;
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
            tbQuestLog = new RichTextBox();
            lblWelcome = new Label();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            panelMainContent.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
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
            panelSidebar.Size = new Size(333, 742);
            panelSidebar.TabIndex = 1;
            // 
            // pbAvatar
            // 
            pbAvatar.BackColor = Color.FromArgb(50, 50, 50);
            pbAvatar.BorderStyle = BorderStyle.FixedSingle;
            pbAvatar.Location = new Point(100, 77);
            pbAvatar.Margin = new Padding(4, 5, 4, 5);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.Size = new Size(133, 153);
            pbAvatar.TabIndex = 3;
            pbAvatar.TabStop = false;
            pbAvatar.Tag = "Placeholder for Hero Avatar";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Courier New", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUserName.ForeColor = Color.FromArgb(217, 119, 6);
            lblUserName.Location = new Point(29, 277);
            lblUserName.Margin = new Padding(4, 0, 4, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(208, 27);
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
            btn_createroom.Size = new Size(297, 45);
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
            btn_joinroom.Size = new Size(297, 45);
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
            panelMainContent.Controls.Add(tbQuestLog);
            panelMainContent.Controls.Add(lblWelcome);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(333, 0);
            panelMainContent.Margin = new Padding(4, 5, 4, 5);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(905, 742);
            panelMainContent.TabIndex = 0;
            // 
            // tbQuestLog
            // 
            tbQuestLog.BackColor = Color.FromArgb(50, 40, 35);
            tbQuestLog.BorderStyle = BorderStyle.None;
            tbQuestLog.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbQuestLog.ForeColor = Color.WhiteSmoke;
            tbQuestLog.Location = new Point(33, 106);
            tbQuestLog.Margin = new Padding(4, 5, 4, 5);
            tbQuestLog.Name = "tbQuestLog";
            tbQuestLog.ReadOnly = true;
            tbQuestLog.Size = new Size(807, 583);
            tbQuestLog.TabIndex = 1;
            tbQuestLog.Text = "";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Courier New", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(33, 46);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(807, 53);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "WELCOME TO FIGHTER X FIGHTER";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 15, 8);
            ClientSize = new Size(1238, 742);
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
    }
}
