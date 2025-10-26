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
        private System.Windows.Forms.Timer otpTimer;
        private int remainingSeconds = 300;

        public FormXacThucOTP(string username)
        {
            InitializeComponent();
            _username = username;
            _databaseService = new DatabaseService();

            InitializeTimer();
            InitializeOTPAutoFocus();
        }

        private void InitializeTimer()
        {
            otpTimer = new System.Windows.Forms.Timer();
            otpTimer.Interval = 1000;
            otpTimer.Tick += OtpTimer_Tick;
            otpTimer.Start();
        }

        private void InitializeOTPAutoFocus()
        {
            // Auto focus giữa các ô OTP
            tb_otp1.TextChanged += (s, e) => { if (tb_otp1.Text.Length == 1) tb_otp2.Focus(); };
            tb_otp2.TextChanged += (s, e) => { if (tb_otp2.Text.Length == 1) tb_otp3.Focus(); };
            tb_otp3.TextChanged += (s, e) => { if (tb_otp3.Text.Length == 1) tb_otp4.Focus(); };
            tb_otp4.TextChanged += (s, e) => { if (tb_otp4.Text.Length == 1) tb_otp5.Focus(); };
            tb_otp5.TextChanged += (s, e) => { if (tb_otp5.Text.Length == 1) tb_otp6.Focus(); };

            // Chỉ cho phép nhập số
            tb_otp1.KeyPress += OtpBox_KeyPress;
            tb_otp2.KeyPress += OtpBox_KeyPress;
            tb_otp3.KeyPress += OtpBox_KeyPress;
            tb_otp4.KeyPress += OtpBox_KeyPress;
            tb_otp5.KeyPress += OtpBox_KeyPress;
            tb_otp6.KeyPress += OtpBox_KeyPress;
        }
        private void OtpTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;

            if (remainingSeconds <= 0)
            {
                otpTimer.Stop();
                lbl_timer.Text = "OTP đã hết hạn!";
                lbl_timer.ForeColor = Color.Red;
                btn_verify.Enabled = false;
                return;
            }

            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            lbl_timer.Text = $"Code expires in: {minutes:D2}:{seconds:D2}";

            // Cảnh báo khi còn 30 giây
            if (remainingSeconds <= 30)
            {
                lbl_timer.ForeColor = Color.Red;
            }
        }


        private void btn_verify_Click(object sender, EventArgs e)
        {
            lblOTPError.Text = "";
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
                lblOTPError.Text = "Please enter all 6 digits of the OTP!";
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
            MessageBox.Show($"Your new OTP is: {newOtp}\n(This is shown for testing only)",
                "New OTP", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset timer
            remainingSeconds = 300;
            lbl_timer.ForeColor = Color.White;
            btn_verify.Enabled = true;
            otpTimer.Start();
        }
        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            otpTimer?.Stop();
            otpTimer?.Dispose();
            base.OnFormClosing(e);
        }


        private void OtpBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }


}
