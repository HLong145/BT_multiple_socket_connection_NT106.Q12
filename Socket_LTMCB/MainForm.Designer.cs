namespace Socket_LTMCB
{
    partial class MainForm
    {
        /// <summary>
        /// Biến designer bắt buộc.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Dọn dẹp các tài nguyên đang được sử dụng.
        /// </summary>
        /// <param name="disposing">true nếu tài nguyên được quản lý nên được loại bỏ; ngược lại, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Phương thức bắt buộc cho Designer support - không được chỉnh sửa
        /// nội dung của phương thức này bằng trình chỉnh sửa mã.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600); // Kích thước cơ bản
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        // KHÔNG CÓ khai báo frm_DangNhap, frm_DangKy, pnl_Overlay ở đây. 
        // Chúng được khai báo trong MainForm.cs để tránh lỗi Ambiguity.
    }
}
