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
            btnLogout = new Button();
            lblUserStatus = new Label();
            lblUserName = new Label();
            pbAvatar = new PictureBox();
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
            panelSidebar.BackColor = Color.FromArgb(30, 20, 15);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Controls.Add(lblUserStatus);
            panelSidebar.Controls.Add(lblUserName);
            panelSidebar.Controls.Add(pbAvatar);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(4, 5, 4, 5);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(333, 923);
            panelSidebar.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(194, 24, 91);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(33, 800);
            btnLogout.Margin = new Padding(4, 5, 4, 5);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(267, 69);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "LOGOUT (Rút lui)";
            btnLogout.UseVisualStyleBackColor = false;
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
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUserName.ForeColor = Color.FromArgb(217, 119, 6);
            lblUserName.Location = new Point(29, 277);
            lblUserName.Margin = new Padding(4, 0, 4, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(232, 29);
            lblUserName.TabIndex = 2;
            lblUserName.Text = "HERO NAME HERE";
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
            // panelMainContent
            // 
            panelMainContent.Controls.Add(tbQuestLog);
            panelMainContent.Controls.Add(lblWelcome);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(333, 0);
            panelMainContent.Margin = new Padding(4, 5, 4, 5);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(996, 923);
            panelMainContent.TabIndex = 0;
            // 
            // tbQuestLog
            // 
            tbQuestLog.BackColor = Color.FromArgb(50, 40, 35);
            tbQuestLog.BorderStyle = BorderStyle.None;
            tbQuestLog.Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbQuestLog.ForeColor = Color.WhiteSmoke;
            tbQuestLog.Location = new Point(40, 231);
            tbQuestLog.Margin = new Padding(4, 5, 4, 5);
            tbQuestLog.Name = "tbQuestLog";
            tbQuestLog.ReadOnly = true;
            tbQuestLog.Size = new Size(787, 615);
            tbQuestLog.TabIndex = 1;
            tbQuestLog.Text = "";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Arial", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(33, 46);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(831, 55);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "WELCOME TO FIGHTER X FIGHTER";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 15, 8);
            ClientSize = new Size(1329, 923);
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
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.PictureBox pbAvatar;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.RichTextBox tbQuestLog;
    }
}
