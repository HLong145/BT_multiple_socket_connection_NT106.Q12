using Socket_LTMCB.Services;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace Socket_LTMCB
{
    public partial class FormDangNhap : Form
    {
        private int floatingOffset = 0;
        private Random random = new Random();
        private System.Windows.Forms.Timer floatingItemsTimer;
        public string ReturnedUsername { get; private set; }
        public string Token { get; private set; }
        private void ClearSavedCredentials()
        {
            Properties.Settings.Default.RememberMe = false;
            Properties.Settings.Default.SavedUsername = "";
            Properties.Settings.Default.SavedPassword = "";
            Properties.Settings.Default.SavedToken = "";
            Properties.Settings.Default.Save();
        }

        private readonly TcpClientService tcpClient;
        private readonly DatabaseService dbService = new DatabaseService();
        private static bool isAutoLoginPerformed = false;
        public event EventHandler SwitchToRegister;

        public FormDangNhap()
        {
            InitializeComponent();
            SetupFloatingAnimation();

            tcpClient = new TcpClientService("127.0.0.1", 8080);
            if (!isAutoLoginPerformed)
            {
                this.Shown += async (sender, e) =>
                {
                    await LoadRememberedLoginAsync();
                };
            }
        }

        // =========================
        // ✅ Remember Login (ASYNC)
        // =========================
        private string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            var bytes = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plainText),
                null,
                DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(bytes);
        }

        private string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            try
            {
                var bytes = ProtectedData.Unprotect(
                    Convert.FromBase64String(cipherText),
                    null,
                    DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return ""; // token bị lỗi hoặc user khác
            }
        }
        private async Task LoadRememberedLoginAsync()
        {
            if (isAutoLoginPerformed) return;

            if (Properties.Settings.Default.RememberMe)
            {
                string savedUsername = Properties.Settings.Default.SavedUsername;
                string savedPassword = Decrypt(Properties.Settings.Default.SavedPassword);
                string savedToken = Decrypt(Properties.Settings.Default.SavedToken);

                if (string.IsNullOrEmpty(savedUsername))
                {
                    return;
                }

                tb_Username.Text = savedUsername;
                tb_Password.Text = savedPassword;

                // ✅ QUAN TRỌNG: Nếu không có token, KHÔNG thử auto login
                // Chỉ điền sẵn username/password để người dùng click Login
                if (string.IsNullOrEmpty(savedToken))
                {
                    return; // Chỉ điền form, không auto login
                }

                // ✅ Nếu có token, thử verify
                try
                {
                    var verifyResponse = await tcpClient.VerifyTokenAsync(savedToken);

                    if (verifyResponse.Success)
                    {
                        string usernameFromToken = verifyResponse.GetDataValue("username");

                        MessageBox.Show($"🎉 Auto login successful!\n\nWelcome back {usernameFromToken}!",
                            "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        isAutoLoginPerformed = true;

                        this.Hide();
                        MainForm mainForm = new MainForm(usernameFromToken, savedToken);
                        mainForm.FormClosed += (s, args) =>
                        {
                            isAutoLoginPerformed = false;
                            this.Close();
                        };
                        mainForm.Show();
                        return;
                    }
                    else
                    {
                        // ❌ Token không hợp lệ, nhưng KHÔNG xóa thông tin remember me
                        // Chỉ xóa token, giữ username/password
                        Properties.Settings.Default.SavedToken = "";
                        Properties.Settings.Default.Save();

                        // Thông báo và để người dùng đăng nhập thủ công
                        MessageBox.Show("Your session has expired. Please click Login button.",
                            "⚠ Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Token verification failed: {ex.Message}");
                    // ❌ Lỗi kết nối, không làm gì cả - để người dùng đăng nhập thủ công
                }
            }
        }
        private void SaveRememberedLogin(string username, string password, string token)
        {
            if (chk_Remember.Checked)
            {
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.SavedPassword = Encrypt(password); // 🔒 mã hóa password
                Properties.Settings.Default.SavedToken = Encrypt(token);
            }
            else
            {
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.SavedPassword = "";
                Properties.Settings.Default.SavedToken = "";
            }

            Properties.Settings.Default.Save();
        }
        // =========================
        // ✅ Button Login (ASYNC)
        // =========================
        private async void btn_Login_Click(object sender, EventArgs e)
        {
            string contact = tb_Username.Text.Trim();
            string password = tb_Password.Text;

            // Kiểm tra captcha
            if (!chk_Captcha.Checked)
            {
                MessageBox.Show("Please confirm that you are not a robot!",
                    "⚠ Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra thông tin login
            if (string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all required login information!",
                    "⚠ Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = contact;
            bool isEmail = IsValidEmail(contact);
            bool isPhone = IsValidPhone(contact);

            // Nếu nhập email hoặc phone, tìm username
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

            try
            {
                btn_Login.Enabled = false;

                // Gọi server login
                var response = await tcpClient.LoginAsync(username, password);

                if (response.Success)
                {
                    string token = response.GetDataValue("token");
                    string returnedUsername = response.GetDataValue("username");

                    if (string.IsNullOrEmpty(token))
                    {
                        MessageBox.Show("Server did not return authentication token.",
                            "❌ Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ✅ THAY ĐỔI: Không cần gọi tokenManager.GenerateToken ở client
                    // Token đã được server tạo và trả về trong response

                    // Lưu RememberMe
                    if (chk_Remember.Checked)
                    {
                        Properties.Settings.Default.RememberMe = true;
                        Properties.Settings.Default.SavedUsername = returnedUsername;
                        Properties.Settings.Default.SavedPassword = Encrypt(password);
                        Properties.Settings.Default.SavedToken = Encrypt(token); // Lưu token từ server
                    }
                    else
                    {
                        Properties.Settings.Default.RememberMe = false;
                        Properties.Settings.Default.SavedUsername = "";
                        Properties.Settings.Default.SavedPassword = "";
                        Properties.Settings.Default.SavedToken = "";
                    }
                    Properties.Settings.Default.Save();

                    MessageBox.Show($"🎉 Login successful!\n\nWelcome {returnedUsername}!",
                        "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở MainForm và đóng FormDangNhap
                    MainForm mainForm = new MainForm(returnedUsername, token);
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(response.Message,
                        "❌ Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting to server:\n" + ex.Message,
                    "⚠ Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_Login.Enabled = true;
            }
        }

        // =========================
        // Helpers
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
        // Floating Animation
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

        private void chk_Remember_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
