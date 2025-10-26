using System;
using System.Windows.Forms;
using ServerApp;

namespace Socket_LTMCB.Client
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();

            // ✅ CHỈ ĐĂNG KÝ 1 LẦN
            btn_Client.Click += Btn_Client_Click;
            btn_Server.Click += Btn_Server_Click;
        }

        /// <summary>
        /// Mở Client mode (Login/Register)
        /// </summary>
        private void Btn_Client_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();

                var clientController = new ClientApplicationController(this);

                MessageBox.Show("Client mode started!\n\nPlease login or register to continue.",
                    "🎮 Client Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting client: {ex.Message}",
                    "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }

        /// <summary>
        /// Mở Server mode
        /// </summary>
        private void Btn_Server_Click(object sender, EventArgs e)
        {
            try
            {
                ServerForm serverForm = new ServerForm();
                serverForm.Show();

                MessageBox.Show("Server window opened!\n\nClick 'Start' to begin listening for connections on port 8080.",
                    "🖥️ Server Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);

                serverForm.FormClosed += (s, args) =>
                {
                    this.Show();
                    this.BringToFront();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server: {ex.Message}",
                    "❌ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "⚠️ Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Exit();
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Controller quản lý Client flow (Login/Register)
        /// </summary>
        public class ClientApplicationController : ApplicationContext
        {
            private FormDangNhap loginForm;
            private FormDangKy registerForm;
            private Dashboard dashboardForm;
            private bool isExiting = false;

            public ClientApplicationController(Dashboard dashboard)
            {
                dashboardForm = dashboard;
                ShowLoginForm();
            }

            private void ShowLoginForm()
            {
                if (loginForm == null || loginForm.IsDisposed)
                {
                    loginForm = new FormDangNhap();
                    loginForm.SwitchToRegister += OnSwitchToRegister;
                    loginForm.FormClosed += LoginForm_FormClosed;
                }

                this.MainForm = loginForm;
                loginForm.Show();
                loginForm.BringToFront();
            }

            private void ShowRegisterForm()
            {
                if (registerForm == null || registerForm.IsDisposed)
                {
                    registerForm = new FormDangKy();
                    registerForm.SwitchToLogin += OnSwitchToLogin;
                    registerForm.FormClosed += RegisterForm_FormClosed;
                }

                registerForm.Show();
                registerForm.BringToFront();
            }

            private void OnSwitchToRegister(object sender, EventArgs e)
            {
                loginForm?.Hide();
                ShowRegisterForm();
            }

            private void OnSwitchToLogin(object sender, EventArgs e)
            {
                registerForm?.Hide();
                ShowLoginForm();
            }

            private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                if (!isExiting)
                {
                    isExiting = true;
                    registerForm?.Close();

                    // ✅ QUAY LẠI DASHBOARD
                    dashboardForm?.Show();
                    dashboardForm?.BringToFront();

                    ExitThread();
                }
            }

            private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                // Nếu form đăng ký đóng mà không chuyển form
                if (registerForm != null && (loginForm == null || !loginForm.Visible))
                {
                    ShowLoginForm();
                }
            }

            protected override void OnMainFormClosed(object sender, EventArgs e)
            {
                if (!isExiting)
                {
                    isExiting = true;
                    registerForm?.Close();
                    loginForm?.Close();

                    // ✅ QUAY LẠI DASHBOARD
                    dashboardForm?.Show();
                    dashboardForm?.BringToFront();
                }
                base.OnMainFormClosed(sender, e);
            }
        }
    }
}