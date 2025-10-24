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

        public ApplicationController()
        {
            loginForm = new FormDangNhap();

            // Đăng ký sự kiện chuyển sang Form Register
            loginForm.SwitchToRegister += OnSwitchToRegister;

            this.MainForm = loginForm;
            loginForm.Show();
        }

        private void OnSwitchToRegister(object sender, EventArgs e)
        {
            loginForm.Hide();

            if (registerForm == null)
            {
                registerForm = new FormDangKy();
                registerForm.SwitchToLogin += OnSwitchToLogin;

                // Xử lý khi người dùng đóng Form đăng ký bằng nút X (thoát)
                registerForm.FormClosed += (s, args) => {
                    // Nếu Form đăng ký đóng và không còn hiển thị, quay lại Form đăng nhập
                    if (registerForm.Visible == false)
                    {
                        this.MainForm = loginForm;
                        loginForm.Show();
                    }
                };
            }

            registerForm.Show();
        }

        private void OnSwitchToLogin(object sender, EventArgs e)
        {
            registerForm.Hide();
            // Đảm bảo Form Dang Nhap được đặt làm MainForm (để khi đóng nó thì ứng dụng thoát)
            this.MainForm = loginForm;
            loginForm.Show();
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            // Chỉ đóng ứng dụng khi Form chính (FormDangNhap) đóng
            if (sender == loginForm)
            {
                base.OnMainFormClosed(sender, e);
            }
        }
    }
}
