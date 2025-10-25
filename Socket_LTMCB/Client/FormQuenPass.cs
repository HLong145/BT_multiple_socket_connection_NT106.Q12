using System;
using System.Windows.Forms;
using Socket_LTMCB.Services;

namespace Socket_LTMCB
{
    public partial class FormQuenPass : Form
    {
        private readonly DatabaseService _databaseService;

        public FormQuenPass()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }
        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_continue_Click(object sender, EventArgs e)
        {
            string input = tb_contact.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please enter your email or phone number!",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isEmail = IsValidEmail(input);
            bool isPhone = IsValidPhone(input);

            if (!isEmail && !isPhone)
            {
                MessageBox.Show("Please enter a valid email or phone number format!",
                    "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string username = _databaseService.GetUsernameByContact(input, isEmail);

                if (username == null)
                {
                    MessageBox.Show("No account found matching this information!",
                        "Account Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string otp = _databaseService.GenerateOtp(username);

                MessageBox.Show($"Your OTP code is: {otp}\n(This is shown for testing purposes only. In production, it will be sent via email/SMS.)",
                    "OTP Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormXacThucOTP formOtp = new FormXacThucOTP(username);
                formOtp.Show();
                this.Hide();
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
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0\d{9}$");
        }
    }
}
