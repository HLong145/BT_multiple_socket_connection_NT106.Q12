using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangKy : Form
    {
        public event EventHandler SwitchToLogin;

        public FormDangKy()
        {
            InitializeComponent();
            InitializeCustomUI();
            this.AutoScroll = true; // Cho phép cuộn nếu form quá nhỏ (tùy chọn)
        }

        private void InitializeCustomUI()
        {
            // --- Cài đặt Placeholder và Mật khẩu ---
            SetPlaceholder(tb_username, "ENTER NAME");
            SetPlaceholder(tb_email, "EMAIL OR PHONE");

            // Đặt Placeholder và bật/tắt chế độ ẩn ký tự
            SetPasswordPlaceholder(tb_password, "ENTER PASS");
            SetPasswordPlaceholder(tb_confirmPassword, "CONFIRM PASS");

            // --- Icon ---
            DrawLockIcon(pictureBoxLock1, "🔒");
            DrawLockIcon(pictureBoxLock2, "🔒");
            DrawSwordsIcon(pictureBoxSwords, "⚔");

            // Căn giữa icon thanh kiếm sau khi InitializeComponent hoàn tất
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
                // Cập nhật text của checkbox khi được chọn
                chkNotRobot.Text = chkNotRobot.Checked ? "  ☑ I'M NOT A ROBOT  🤖" : "  ☐ I'M NOT A ROBOT  🤖";
            };
        }

        // Hàm đặt Placeholder thông thường
        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.FromArgb(87, 83, 78); // Màu xám nhạt (Placeholder Color)
            tb.Enter += (s, e) => { if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.FromArgb(214, 211, 209); } }; // Màu trắng khi focus
            tb.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.FromArgb(87, 83, 78); } };
        }

        // Hàm đặt Placeholder cho Mật khẩu (có thêm logic ẩn/hiện ký tự)
        private void SetPasswordPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.FromArgb(87, 83, 78);
            tb.PasswordChar = '\0'; // Hiển thị placeholder
            tb.Enter += (s, e) =>
            {
                if (tb.Text == placeholder)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.FromArgb(214, 211, 209);
                    tb.PasswordChar = '●'; // Ẩn ký tự khi nhập
                }
            };
            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = Color.FromArgb(87, 83, 78);
                    tb.PasswordChar = '\0'; // Hiển thị placeholder trở lại
                }
            };
        }

        // Vẽ icon khóa trong PictureBox
        private void DrawLockIcon(PictureBox pb, string icon)
        {
            pb.Paint += (s, e) =>
            {
                // Căn giữa icon trong PictureBox (Font size 16)
                e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 16), new SolidBrush(Color.FromArgb(217, 119, 6)), 5, 5);
            };
        }

        // Vẽ icon kiếm trong PictureBox
        private void DrawSwordsIcon(PictureBox pb, string icon)
        {
            pb.Paint += (s, e) =>
            {
                // Căn giữa icon trong PictureBox (Font size 14)
                e.Graphics.DrawString(icon, new Font("Segoe UI Emoji", 14), new SolidBrush(Color.FromArgb(168, 162, 158)), 10, 2);
            };
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            // Đặt lại các thông báo lỗi trước khi kiểm tra
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
            this.Hide();
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            // Kiểm tra Username
            if (tb_username.Text.Trim() == "ENTER NAME" || tb_username.Text.Length < 3)
            {
                lblUsernameError.Text = "⚠ USERNAME TOO SHORT (MIN 3 CHARACTERS)!";
                isValid = false;
            }

            // Kiểm tra Contact (Email/Phone)
            string contact = tb_email.Text.Trim();
            if (contact == "EMAIL OR PHONE" || (!contact.Contains("@") && !Regex.IsMatch(contact, @"^\d{10,}$")))
            {
                lblContactError.Text = "⚠ INVALID EMAIL OR PHONE!";
                isValid = false;
            }

            // Kiểm tra Mật khẩu
            if (tb_password.Text == "ENTER PASS" || tb_password.Text.Length < 6)
            {
                lblPasswordError.Text = "⚠ PASSWORD TOO WEAK (MIN 6 CHARACTERS)!";
                isValid = false;
            }

            // Kiểm tra Xác nhận Mật khẩu
            if (tb_confirmPassword.Text == "CONFIRM PASS" || tb_password.Text != tb_confirmPassword.Text)
            {
                lblConfirmPasswordError.Text = "⚠ PASSWORDS DON'T MATCH!";
                isValid = false;
            }

            // Kiểm tra Robot
            if (!chkNotRobot.Checked)
            {
                lblRobotError.Text = "⚠ VERIFY YOU'RE NOT A ROBOT!";
                isValid = false;
            }

            return isValid;
        }

        private void ResetForm()
        {
            // Reset các trường nhập liệu về trạng thái Placeholder
            tb_username.Text = "ENTER NAME"; tb_username.ForeColor = Color.FromArgb(87, 83, 78);
            tb_email.Text = "EMAIL OR PHONE"; tb_email.ForeColor = Color.FromArgb(87, 83, 78);

            tb_password.Text = "ENTER PASS"; tb_password.ForeColor = Color.FromArgb(87, 83, 78); tb_password.PasswordChar = '\0';
            tb_confirmPassword.Text = "CONFIRM PASS"; tb_confirmPassword.ForeColor = Color.FromArgb(87, 83, 78); tb_confirmPassword.PasswordChar = '\0';

            // Reset Checkbox và Text
            chkNotRobot.Checked = false;
            chkNotRobot.Text = "  ☐ I'M NOT A ROBOT  🤖";

            // Xóa thông báo lỗi
            lblUsernameError.Text = ""; lblContactError.Text = ""; lblPasswordError.Text = ""; lblConfirmPasswordError.Text = ""; lblRobotError.Text = "";
        }
        private void FormDangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // Người dùng bấm X
            {
                e.Cancel = true;          // Hủy hành vi đóng mặc định
                StopAnimations();         // Dừng timer / animation
                this.Hide();              // Ẩn form đăng ký
                SwitchToLogin?.Invoke(this, EventArgs.Empty); // Hiển thị form đăng nhập
            }
        }

        private System.Windows.Forms.Timer myTimer;

        private void StopAnimations()
        {
            if (myTimer != null)
            {
                myTimer.Stop();
                myTimer.Dispose();
                myTimer = null;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopAnimations(); // Đảm bảo Timer bị hủy
            base.OnFormClosing(e);
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