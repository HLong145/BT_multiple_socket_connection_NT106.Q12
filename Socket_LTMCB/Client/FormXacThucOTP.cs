using System;
using System.Linq;
using System.Windows.Forms;
using ServerApp.Services; // ✅ để dùng DatabaseService

namespace Socket_LTMCB
{
    public partial class FormXacThucOTP : Form
    {
        private readonly string _username;
        private readonly DatabaseService _databaseService;

        // ✅ Constructor nhận username từ form quên mật khẩu
        public FormXacThucOTP(string username)
        {
            InitializeComponent();
            _username = username;
            _databaseService = new DatabaseService();
        }

        // 🔹 Khi nhấn nút "XÁC THỰC"
        private void btn_verify_Click(object sender, EventArgs e)
        {
            // Ghép 6 ô nhập thành một chuỗi OTP
            string otp = string.Concat(
                tb_otp1.Text.Trim(),
                tb_otp2.Text.Trim(),
                tb_otp3.Text.Trim(),
                tb_otp4.Text.Trim(),
                tb_otp5.Text.Trim(),
                tb_otp6.Text.Trim()
            );

            if (otp.Length != 6 || !otp.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập đủ 6 chữ số OTP!",
                    "Thiếu OTP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Kiểm tra OTP trong DatabaseService
            var result = _databaseService.VerifyOtp(_username, otp);

            if (result.IsValid)
            {
                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormResetPass formReset = new FormResetPass(_username);
                formReset.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Khi nhấn nút "GỬI LẠI MÃ"
        private void btn_resend_Click(object sender, EventArgs e)
        {
            string newOtp = _databaseService.GenerateOtp(_username);
            MessageBox.Show($"Mã OTP mới của bạn là: {newOtp}\n(Chỉ hiển thị để test, sau này có thể gửi qua Email/SMS)",
                "OTP mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 🔹 Khi nhấn nút "QUAY LẠI ĐĂNG NHẬP"
        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
