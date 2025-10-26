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
