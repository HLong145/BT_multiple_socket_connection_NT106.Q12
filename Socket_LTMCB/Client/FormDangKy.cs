using Socket_LTMCB.Services;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangKy : Form
    {
        private readonly TcpClientService tcpClient; // ✅ TCP CLIENT
        private readonly DatabaseService dbService;  // ✅ DATABASE SERVICE
        private readonly ValidationService validationService = new ValidationService();

        public event EventHandler SwitchToLogin;

        private System.Windows.Forms.Timer myTimer;

        // ✅ CẤU HÌNH: true = dùng Server, false = dùng Database trực tiếp
        private bool useServer = true;

        public FormDangKy()
        {
            InitializeComponent();
            InitializeCustomUI();
            this.AutoScroll = true;
            for (int i = 0; i < 30; i++)
            {
                Button btn = new Button();
                btn.Text = "Button " + i;
                btn.Location = new Point(20, 30 * i);
                this.Controls.Add(btn);
            }

            // ✅ KHỞI TẠO CẢ HAI SERVICE
            tcpClient = new TcpClientService("127.0.0.1", 8080);
            dbService = new DatabaseService();
        }

        // =========================
        // GIAO DIỆN & PLACEHOLDER
        // =========================
        private void InitializeCustomUI()
        {
            SetPlaceholder(tb_username, "ENTER USERNAME");
            SetPlaceholder(tb_contact, "EMAIL OR PHONE");
            SetPasswordPlaceholder(tb_password, "ENTER PASSWORD");
            SetPasswordPlaceholder(tb_confirmPassword, "CONFIRM PASSWORD");

            DrawLockIcon(pictureBoxLock1, "🔒");
            DrawLockIcon(pictureBoxLock2, "🔒");

            chkNotRobot.Text = "  ☐ I'M NOT A ROBOT  🤖";
            chkNotRobot.CheckedChanged += (s, e) =>
            {
                lblRobotError.Text = "";
                chkNotRobot.Text = chkNotRobot.Checked ? "  ☑ I'M NOT A ROBOT  🤖" : "  ☐ I'M NOT A ROBOT  🤖";
            };

            this.AutoScroll = true;
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
        // VALIDATION
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
        // ✅ ĐĂNG KÝ - SERVER / DATABASE
        // =========================
        private async void btn_register_Click(object sender, EventArgs e)
        {
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

            // --- [2. ĐĂNG KÝ XỬ LÝ] ---
            bool success = false;
            string message = "";

            if (useServer)
            {
                var response = await tcpClient.RegisterAsync(
                username,
                isEmail ? contact : null,
                isPhone ? contact : null,
                password
                );
                success = response.Success;
                message = response.Message;
            }
            else
            {
                if (dbService.IsUserExists(username, isEmail ? contact : null, isPhone ? contact : null))
                {
                    message = "Username, email or phone already exists";
                }
                else
                {
                    string salt = dbService.CreateSalt();
                    string hash = dbService.HashPassword_Sha256(password, salt);
                    success = dbService.SaveUserToDatabase(
                        username,
                        isEmail ? contact : null,
                        isPhone ? contact : null,
                        hash,
                        salt
                    );
                    message = success ? "Registration successful" : "Registration failed";
                }
            }

            if (success)
            {
                MessageBox.Show("🎉 Registration Successful!\n\nWelcome, " + username + "!",
                    "✓ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(message,
                    "❌ Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // CHUYỂN VỀ FORM LOGIN
        // =========================
        private void btn_alreadyHaveAccount_Click(object sender, EventArgs e)
        {
            StopAnimations();
            SwitchToLogin?.Invoke(this, EventArgs.Empty);
        }

        // =========================
        // ANIMATION
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

        private void tb_username_TextChanged(object sender, EventArgs e) { }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            StartAnimations();
        }
    }
}
