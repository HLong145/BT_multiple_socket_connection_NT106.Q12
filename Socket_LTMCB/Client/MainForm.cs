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
            this.FormClosing += MainForm_FormClosing;
        }

        public MainForm(string username, string token) : this()
        {
            this.username = username;
            this.token = token;
            this.isLoggedIn = true;

            // ✅ KHỞI TẠO TCP CLIENT
            tcpClient = new TcpClientService("127.0.0.1", 8080);

            // ✅ DEBUG: Kiểm tra username nhận được
            Console.WriteLine($"🎯 MainForm constructor - Username: {username}, Token: {token}");

            // ✅ CẬP NHẬT UI VỚI USERNAME
            UpdateUsernameDisplay(username);
        }

        // ✅ PHƯƠNG THỨC CẬP NHẬT USERNAME
        public void UpdateUsernameDisplay(string newUsername)
        {
            username = newUsername;

            if (!string.IsNullOrEmpty(username))
            {
                // ✅ CẬP NHẬT LABEL USERNAME TRONG SIDEBAR
                if (lblUserName != null)
                {
                    lblUserName.Text = username.ToUpper();
                    Console.WriteLine($"✅ Updated lblUserName to: {username}");
                }

                // ✅ CẬP NHẬT TIÊU ĐỀ FORM
                this.Text = $"Adventure App - Welcome {username}";

                // ✅ ẨN FORM LOGIN/REGISTER NẾU ĐÃ LOGIN
                if (pnl_Overlay != null)
                {
                    pnl_Overlay.Visible = false;
                    Console.WriteLine("✅ Hidden login/register overlay");
                }
            }
        }

        private void InitializeCustomUI()
        {
            // ⚙️ Cấu hình Form chính
            this.Text = "Adventure Login / Register";
            this.ClientSize = new Size(1312, 742);
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
                this.BackColor = Color.FromArgb(34, 25, 18);
            }

            // 🌑 Overlay tối nhẹ (chỉ hiển thị khi chưa login)
            if (!isLoggedIn)
            {
                pnl_Overlay = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(100, 0, 0, 0)
                };
                this.Controls.Add(pnl_Overlay);
                pnl_Overlay.BringToFront();

                InitializeLoginForms();
            }
        }

        private void InitializeLoginForms()
        {
            // 🧙‍♂️ Form đăng nhập
            frm_DangNhap = new FormDangNhap
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None
            };
            frm_DangNhap.SwitchToRegister += OnSwitchToDangKy;

            // 🧝‍♀️ Form đăng ký
            frm_DangKy = new FormDangKy
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None
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
            try
            {
                Console.WriteLine($"🚪 Logging out user: {username}");
                Console.WriteLine($"🔍 RememberMe setting: {Properties.Settings.Default.RememberMe}");

                // ✅ XỬ LÝ LOGOUT DỰA TRÊN REMEMBER ME (KHÔNG HIỂN THỊ HỘP THOẠI)
                if (Properties.Settings.Default.RememberMe)
                {
                    // ✅ USER CÓ TICK REMEMBER ME -> LOGOUT BÌNH THƯỜNG (KHÔNG REVOKE TOKEN)
                    try
                    {
                        await tcpClient.LogoutAsync(token, "normal");
                        Console.WriteLine("✅ Normal logout (token preserved for Remember Me)");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Normal logout error: {ex.Message}");
                    }

                    // ✅ CHỈ XÓA TOKEN, GIỮ USERNAME/PASSWORD
                    Properties.Settings.Default.SavedToken = "";
                    Properties.Settings.Default.Save();
                    Console.WriteLine("✅ Token cleared, username/password preserved");
                }
                else
                {
                    // ✅ USER KHÔNG TICK REMEMBER ME -> LOGOUT HOÀN TOÀN (REVOKE TOKEN)
                    try
                    {
                        await tcpClient.LogoutAsync(token, "complete");
                        Console.WriteLine("✅ Complete logout (token revoked)");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Complete logout error: {ex.Message}");
                    }

                    // ✅ XÓA TẤT CẢ THÔNG TIN
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.SavedUsername = "";
                    Properties.Settings.Default.SavedPassword = "";
                    Properties.Settings.Default.SavedToken = "";
                    Properties.Settings.Default.Save();
                    Console.WriteLine("✅ All login data cleared");
                }

                // ✅ MỞ LẠI FORM ĐĂNG NHẬP
                FormDangNhap loginForm = new FormDangNhap();
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.Show();

                // ✅ ĐÓNG FORM HIỆN TẠI
                this.Close();

                Console.WriteLine("✅ Logout completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Logout error: {ex.Message}");
                MessageBox.Show($"Logout error: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Property để truy cập username
        public string CurrentUsername
        {
            get { return username; }
            set { UpdateUsernameDisplay(value); }
        }
    }
}