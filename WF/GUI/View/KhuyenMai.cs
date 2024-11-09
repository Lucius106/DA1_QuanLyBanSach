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

namespace WF.Form_Chức_Năng.Form_Chức_Năng___ADMIN
{
    public partial class KhuyenMai : Form
    {
        public KhuyenMai()
        {
            InitializeComponent();
        }
        VoucherService vouchersv;
        int idwhenclick = new int();
        private void KhuyenMai_Load(object sender, EventArgs e)
        {
            vouchersv = new VoucherService();
            LoadVoucher();
        }
        public void LoadVoucher()
        {
            int STT = 1;
            dgvDanhSachMaGiamGia.ColumnCount = 9;
            dgvDanhSachMaGiamGia.Rows.Clear();
            dgvDanhSachMaGiamGia.Columns[0].Name = "ID";
            dgvDanhSachMaGiamGia.Columns[1].Name = "STT";
            dgvDanhSachMaGiamGia.Columns[2].Name = "Mã giảm giá";
            dgvDanhSachMaGiamGia.Columns[3].Name = "Ngày bắt đầu";
            dgvDanhSachMaGiamGia.Columns[4].Name = "Ngày kết thúc";
            dgvDanhSachMaGiamGia.Columns[5].Name = "Tỉ lệ giảm(%)";
            dgvDanhSachMaGiamGia.Columns[6].Name = "Đơn hàng tối thiểu";
            dgvDanhSachMaGiamGia.Columns[7].Name = "Giảm tối đa";
            dgvDanhSachMaGiamGia.Columns[8].Name = "Số Lượng";
            dgvDanhSachMaGiamGia.Columns[3].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvDanhSachMaGiamGia.Columns[4].DefaultCellStyle.Format = "dd-MM-yyyy";

            dgvDanhSachMaGiamGia.Columns[0].Visible = false;

            foreach (var item in vouchersv.GetAllVouchersv())
            {
                dgvDanhSachMaGiamGia.Rows.Add(item.Id, STT++, item.MaVoucher, item.NgayBatDau, item.NgayKetThuc, item.TiLeGiam, item.DonHangToiThieu, item.GiamToiDa, item.SoLuong);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu txtMaGiamGia.Text để trống
                if (string.IsNullOrEmpty(txtMaGiamGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập thông tin");
                    return; // Dừng thực thi tiếp theo nếu không có dữ liệu
                }

                // Kiểm tra mã voucher đã tồn tại
                bool check = vouchersv.GetAllVouchersv().Any(x => x.MaVoucher == txtMaGiamGia.Text);
                if (check)
                {
                    MessageBox.Show("Mã đã tồn tại");
                }
                else
                {
                    Voucher vc = new Voucher();
                    vc.MaVoucher = txtMaGiamGia.Text;
                    vc.NgayBatDau = DateTime.ParseExact(dtpNgayBatDau.Text.Trim(), "dd-MM-yyyy", null);
                    vc.TiLeGiam = Convert.ToInt64(txtTiLeGiam.Text);
                    vc.NgayKetThuc = DateTime.ParseExact(dtpNgayKetThuc.Text.Trim(), "dd-MM-yyyy", null);
                    vc.DonHangToiThieu = Convert.ToInt64(txtDonToiThieu.Text);
                    vc.SoLuong = int.Parse(txtSoLuong.Text);
                    vc.GiamToiDa = Convert.ToInt64(txtGiamToiDa.Text);
                    MessageBox.Show(vouchersv.Them(vc));
                    LoadVoucher();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaGiamGia.Text = "";
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;
            txtTiLeGiam.Text = "";
            txtDonToiThieu.Text = "";
            txtGiamToiDa.Text = "";
            txtSoLuong.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {

                Voucher vc = new Voucher();
                vc.MaVoucher = txtMaGiamGia.Text;
                vc.NgayBatDau = DateTime.ParseExact(dtpNgayBatDau.Text.Trim(), "dd-MM-yyyy", null);
                vc.NgayKetThuc = DateTime.ParseExact(dtpNgayKetThuc.Text.Trim(), "dd-MM-yyyy", null);
                vc.TiLeGiam = int.Parse(txtTiLeGiam.Text);
                vc.DonHangToiThieu = int.Parse(txtDonToiThieu.Text);
                vc.GiamToiDa = int.Parse(txtGiamToiDa.Text);
                vc.SoLuong = int.Parse(txtSoLuong.Text);
                MessageBox.Show(vouchersv.sua(vc, idwhenclick));
                LoadVoucher();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvDanhSachMaGiamGia_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvDanhSachMaGiamGia.Rows[e.RowIndex];

                // Set textbox values based on the selected row
                idwhenclick = int.Parse(dgvDanhSachMaGiamGia.CurrentRow.Cells[0].Value.ToString());
                txtMaGiamGia.Text = selectedRow.Cells[2].Value.ToString();
                dtpNgayBatDau.Value = Convert.ToDateTime(selectedRow.Cells[3].Value);
                dtpNgayKetThuc.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
                txtTiLeGiam.Text = selectedRow.Cells[5].Value.ToString();
                txtDonToiThieu.Text = selectedRow.Cells[6].Value.ToString();
                txtGiamToiDa.Text = selectedRow.Cells[7].Value.ToString();
                txtSoLuong.Text = selectedRow.Cells[8].Value.ToString();
            }
        }
        public void LoadVoucher(string name)
        {
            int STT = 1;
            dgvDanhSachMaGiamGia.ColumnCount = 9;
            dgvDanhSachMaGiamGia.Rows.Clear();
            dgvDanhSachMaGiamGia.Columns[0].Name = "ID";
            dgvDanhSachMaGiamGia.Columns[1].Name = "STT";
            dgvDanhSachMaGiamGia.Columns[2].Name = "Mã giảm giá";
            dgvDanhSachMaGiamGia.Columns[3].Name = "Ngày bắt đầu";
            dgvDanhSachMaGiamGia.Columns[4].Name = "Ngày kết thúc";
            dgvDanhSachMaGiamGia.Columns[5].Name = "Tỉ lệ giảm(%)";
            dgvDanhSachMaGiamGia.Columns[6].Name = "Đơn hàng tối thiểu";
            dgvDanhSachMaGiamGia.Columns[7].Name = "Giảm tối đa";
            dgvDanhSachMaGiamGia.Columns[8].Name = "Số Lượng";
            dgvDanhSachMaGiamGia.Columns[3].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvDanhSachMaGiamGia.Columns[4].DefaultCellStyle.Format = "dd-MM-yyyy";

            dgvDanhSachMaGiamGia.Columns[0].Visible = false;

            foreach (var item in vouchersv.TimkiemMa(name))
            {
                dgvDanhSachMaGiamGia.Rows.Add(item.Id, STT++, item.MaVoucher, item.NgayBatDau, item.NgayKetThuc, item.TiLeGiam, item.DonHangToiThieu, item.GiamToiDa, item.SoLuong);
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadVoucher(txtTimKiem.Text);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
