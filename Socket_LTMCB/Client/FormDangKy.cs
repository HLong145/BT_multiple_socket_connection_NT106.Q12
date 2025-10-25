using ServerApp.Services;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangKy : Form
    {
        private readonly DatabaseService dbService = new DatabaseService();
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
                e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 16), new SolidBrush(Color.FromArgb(217, 119, 6)), 5, 5);
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
            // At least 8 characters, with upper/lowercase, number, and special character
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
        }

        // =========================
        // Register Button Click
        // =========================
        private void btn_register_Click(object sender, EventArgs e)
        {
            string username = tb_username.Text.Trim();
            string contact = tb_contact.Text.Trim();
            string password = tb_password.Text;
            string confirm = tb_confirmPassword.Text;

            // --- [1. Input Validation] ---
            if (string.IsNullOrEmpty(username) || username == "ENTER USERNAME")
            {
                MessageBox.Show("Please enter your username.", "⚠ Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(contact) || contact == "EMAIL OR PHONE")
            {
                MessageBox.Show("Please enter your Email or Phone number.", "⚠ Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!chkNotRobot.Checked)
            {
                MessageBox.Show("Please verify the captcha.", "⚠ Verification Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isEmail = IsValidEmail(contact);
            bool isPhone = IsValidPhone(contact);

            if (!isEmail && !isPhone)
            {
                MessageBox.Show("Please enter a valid Email or Phone number.", "⚠ Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Weak password. Must contain at least 8 characters, including upper/lowercase letters, numbers, and special characters.",
                    "⚠ Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Password confirmation does not match.", "⚠ Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dbService.IsUserExists(username, isEmail ? contact : null, isPhone ? contact : null))
            {
                MessageBox.Show("Username or Email/Phone already exists.", "⚠ Duplicate Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- [2. Create Salt & Hash Password] ---
            string salt = dbService.CreateSalt();
            string hashedPassword = dbService.HashPassword_Sha256(password, salt);

            // --- [3. Save to Database] ---
            bool success = dbService.SaveUserToDatabase(
                username,
                isEmail ? contact : null,
                isPhone ? contact : null,
                hashedPassword,
                salt
            );

            if (success)
            {
                MessageBox.Show("🎉 Registration Successful!\n\nWelcome, " + username + "!",
                    "✓ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormDangNhap loginForm = new FormDangNhap();
                loginForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while saving your information.\nPlease try again later.",
                    "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Animation logic...
        }
    }
}
