using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Socket_LTMCB.Services;

namespace Socket_LTMCB
{
    public partial class FormQuenPass : Form
    {
        private readonly TcpClientService tcpClient; // ✅ Dùng TCP thay vì DatabaseService

        public FormQuenPass()
        {
            InitializeComponent();
            tcpClient = new TcpClientService("127.0.0.1", 8080); // ✅ Kết nối TCP server
            lblContactError.Text = "";
        }

        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {
            lblContactError.Text = "";
            string input = tb_contact.Text.Trim();

            // 1️⃣ Kiểm tra đầu vào
            if (string.IsNullOrEmpty(input))
            {
                lblContactError.Text = "⚠ Please enter your Email or Phone number.";
                return;
            }

            bool isEmail = IsValidEmail(input);
            bool isPhone = IsValidPhone(input);

            if (!isEmail && !isPhone)
            {
                lblContactError.Text = "Please enter a valid email or phone number format!";
                return;
            }

            try
            {
                // 2️⃣ Gọi ForgotPassword (server tự kiểm tra contact type)
                var response = tcpClient.ForgotPassword(input);

                // 3️⃣ Kiểm tra phản hồi
                if (response != null && response.Success)
                {
                    string username = response.GetDataValue("username");
                    string otp = response.GetDataValue("otp");

                    MessageBox.Show(
                        $"✅ OTP sent successfully!\n\nYour OTP code is: {otp}\n(This is shown for testing — in production it will be sent via Email/SMS.)",
                        "OTP Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormXacThucOTP formOtp = new FormXacThucOTP(username);
                    formOtp.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(response?.Message ?? "Account not found!",
                        "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message,
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }
    }
}