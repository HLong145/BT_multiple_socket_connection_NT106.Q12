using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangKy : Form
    {
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
            // --- Cài đặt Placeholder và Mật khẩu ---
            SetPlaceholder(tb_username, "ENTER NAME");
            SetPlaceholder(tb_email, "EMAIL OR PHONE");

            SetPasswordPlaceholder(tb_password, "ENTER PASS");
            SetPasswordPlaceholder(tb_confirmPassword, "CONFIRM PASS");

            // --- Icon ---
            DrawLockIcon(pictureBoxLock1, "🔒");
            DrawLockIcon(pictureBoxLock2, "🔒");
            DrawSwordsIcon(pictureBoxSwords, "⚔");

            // Căn giữa icon thanh kiếm
            this.pictureBoxSwords.Location = new Point(
                (this.panelHeader.Width - this.pictureBoxSwords.Width) / 2,
                this.lblTitle.Location.Y + this.lblTitle.Height - 10
            );

            // Cập nhật text ban đầu của checkbox
            chkNotRobot.Text = "  ☐ I'M NOT A ROBOT  🤖";

            // --- Events ---
            btn_alreadyHaveAccount.Click += btn_alreadyHaveAccount_Click;
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
            tb.Enter += (s, e) => { if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.FromArgb(214, 211, 209); } };
            tb.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.FromArgb(87, 83, 78); } };
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

        private void DrawSwordsIcon(PictureBox pb, string icon)
        {
            pb.Paint += (s, e) =>
            {
                e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 14), new SolidBrush(Color.FromArgb(168, 162, 158)), 10, 2);
            };
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            lblUsernameError.Text = "";
            lblContactError.Text = "";
            lblPasswordError.Text = "";
            lblConfirmPasswordError.Text = "";
            lblRobotError.Text = "";

            if (!ValidateForm()) return;

            MessageBox.Show(
                $"🎮 ACCOUNT CREATED!\n\nHERO NAME: {tb_username.Text.ToUpper()}\nCONTACT: {tb_email.Text}\n\nREADY PLAYER ONE!",
                "✓ QUEST COMPLETE",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ResetForm();
        }

        private void btn_alreadyHaveAccount_Click(object sender, EventArgs e)
        {
            StopAnimations();
            SwitchToLogin?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (tb_username.Text.Trim() == "ENTER NAME" || tb_username.Text.Length < 3)
            {
                lblUsernameError.Text = "⚠ USERNAME TOO SHORT (MIN 3 CHARACTERS)!";
                isValid = false;
            }

            string contact = tb_email.Text.Trim();
            if (contact == "EMAIL OR PHONE" || (!contact.Contains("@") && !Regex.IsMatch(contact, @"^\d{10,}$")))
            {
                lblContactError.Text = "⚠ INVALID EMAIL OR PHONE!";
                isValid = false;
            }

            if (tb_password.Text == "ENTER PASS" || tb_password.Text.Length < 6)
            {
                lblPasswordError.Text = "⚠ PASSWORD TOO WEAK (MIN 6 CHARACTERS)!";
                isValid = false;
            }

            if (tb_confirmPassword.Text == "CONFIRM PASS" || tb_password.Text != tb_confirmPassword.Text)
            {
                lblConfirmPasswordError.Text = "⚠ PASSWORDS DON'T MATCH!";
                isValid = false;
            }

            if (!chkNotRobot.Checked)
            {
                lblRobotError.Text = "⚠ VERIFY YOU'RE NOT A ROBOT!";
                isValid = false;
            }

            return isValid;
        }

        private void ResetForm()
        {
            tb_username.Text = "ENTER NAME";
            tb_username.ForeColor = Color.FromArgb(87, 83, 78);

            tb_email.Text = "EMAIL OR PHONE";
            tb_email.ForeColor = Color.FromArgb(87, 83, 78);

            tb_password.Text = "ENTER PASS";
            tb_password.ForeColor = Color.FromArgb(87, 83, 78);
            tb_password.PasswordChar = '\0';

            tb_confirmPassword.Text = "CONFIRM PASS";
            tb_confirmPassword.ForeColor = Color.FromArgb(87, 83, 78);
            tb_confirmPassword.PasswordChar = '\0';

            chkNotRobot.Checked = false;
            chkNotRobot.Text = "  ☐ I'M NOT A ROBOT  🤖";

            lblUsernameError.Text = "";
            lblContactError.Text = "";
            lblPasswordError.Text = "";
            lblConfirmPasswordError.Text = "";
            lblRobotError.Text = "";
        }

        // ✅ CHO PHÉP ĐÓNG FORM BÌNH THƯỜNG
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