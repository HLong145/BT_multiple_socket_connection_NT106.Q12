using System;
using System.Linq;
using System.Windows.Forms;
using Socket_LTMCB.Services;

namespace Socket_LTMCB
{
    public partial class FormXacThucOTP : Form
    {
        private readonly string _username;
        private readonly DatabaseService _databaseService;

        public FormXacThucOTP(string username)
        {
            InitializeComponent();
            _username = username;
            _databaseService = new DatabaseService();
        }

        private void btn_verify_Click(object sender, EventArgs e)
        {
            string otp = string.Concat(
                tb_otp1.Text.Trim(),
                tb_otp2.Text.Trim(),
                tb_otp3.Text.Trim(),
                tb_otp4.Text.Trim(),
                tb_otp5.Text.Trim(),
                tb_otp6.Text.Trim()
            );

            if (otp.Length != 6 || !otp.All(char.IsDigit))
            {
                MessageBox.Show("Please enter all 6 digits of the OTP!",
                    "Missing OTP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = _databaseService.VerifyOtp(_username, otp);

            if (result.IsValid)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormResetPass formReset = new FormResetPass(_username);
                formReset.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_resend_Click(object sender, EventArgs e)
        {
            string newOtp = _databaseService.GenerateOtp(_username);
            MessageBox.Show($"Your new OTP is: {newOtp}\n(This is shown for testing only, later it will be sent via Email/SMS)",
                "New OTP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
