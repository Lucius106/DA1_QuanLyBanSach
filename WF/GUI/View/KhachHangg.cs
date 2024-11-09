using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;
using WF.BLL.Service;
using WF.DAL.Models;
using WF.DAL.Reposistoris;
using WF.Form_Chức_Năng.Form_Chức_Năng___ADMIN;

namespace WF.Form_Chức_Năng.Form_Chức_Năng___NhanVien
{
    public partial class KhachHangg : Form
    {
        public KhachHangg()
        {
            InitializeComponent();
        }
        KhachHangService khachhangsv;
        int idwhenclick = new int();
        private void KhachHang_Load(object sender, EventArgs e)
        {
            khachhangsv = new KhachHangService();
            LoadKhachHang();
        }
        public void LoadKhachHang()
        {
            int STT = 1;
            dgvDanhSachkh.ColumnCount = 7;
            dgvDanhSachkh.Rows.Clear();
            dgvDanhSachkh.Columns[0].Name = "ID";
            dgvDanhSachkh.Columns[1].Name = "STT";
            dgvDanhSachkh.Columns[2].Name = "Mã khách hàng";
            dgvDanhSachkh.Columns[3].Name = "Tên khách hàng";
            dgvDanhSachkh.Columns[4].Name = "Giới tính";
            dgvDanhSachkh.Columns[5].Name = "Số điện thoại";
            dgvDanhSachkh.Columns[6].Name = "Địa Chỉ";

            dgvDanhSachkh.Columns[0].Visible = false;

            foreach (var item in khachhangsv.GetAllKhachHangsv())
            {
                dgvDanhSachkh.Rows.Add(item.Id, STT++, item.MaKhachHang, item.TenKhachHang, item.GioiTinh, item.Sđt, item.DiaChi);
            }
        }

        private void dgvDanhSachkh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idwhenclick = int.Parse(dgvDanhSachkh.CurrentRow.Cells[0].Value.ToString());
            txtMaKH.Text = dgvDanhSachkh.CurrentRow.Cells[2].Value.ToString();
            txtTenKh.Text = dgvDanhSachkh.CurrentRow.Cells[3].Value.ToString();
            if (dgvDanhSachkh.CurrentRow.Cells[4].Value.ToString().Equals("Nam"))
            {
                rdoNam.Checked = true;
            }
            else
            {
                rdoNu.Checked = true;
            }
            txtSĐT.Text = dgvDanhSachkh.CurrentRow.Cells[5].Value.ToString();
            txtDiaChi.Text = dgvDanhSachkh.CurrentRow.Cells[6].Value.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin");
                return; // Dừng thực thi tiếp theo nếu không có dữ liệu
            }

            // Kiểm tra mã khách hàng đã tồn tại
            bool check = khachhangsv.GetAllKhachHangsv().Any(x => x.MaKhachHang == txtMaKH.Text);
            if (check)
            {
                MessageBox.Show("Mã đã tồn tại");
            }
            else
            {
                KhachHang kh = new KhachHang();
                kh.MaKhachHang = txtMaKH.Text;
                kh.TenKhachHang = txtTenKh.Text;
                kh.GioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
                kh.Sđt = txtSĐT.Text;
                kh.DiaChi = txtDiaChi.Text;
                MessageBox.Show(khachhangsv.Them(kh));
                LoadKhachHang();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.MaKhachHang = txtMaKH.Text;
                kh.TenKhachHang = txtTenKh.Text;
                kh.GioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
                kh.Sđt = txtSĐT.Text;
                kh.DiaChi = txtDiaChi.Text;
                MessageBox.Show(khachhangsv.sua(kh, idwhenclick));
                LoadKhachHang();
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi rồi");
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtTenKh.Text = "";
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            txtSĐT.Text = "";
            txtDiaChi.Text = "";
        }
        public void LoadKhachHang(string name)
        {
            int STT = 1;
            dgvDanhSachkh.ColumnCount = 7;
            dgvDanhSachkh.Rows.Clear();
            dgvDanhSachkh.Columns[0].Name = "ID";
            dgvDanhSachkh.Columns[1].Name = "STT";
            dgvDanhSachkh.Columns[2].Name = "Mã khách hàng";
            dgvDanhSachkh.Columns[3].Name = "Tên khách hàng";
            dgvDanhSachkh.Columns[4].Name = "Giới tính";
            dgvDanhSachkh.Columns[5].Name = "Số điện thoại";
            dgvDanhSachkh.Columns[6].Name = "Địa Chỉ";

            dgvDanhSachkh.Columns[0].Visible = false;

            foreach (var item in khachhangsv.FindName(name))
            {
                dgvDanhSachkh.Rows.Add(item.Id, STT++, item.MaKhachHang, item.TenKhachHang, item.GioiTinh, item.Sđt, item.DiaChi);
            }
        }

        private void txtTimKiemkh_TextChanged(object sender, EventArgs e)
        {
            LoadKhachHang(txtTimKiemkh.Text);
        }
    }
}
