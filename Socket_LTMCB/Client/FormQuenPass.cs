using System;
using System.Windows.Forms;
using ServerApp.Services; // ✅ để dùng DatabaseService

namespace Socket_LTMCB
{
    public partial class FormQuenPass : Form
    {
        private readonly DatabaseService _databaseService;

        public FormQuenPass()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        // 🔙 Nút quay lại đăng nhập
        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close(); // chỉ cần đóng form, trở về màn hình login
        }

        // ▶ Nút TIẾP TỤC
        private void btn_continue_Click(object sender, EventArgs e)
        {
            string input = tb_contact.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập email hoặc số điện thoại!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác định là email hay sđt
            bool isEmail = IsValidEmail(input);
            bool isPhone = IsValidPhone(input);

            if (!isEmail && !isPhone)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng email hoặc số điện thoại!",
                    "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string username = _databaseService.GetUsernameByContact(input, isEmail);

                if (username == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản nào khớp với thông tin này!",
                        "Không tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Sinh OTP
                string otp = _databaseService.GenerateOtp(username);

                MessageBox.Show($"Mã OTP của bạn là: {otp}\n(Chỉ hiển thị để test, sau này sẽ gửi qua email/SMS.)",
                    "OTP được tạo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở form xác thực OTP (nếu bạn có form này)
                FormXacThucOTP formOtp = new FormXacThucOTP(username);
                formOtp.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== Các hàm kiểm tra định dạng ==================

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            // ✅ SĐT phải bắt đầu bằng 0 và có đúng 10 chữ số
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0\d{9}$");
        }
    }
}
