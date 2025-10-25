namespace Socket_LTMCB
{
    partial class FormDangNhap
    {
        /// <summary>
        /// Required designer variable
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Pnl_Pixel pnl_Main;
        private Pnl_Pixel pnl_Title;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_Subtitle;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.Label lbl_Password;
        private Tb_Pixel tb_Username;
        private Tb_Pixel tb_Password;
        private Btn_Pixel btn_Login;
        private Btn_Pixel btn_Register;
        private Btn_Pixel btn_Forgot;
        private System.Windows.Forms.CheckBox chk_ShowPassword;
        private System.Windows.Forms.CheckBox chk_Captcha;
        private System.Windows.Forms.CheckBox chk_Remember;
        private System.Windows.Forms.Timer timer_FloatingItems;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer_FloatingItems = new System.Windows.Forms.Timer(components);
            pnl_Main = new Pnl_Pixel();
            chk_Remember = new CheckBox();
            pnl_Title = new Pnl_Pixel();
            lbl_Subtitle = new Label();
            lbl_Title = new Label();
            lbl_Username = new Label();
            tb_Username = new Tb_Pixel();
            lbl_Password = new Label();
            tb_Password = new Tb_Pixel();
            chk_ShowPassword = new CheckBox();
            chk_Captcha = new CheckBox();
            btn_Login = new Btn_Pixel();
            btn_Register = new Btn_Pixel();
            btn_Forgot = new Btn_Pixel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pnl_Main.SuspendLayout();
            pnl_Title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // pnl_Main
            // 
            pnl_Main.BackColor = Color.FromArgb(210, 105, 30);
            pnl_Main.Controls.Add(pictureBox1);
            pnl_Main.Controls.Add(pictureBox2);
            pnl_Main.Controls.Add(pictureBox3);
            pnl_Main.Controls.Add(chk_Remember);
            pnl_Main.Controls.Add(pnl_Title);
            pnl_Main.Controls.Add(lbl_Username);
            pnl_Main.Controls.Add(tb_Username);
            pnl_Main.Controls.Add(lbl_Password);
            pnl_Main.Controls.Add(tb_Password);
            pnl_Main.Controls.Add(chk_ShowPassword);
            pnl_Main.Controls.Add(chk_Captcha);
            pnl_Main.Controls.Add(btn_Login);
            pnl_Main.Controls.Add(btn_Register);
            pnl_Main.Controls.Add(btn_Forgot);
            pnl_Main.Location = new Point(87, 29);
            pnl_Main.Name = "pnl_Main";
            pnl_Main.Size = new Size(413, 573);
            pnl_Main.TabIndex = 0;
            // 
            // chk_Remember
            // 
            chk_Remember.BackColor = Color.Transparent;
            chk_Remember.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chk_Remember.ForeColor = Color.White;
            chk_Remember.Location = new Point(246, 300);
            chk_Remember.Name = "chk_Remember";
            chk_Remember.Size = new Size(167, 25);
            chk_Remember.TabIndex = 7;
            chk_Remember.Text = "REMEMBER ME";
            chk_Remember.UseVisualStyleBackColor = false;
            // 
            // pnl_Title
            // 
            pnl_Title.BackColor = Color.FromArgb(210, 105, 30);
            pnl_Title.Controls.Add(pictureBox5);
            pnl_Title.Controls.Add(lbl_Title);
            pnl_Title.Controls.Add(pictureBox4);
            pnl_Title.Controls.Add(lbl_Subtitle);
            pnl_Title.Location = new Point(20, 20);
            pnl_Title.Name = "pnl_Title";
            pnl_Title.Size = new Size(360, 100);
            pnl_Title.TabIndex = 0;
            // 
            // lbl_Subtitle
            // 
            lbl_Subtitle.BackColor = Color.Transparent;
            lbl_Subtitle.Font = new Font("Courier New", 7F, FontStyle.Bold);
            lbl_Subtitle.ForeColor = Color.White;
            lbl_Subtitle.Location = new Point(20, 48);
            lbl_Subtitle.Name = "lbl_Subtitle";
            lbl_Subtitle.Size = new Size(325, 20);
            lbl_Subtitle.TabIndex = 1;
            lbl_Subtitle.Text = "ENTER YOUR CREDENTIALS";
            lbl_Subtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Title
            // 
            lbl_Title.BackColor = Color.Transparent;
            lbl_Title.Font = new Font("Courier New", 14F, FontStyle.Bold);
            lbl_Title.ForeColor = Color.Gold;
            lbl_Title.Location = new Point(-17, 18);
            lbl_Title.Name = "lbl_Title";
            lbl_Title.Size = new Size(397, 30);
            lbl_Title.TabIndex = 0;
            lbl_Title.Text = "⚔️ FIGHTER X FIGHTER ⚔️";
            lbl_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_Username
            // 
            lbl_Username.BackColor = Color.Transparent;
            lbl_Username.Font = new Font("Courier New", 8F, FontStyle.Bold);
            lbl_Username.ForeColor = Color.White;
            lbl_Username.Location = new Point(20, 140);
            lbl_Username.Name = "lbl_Username";
            lbl_Username.Size = new Size(120, 20);
            lbl_Username.TabIndex = 1;
            lbl_Username.Text = "USERNAME:";
            // 
            // tb_Username
            // 
            tb_Username.BackColor = Color.FromArgb(42, 31, 26);
            tb_Username.BorderStyle = BorderStyle.None;
            tb_Username.Font = new Font("Courier New", 10F, FontStyle.Bold);
            tb_Username.ForeColor = Color.White;
            tb_Username.Location = new Point(20, 165);
            tb_Username.Multiline = true;
            tb_Username.Name = "tb_Username";
            tb_Username.Size = new Size(360, 35);
            tb_Username.TabIndex = 2;
            // 
            // lbl_Password
            // 
            lbl_Password.BackColor = Color.Transparent;
            lbl_Password.Font = new Font("Courier New", 8F, FontStyle.Bold);
            lbl_Password.ForeColor = Color.White;
            lbl_Password.Location = new Point(20, 220);
            lbl_Password.Name = "lbl_Password";
            lbl_Password.Size = new Size(120, 20);
            lbl_Password.TabIndex = 3;
            lbl_Password.Text = "PASSWORD:";
            // 
            // tb_Password
            // 
            tb_Password.BackColor = Color.FromArgb(42, 31, 26);
            tb_Password.BorderStyle = BorderStyle.None;
            tb_Password.Font = new Font("Courier New", 10F, FontStyle.Bold);
            tb_Password.ForeColor = Color.White;
            tb_Password.Location = new Point(20, 245);
            tb_Password.Multiline = true;
            tb_Password.Name = "tb_Password";
            tb_Password.PasswordChar = '●';
            tb_Password.Size = new Size(360, 35);
            tb_Password.TabIndex = 4;
            // 
            // chk_ShowPassword
            // 
            chk_ShowPassword.BackColor = Color.Transparent;
            chk_ShowPassword.ForeColor = Color.Gold;
            chk_ShowPassword.Location = new Point(340, 250);
            chk_ShowPassword.Name = "chk_ShowPassword";
            chk_ShowPassword.Size = new Size(40, 25);
            chk_ShowPassword.TabIndex = 5;
            chk_ShowPassword.Text = "👁️";
            chk_ShowPassword.UseVisualStyleBackColor = false;
            chk_ShowPassword.CheckedChanged += ShowPasswordCheckBox_CheckedChanged;
            // 
            // chk_Captcha
            // 
            chk_Captcha.BackColor = Color.Transparent;
            chk_Captcha.Font = new Font("Courier New", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chk_Captcha.ForeColor = Color.Gold;
            chk_Captcha.Location = new Point(20, 300);
            chk_Captcha.Name = "chk_Captcha";
            chk_Captcha.Size = new Size(250, 25);
            chk_Captcha.TabIndex = 6;
            chk_Captcha.Text = "I'M NOT A ROBOT 🤖";
            chk_Captcha.UseVisualStyleBackColor = false;
            // 
            // btn_Login
            // 
            btn_Login.BtnColor = Color.FromArgb(34, 139, 34);
            btn_Login.FlatStyle = FlatStyle.Flat;
            btn_Login.Font = new Font("Courier New", 10F, FontStyle.Bold);
            btn_Login.ForeColor = Color.White;
            btn_Login.Location = new Point(20, 360);
            btn_Login.Name = "btn_Login";
            btn_Login.Size = new Size(360, 50);
            btn_Login.TabIndex = 6;
            btn_Login.Text = "▶ START GAME ◀";
            btn_Login.Click += btn_Login_Click;
            // 
            // btn_Register
            // 
            btn_Register.BtnColor = Color.FromArgb(205, 133, 63);
            btn_Register.FlatStyle = FlatStyle.Flat;
            btn_Register.Font = new Font("Courier New", 8F, FontStyle.Bold);
            btn_Register.ForeColor = Color.White;
            btn_Register.Location = new Point(20, 430);
            btn_Register.Name = "btn_Register";
            btn_Register.Size = new Size(170, 40);
            btn_Register.TabIndex = 7;
            btn_Register.Text = "REGISTER";
            btn_Register.Click += btn_Register_Click;
            // 
            // btn_Forgot
            // 
            btn_Forgot.BtnColor = Color.FromArgb(139, 69, 19);
            btn_Forgot.FlatStyle = FlatStyle.Flat;
            btn_Forgot.Font = new Font("Courier New", 8F, FontStyle.Bold);
            btn_Forgot.ForeColor = Color.White;
            btn_Forgot.Location = new Point(210, 430);
            btn_Forgot.Name = "btn_Forgot";
            btn_Forgot.Size = new Size(170, 40);
            btn_Forgot.TabIndex = 8;
            btn_Forgot.Text = "FORGOT?";
            btn_Forgot.Click += btn_Forgot_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.mây;
            pictureBox1.Location = new Point(-93, 494);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(182, 93);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.mây;
            pictureBox2.Location = new Point(256, 494);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(233, 93);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = Properties.Resources.mây;
            pictureBox3.Location = new Point(71, 494);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(179, 93);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = Properties.Resources.mây;
            pictureBox4.Location = new Point(226, 29);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(136, 91);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 10;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.mây;
            pictureBox5.Location = new Point(-30, 51);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(176, 51);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            // 
            // FormDangNhap
            // 
            BackColor = SystemColors.ControlDark;
            BackgroundImage = Properties.Resources.background2;
            ClientSize = new Size(588, 641);
            Controls.Add(pnl_Main);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            pnl_Main.ResumeLayout(false);
            pnl_Main.PerformLayout();
            pnl_Title.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
    }
}
