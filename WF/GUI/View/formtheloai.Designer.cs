namespace WF.GUI.View
{
    partial class formtheloai
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
            label1 = new Label();
            label2 = new Label();
            txtMaTl = new TextBox();
            txtTenTl = new TextBox();
            btnThem = new Button();
            dgvDanhSachTl = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSachTl).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(21, 44);
            label1.Name = "label1";
            label1.Size = new Size(149, 27);
            label1.TabIndex = 0;
            label1.Text = "Mã Thể Loại :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(16, 96);
            label2.Name = "label2";
            label2.Size = new Size(154, 27);
            label2.TabIndex = 1;
            label2.Text = "Tên Thể Loại :";
            // 
            // txtMaTl
            // 
            txtMaTl.Location = new Point(176, 44);
            txtMaTl.Name = "txtMaTl";
            txtMaTl.Size = new Size(242, 31);
            txtMaTl.TabIndex = 2;
            // 
            // txtTenTl
            // 
            txtTenTl.Location = new Point(176, 96);
            txtTenTl.Name = "txtTenTl";
            txtTenTl.Size = new Size(242, 31);
            txtTenTl.TabIndex = 3;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnThem.Location = new Point(154, 142);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(112, 34);
            btnThem.TabIndex = 4;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // dgvDanhSachTl
            // 
            dgvDanhSachTl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachTl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDanhSachTl.Dock = DockStyle.Bottom;
            dgvDanhSachTl.Location = new Point(0, 182);
            dgvDanhSachTl.Name = "dgvDanhSachTl";
            dgvDanhSachTl.RowHeadersWidth = 62;
            dgvDanhSachTl.RowTemplate.Height = 33;
            dgvDanhSachTl.Size = new Size(430, 442);
            dgvDanhSachTl.TabIndex = 5;
            dgvDanhSachTl.CellDoubleClick += dgvDanhSachTl_CellDoubleClick;
            // 
            // formtheloai
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 624);
            Controls.Add(dgvDanhSachTl);
            Controls.Add(btnThem);
            Controls.Add(txtTenTl);
            Controls.Add(txtMaTl);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "formtheloai";
            Text = "TheLoai";
            Load += formtheloai_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDanhSachTl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtMaTl;
        private TextBox txtTenTl;
        private Button btnThem;
        private DataGridView dgvDanhSachTl;
    }
}