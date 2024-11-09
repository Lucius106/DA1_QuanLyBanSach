using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;
using WF.BLL.Service;
using WF.DAL.Models;
using WF.DAL.Reposistoris;

namespace WF.Form_Chức_Năng.Form_Chức_Năng___ADMIN
{
    public partial class Nhanvien : Form
    {
        public Nhanvien()
        {
            InitializeComponent();
        }
        NhanVienService nhanviensv;
        int idwhenclick = new int();
        string pathimg;
        dynamic imgLoad;
        private void Nhanvien_Load(object sender, EventArgs e)
        {
            nhanviensv = new NhanVienService();
            LoadNhanVien();
        }
        public void LoadNhanVien()
        {
            int STT = 1;
            dgvDanhSach.ColumnCount = 15;
            dgvDanhSach.Rows.Clear();
            dgvDanhSach.Columns[0].Name = "ID";
            dgvDanhSach.Columns[1].Name = "STT";
            dgvDanhSach.Columns[2].Name = "Mã NV";
            dgvDanhSach.Columns[3].Name = "Tên NV";
            dgvDanhSach.Columns[4].Name = "Hình ảnh";
            dgvDanhSach.Columns[5].Name = "CCCD";
            dgvDanhSach.Columns[6].Name = "Ngày sinh";
            dgvDanhSach.Columns[7].Name = "Giới tính";
            dgvDanhSach.Columns[8].Name = "Email";
            dgvDanhSach.Columns[9].Name = "SĐT";
            dgvDanhSach.Columns[10].Name = "Tài khoản";
            dgvDanhSach.Columns[11].Name = "Mật khẩu";
            dgvDanhSach.Columns[12].Name = "Vai trò";
            dgvDanhSach.Columns[13].Name = "Địa chỉ";
            dgvDanhSach.Columns[14].Name = "Trạng thái";
            dgvDanhSach.Columns[6].DefaultCellStyle.Format = "dd-MM-yyyy";

            dgvDanhSach.Columns[0].Visible = false;

            foreach (var item in nhanviensv.GetAllNhanViensv())
            {
                dgvDanhSach.Rows.Add(item.Id, STT++, item.MaNv, item.HoTenNv, item.Hinh, item.Cccd, item.NgaySinh, item.GioiTinh, item.Email, item.Sđt, item.TenTk, item.MatKhau, item.VaiTro, item.DiaChi, item.TrangThai);
            }
        }
        public byte[] ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imagebytes = ms.ToArray();
                return imagebytes;
            }
        }

        public Image Base64ToImage(byte[] imagebytes)
        {
            MemoryStream ms = new MemoryStream(imagebytes, 0, imagebytes.Length);
            ms.Write(imagebytes, 0, imagebytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    Image image = Image.FromFile(openFileDialog.FileName);


                    pictureNhanvien.Image = image;

                    pathimg = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải hình ảnh: " + ex.Message);
                }
            }
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pathimg))
            {
                bool check = nhanviensv.GetAllNhanViensv().Any(x => x.MaNv == txtMaNV.Text);
                if (check)
                {
                    MessageBox.Show("Mã đã tồn tại");
                }
                else
                {
                    byte[] imageBytes = File.ReadAllBytes(pathimg);
                    NhanVien nv = new NhanVien();
                    nv.MaNv = txtMaNV.Text;
                    nv.HoTenNv = txtTen.Text;
                    nv.Hinh = imageBytes;
                    nv.Cccd = txtcccd.Text;
                    nv.NgaySinh = DateTime.ParseExact(dtpNgaySinh.Text.Trim(), "dd-MM-yyyy", null);
                    nv.GioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
                    nv.Email = txtemail.Text;
                    nv.Sđt = txtsđt.Text;
                    nv.TenTk = txttaikhoan.Text;
                    nv.MatKhau = txtmatkhau.Text;
                    nv.VaiTro = rdoNhanvien.Checked ? "Nhân viên" : "Quản lý";
                    nv.DiaChi = txtdiachi.Text;
                    nv.TrangThai = rdoDangLam.Checked ? "Đang làm" : "Nghỉ làm";
                    MessageBox.Show(nhanviensv.Them(nv));
                    LoadNhanVien();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hình ảnh trước khi thêm sản phẩm.", "Thông báo");
            }
        }

        private void txtSua_Click_1(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = new NhanVien();
                if (pathimg != null)
                {
                    byte[] imageBytes = File.ReadAllBytes(pathimg);
                    nv.Hinh = imageBytes;
                }
                else
                    nv.Hinh = imgLoad;
                nv.MaNv = txtMaNV.Text;
                nv.HoTenNv = txtTen.Text;
                nv.Cccd = txtcccd.Text;
                nv.NgaySinh = DateTime.ParseExact(dtpNgaySinh.Text.Trim(), "dd-MM-yyyy", null);
                nv.GioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
                nv.Email = txtemail.Text;
                nv.Sđt = txtsđt.Text;
                nv.TenTk = txttaikhoan.Text;
                nv.MatKhau = txtmatkhau.Text;
                nv.VaiTro = rdoNhanvien.Checked ? "Nhân viên" : "Quản lý";
                nv.DiaChi = txtdiachi.Text;
                nv.TrangThai = rdoDangLam.Checked ? "Đang làm" : "Nghỉ làm";
                MessageBox.Show(nhanviensv.sua(nv, idwhenclick));
                LoadNhanVien();
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi rồi");
            }
        }

        private void txtLammoi_Click_1(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtTen.Text = "";
            txtcccd.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            rdoNam.Checked = false;
            rdonu.Checked = false;
            txtemail.Text = "";
            txtsđt.Text = "";
            txttaikhoan.Text = "";
            txtmatkhau.Text = "";
            rdoNhanvien.Checked = false;
            rdoquanly.Checked = false;
            txtdiachi.Text = "";
            rdoDangLam.Checked = false;
            rdoNghilam.Checked = false;
            pictureNhanvien.Image = null;
        }

        private void dgvDanhSach_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            idwhenclick = int.Parse(dgvDanhSach.CurrentRow.Cells[0].Value.ToString());
            txtMaNV.Text = dgvDanhSach.CurrentRow.Cells[2].Value.ToString();
            txtTen.Text = dgvDanhSach.CurrentRow.Cells[3].Value.ToString();
            txtcccd.Text = dgvDanhSach.CurrentRow.Cells[5].Value.ToString();
            dtpNgaySinh.Value = Convert.ToDateTime(dgvDanhSach.CurrentRow.Cells[6].Value);
            if (dgvDanhSach.CurrentRow.Cells[7].Value.ToString().Equals("Nam", StringComparison.OrdinalIgnoreCase))
            {
                rdoNam.Checked = true;
            }
            else
            {
                rdonu.Checked = true;
            }
            txtemail.Text = dgvDanhSach.CurrentRow.Cells[8].Value.ToString();
            txtsđt.Text = dgvDanhSach.CurrentRow.Cells[9].Value.ToString();
            txttaikhoan.Text = dgvDanhSach.CurrentRow.Cells[10].Value.ToString();
            txtmatkhau.Text = dgvDanhSach.CurrentRow.Cells[11].Value.ToString();
            if (dgvDanhSach.CurrentRow.Cells[12].Value.ToString().Equals("Nhân Viên", StringComparison.OrdinalIgnoreCase))
            {
                rdoNhanvien.Checked = true;
            }
            else
            {
                rdoquanly.Checked = true;
            }
            txtdiachi.Text = dgvDanhSach.CurrentRow.Cells[13].Value.ToString();
            if (dgvDanhSach.CurrentRow.Cells[14].Value.ToString().Equals("Đang làm", StringComparison.OrdinalIgnoreCase))
            {
                rdoDangLam.Checked = true;
            }
            else
            {
                rdoNghilam.Checked = true;
            }

            var s = nhanviensv.Findid(idwhenclick);
            if (s != null && s.Hinh != null)
            {
                byte[] imageData = s.Hinh;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureNhanvien.Image = Image.FromStream(ms);
                    imgLoad = s.Hinh;
                }
            }
            else
            {
                pictureNhanvien.Image = null;
                imgLoad = null;
            }
        }
        public void LoadNhanVien(string name)
        {
            int STT = 1;
            dgvDanhSach.ColumnCount = 15;
            dgvDanhSach.Rows.Clear();
            dgvDanhSach.Columns[0].Name = "ID";
            dgvDanhSach.Columns[1].Name = "STT";
            dgvDanhSach.Columns[2].Name = "Mã NV";
            dgvDanhSach.Columns[3].Name = "Tên NV";
            dgvDanhSach.Columns[4].Name = "Hình ảnh";
            dgvDanhSach.Columns[5].Name = "CCCD";
            dgvDanhSach.Columns[6].Name = "Ngày sinh";
            dgvDanhSach.Columns[7].Name = "Giới tính";
            dgvDanhSach.Columns[8].Name = "Email";
            dgvDanhSach.Columns[9].Name = "SĐT";
            dgvDanhSach.Columns[10].Name = "Tài khoản";
            dgvDanhSach.Columns[11].Name = "Mật khẩu";
            dgvDanhSach.Columns[12].Name = "Vai trò";
            dgvDanhSach.Columns[13].Name = "Địa chỉ";
            dgvDanhSach.Columns[14].Name = "Trạng thái";
            dgvDanhSach.Columns[6].DefaultCellStyle.Format = "dd-MM-yyyy";

            dgvDanhSach.Columns[0].Visible = false;

            foreach (var item in nhanviensv.FindName(name))
            {
                dgvDanhSach.Rows.Add(item.Id, STT++, item.MaNv, item.HoTenNv, item.Hinh, item.Cccd, item.NgaySinh, item.GioiTinh, item.Email, item.Sđt, item.TenTk, item.MatKhau, item.VaiTro, item.DiaChi, item.TrangThai);
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadNhanVien(txtTimKiem.Text);
        }
    }
}
