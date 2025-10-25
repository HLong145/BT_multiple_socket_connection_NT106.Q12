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
        private readonly SecurityService securityService = new SecurityService();

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
                string encrypted = Properties.Settings.Default.SavedPassword;
                tb_Password.Text = securityService.DecryptPassword(encrypted);
                chk_Remember.Checked = true;
            }
        }
        private void SaveRememberedLogin(string username, string password)
        {
            if (chk_Remember.Checked)
            {
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.SavedPassword = securityService.EncryptPassword(password);
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
            // ✅ Must start with 0 and have exactly 10 digits
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

            // 4. Login verification
            bool loginSuccess = dbService.VerifyUserLogin(username, password);

            if (loginSuccess)
            {
                SaveRememberedLogin(username, password);
                MessageBox.Show($"🎉 Login successful!\n\nWelcome {username}!",
                    "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // TODO: open main form here (if any)
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password!",
                    "❌ Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            SwitchToRegister?.Invoke(this, EventArgs.Empty);
        }

        private void btn_Forgot_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormQuenPass formQuenPass = new FormQuenPass();
            formQuenPass.FormClosed += (s, args) => this.Show(); // Return to login when the other form is closed
            formQuenPass.Show();
        }
    }
}
