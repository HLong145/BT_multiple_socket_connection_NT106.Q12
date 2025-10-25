using System.Drawing;

namespace Socket_LTMCB
{
    partial class FormResetPass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
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
            pnl_Main = new Pnl_Pixel();
            pnl_Title = new Pnl_Pixel();
            lbl_Title = new Label();
            lbl_Subtitle = new Label();
            lbl_Description = new Label();
            panelNewPassword = new Panel();
            lblNewPasswordError = new Label();
            pictureBoxLock1 = new PictureBox();
            tb_newPassword = new Tb_Pixel();
            lblNewPassword = new Label();
            panelConfirmPassword = new Panel();
            lblConfirmPasswordError = new Label();
            pictureBoxLock2 = new PictureBox();
            tb_confirmPassword = new Tb_Pixel();
            lblConfirmPassword = new Label();
            btn_complete = new Btn_Pixel();
            btn_backToLogin = new Btn_Pixel();
            pnl_Main.SuspendLayout();
            pnl_Title.SuspendLayout();
            panelNewPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock1).BeginInit();
            panelConfirmPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock2).BeginInit();
            SuspendLayout();
            // 
            // pnl_Main
            // 
            pnl_Main.BackColor = Color.FromArgb(210, 105, 30);
            pnl_Main.Controls.Add(pnl_Title);
            pnl_Main.Controls.Add(lbl_Description);
            pnl_Main.Controls.Add(panelNewPassword);
            pnl_Main.Controls.Add(panelConfirmPassword);
            pnl_Main.Controls.Add(btn_complete);
            pnl_Main.Controls.Add(btn_backToLogin);
            pnl_Main.Location = new Point(87, 29);
            pnl_Main.Name = "pnl_Main";
            pnl_Main.Size = new Size(413, 553);
            pnl_Main.TabIndex = 0;
            // 
            // pnl_Title
            // 
            pnl_Title.BackColor = Color.FromArgb(210, 105, 30);
            pnl_Title.Controls.Add(lbl_Title);
            pnl_Title.Controls.Add(lbl_Subtitle);
            pnl_Title.Location = new Point(20, 20);
            pnl_Title.Name = "pnl_Title";
            pnl_Title.Size = new Size(360, 100);
            pnl_Title.TabIndex = 0;
            // 
            // lbl_Title
            // 
            lbl_Title.BackColor = Color.Transparent;
            lbl_Title.Font = new Font("Courier New", 14F, FontStyle.Bold);
            lbl_Title.ForeColor = Color.Gold;
            lbl_Title.Location = new Point(16, 19);
            lbl_Title.Name = "lbl_Title";
            lbl_Title.Size = new Size(341, 30);
            lbl_Title.TabIndex = 0;
            lbl_Title.Text = "🔓 RESET PASSWORD 🔓";
            lbl_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Subtitle
            // 
            lbl_Subtitle.BackColor = Color.Transparent;
            lbl_Subtitle.Font = new Font("Courier New", 7F, FontStyle.Bold);
            lbl_Subtitle.ForeColor = Color.White;
            lbl_Subtitle.Location = new Point(16, 49);
            lbl_Subtitle.Name = "lbl_Subtitle";
            lbl_Subtitle.Size = new Size(325, 20);
            lbl_Subtitle.TabIndex = 1;
            lbl_Subtitle.Text = "CREATE NEW PASSWORD";
            lbl_Subtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Description
            // 
            lbl_Description.BackColor = Color.Transparent;
            lbl_Description.Font = new Font("Courier New", 8F, FontStyle.Regular);
            lbl_Description.ForeColor = Color.White;
            lbl_Description.Location = new Point(20, 140);
            lbl_Description.Name = "lbl_Description";
            lbl_Description.Size = new Size(360, 60);
            lbl_Description.TabIndex = 1;
            lbl_Description.Text = "Please enter your new password.\r\nMake sure it's strong and secure!\r\n(Min 6 characters)";
            lbl_Description.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelNewPassword
            // 
            panelNewPassword.Controls.Add(lblNewPasswordError);
            panelNewPassword.Controls.Add(pictureBoxLock1);
            panelNewPassword.Controls.Add(tb_newPassword);
            panelNewPassword.Controls.Add(lblNewPassword);
            panelNewPassword.Location = new Point(20, 220);
            panelNewPassword.Name = "panelNewPassword";
            panelNewPassword.Size = new Size(360, 80);
            panelNewPassword.TabIndex = 2;
            // 
            // lblNewPasswordError
            // 
            lblNewPasswordError.BackColor = Color.FromArgb(128, 64, 0);
            lblNewPasswordError.Dock = DockStyle.Bottom;
            lblNewPasswordError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblNewPasswordError.ForeColor = Color.Red;
            lblNewPasswordError.Location = new Point(0, 58);
            lblNewPasswordError.Name = "lblNewPasswordError";
            lblNewPasswordError.Size = new Size(360, 22);
            lblNewPasswordError.TabIndex = 0;
            // 
            // pictureBoxLock1
            // 
            pictureBoxLock1.BackColor = Color.FromArgb(41, 37, 36);
            pictureBoxLock1.Location = new Point(310, 25);
            pictureBoxLock1.Name = "pictureBoxLock1";
            pictureBoxLock1.Size = new Size(50, 35);
            pictureBoxLock1.TabIndex = 2;
            pictureBoxLock1.TabStop = false;
            // 
            // tb_newPassword
            // 
            tb_newPassword.BackColor = Color.FromArgb(42, 31, 26);
            tb_newPassword.BorderStyle = BorderStyle.None;
            tb_newPassword.Font = new Font("Courier New", 10F, FontStyle.Bold);
            tb_newPassword.ForeColor = Color.White;
            tb_newPassword.Location = new Point(0, 25);
            tb_newPassword.Multiline = true;
            tb_newPassword.Name = "tb_newPassword";
            tb_newPassword.PasswordChar = '●';
            tb_newPassword.Size = new Size(360, 35);
            tb_newPassword.TabIndex = 1;
            // 
            // lblNewPassword
            // 
            lblNewPassword.BackColor = Color.Transparent;
            lblNewPassword.Font = new Font("Courier New", 8F, FontStyle.Bold);
            lblNewPassword.ForeColor = Color.White;
            lblNewPassword.Location = new Point(0, 0);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(200, 20);
            lblNewPassword.TabIndex = 3;
            lblNewPassword.Text = "🔒 NEW PASSWORD:";
            // 
            // panelConfirmPassword
            // 
            panelConfirmPassword.Controls.Add(lblConfirmPasswordError);
            panelConfirmPassword.Controls.Add(pictureBoxLock2);
            panelConfirmPassword.Controls.Add(tb_confirmPassword);
            panelConfirmPassword.Controls.Add(lblConfirmPassword);
            panelConfirmPassword.Location = new Point(20, 320);
            panelConfirmPassword.Name = "panelConfirmPassword";
            panelConfirmPassword.Size = new Size(360, 80);
            panelConfirmPassword.TabIndex = 3;
            // 
            // lblConfirmPasswordError
            // 
            lblConfirmPasswordError.BackColor = Color.FromArgb(128, 64, 0);
            lblConfirmPasswordError.Dock = DockStyle.Bottom;
            lblConfirmPasswordError.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblConfirmPasswordError.ForeColor = Color.Red;
            lblConfirmPasswordError.Location = new Point(0, 58);
            lblConfirmPasswordError.Name = "lblConfirmPasswordError";
            lblConfirmPasswordError.Size = new Size(360, 22);
            lblConfirmPasswordError.TabIndex = 0;
            // 
            // pictureBoxLock2
            // 
            pictureBoxLock2.BackColor = Color.FromArgb(41, 37, 36);
            pictureBoxLock2.Location = new Point(310, 25);
            pictureBoxLock2.Name = "pictureBoxLock2";
            pictureBoxLock2.Size = new Size(50, 35);
            pictureBoxLock2.TabIndex = 2;
            pictureBoxLock2.TabStop = false;
            // 
            // tb_confirmPassword
            // 
            tb_confirmPassword.BackColor = Color.FromArgb(42, 31, 26);
            tb_confirmPassword.BorderStyle = BorderStyle.None;
            tb_confirmPassword.Font = new Font("Courier New", 10F, FontStyle.Bold);
            tb_confirmPassword.ForeColor = Color.White;
            tb_confirmPassword.Location = new Point(0, 25);
            tb_confirmPassword.Multiline = true;
            tb_confirmPassword.Name = "tb_confirmPassword";
            tb_confirmPassword.PasswordChar = '●';
            tb_confirmPassword.Size = new Size(360, 35);
            tb_confirmPassword.TabIndex = 1;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.BackColor = Color.Transparent;
            lblConfirmPassword.Font = new Font("Courier New", 8F, FontStyle.Bold);
            lblConfirmPassword.ForeColor = Color.White;
            lblConfirmPassword.Location = new Point(0, 0);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(250, 20);
            lblConfirmPassword.TabIndex = 3;
            lblConfirmPassword.Text = "🔒 CONFIRM PASSWORD:";
            // 
            // btn_complete
            // 
            btn_complete.BtnColor = Color.FromArgb(34, 139, 34);
            btn_complete.FlatStyle = FlatStyle.Flat;
            btn_complete.Font = new Font("Courier New", 12F, FontStyle.Bold);
            btn_complete.ForeColor = Color.White;
            btn_complete.Location = new Point(20, 430);
            btn_complete.Name = "btn_complete";
            btn_complete.Size = new Size(360, 50);
            btn_complete.TabIndex = 4;
            btn_complete.Text = "★ COMPLETE RESET ★";
            // 
            // btn_backToLogin
            // 
            btn_backToLogin.BtnColor = Color.FromArgb(139, 69, 19);
            btn_backToLogin.FlatStyle = FlatStyle.Flat;
            btn_backToLogin.Font = new Font("Courier New", 8F, FontStyle.Bold);
            btn_backToLogin.ForeColor = Color.White;
            btn_backToLogin.Location = new Point(20, 490);
            btn_backToLogin.Name = "btn_backToLogin";
            btn_backToLogin.Size = new Size(360, 40);
            btn_backToLogin.TabIndex = 5;
            btn_backToLogin.Text = "← BACK TO LOGIN";
            // 
            // FormResetPass
            // 
            BackColor = SystemColors.ControlDark;
            BackgroundImage = Properties.Resources.background2;
            ClientSize = new Size(581, 621);
            Controls.Add(pnl_Main);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormResetPass";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pixel Quest - Reset Password";
            pnl_Main.ResumeLayout(false);
            pnl_Title.ResumeLayout(false);
            panelNewPassword.ResumeLayout(false);
            panelNewPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock1).EndInit();
            panelConfirmPassword.ResumeLayout(false);
            panelConfirmPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLock2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Pnl_Pixel pnl_Main;
        private Pnl_Pixel pnl_Title;
        private Label lbl_Title;
        private Label lbl_Subtitle;
        private Label lbl_Description;
        private Panel panelNewPassword;
        private Label lblNewPasswordError;
        private PictureBox pictureBoxLock1;
        private Tb_Pixel tb_newPassword;
        private Label lblNewPassword;
        private Panel panelConfirmPassword;
        private Label lblConfirmPasswordError;
        private PictureBox pictureBoxLock2;
        private Tb_Pixel tb_confirmPassword;
        private Label lblConfirmPassword;
        private Btn_Pixel btn_complete;
        private Btn_Pixel btn_backToLogin;
    }
}