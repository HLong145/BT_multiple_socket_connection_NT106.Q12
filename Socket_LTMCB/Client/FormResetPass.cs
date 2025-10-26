using System;
using System.Windows.Forms;
using Socket_LTMCB.Services;

namespace Socket_LTMCB
{
    public partial class FormResetPass : Form
    {
        private readonly string _username;
        private readonly TcpClientService tcpClient; // ✅ dùng TCP thay vì DatabaseService
        private readonly ValidationService _validationService;

        public FormResetPass(string username)
        {
            InitializeComponent();
            _username = username;
            tcpClient = new TcpClientService("127.0.0.1", 8080); // ✅ TCP client
            _validationService = new ValidationService();
        }

        public FormResetPass() : this(string.Empty)
        {
        }


        private void btn_complete_Click(object sender, EventArgs e)
        {
            lblNewPasswordError.Text = "";
            lblConfirmPasswordError.Text = "";

            string newPass = tb_newPassword.Text.Trim();
            string confirmPass = tb_confirmPassword.Text.Trim();

            // 1️⃣ Kiểm tra đầu vào
            if (string.IsNullOrEmpty(newPass))
            {
                lblNewPasswordError.Text = "Please enter a new password!";
                return;
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                lblConfirmPasswordError.Text = "Please confirm your password!";
                return;
            }

            if (newPass != confirmPass)
            {
                lblConfirmPasswordError.Text = "Password confirmation does not match!";
                return;
            }

            // 2️⃣ Kiểm tra độ mạnh của mật khẩu
            if (!_validationService.IsValidPassword(newPass))
            {
                MessageBox.Show(
                    "Password must be at least 8 characters long, include uppercase, lowercase, and a special character!",
                    "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 3️⃣ Gửi yêu cầu RESET_PASSWORD đến server qua TCP (giống ForgotPassword)
                var response = tcpClient.ResetPassword(_username, newPass);

                // 4️⃣ Kiểm tra phản hồi từ server
                if (response != null && response.Success)
                {
                    MessageBox.Show(
                        "✅ Password has been reset successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở lại form đăng nhập
                    this.Close();
                    FormDangNhap formLogin = new FormDangNhap();
                    formLogin.Show();
                }
                else
                {
                    MessageBox.Show(
                        response?.Message ?? "❌ Unable to reset password. Please try again!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"⚠ An unexpected error occurred:\n{ex.Message}",
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
