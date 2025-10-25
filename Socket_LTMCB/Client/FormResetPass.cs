using System;
using System.Windows.Forms;
using Socket_LTMCB.Services;

namespace Socket_LTMCB
{
    public partial class FormResetPass : Form
    {
        private readonly string _username;
        private readonly DatabaseService _databaseService;
        private readonly ValidationService _validationService;

        public FormResetPass(string username)
        {
            InitializeComponent();
            _username = username;
            _databaseService = new DatabaseService();
            _validationService = new ValidationService();
        }

        public FormResetPass() : this(string.Empty)
        {
        }

        private void btn_complete_Click(object sender, EventArgs e)
        {
            string newPass = tb_newPassword.Text.Trim();
            string confirmPass = tb_confirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Please enter both password fields!",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Password confirmation does not match!",
                    "Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Mật khẩu phải có tối thiểu 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt!",
                            "Mật khẩu không hợp lệ", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning);

            bool updated = _databaseService.ResetPassword(_username, newPass);

            if (updated)
            {
                MessageBox.Show("Password has been reset successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

                FormDangNhap formLogin = new FormDangNhap();
                formLogin.Show();
            }
            else
            {
                MessageBox.Show("Unable to update password. Please try again!",
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_backToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            FormDangNhap formLogin = new FormDangNhap();
            formLogin.Show();
        }
    }
}
