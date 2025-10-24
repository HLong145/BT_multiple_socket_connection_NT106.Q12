using System;
using System.Drawing;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class MainForm : Form
    {
        private FormDangNhap frm_DangNhap;
        private FormDangKy frm_DangKy;
        private Panel pnl_Overlay;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomUI();
            InitForms();

            // Đăng ký sự kiện khi MainForm đóng
            this.FormClosing += MainForm_FormClosing;
        }
        private void InitForms()
        {
            frm_DangNhap = new FormDangNhap();
            frm_DangKy = new FormDangKy();

            // Đăng ký event
            frm_DangNhap.SwitchToRegister += (s, e) =>
            {
                frm_DangNhap.Hide();
                frm_DangKy.Show();
            };

            frm_DangKy.SwitchToLogin += (s, e) =>
            {
                frm_DangKy.Hide();
                frm_DangNhap.Show();
            };

            // Thiết lập TopLevel = false nếu muốn add vào Panel
            frm_DangNhap.TopLevel = true;
            frm_DangKy.TopLevel = true;

            // Mở form đăng nhập đầu tiên
            frm_DangNhap.Show();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Đảm bảo tất cả form con đóng hẳn
            frm_DangNhap?.Close();
            frm_DangKy?.Close();
            base.OnFormClosing(e);
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
                // Giả định texture có sẵn, dùng try/catch để đảm bảo code không crash
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

            // 🧙‍♂️ Form đăng nhập
            frm_DangNhap = new FormDangNhap
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                Name = "frm_DangNhap" // Cần Name nếu muốn tìm kiếm, nhưng ta dùng biến thành viên thì không cần.
            };
            frm_DangNhap.SwitchToRegister += OnSwitchToDangKy; // Sự kiện chuyển sang form đăng ký

            // 🧝‍♀️ Form đăng ký
            frm_DangKy = new FormDangKy
            {
                TopLevel = false,
                Dock = DockStyle.Fill,
                Name = "frm_DangKy"
            };
            frm_DangKy.SwitchToLogin += OnSwitchToDangNhap; // Sự kiện chuyển sang form đăng nhập

            // 🧩 Thêm cả hai vào overlay panel
            pnl_Overlay.Controls.Add(frm_DangNhap);
            pnl_Overlay.Controls.Add(frm_DangKy);

            // Mặc định hiển thị Form đăng nhập
            frm_DangNhap.Show();
            frm_DangKy.Hide();
            // Đảm bảo Form đăng nhập nằm trên cùng khi khởi tạo
            frm_DangNhap.BringToFront();
        }

        // 🔁 Chuyển sang Form đăng nhập
        private void OnSwitchToDangNhap(object sender, EventArgs e)
        {
            // Sử dụng biến thành viên (Field) để truy cập trực tiếp, nhanh và an toàn hơn Find().
            frm_DangNhap.Show();
            frm_DangKy.Hide();
            // 🌟 FIX Z-ORDER: Đưa Form đăng nhập lên trước
            frm_DangNhap.BringToFront();
        }

        // 🔁 Chuyển sang Form đăng ký
        private void OnSwitchToDangKy(object sender, EventArgs e)
        {
            // Sử dụng biến thành viên (Field) để truy cập trực tiếp, nhanh và an toàn hơn Find().
            frm_DangKy.Show();
            frm_DangNhap.Hide();
            // 🌟 FIX Z-ORDER: Đưa Form đăng ký lên trước
            frm_DangKy.BringToFront();
        }

        // Đóng tất cả các Form khi MainForm đóng
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Sử dụng toán tử null-conditional an toàn để gọi Close()
            frm_DangNhap?.Close();
            frm_DangKy?.Close();
        }
    }
}
