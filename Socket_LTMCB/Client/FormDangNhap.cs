using Socket_LTMCB.Services;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangNhap : Form
    {
        private int floatingOffset = 0;
        private Random random = new Random();
        private System.Windows.Forms.Timer floatingItemsTimer;

        private readonly TcpClientService tcpClient;
        private readonly DatabaseService dbService = new DatabaseService();

        public event EventHandler SwitchToRegister;

        public FormDangNhap()
        {
            InitializeComponent();
            SetupFloatingAnimation();

            tcpClient = new TcpClientService("127.0.0.1", 8080);
            _ = LoadRememberedLoginAsync(); // ✅ auto login nếu có token
        }

        // =========================
        // ✅ Remember Login (ASYNC)
        // =========================
        private async Task LoadRememberedLoginAsync()
        {
            if (Properties.Settings.Default.RememberMe)
            {
                string savedUsername = Properties.Settings.Default.SavedUsername;
                string savedToken = Properties.Settings.Default.SavedToken;

                tb_Username.Text = savedUsername;

                if (!string.IsNullOrEmpty(savedToken))
                {
                    var response = await tcpClient.VerifyTokenAsync(savedToken);

                    if (response.Success)
                    {
                        string username = response.GetDataValue("username");

                        MessageBox.Show($"🎉 Auto login successful!\n\nWelcome back {username}!",
                            "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // ✅ MỞ MAINFORM (nếu có)
                        // MainForm mainForm = new MainForm(username, savedToken);
                        // mainForm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Your session has expired. Please login again.",
                            "⚠ Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                Properties.Settings.Default.SavedToken = token;
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
        // ✅ Button Login (ASYNC)
        // =========================
        private async void btn_Login_Click(object sender, EventArgs e)
        {
            string contact = tb_Username.Text.Trim();
            string password = tb_Password.Text;

            if (!chk_Captcha.Checked)
            {
                MessageBox.Show("Please confirm that you are not a robot!",
                    "⚠ Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all required login information!",
                    "⚠ Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            try
            {
                btn_Login.Enabled = false;

                // ✅ GỬI REQUEST ĐẾN SERVER (ASYNC)
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

                    SaveRememberedLogin(returnedUsername, token);

                    MessageBox.Show($"🎉 Login successful!\n\nWelcome {returnedUsername}!",
                        "✅ Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ MỞ MAINFORM 
                    this.Hide();
                    MainForm mainForm = new MainForm(returnedUsername, token);
                    mainForm.FormClosed += (s, args) => this.Close(); // khi MainForm đóng, thì login form cũng đóng
                    mainForm.Show();

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
    }
}
