using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.BLL.Service;
using WF.DAL.Models;
using WF.Form_Chức_Năng.Form_Chức_Năng___ADMIN;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WF.GUI.View
{
    public partial class formtheloai : Form
    {
        public formtheloai()
        {
            InitializeComponent();
        }
        TheLoaiService theloaisv;
        int idwhenClick = new int();
        private void formtheloai_Load(object sender, EventArgs e)
        {
            theloaisv = new TheLoaiService();
            LoadTheLoai();
        }
        public void LoadTheLoai()
        {
            int STT = 1;
            dgvDanhSachTl.ColumnCount = 4;
            dgvDanhSachTl.Rows.Clear();
            dgvDanhSachTl.Columns[0].Name = "ID";
            dgvDanhSachTl.Columns[1].Name = "STT";
            dgvDanhSachTl.Columns[2].Name = "Mã Thể loại";
            dgvDanhSachTl.Columns[3].Name = "Tên Thể loại";

            dgvDanhSachTl.Columns[0].Visible = false;

            foreach (var item in theloaisv.GetAllTheLoaisv())
            {
                dgvDanhSachTl.Rows.Add(item.Id, STT++, item.MaTheLoai, item.TenTheLoai);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bool check = theloaisv.GetAllTheLoaisv().Any(x => x.MaTheLoai == txtMaTl.Text);
            if (check)
            {
                MessageBox.Show("Mã đã tồn tại");
            }
            else
            {
                TheLoai tl = new TheLoai();
                tl.MaTheLoai = txtMaTl.Text;
                tl.TenTheLoai = txtTenTl.Text;
                MessageBox.Show(theloaisv.Them(tl));
                LoadTheLoai();
            }
            this.Close();
        }
        private SanPham formtl;
        private void dgvDanhSachTl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    object cellValue = dgvDanhSachTl.Rows[e.RowIndex].Cells[3].Value;

            //    if (cellValue != null)
            //    {
            //        // Kiểm tra nếu formtl chưa được khởi tạo, khởi tạo nó
            //        if (formtl == null)
            //        {
            //            formtl = new SanPham();
            //        }

            //        // Gán giá trị cho TenTheLoaiText của formtl
            //        formtl.TenTheLoaiText = cellValue.ToString();
            //        // Ẩn form SanPham
            //        formtl.Hide();
            //    }
            //}
        }
    }
}
