using System.Drawing;

namespace Socket_LTMCB
{
    partial class FormDangKy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelOuter;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.PictureBox pictureBoxSwords;

        private System.Windows.Forms.Panel panelUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label lblUsernameError;

        private System.Windows.Forms.Panel panelContact;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.Label lblContactError;

        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.PictureBox pictureBoxLock1;
        private System.Windows.Forms.Label lblPasswordError;

        private System.Windows.Forms.Panel panelConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox tb_confirmPassword;
        private System.Windows.Forms.PictureBox pictureBoxLock2;
        private System.Windows.Forms.Label lblConfirmPasswordError;

        private System.Windows.Forms.Panel panelRobotCheck;
        private System.Windows.Forms.CheckBox chkNotRobot;
        private System.Windows.Forms.Label lblRobotError;

        // ĐÃ SỬA: Thay System.Windows.Forms.Button thành Btn_Pixel
        private Btn_Pixel btn_register;
        private Btn_Pixel btn_alreadyHaveAccount;

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblFooterTitle;
        private System.Windows.Forms.Label lblFooterSubtitle;

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
        private void InitializeComponent()
        {
            panelOuter = new Panel();
            panelMain = new Panel();
            panelHeader = new Panel();
            pictureBoxSwords = new PictureBox();
            lblSubtitle = new Label();
            lblTitle = new Label();
            panelUsername = new Panel();
            lblUsernameError = new Label();
            tb_username = new TextBox();
            lblUsername = new Label();
            panelContact = new Panel();
            lblContactError = new Label();
            tb_email = new TextBox();
            lblContact = new Label();
            panelPassword = new Panel();
            lblPasswordError = new Label();
            pictureBoxLock1 = new PictureBox();
            tb_password = new TextBox();
            lblPassword = new Label();
            panelConfirmPassword = new Panel();
            lblConfirmPasswordError = new Label();
            pictureBoxLock2 = new PictureBox();
            tb_confirmPassword = new TextBox();
            lblConfirmPassword = new Label();
            panelRobotCheck = new Panel();
            lblRobotError = new Label();
            chkNotRobot = new CheckBox();
            btn_register = new Btn_Pixel();
            btn_alreadyHaveAccount = new Btn_Pixel();
            panelFooter = new Panel();
            lblFooterSubtitle = new Label();
            lblFooterTitle = new Label();
            panelOuter.SuspendLayout();
            panelMain.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSwords).BeginInit();
            panelUsername.SuspendLayout();
            panelContact.SuspendLayout();
            panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock1).BeginInit();
            panelConfirmPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock2).BeginInit();
            panelRobotCheck.SuspendLayout();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // panelOuter
            // 
            panelOuter.BackColor = Color.FromArgb(180, 83, 9);
            panelOuter.Controls.Add(panelMain);
            panelOuter.Dock = DockStyle.Fill;
            panelOuter.Location = new Point(0, 0);
            panelOuter.Name = "panelOuter";
            panelOuter.Padding = new Padding(12);
            panelOuter.Size = new Size(677, 880);
            panelOuter.TabIndex = 0;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(146, 64, 14);
            panelMain.Controls.Add(panelHeader);
            panelMain.Controls.Add(panelUsername);
            panelMain.Controls.Add(panelContact);
            panelMain.Controls.Add(panelPassword);
            panelMain.Controls.Add(panelConfirmPassword);
            panelMain.Controls.Add(panelRobotCheck);
            panelMain.Controls.Add(btn_register);
            panelMain.Controls.Add(btn_alreadyHaveAccount);
            panelMain.Controls.Add(panelFooter);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(12, 12);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(32);
            panelMain.Size = new Size(653, 856);
            panelMain.TabIndex = 0;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(41, 37, 36);
            panelHeader.Controls.Add(pictureBoxSwords);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(32, 32);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(589, 90);
            panelHeader.TabIndex = 0;
            // 
            // pictureBoxSwords
            // 
            pictureBoxSwords.Image = Properties.Resources.c6d82a81_54de_43be_a1c7_439b407ae76c;
            pictureBoxSwords.Location = new Point(397, 3);
            pictureBoxSwords.Name = "pictureBoxSwords";
            pictureBoxSwords.Size = new Size(50, 30);
            pictureBoxSwords.TabIndex = 2;
            pictureBoxSwords.TabStop = false;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Courier New", 8F);
            lblSubtitle.ForeColor = Color.White;
            lblSubtitle.Location = new Point(0, 61);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(589, 29);
            lblSubtitle.TabIndex = 3;
            lblSubtitle.Text = "CREATE YOUR ACCOUNT";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Courier New", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(250, 204, 21);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(589, 47);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "NEW PLAYER";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUsername
            // 
            panelUsername.Controls.Add(lblUsernameError);
            panelUsername.Controls.Add(tb_username);
            panelUsername.Controls.Add(lblUsername);
            panelUsername.Location = new Point(32, 140);
            panelUsername.Name = "panelUsername";
            panelUsername.Size = new Size(592, 80);
            panelUsername.TabIndex = 1;
            // 
            // lblUsernameError
            // 
            lblUsernameError.Dock = DockStyle.Bottom;
            lblUsernameError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblUsernameError.ForeColor = Color.Red;
            lblUsernameError.Location = new Point(0, 58);
            lblUsernameError.Name = "lblUsernameError";
            lblUsernameError.Size = new Size(592, 22);
            lblUsernameError.TabIndex = 0;
            // 
            // tb_username
            // 
            tb_username.BackColor = Color.FromArgb(41, 37, 36);
            tb_username.BorderStyle = BorderStyle.None;
            tb_username.Font = new Font("Courier New", 14F);
            tb_username.ForeColor = Color.FromArgb(214, 211, 209);
            tb_username.Location = new Point(0, 25);
            tb_username.Multiline = true;
            tb_username.Name = "tb_username";
            tb_username.Size = new Size(592, 35);
            tb_username.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(0, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(150, 20);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "👤 USERNAME:";
            // 
            // panelContact
            // 
            panelContact.Controls.Add(lblContactError);
            panelContact.Controls.Add(tb_email);
            panelContact.Controls.Add(lblContact);
            panelContact.Location = new Point(32, 240);
            panelContact.Name = "panelContact";
            panelContact.Size = new Size(592, 80);
            panelContact.TabIndex = 2;
            // 
            // lblContactError
            // 
            lblContactError.Dock = DockStyle.Bottom;
            lblContactError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblContactError.ForeColor = Color.Red;
            lblContactError.Location = new Point(0, 58);
            lblContactError.Name = "lblContactError";
            lblContactError.Size = new Size(592, 22);
            lblContactError.TabIndex = 0;
            // 
            // tb_email
            // 
            tb_email.BackColor = Color.FromArgb(41, 37, 36);
            tb_email.BorderStyle = BorderStyle.None;
            tb_email.Font = new Font("Courier New", 14F);
            tb_email.ForeColor = Color.FromArgb(214, 211, 209);
            tb_email.Location = new Point(0, 25);
            tb_email.Multiline = true;
            tb_email.Name = "tb_email";
            tb_email.Size = new Size(592, 35);
            tb_email.TabIndex = 1;
            // 
            // lblContact
            // 
            lblContact.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblContact.ForeColor = Color.White;
            lblContact.Location = new Point(0, 0);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(250, 20);
            lblContact.TabIndex = 2;
            lblContact.Text = "✉/📞 EMAIL/PHONE:";
            // 
            // panelPassword
            // 
            panelPassword.Controls.Add(lblPasswordError);
            panelPassword.Controls.Add(pictureBoxLock1);
            panelPassword.Controls.Add(tb_password);
            panelPassword.Controls.Add(lblPassword);
            panelPassword.Location = new Point(32, 340);
            panelPassword.Name = "panelPassword";
            panelPassword.Size = new Size(592, 80);
            panelPassword.TabIndex = 3;
            // 
            // lblPasswordError
            // 
            lblPasswordError.Dock = DockStyle.Bottom;
            lblPasswordError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblPasswordError.ForeColor = Color.Red;
            lblPasswordError.Location = new Point(0, 58);
            lblPasswordError.Name = "lblPasswordError";
            lblPasswordError.Size = new Size(592, 22);
            lblPasswordError.TabIndex = 0;
            // 
            // pictureBoxLock1
            // 
            pictureBoxLock1.BackColor = Color.FromArgb(41, 37, 36);
            pictureBoxLock1.Location = new Point(542, 25);
            pictureBoxLock1.Name = "pictureBoxLock1";
            pictureBoxLock1.Size = new Size(50, 35);
            pictureBoxLock1.TabIndex = 2;
            pictureBoxLock1.TabStop = false;
            // 
            // tb_password
            // 
            tb_password.BackColor = Color.FromArgb(41, 37, 36);
            tb_password.BorderStyle = BorderStyle.None;
            tb_password.Font = new Font("Courier New", 14F);
            tb_password.ForeColor = Color.FromArgb(214, 211, 209);
            tb_password.Location = new Point(0, 25);
            tb_password.Multiline = true;
            tb_password.Name = "tb_password";
            tb_password.PasswordChar = '●';
            tb_password.Size = new Size(592, 35);
            tb_password.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.White;
            lblPassword.Location = new Point(0, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(150, 20);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "🔒 PASSWORD:";
            // 
            // panelConfirmPassword
            // 
            panelConfirmPassword.Controls.Add(lblConfirmPasswordError);
            panelConfirmPassword.Controls.Add(pictureBoxLock2);
            panelConfirmPassword.Controls.Add(tb_confirmPassword);
            panelConfirmPassword.Controls.Add(lblConfirmPassword);
            panelConfirmPassword.Location = new Point(32, 440);
            panelConfirmPassword.Name = "panelConfirmPassword";
            panelConfirmPassword.Size = new Size(592, 80);
            panelConfirmPassword.TabIndex = 4;
            // 
            // lblConfirmPasswordError
            // 
            lblConfirmPasswordError.Dock = DockStyle.Bottom;
            lblConfirmPasswordError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblConfirmPasswordError.ForeColor = Color.Red;
            lblConfirmPasswordError.Location = new Point(0, 58);
            lblConfirmPasswordError.Name = "lblConfirmPasswordError";
            lblConfirmPasswordError.Size = new Size(592, 22);
            lblConfirmPasswordError.TabIndex = 0;
            // 
            // pictureBoxLock2
            // 
            pictureBoxLock2.BackColor = Color.FromArgb(41, 37, 36);
            pictureBoxLock2.Location = new Point(542, 25);
            pictureBoxLock2.Name = "pictureBoxLock2";
            pictureBoxLock2.Size = new Size(50, 35);
            pictureBoxLock2.TabIndex = 2;
            pictureBoxLock2.TabStop = false;
            // 
            // tb_confirmPassword
            // 
            tb_confirmPassword.BackColor = Color.FromArgb(41, 37, 36);
            tb_confirmPassword.BorderStyle = BorderStyle.None;
            tb_confirmPassword.Font = new Font("Courier New", 14F);
            tb_confirmPassword.ForeColor = Color.FromArgb(214, 211, 209);
            tb_confirmPassword.Location = new Point(0, 25);
            tb_confirmPassword.Multiline = true;
            tb_confirmPassword.Name = "tb_confirmPassword";
            tb_confirmPassword.PasswordChar = '●';
            tb_confirmPassword.Size = new Size(592, 35);
            tb_confirmPassword.TabIndex = 1;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = Color.White;
            lblConfirmPassword.Location = new Point(0, 0);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(250, 20);
            lblConfirmPassword.TabIndex = 3;
            lblConfirmPassword.Text = "🔒 CONFIRM PASS:";
            // 
            // panelRobotCheck
            // 
            panelRobotCheck.BackColor = Color.FromArgb(41, 37, 36);
            panelRobotCheck.Controls.Add(lblRobotError);
            panelRobotCheck.Controls.Add(chkNotRobot);
            panelRobotCheck.Location = new Point(32, 540);
            panelRobotCheck.Name = "panelRobotCheck";
            panelRobotCheck.Size = new Size(592, 45);
            panelRobotCheck.TabIndex = 5;
            // 
            // lblRobotError
            // 
            lblRobotError.Dock = DockStyle.Bottom;
            lblRobotError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblRobotError.ForeColor = Color.Red;
            lblRobotError.Location = new Point(0, 33);
            lblRobotError.Name = "lblRobotError";
            lblRobotError.Size = new Size(592, 12);
            lblRobotError.TabIndex = 0;
            // 
            // chkNotRobot
            // 
            chkNotRobot.Appearance = Appearance.Button;
            chkNotRobot.BackColor = Color.Transparent;
            chkNotRobot.Dock = DockStyle.Fill;
            chkNotRobot.FlatAppearance.BorderSize = 0;
            chkNotRobot.FlatAppearance.CheckedBackColor = Color.Green;
            chkNotRobot.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
            chkNotRobot.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            chkNotRobot.FlatStyle = FlatStyle.Flat;
            chkNotRobot.Font = new Font("Courier New", 14F, FontStyle.Bold);
            chkNotRobot.ForeColor = Color.White;
            chkNotRobot.Location = new Point(0, 0);
            chkNotRobot.Name = "chkNotRobot";
            chkNotRobot.Size = new Size(592, 45);
            chkNotRobot.TabIndex = 1;
            chkNotRobot.Text = "  ☐ I'M NOT A ROBOT  🤖";
            chkNotRobot.UseVisualStyleBackColor = false;
            // 
            // btn_register
            // 
            btn_register.BackColor = Color.FromArgb(34, 197, 94);
            btn_register.BtnColor = Color.FromArgb(34, 197, 94);
            btn_register.FlatAppearance.BorderSize = 0;
            btn_register.FlatStyle = FlatStyle.Flat;
            btn_register.Font = new Font("Courier New", 18F, FontStyle.Bold);
            btn_register.ForeColor = Color.FromArgb(41, 37, 36);
            btn_register.Location = new Point(32, 609);
            btn_register.Name = "btn_register";
            btn_register.Size = new Size(592, 60);
            btn_register.TabIndex = 6;
            btn_register.Text = "★ REGISTER ★";
            btn_register.UseVisualStyleBackColor = false;
            btn_register.Click += btn_register_Click;
            // 
            // btn_alreadyHaveAccount
            // 
            btn_alreadyHaveAccount.BackColor = Color.FromArgb(217, 119, 6);
            btn_alreadyHaveAccount.BtnColor = Color.FromArgb(217, 119, 6);
            btn_alreadyHaveAccount.FlatAppearance.BorderSize = 0;
            btn_alreadyHaveAccount.FlatStyle = FlatStyle.Flat;
            btn_alreadyHaveAccount.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btn_alreadyHaveAccount.ForeColor = Color.White;
            btn_alreadyHaveAccount.Location = new Point(32, 675);
            btn_alreadyHaveAccount.Name = "btn_alreadyHaveAccount";
            btn_alreadyHaveAccount.Size = new Size(592, 62);
            btn_alreadyHaveAccount.TabIndex = 7;
            btn_alreadyHaveAccount.Text = "← HAVE ACCOUNT? LOGIN";
            btn_alreadyHaveAccount.UseVisualStyleBackColor = false;
            btn_alreadyHaveAccount.Click += btn_alreadyHaveAccount_Click;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.FromArgb(41, 37, 36);
            panelFooter.Controls.Add(lblFooterSubtitle);
            panelFooter.Controls.Add(lblFooterTitle);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(32, 774);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(589, 50);
            panelFooter.TabIndex = 8;
            // 
            // lblFooterSubtitle
            // 
            lblFooterSubtitle.Dock = DockStyle.Bottom;
            lblFooterSubtitle.Font = new Font("Courier New", 8F);
            lblFooterSubtitle.ForeColor = Color.FromArgb(168, 162, 158);
            lblFooterSubtitle.Location = new Point(0, 27);
            lblFooterSubtitle.Name = "lblFooterSubtitle";
            lblFooterSubtitle.Size = new Size(589, 23);
            lblFooterSubtitle.TabIndex = 0;
            lblFooterSubtitle.Text = "CREATE HERO & START";
            lblFooterSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFooterTitle
            // 
            lblFooterTitle.Dock = DockStyle.Top;
            lblFooterTitle.Font = new Font("Courier New", 16F, FontStyle.Bold);
            lblFooterTitle.ForeColor = Color.FromArgb(250, 204, 21);
            lblFooterTitle.Location = new Point(0, 0);
            lblFooterTitle.Name = "lblFooterTitle";
            lblFooterTitle.Size = new Size(589, 27);
            lblFooterTitle.TabIndex = 1;
            lblFooterTitle.Text = "JOIN THE QUEST!";
            lblFooterTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormDangKy
            // 
            ClientSize = new Size(677, 880);
            Controls.Add(panelOuter);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormDangKy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ADVENTURE - NEW PLAYER REGISTRATION";
            panelOuter.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxSwords).EndInit();
            panelUsername.ResumeLayout(false);
            panelUsername.PerformLayout();
            panelContact.ResumeLayout(false);
            panelContact.PerformLayout();
            panelPassword.ResumeLayout(false);
            panelPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock1).EndInit();
            panelConfirmPassword.ResumeLayout(false);
            panelConfirmPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock2).EndInit();
            panelRobotCheck.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}