using System;
using System.Drawing;
using System.Windows.Forms;

namespace Socket_LTMCB
{
    public partial class FormDangNhap : Form
    {
        private int floatingOffset = 0;
        private Random random = new Random();
        private System.Windows.Forms.Timer floatingItemsTimer;

        public event EventHandler SwitchToRegister;

        public FormDangNhap()
        {
            InitializeComponent();
            SetupFloatingAnimation();
        }

        private void SetupFloatingAnimation()
        {
            floatingItemsTimer = new System.Windows.Forms.Timer();
            floatingItemsTimer.Interval = 50;
            floatingItemsTimer.Tick += FloatingItemsTimer_Tick;
            floatingItemsTimer.Start();
        }

        private void FloatingItemsTimer_Tick(object sender, EventArgs e)
        {
            floatingOffset += 2;
            if (floatingOffset > this.Height + 50)
                floatingOffset = -50;
            this.Invalidate();
        }

        private void FormDangNhap_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            for (int i = 0; i < 8; i++)
            {
                int x = (i * 15 + 10) * this.Width / 100;
                int y = (floatingOffset + (i * 80)) % (this.Height + 100) - 50;

                using (SolidBrush brush = new SolidBrush(Color.Gold))
                    g.FillRectangle(brush, x, y, 12, 12);

                using (Pen pen = new Pen(Color.Black, 2))
                    g.DrawRectangle(pen, x, y, 12, 12);
            }
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tb_Password.UseSystemPasswordChar = !chk_ShowPassword.Checked;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Login clicked!");
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            SwitchToRegister?.Invoke(this, EventArgs.Empty);
        }

        private void btn_Forgot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Forgot password clicked!");
        }

        // ✅ BỎ LOGIC CANCEL - Cho phép form đóng bình thường
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dừng animation khi form đóng
            floatingItemsTimer?.Stop();
            floatingItemsTimer?.Dispose();

            base.OnFormClosing(e);
        }
    }
}