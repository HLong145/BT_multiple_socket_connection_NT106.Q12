using Socket_LTMCB.Services;
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

        private readonly TcpClientService tcpClient; // ✅ THÊM
        private readonly DatabaseService dbService = new DatabaseService();

        public event EventHandler SwitchToRegister;

        public FormDangNhap()
        {
            InitializeComponent();
            SetupFloatingAnimation();

            // ✅ KHỞI TẠO TCP CLIENT
            tcpClient = new TcpClientService("127.0.0.1", 8080);

            // ✅ TỰ ĐỘNG ĐĂNG NHẬP NẾU CÓ TOKEN
            LoadRememberedLogin();
        }

        // =========================
        // 2️⃣ Remember Login - ✅ SỬA LẠI
        // =========================
        private void LoadRememberedLogin()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                string savedUsername = Properties.Settings.Default.SavedUsername;
                string savedToken = Properties.Settings.Default.SavedToken;

                tb_Username.Text = savedUsername;

                // Nếu có token, verify với server
                if (!string.IsNullOrEmpty(savedToken))
                {
                    var response = tcpClient.VerifyToken(savedToken);

                    if (response.Success)
                    {
                        string username = response.GetDataValue("username");

                        MessageBox.Show($"🎉 Auto login successful!\n\nWelcome back {username}!",
                            "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // ✅ MỞ MAINFORM (nếu có)
                        // MainForm mainForm = new MainForm(username, savedToken);
                        // mainForm.Show();
                        // this.Hide();

                        this.Close();
                    }
                    else
                    {
                        // Token hết hạn hoặc không hợp lệ
                        MessageBox.Show("Your session has expired. Please login again.",
                            "⚠ Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Xóa token cũ
                        Properties.Settings.Default.SavedToken = "";
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        private void SaveRememberedLogin(string username, string token)
        {
            if (chk_Remember.Checked)
            {
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.SavedToken = token; // ✅ LƯU TOKEN
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.SavedToken = "";
            }

            Properties.Settings.Default.Save();
        }

        // =========================
        // 4️⃣ Button events - ✅ SỬA LẠI
        // =========================
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string contact = tb_Username.Text.Trim();
            string password = tb_Password.Text;

            // 1. Captcha check
            if (!chk_Captcha.Checked)
            {
                MessageBox.Show("Please confirm that you are not a robot!",
                    "⚠ Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Empty check
            if (string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all required login information!",
                    "⚠ Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Determine contact type (username / email / phone)
            string username = contact;
            bool isEmail = IsValidEmail(contact);
            bool isPhone = IsValidPhone(contact);

            if (isEmail || isPhone)
            {
                username = dbService.GetUsernameByContact(contact, isEmail);
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("No account found for this information.",
                        "❌ Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // ✅ 4. GỬI REQUEST ĐẾN SERVER
            var response = tcpClient.Login(username, password);

            if (response.Success)
            {
                // ✅ Lấy token từ response
                string token = response.GetDataValue("token");
                string returnedUsername = response.GetDataValue("username");

                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Server did not return authentication token.",
                        "❌ Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lưu token để Remember Me
                SaveRememberedLogin(returnedUsername, token);

                MessageBox.Show($"🎉 Login successful!\n\nWelcome {returnedUsername}!",
                    "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ MỞ MAINFORM (nếu có)
                // MainForm mainForm = new MainForm(returnedUsername, token);
                // mainForm.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show(response.Message,
                    "❌ Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // 3️⃣ Validation helpers (GIỮ NGUYÊN)
        // =========================
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tb_Password.UseSystemPasswordChar = !chk_ShowPassword.Checked;
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            SwitchToRegister?.Invoke(this, EventArgs.Empty);
        }

        private void btn_Forgot_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuenPass formQuenPass = new FormQuenPass();
            formQuenPass.FormClosed += (s, args) => this.Show();
            formQuenPass.Show();
        }

        // =========================
        // 1️⃣ Animation setup (GIỮ NGUYÊN)
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
    }
}