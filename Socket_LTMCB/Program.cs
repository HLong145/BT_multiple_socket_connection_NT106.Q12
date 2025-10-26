using System;
using System.Windows.Forms;
using ServerApp; // Namespace của ServerForm

namespace Socket_LTMCB
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ HIỂN thị Dashboard để chọn Server hoặc Client
            Application.Run(new DashboardForm());
        }
    }

    /// <summary>
    /// Dashboard form để chọn chạy Server hoặc Client
    /// </summary>
    public class DashboardForm : Form
    {
        private Button btnServer;
        private Button btnClient;
        private Label lblTitle;
        private Panel pnlMain;

        public DashboardForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Form settings
            this.Text = "Fighter x Fighter - Launcher";
            this.Size = new System.Drawing.Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(26, 32, 44);

            // Panel chính
            pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(26, 32, 44)
            };
            this.Controls.Add(pnlMain);

            // Title
            lblTitle = new Label
            {
                Text = "⚔️ FIGHTER x FIGHTER ⚔️\n\nSelect Mode",
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(255, 215, 0),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Location = new System.Drawing.Point(50, 30),
                Size = new System.Drawing.Size(400, 100),
                BackColor = System.Drawing.Color.Transparent
            };
            pnlMain.Controls.Add(lblTitle);

            // Button Server
            btnServer = new Button
            {
                Text = "🖥️ START SERVER\n\nRun TCP Server\n(Port 8080)",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(180, 120),
                Location = new System.Drawing.Point(50, 150),
                BackColor = System.Drawing.Color.FromArgb(72, 187, 120),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnServer.FlatAppearance.BorderSize = 0;
            btnServer.Click += BtnServer_Click;
            pnlMain.Controls.Add(btnServer);

            // Button Client
            btnClient = new Button
            {
                Text = "🎮 START CLIENT\n\nPlay Game\n(Login/Register)",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                Size = new System.Drawing.Size(180, 120),
                Location = new System.Drawing.Point(270, 150),
                BackColor = System.Drawing.Color.FromArgb(66, 153, 225),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClient.FlatAppearance.BorderSize = 0;
            btnClient.Click += BtnClient_Click;
            pnlMain.Controls.Add(btnClient);

            // Hover effects
            btnServer.MouseEnter += (s, e) => btnServer.BackColor = System.Drawing.Color.FromArgb(56, 161, 105);
            btnServer.MouseLeave += (s, e) => btnServer.BackColor = System.Drawing.Color.FromArgb(72, 187, 120);

            btnClient.MouseEnter += (s, e) => btnClient.BackColor = System.Drawing.Color.FromArgb(49, 130, 206);
            btnClient.MouseLeave += (s, e) => btnClient.BackColor = System.Drawing.Color.FromArgb(66, 153, 225);
        }

        private void BtnServer_Click(object sender, EventArgs e)
        {
            try
            {
                // MỞ SERVER FORM
                ServerForm serverForm = new ServerForm();
                serverForm.Show();

                MessageBox.Show("Server window opened!\n\nClick 'Start' to begin listening for connections.",
                    "Server Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  ẨN DASHBOARD (không đóng để có thể quay lại)

                // Khi server form đóng, hiện lại dashboard
                serverForm.FormClosed += (s, args) => this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting server: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClient_Click(object sender, EventArgs e)
        {
            try
            {
                //  MỞ CLIENT (Login/Register flow)

                var clientController = new ClientApplicationController(this);
                // ApplicationContext sẽ tự quản lý
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting client: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }
    }

    /// <summary>
    /// ApplicationController cho Client - quản lý Login/Register flow
    /// </summary>
    public class ClientApplicationController : ApplicationContext
    {
        private FormDangNhap loginForm;
        private FormDangKy registerForm;
        private DashboardForm dashboardForm;
        private bool isExiting = false;

        public ClientApplicationController(DashboardForm dashboard)
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

                ExitThread();
            }
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Nếu form đăng ký đóng nhưng không phải từ việc chuyển form
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
            }
            base.OnMainFormClosed(sender, e);
        }
    }
}