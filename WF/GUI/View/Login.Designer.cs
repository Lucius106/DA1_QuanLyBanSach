namespace WF.Form_Login_TrangChu
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            bt_Login = new Button();
            panel4 = new Panel();
            pictureBox4 = new PictureBox();
            txt_password = new TextBox();
            panel3 = new Panel();
            pictureBox3 = new PictureBox();
            txt_username = new TextBox();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            Close = new Guna.UI2.WinForms.Guna2Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = null;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(98, 20);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(349, 270);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // bt_Login
            // 
            bt_Login.Anchor = AnchorStyles.None;
            bt_Login.BackColor = Color.White;
            bt_Login.FlatStyle = FlatStyle.Flat;
            bt_Login.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            bt_Login.Location = new Point(98, 516);
            bt_Login.Margin = new Padding(3, 4, 3, 4);
            bt_Login.Name = "bt_Login";
            bt_Login.Size = new Size(334, 57);
            bt_Login.TabIndex = 11;
            bt_Login.Text = "LOGIN";
            bt_Login.UseVisualStyleBackColor = false;
            bt_Login.Click += bt_Login_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Lavender;
            panel4.Controls.Add(pictureBox4);
            panel4.Controls.Add(txt_password);
            panel4.Location = new Point(65, 385);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(415, 74);
            panel4.TabIndex = 10;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.None;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(11, 13);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(50, 48);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // txt_password
            // 
            txt_password.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txt_password.BackColor = Color.Lavender;
            txt_password.BorderStyle = BorderStyle.None;
            txt_password.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point);
            txt_password.Location = new Point(85, 24);
            txt_password.Margin = new Padding(3, 4, 3, 4);
            txt_password.Name = "txt_password";
            txt_password.PlaceholderText = "password";
            txt_password.Size = new Size(263, 37);
            txt_password.TabIndex = 0;
            txt_password.UseSystemPasswordChar = true;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Lavender;
            panel3.Controls.Add(pictureBox3);
            panel3.Controls.Add(txt_username);
            panel3.Location = new Point(64, 297);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(416, 80);
            panel3.TabIndex = 9;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.None;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(12, 13);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(62, 63);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // txt_username
            // 
            txt_username.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txt_username.BackColor = Color.Lavender;
            txt_username.BorderStyle = BorderStyle.None;
            txt_username.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point);
            txt_username.Location = new Point(86, 22);
            txt_username.Margin = new Padding(3, 4, 3, 4);
            txt_username.Name = "txt_username";
            txt_username.PlaceholderText = "username";
            txt_username.Size = new Size(249, 37);
            txt_username.TabIndex = 0;
            txt_username.Tag = "";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 20;
            guna2Elipse1.TargetControl = this;
            // 
            // Close
            // 
            Close.BackColor = Color.Transparent;
            Close.BorderColor = Color.Transparent;
            Close.BorderRadius = 30;
            Close.CustomizableEdges = customizableEdges1;
            Close.DisabledState.BorderColor = Color.DarkGray;
            Close.DisabledState.CustomBorderColor = Color.DarkGray;
            Close.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            Close.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            Close.FillColor = Color.White;
            Close.Font = new Font("Showcard Gothic", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            Close.ForeColor = Color.Black;
            Close.Location = new Point(472, 2);
            Close.Margin = new Padding(3, 4, 3, 4);
            Close.Name = "Close";
            Close.PressedColor = Color.WhiteSmoke;
            Close.ShadowDecoration.CustomizableEdges = customizableEdges2;
            Close.Size = new Size(78, 75);
            Close.TabIndex = 16;
            Close.Text = "X";
            Close.Click += Close_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaption;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(-339, -25);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(347, 37);
            textBox1.TabIndex = 2;
            textBox1.Tag = "";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(552, 638);
            Controls.Add(textBox1);
            Controls.Add(Close);
            Controls.Add(bt_Login);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button bt_Login;
        private Panel panel4;
        private PictureBox pictureBox4;
        private TextBox txt_password;
        private Panel panel3;
        private PictureBox pictureBox3;
        private TextBox txt_username;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Button Close;
        private TextBox textBox1;
    }
}