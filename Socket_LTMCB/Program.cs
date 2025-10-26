using System;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy bộ điều khiển ứng dụng để quản lý các Form
            Application.Run(new ApplicationController());
        }
    }

    /// <summary>
    /// ApplicationController sử dụng ApplicationContext để quản lý vòng đời và chuyển đổi giữa các Form.
    /// </summary>
    public class ApplicationController : ApplicationContext
    {
        private FormDangNhap loginForm;
        private FormDangKy registerForm;
        private bool isExiting = false;

        public ApplicationController()
        {
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
                ExitThread();
            }
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Nếu form đăng ký đóng nhưng không phải từ việc chuyển form
            if (registerForm != null && !loginForm.Visible)
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
            }
            base.OnMainFormClosed(sender, e);
        }

    }
}