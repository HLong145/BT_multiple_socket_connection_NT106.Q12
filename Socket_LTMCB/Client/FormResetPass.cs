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

            if (!_validationService.IsValidPassword(newPass))
            {
                MessageBox.Show("Password must be at least 8 characters long, including uppercase, lowercase, and a special character!",
                    "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
