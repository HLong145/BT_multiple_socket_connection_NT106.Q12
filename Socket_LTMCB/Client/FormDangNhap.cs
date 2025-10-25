using ServerApp.Services;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangNhap : Form
    {
        private int floatingOffset = 0;
        private Random random = new Random();
        private System.Windows.Forms.Timer floatingItemsTimer;

        private readonly DatabaseService dbService = new DatabaseService();
        public event EventHandler SwitchToRegister;

        public FormDangNhap()
        {
            InitializeComponent();
            SetupFloatingAnimation();
            LoadRememberedLogin();
        }

        // =========================
        // 1️⃣ Animation setup
        // =========================
        private void SetupFloatingAnimation()
        {
            floatingItemsTimer = new System.Windows.Forms.Timer();
            floatingItemsTimer.Interval = 50;
            floatingItemsTimer.Tick += FloatingItemsTimer_Tick;
            floatingItemsTimer.Start();
        }

        private void FloatingItemsTimer_Tick(object sender, EventArgs e)
        {
            floatingOffset += 2;
            if (floatingOffset > this.Height + 50)
                floatingOffset = -50;
            this.Invalidate();
        }

        private void FormDangNhap_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            for (int i = 0; i < 8; i++)
            {
                int x = (i * 15 + 10) * this.Width / 100;
                int y = (floatingOffset + (i * 80)) % (this.Height + 100) - 50;

                using (SolidBrush brush = new SolidBrush(Color.Gold))
                    g.FillRectangle(brush, x, y, 12, 12);

                using (Pen pen = new Pen(Color.Black, 2))
                    g.DrawRectangle(pen, x, y, 12, 12);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            floatingItemsTimer?.Stop();
            floatingItemsTimer?.Dispose();
            base.OnFormClosing(e);
        }

        // =========================
        // 2️⃣ Remember Login
        // =========================
        private void LoadRememberedLogin()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                tb_Username.Text = Properties.Settings.Default.SavedUsername;
                tb_Password.Text = Properties.Settings.Default.SavedPassword;
                chk_Remember.Checked = true;
            }
        }

        private void SaveRememberedLogin(string username, string password)
        {
            if (chk_Remember.Checked)
            {
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.SavedPassword = password;
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.SavedPassword = "";
            }

            Properties.Settings.Default.Save();
        }

        // =========================
        // 3️⃣ Validation helpers
        // =========================
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidPhone(string phone)
        {
            // ✅ Bắt đầu bằng 0 và có đúng 10 chữ số
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }

        // =========================
        // 4️⃣ Button events
        // =========================
        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tb_Password.UseSystemPasswordChar = !chk_ShowPassword.Checked;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string contact = tb_Username.Text.Trim();
            string password = tb_Password.Text;

            // 1. Kiểm tra captcha
            if (!chk_Captcha.Checked)
            {
                MessageBox.Show("Vui lòng xác nhận bạn không phải robot!",
                    "⚠ Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra trống
            if (string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập!",
                    "⚠ Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Xác định loại liên hệ (username / email / phone)
            string username = contact;
            bool isEmail = IsValidEmail(contact);
            bool isPhone = IsValidPhone(contact);

            if (isEmail || isPhone)
            {
                username = dbService.GetUsernameByContact(contact, isEmail);
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Không tìm thấy tài khoản phù hợp với thông tin này.",
                        "❌ Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 4. Xác thực đăng nhập
            bool loginSuccess = dbService.VerifyUserLogin(username, password);

            if (loginSuccess)
            {
                SaveRememberedLogin(username, password);
                MessageBox.Show($"🎉 Đăng nhập thành công!\n\nChào mừng {username}!",
                    "✅ Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // TODO: mở form chính ở đây (nếu có)
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!",
                    "❌ Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            SwitchToRegister?.Invoke(this, EventArgs.Empty);
        }

        private void btn_Forgot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng quên mật khẩu đang được phát triển.",
                "🔧 Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
