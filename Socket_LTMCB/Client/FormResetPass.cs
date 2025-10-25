using System;
using System.Windows.Forms;
using ServerApp.Services; // ✅ để dùng DatabaseService

namespace Socket_LTMCB
{
    public partial class FormResetPass : Form
    {
        private readonly string _username;
        private readonly DatabaseService _databaseService;
        private readonly ValidationService _validationService;
        // ✅ Constructor nhận username từ form xác thực OTP
        public FormResetPass(string username)
        {
            InitializeComponent();
            _username = username;
            _databaseService = new DatabaseService();
            _validationService = new ValidationService();
        }

        // ✅ Constructor mặc định (để Visual Studio Designer hoạt động)
        public FormResetPass() : this(string.Empty)
        {
        }

        // 🔹 Nút "HOÀN TẤT"
        private void btn_complete_Click(object sender, EventArgs e)
        {
            string newPass = tb_newPassword.Text.Trim();
            string confirmPass = tb_confirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!",
                    "Sai xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_validationService.IsValidPassword(newPass))
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và ký tự đặc biệt!",
                    "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Cập nhật mật khẩu
            bool updated = _databaseService.ResetPassword(_username, newPass);

            if (updated)
            {
                MessageBox.Show("Đặt lại mật khẩu thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

                // Quay lại form đăng nhập
                FormDangNhap formLogin = new FormDangNhap();
                formLogin.Show();
            }
            else
            {
                MessageBox.Show("Không thể cập nhật mật khẩu. Vui lòng thử lại!",
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Nút "QUAY LẠI ĐĂNG NHẬP"
        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            FormDangNhap formLogin = new FormDangNhap();
            formLogin.Show();
        }
    }
}
