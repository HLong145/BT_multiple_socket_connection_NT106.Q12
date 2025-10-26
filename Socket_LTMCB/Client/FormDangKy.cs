using Socket_LTMCB.Services;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangKy : Form
    {
        private readonly TcpClientService tcpService = new TcpClientService("127.0.0.1", 8080);
        private readonly ValidationService validationService = new ValidationService();

        public event EventHandler SwitchToLogin;
        private System.Windows.Forms.Timer myTimer;

        public FormDangKy()
        {
            InitializeComponent();
            InitializeCustomUI();
            this.AutoScroll = true;
        }

        private void InitializeCustomUI()
        {
            // --- Placeholder setup ---
            SetPlaceholder(tb_username, "ENTER USERNAME");
            SetPlaceholder(tb_contact, "EMAIL OR PHONE");

            SetPasswordPlaceholder(tb_password, "ENTER PASSWORD");
            SetPasswordPlaceholder(tb_confirmPassword, "CONFIRM PASSWORD");

            // --- Icons ---
            DrawLockIcon(pictureBoxLock1, "🔒");
            DrawLockIcon(pictureBoxLock2, "🔒");

            // Checkbox default
            chkNotRobot.Text = "  ☐ I'M NOT A ROBOT  🤖";
            chkNotRobot.CheckedChanged += (s, e) =>
            {
                lblRobotError.Text = "";
                chkNotRobot.Text = chkNotRobot.Checked ? "  ☑ I'M NOT A ROBOT  🤖" : "  ☐ I'M NOT A ROBOT  🤖";
            };

            // ✅ Bật chế độ cuộn cho form
            this.AutoScroll = true;

            // Ví dụ: thêm 50 button, chắc chắn bị tràn
            for (int i = 0; i < 30; i++)
            {
                Button btn = new Button();
                btn.Text = "Button " + i;
                btn.Location = new Point(20, 30 * i);
                this.Controls.Add(btn);
            }
        }

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.FromArgb(87, 83, 78);
            tb.Enter += (s, e) =>
            {
                if (tb.Text == placeholder)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.FromArgb(214, 211, 209);
                }
            };
            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = Color.FromArgb(87, 83, 78);
                }
            };
        }

        private void SetPasswordPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.FromArgb(87, 83, 78);
            tb.PasswordChar = '\0';
            tb.Enter += (s, e) =>
            {
                if (tb.Text == placeholder)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.FromArgb(214, 211, 209);
                    tb.PasswordChar = '●';
                }
            };
            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = Color.FromArgb(87, 83, 78);
                    tb.PasswordChar = '\0';
                }
            };
        }

        private void DrawLockIcon(PictureBox pb, string icon)
        {
            pb.Paint += (s, e) =>
            {
                e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 16),
                    new SolidBrush(Color.FromArgb(217, 119, 6)), 5, 5);
            };
        }

        // =========================
        // Validation Helpers
        // =========================
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }

        private bool IsValidPassword(string password)
        {
            return validationService.IsValidPassword(password);
        }


        // =========================
        // Register Button Click
        // =========================
        private readonly TcpClientService tcpClient = new TcpClientService();
        private async void btn_register_Click(object sender, EventArgs e)
        {
            // Clear all error labels
            lblUsernameError.Text = "";
            lblContactError.Text = "";
            lblPasswordError.Text = "";
            lblConfirmPasswordError.Text = "";
            lblRobotError.Text = "";

            string username = tb_username.Text.Trim();
            string contact = tb_contact.Text.Trim();
            string password = tb_password.Text;
            string confirm = tb_confirmPassword.Text;

            // --- [1. Input Validation] ---
            if (string.IsNullOrEmpty(username) || username == "ENTER USERNAME")
            {
                lblUsernameError.Text = "⚠ Please enter your username.";
                return;
            }

            if (string.IsNullOrEmpty(contact) || contact == "EMAIL OR PHONE")
            {
                lblContactError.Text = "⚠ Please enter your Email or Phone number.";
                return;
            }

            if (!chkNotRobot.Checked)
            {
                lblRobotError.Text = "⚠ Please verify the captcha.";
                return;
            }

            bool isEmail = IsValidEmail(contact);
            bool isPhone = IsValidPhone(contact);

            if (!isEmail && !isPhone)
            {
                lblContactError.Text = "⚠ Please enter a valid Email or Phone number.";
                return;
            }

            if (!IsValidPassword(password))
            {
                lblPasswordError.Text = "⚠ Weak password. Must contain ≥8 chars, upper/lowercase, number, symbol.";
                return;
            }

            if (password != confirm)
            {
                lblConfirmPasswordError.Text = "⚠ Password confirmation does not match.";
                return;
            }

            // --- [2. Send request to TCP server] ---
            try
            {
                // ⏳ Chặn spam & hiển thị trạng thái chờ
                btn_register.Enabled = false;
                Cursor = Cursors.WaitCursor;

                // ✅ Gọi server trong Task riêng để không chặn UI
                var response = await Task.Run(() => tcpClient.Register(
                    username,
                    isEmail ? contact : "",
                    isPhone ? contact : "",
                    password // gửi plain password, server sẽ tự hash nếu cần
                ));

                // ✅ Xử lý kết quả (UI thread)
                if (response != null && response.Success)
                {
                    MessageBox.Show(
                        "🎉 Registration Successful!\n\nWelcome, " + username + "!",
                        "✓ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FormDangNhap loginForm = new FormDangNhap();
                    loginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "❌ " + (response?.Message ?? "Unknown server response."),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "⚠ Network error: " + ex.Message,
                    "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                // 🔄 Khôi phục trạng thái UI
                btn_register.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        // =========================
        // Switch to Login
        // =========================
        private void btn_alreadyHaveAccount_Click(object sender, EventArgs e)
        {
            StopAnimations();
            SwitchToLogin?.Invoke(this, EventArgs.Empty);
        }

        // =========================
        // Animation Control
        // =========================
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopAnimations();
            base.OnFormClosing(e);
        }

        private void StopAnimations()
        {
            if (myTimer != null)
            {
                myTimer.Stop();
                myTimer.Dispose();
                myTimer = null;
            }
        }

        public void StartAnimations()
        {
            if (myTimer == null)
            {
                myTimer = new System.Windows.Forms.Timer();
                myTimer.Interval = 50;
                myTimer.Tick += MyTimer_Tick;
            }
            myTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left -= 2;
            if (pictureBox1.Right < 0)
                pictureBox1.Left = this.Width;
        }

        private void tb_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            StartAnimations();
        }
    }
}
