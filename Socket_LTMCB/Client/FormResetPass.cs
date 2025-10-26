﻿using System;
using System.Windows.Forms;
using Socket_LTMCB.Services;

namespace Socket_LTMCB
{
    public partial class FormResetPass : Form
    {
        private readonly string _username;
        private readonly TcpClientService tcpClient; // ✅ TCP CLIENT
        private readonly DatabaseService dbService;  // ✅ DATABASE SERVICE
        private readonly ValidationService _validationService;

        // ✅ CẤU HÌNH: true = dùng Server, false = dùng Database trực tiếp
        private bool useServer = true;

        public FormResetPass(string username)
        {
            InitializeComponent();
            _username = username;
            _validationService = new ValidationService();

            // ✅ KHỞI TẠO CẢ HAI SERVICE
            tcpClient = new TcpClientService("127.0.0.1", 8080);
            dbService = new DatabaseService();
        }

        public FormResetPass() : this(string.Empty)
        {
        }

        // ✅ RESET PASSWORD (ASYNC)
        private async void btn_complete_Click(object sender, EventArgs e)
        {
            lblNewPasswordError.Text = "";
            lblConfirmPasswordError.Text = "";

            string newPass = tb_newPassword.Text.Trim();
            string confirmPass = tb_confirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPass))
            {
                lblNewPasswordError.Text = "Please enter both password fields!";
                return;
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                lblConfirmPasswordError.Text = "Please enter both password fields!";
                return;
            }

            if (newPass != confirmPass)
            {
                lblConfirmPasswordError.Text = "Password confirmation does not match!";
                return;
            }

            // ✅ VALIDATE PASSWORD
            if (!_validationService.IsValidPassword(newPass))
            {
                MessageBox.Show(
                    "Password must be at least 8 characters long, including uppercase, lowercase, number and a special character!",
                    "Invalid Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            bool success = false;
            string message = "";

            // ✅ Disable nút trong lúc đang xử lý
            btn_complete.Enabled = false;

            if (useServer)
            {
                // ✅ DÙNG SERVER (ASYNC)
                var response = await tcpClient.ResetPasswordAsync(_username, newPass);
                success = response.Success;
                message = response.Message;
            }
            else
            {
                // ✅ DÙNG DATABASE TRỰC TIẾP
                success = dbService.ResetPassword(_username, newPass);
                message = success ? "Password reset successful" : "Password reset failed";
            }

            btn_complete.Enabled = true; // Re-enable button

            if (success)
            {
                MessageBox.Show(
                    "Password has been reset successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
                FormDangNhap formLogin = new FormDangNhap();
                formLogin.Show();
            }
            else
            {
                MessageBox.Show(
                    message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            FormDangNhap formLogin = new FormDangNhap();
            formLogin.Show();
        }
    }
}
