using Socket_LTMCB.Services;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class MainForm : Form
    {
        private FormDangNhap frm_DangNhap;
        private FormDangKy frm_DangKy;
        private Panel pnl_Overlay;
        private string username;
        private string token;
        private bool isLoggedIn = false;
        private readonly TcpClientService tcpClient;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomUI();

            // Đăng ký sự kiện khi MainForm đóng
            this.FormClosing += MainForm_FormClosing;
        }

        public MainForm(string username, string token) : this()
        {
            this.username = username;
            this.token = token;
            this.Text = $"Adventure App - Welcome {username}";

            // ✅ THÊM: Ẩn form login/register nếu đã login
            pnl_Overlay.Visible = false;

            // ✅ THÊM: Hiển thị giao diện chính của app
            InitializeMainAppUI();
        }

        private void InitializeMainAppUI()
        {
            // ✅ THÊM: Tạo giao diện chính sau khi login
            var lblWelcome = new Label()
            {
                Text = $"Welcome, {username}!",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 50)
            };

            var btnLogout = new Button()
            {
                Text = "Logout",
                Size = new Size(100, 40),
                Location = new Point(50, 100),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            btnLogout.Click += btnLogout_Click;

            this.Controls.Add(lblWelcome);
            this.Controls.Add(btnLogout);
        }

        private void InitializeCustomUI()
        {
            // ⚙️ Cấu hình Form chính
            this.Text = "Adventure Login / Register";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // 🌲 Nền gỗ
            try
            {
                this.BackgroundImage = new Bitmap("wood_texture.jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                this.BackColor = Color.FromArgb(34, 25, 18); // fallback
            }

            // 🌑 Overlay tối nhẹ
            pnl_Overlay = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(100, 0, 0, 0)
            };
            this.Controls.Add(pnl_Overlay);

            if (!isLoggedIn)
            {
                InitializeLoginForms();
            }
        }
        private void InitializeLoginForms()
        {
            // 🧙‍♂️ Form đăng nhập
            frm_DangNhap = new FormDangNhap
            {
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            frm_DangNhap.SwitchToRegister += OnSwitchToDangKy;

            // 🧝‍♀️ Form đăng ký
            frm_DangKy = new FormDangKy
            {
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            frm_DangKy.SwitchToLogin += OnSwitchToDangNhap;

            // 🧩 Thêm cả hai vào overlay panel
            pnl_Overlay.Controls.Add(frm_DangNhap);
            pnl_Overlay.Controls.Add(frm_DangKy);

            // Mặc định hiển thị Form đăng nhập
            frm_DangNhap.Show();
            frm_DangKy.Hide();
            frm_DangNhap.BringToFront();
        }

        // 🔁 Chuyển sang Form đăng nhập
        private void OnSwitchToDangNhap(object sender, EventArgs e)
        {
            frm_DangNhap.Show();
            frm_DangKy.Hide();
            frm_DangNhap.BringToFront();
        }

        // 🔁 Chuyển sang Form đăng ký
        private void OnSwitchToDangKy(object sender, EventArgs e)
        {
            frm_DangKy.Show();
            frm_DangNhap.Hide();
            frm_DangKy.BringToFront();
        }

        // Đóng tất cả các Form khi MainForm đóng
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_DangNhap?.Close();
            frm_DangKy?.Close();
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            // ✅ KIỂM TRA: Nếu user KHÔNG tick Remember me thì không hiển thị hộp thoại
            if (!Properties.Settings.Default.RememberMe)
            {
                // ❌ User không tick Remember me -> logout hoàn toàn
                try
                {
                    await tcpClient.LogoutAsync(token, "complete");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Logout error: {ex.Message}");
                }

                // Xóa tất cả thông tin (dù có lỗi kết nối server)
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.SavedPassword = "";
                Properties.Settings.Default.SavedToken = "";
                Properties.Settings.Default.Save();
            }
            else
            {
                // ✅ User CÓ tick Remember me -> hiển thị hộp thoại lựa chọn
                DialogResult result = MessageBox.Show(
                    "Do you want to stay logged in for next time?\n\n" +
                    "Yes: Keep Remember Me (fast login next time)\n" +
                    "No: Remove all saved login information",
                    "Logout Options",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                try
                {
                    if (result == DialogResult.Yes)
                    {
                        // ✅ LOGOUT BÌNH THƯỜNG: Giữ remember me, không revoke token
                        await tcpClient.LogoutAsync(token, "normal");

                        // Chỉ xóa token khỏi settings, giữ username/password
                        Properties.Settings.Default.SavedToken = "";
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        // ✅ LOGOUT HOÀN TOÀN: Xóa everything, revoke token
                        await tcpClient.LogoutAsync(token, "complete");

                        // Xóa tất cả thông tin
                        Properties.Settings.Default.RememberMe = false;
                        Properties.Settings.Default.SavedUsername = "";
                        Properties.Settings.Default.SavedPassword = "";
                        Properties.Settings.Default.SavedToken = "";
                        Properties.Settings.Default.Save();
                    }
                }
                catch (Exception ex)
                {
                    // ❌ Nếu không kết nối được server, vẫn xóa token local
                    Console.WriteLine($"Logout error: {ex.Message}");
                    Properties.Settings.Default.SavedToken = "";
                    Properties.Settings.Default.Save();
                }
            }

            // Mở form đăng nhập mới
            FormDangNhap loginForm = new FormDangNhap();
            loginForm.Show();
            this.Close();
        }
    }
}