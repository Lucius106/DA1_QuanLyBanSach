using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WF.BLL.Service;
using WF.DAL.Models;
using WF.GUI.View;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WF.Form_Chức_Năng.Form_Chức_Năng___ADMIN
{
    public partial class SanPham : Form
    {
        public SanPham()
        {
            InitializeComponent();
        }
        dynamic imgLoad;
        string pathimg;
        TheLoaiService theloaisv;
        NhaXBService Nxbsv;
        SachService sachsv;
        SachCtService sachctsv;
        int idwhenClick = new int();
        private void SanPham_Load(object sender, EventArgs e)
        {
            Nxbsv = new NhaXBService();
            theloaisv = new TheLoaiService();
            sachsv = new SachService();
            sachctsv = new SachCtService();
            LoadNhaXuatBan();
            LoadTheLoai();
            LoadSach();
            LoadttSachct();
        }

        public void LoadNhaXuatBan()
        {
            int STT = 1;
            dgvDsNhaXuatBan.ColumnCount = 8;
            dgvDsNhaXuatBan.Rows.Clear();
            dgvDsNhaXuatBan.Columns[0].Name = "ID";
            dgvDsNhaXuatBan.Columns[1].Name = "STT";
            dgvDsNhaXuatBan.Columns[2].Name = "Mã NXB";
            dgvDsNhaXuatBan.Columns[3].Name = "Tên NXB";
            dgvDsNhaXuatBan.Columns[4].Name = "Địa chỉ";
            dgvDsNhaXuatBan.Columns[5].Name = "Số điện thoại";
            dgvDsNhaXuatBan.Columns[6].Name = "Năm xuất bản";
            dgvDsNhaXuatBan.Columns[7].Name = "Trạng thái";
            dgvDsNhaXuatBan.Columns[6].DefaultCellStyle.Format = "dd-MM-yyyy";

            dgvDsNhaXuatBan.Columns[0].Visible = false;

            foreach (var item in Nxbsv.GetAllNXBsv())
            {
                dgvDsNhaXuatBan.Rows.Add(item.Id, STT++, item.MaNxb, item.TenNxb, item.DiaChi, item.Sđt, item.NamXb, item.TrangThai);
            }
        }
        private void btnThemnxb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNXB.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin");
                return; // Dừng thực thi tiếp theo nếu không có dữ liệu
            }

            // Kiểm tra mã nhà xuất bản đã tồn tại
            bool check = Nxbsv.GetAllNXBsv().Any(x => x.MaNxb == txtMaNXB.Text);
            if (check)
            {
                MessageBox.Show("Mã đã tồn tại");
            }
            else
            {
                NhaXuatBan nxb = new NhaXuatBan();
                nxb.MaNxb = txtMaNXB.Text;
                nxb.TenNxb = txtTenNXB.Text;
                nxb.DiaChi = txtDiaChi.Text;
                nxb.Sđt = txtSĐT.Text;
                nxb.NamXb = DateTime.ParseExact(dtpNanXB.Text.Trim(), "dd-MM-yyyy", null);
                if (rdoHoatDong.Checked)
                {
                    nxb.TrangThai = "Hoạt Động";
                }
                else
                {
                    nxb.TrangThai = "Ngừng Hoạt Động";
                }
                MessageBox.Show(Nxbsv.Them(nxb));
                LoadNhaXuatBan();
            }
        }

        private void btnSuanxb_Click(object sender, EventArgs e)
        {
            try
            {
                NhaXuatBan nxb = new NhaXuatBan();
                nxb.MaNxb = txtMaNXB.Text;
                nxb.TenNxb = txtTenNXB.Text;
                nxb.DiaChi = txtDiaChi.Text;
                nxb.Sđt = txtSĐT.Text;
                nxb.NamXb = DateTime.ParseExact(dtpNanXB.Text.Trim(), "dd-MM-yyyy", null);
                if (rdoHoatDong.Checked)
                {
                    nxb.TrangThai = "Hoạt Động";
                }
                else
                {
                    nxb.TrangThai = "Ngừng Hoạt Động";
                }
                MessageBox.Show(Nxbsv.sua(nxb, idwhenClick));
                LoadNhaXuatBan();
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi rồi");
            }
        }

        private void dgvDsNhaXuatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idwhenClick = int.Parse(dgvDsNhaXuatBan.CurrentRow.Cells[0].Value.ToString());
            txtMaNXB.Text = dgvDsNhaXuatBan.CurrentRow.Cells[2].Value.ToString();
            txtTenNXB.Text = dgvDsNhaXuatBan.CurrentRow.Cells[3].Value.ToString();
            txtDiaChi.Text = dgvDsNhaXuatBan.CurrentRow.Cells[4].Value.ToString();
            txtSĐT.Text = dgvDsNhaXuatBan.CurrentRow.Cells[5].Value.ToString();
            dtpNanXB.Text = dgvDsNhaXuatBan.CurrentRow.Cells[6].Value.ToString();
            if (dgvDsNhaXuatBan.CurrentRow.Cells[7].Value.ToString().Equals("Hoạt Động"))
            {
                rdoHoatDong.Checked = true;
            }
            else
            {
                rdoDungHoatDong.Checked = true;
            }
        }

        private void btnLamMoinxb_Click(object sender, EventArgs e)
        {
            txtMaNXB.Text = "";
            txtTenNXB.Text = "";
            txtDiaChi.Text = "";
            dtpNanXB.Text = "";
            rdoHoatDong.Checked = false;
            rdoDungHoatDong.Checked = false;
            txtSĐT.Text = "";
        }
        public void LoadNhaXuatBan(string name)
        {
            int STT = 1;
            dgvDsNhaXuatBan.ColumnCount = 8;
            dgvDsNhaXuatBan.Rows.Clear();
            dgvDsNhaXuatBan.Columns[0].Name = "ID";
            dgvDsNhaXuatBan.Columns[1].Name = "STT";
            dgvDsNhaXuatBan.Columns[2].Name = "Mã NXB";
            dgvDsNhaXuatBan.Columns[3].Name = "Tên NXB";
            dgvDsNhaXuatBan.Columns[4].Name = "Địa chỉ";
            dgvDsNhaXuatBan.Columns[5].Name = "Số điện thoại";
            dgvDsNhaXuatBan.Columns[6].Name = "Năm xuất bản";
            dgvDsNhaXuatBan.Columns[7].Name = "Trạng thái";

            dgvDsNhaXuatBan.Columns[0].Visible = false;

            foreach (var item in Nxbsv.FindName(name))
            {
                dgvDsNhaXuatBan.Rows.Add(item.Id, STT++, item.MaNxb, item.TenNxb, item.DiaChi, item.Sđt, item.NamXb, item.TrangThai);
            }
        }

        private void txtTimKiemnxb_TextChanged(object sender, EventArgs e)
        {
            LoadNhaXuatBan(txtTimKiemnxb.Text);
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

        private void btnThemTl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTl.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin");
                return; // Dừng thực thi tiếp theo nếu không có dữ liệu
            }

            // Kiểm tra mã thể loại đã tồn tại
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
        }

        private void btnSuatl_Click(object sender, EventArgs e)
        {
            try
            {
                TheLoai tl = new TheLoai();
                tl.MaTheLoai = txtMaTl.Text;
                tl.TenTheLoai = txtTenTl.Text;
                MessageBox.Show(theloaisv.sua(tl, idwhenClick));
                LoadTheLoai();
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi rồi");
            }
        }

        private void btnLammoitl_Click(object sender, EventArgs e)
        {
            txtMaTl.Text = "";
            txtTenTl.Text = "";
        }
        public void LoadTheLoai(string name)
        {
            int STT = 1;
            dgvDanhSachTl.ColumnCount = 4;
            dgvDanhSachTl.Rows.Clear();
            dgvDanhSachTl.Columns[0].Name = "ID";
            dgvDanhSachTl.Columns[1].Name = "STT";
            dgvDanhSachTl.Columns[2].Name = "Mã Thể loại";
            dgvDanhSachTl.Columns[3].Name = "Tên Thể loại";

            dgvDanhSachTl.Columns[0].Visible = false;

            foreach (var item in theloaisv.FindName(name))
            {
                dgvDanhSachTl.Rows.Add(item.Id, STT++, item.MaTheLoai, item.TenTheLoai);
            }
        }

        private void dgvDanhSachTl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idwhenClick = int.Parse(dgvDanhSachTl.CurrentRow.Cells[0].Value.ToString());
            txtMaTl.Text = dgvDanhSachTl.CurrentRow.Cells[2].Value.ToString();
            txtTenTl.Text = dgvDanhSachTl.CurrentRow.Cells[3].Value.ToString();
        }

        private void txtTimKiemTl_TextChanged(object sender, EventArgs e)
        {
            LoadTheLoai(txtTimKiemTl.Text);
        }

        public void LoadSach()
        {
            int STT = 1;
            dgvDanhSachSach.ColumnCount = 8;
            dgvDanhSachSach.Rows.Clear();
            dgvDanhSachSach.Columns[0].Name = "ID";
            dgvDanhSachSach.Columns[1].Name = "STT";
            dgvDanhSachSach.Columns[2].Name = "Mã Sách";
            dgvDanhSachSach.Columns[3].Name = "Tên sách";
            dgvDanhSachSach.Columns[4].Name = "Tác giả";
            dgvDanhSachSach.Columns[5].Name = "Ngôn ngữ";
            dgvDanhSachSach.Columns[6].Name = "Mô tả";
            dgvDanhSachSach.Columns[7].Name = "Trạng thái";

            dgvDanhSachSach.Columns[0].Visible = false;

            foreach (var item in sachsv.GetAllSachsv())
            {
                dgvDanhSachSach.Rows.Add(item.Id, STT++, item.MaSach, item.TieuDe, item.TacGia, item.NgonNgu, item.MoTa, item.TrangThai);
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

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    Image image = Image.FromFile(openFileDialog.FileName);


                    pictureSach.Image = image;

                    pathimg = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải hình ảnh: " + ex.Message);
                }
            }
        }
        private void btnThemsach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSachs.Text))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu");
                return; // Dừng thực thi tiếp theo nếu không có dữ liệu
            }

            // Kiểm tra mã sách đã tồn tại
            bool check = sachsv.GetAllSachsv().Any(x => x.MaSach == txtMaSachs.Text);
            if (check)
            {
                MessageBox.Show("Mã đã tồn tại");
            }
            else
            {
                Sach sach = new Sach();
                sach.MaSach = txtMaSachs.Text;
                sach.TieuDe = txtTieudes.Text;
                sach.TacGia = txttacgias.Text;
                sach.NgonNgu = txtNgonngus.Text;
                sach.MoTa = txtMoTas.Text;
                sach.TrangThai = rdoConhang.Checked ? "Còn hàng" : "Hết hàng";
                MessageBox.Show(sachsv.Them(sach));
                LoadSach();
            }
        }

        private void btncapnhatsach_Click(object sender, EventArgs e)
        {
            try
            {
                Sach sach = new Sach();
                sach.MaSach = txtMaSachs.Text;
                sach.TieuDe = txtTieudes.Text;
                sach.TacGia = txttacgias.Text;
                sach.MoTa = txtMoTas.Text;
                sach.TrangThai = rdoConhang.Checked ? "Còn hàng" : "Hết hàng";
                sach.NgonNgu = txtNgonngus.Text;
                MessageBox.Show(sachsv.sua(sach, idwhenClick));
                LoadSach();
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi rồi");
            }
        }

        private void dgvDanhSachSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idwhenClick = int.Parse(dgvDanhSachSach.CurrentRow.Cells[0].Value.ToString());
            txtMaSachs.Text = dgvDanhSachSach.CurrentRow.Cells[2].Value.ToString();
            txtTieudes.Text = dgvDanhSachSach.CurrentRow.Cells[3].Value.ToString();
            txttacgias.Text = dgvDanhSachSach.CurrentRow.Cells[4].Value.ToString();
            txtNgonngus.Text = dgvDanhSachSach.CurrentRow.Cells[5].Value.ToString();
            txtMoTas.Text = dgvDanhSachSach.CurrentRow.Cells[6].Value.ToString();
            if (dgvDanhSachSach.CurrentRow.Cells[7].Value.ToString().Equals("Còn hàng"))
            {
                rdoConhang.Checked = true;
            }
            else
            {
                rdohethang.Checked = true;
            }
        }

        private void btnlammoisach_Click(object sender, EventArgs e)
        {
            txtMaSachs.Text = "";
            txtTieudes.Text = "";
            txttacgias.Text = "";
            txtNgonngus.Text = "";
            txtMoTas.Text = "";
            rdoConhang.Checked = false;
            rdohethang.Checked = false;
        }
        public void LoadSach(string name)
        {
            int STT = 1;
            dgvDanhSachSach.ColumnCount = 8;
            dgvDanhSachSach.Rows.Clear();
            dgvDanhSachSach.Columns[0].Name = "ID";
            dgvDanhSachSach.Columns[1].Name = "STT";
            dgvDanhSachSach.Columns[2].Name = "Mã Sách";
            dgvDanhSachSach.Columns[3].Name = "Tên sách";
            dgvDanhSachSach.Columns[4].Name = "Tác giả";
            dgvDanhSachSach.Columns[5].Name = "Ngôn ngữ";
            dgvDanhSachSach.Columns[6].Name = "Mô tả";
            dgvDanhSachSach.Columns[7].Name = "Trạng thái";

            dgvDanhSachSach.Columns[0].Visible = false;

            foreach (var item in sachsv.FindName(name))
            {
                dgvDanhSachSach.Rows.Add(item.Id, STT++, item.MaSach, item.TieuDe, item.TacGia, item.NgonNgu, item.MoTa, item.TrangThai);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadSach(txtTimkiemS.Text);
        }

        public void LoadttSachct()
        {
            int index = 0;
            var result = from sct in sachctsv.GetAllSachctsv()
                         join tl in theloaisv.GetAllTheLoaisv() on sct.IdtheLoai equals tl.Id
                         join nxb in Nxbsv.GetAllNXBsv() on sct.Idnxb equals nxb.Id
                         join s in sachsv.GetAllSachsv() on sct.Idsach equals s.Id
                         select new
                         {
                             sct.Id,
                             STT = ++index,
                             sct.MaSachCt,
                             s.TieuDe,
                             sct.HinhAnh,
                             sct.SoLuong,
                             sct.Tap,
                             sct.SoTrang,
                             sct.GiaBan,
                             nxb.TenNxb,
                             tl.TenTheLoai,
                         };

            dgvDanhSachct.DataSource = result.ToList();
            dgvDanhSachct.Columns[0].Visible = false;
            dgvDanhSachct.Columns[1].HeaderText = "STT";
            dgvDanhSachct.Columns[2].HeaderText = "Mã sách";
            dgvDanhSachct.Columns[3].HeaderText = "Tiêu đề";
            dgvDanhSachct.Columns[4].HeaderText = "Hình ảnh";
            dgvDanhSachct.Columns[5].HeaderText = "Số lượng";
            dgvDanhSachct.Columns[6].HeaderText = "Tập";
            dgvDanhSachct.Columns[7].HeaderText = "Số trang";
            dgvDanhSachct.Columns[8].HeaderText = "Giá bán";
            dgvDanhSachct.Columns[9].HeaderText = "Tên nhà XB";
            dgvDanhSachct.Columns[10].HeaderText = "Tên thể loại";


            var loadtheloai = theloaisv.GetAllTheLoaisv().ToList();
            cboTheLoai.DataSource = loadtheloai;
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "Id";

            var loadnxb = Nxbsv.GetAllNXBsv().ToList();
            cboNxb.DataSource = loadnxb;
            cboNxb.DisplayMember = "TenNxb";
            cboNxb.ValueMember = "Id";

            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dgvDanhSachct.Columns[4];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SachChiTiet sachct = new SachChiTiet();
            byte[] imageBytes = File.ReadAllBytes(pathimg);
            sachct.HinhAnh = imageBytes;
            sachct.MaSachCt = txtMaSach.Text;
            sachct.SoLuong = int.Parse(txtSoLuong.Text);
            sachct.Tap = txtTap.Text;
            sachct.SoTrang = int.Parse(txtSotrang.Text);
            sachct.GiaBan = int.Parse(txtGia.Text);
            sachct.Idnxb = int.Parse(cboNxb.SelectedValue.ToString());
            sachct.IdtheLoai = int.Parse(cboTheLoai.SelectedValue.ToString());
            sachct.Idsach = int.Parse(textBoxID.Text);
            MessageBox.Show(sachctsv.Them(sachct));
            LoadttSachct();
        }
        private void txtMaSach_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaSach.Text))
            {
                var sach = sachsv.GetAllSachsv().FirstOrDefault(s => s.MaSach == txtMaSach.Text);

                if (sach != null)
                {
                    txtTieude.Text = sach.TieuDe;
                    textBoxID.Text = sach.Id.ToString();
                }
                else
                {
                    txtTieude.Text = "Not found";
                    textBoxID.Text = "";
                }
            }
            else
            {
                txtTieude.Text = "";
                textBoxID.Text = "";
            }
        }

        private void dgvDanhSachct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idwhenClick = int.Parse(dgvDanhSachct.CurrentRow.Cells[0].Value.ToString());
            txtMaSach.Text = dgvDanhSachct.CurrentRow.Cells[2].Value.ToString();
            txtTieude.Text = dgvDanhSachct.CurrentRow.Cells[3].Value.ToString();
            txtSoLuong.Text = dgvDanhSachct.CurrentRow.Cells[5].Value.ToString();
            txtTap.Text = dgvDanhSachct.CurrentRow.Cells[6].Value.ToString();
            txtSotrang.Text = dgvDanhSachct.CurrentRow.Cells[7].Value.ToString();
            txtGia.Text = dgvDanhSachct.CurrentRow.Cells[8].Value.ToString();
            cboNxb.Text = dgvDanhSachct.CurrentRow.Cells[9].Value.ToString();
            cboTheLoai.Text = dgvDanhSachct.CurrentRow.Cells[10].Value.ToString();

            var s = sachctsv.Findid(idwhenClick);
            if (s != null && s.HinhAnh != null)
            {
                byte[] imageData = s.HinhAnh;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureSach.Image = Image.FromStream(ms);
                    imgLoad = s.HinhAnh;
                }
            }
            else
            {
                pictureSach.Image = null;
                imgLoad = null;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SachChiTiet sachct = new SachChiTiet();
                if (pathimg != null)
                {
                    byte[] imageBytes = File.ReadAllBytes(pathimg);
                    sachct.HinhAnh = imageBytes;
                }
                else
                    sachct.HinhAnh = imgLoad;
                sachct.MaSachCt = txtMaSach.Text;
                sachct.SoLuong = int.Parse(txtSoLuong.Text);
                sachct.Tap = txtTap.Text;
                sachct.SoTrang = int.Parse(txtSotrang.Text);
                sachct.GiaBan = int.Parse(txtGia.Text);
                sachct.Idnxb = int.Parse(cboNxb.SelectedValue.ToString());
                sachct.IdtheLoai = int.Parse(cboTheLoai.SelectedValue.ToString());
                sachct.Idsach = int.Parse(textBoxID.Text);
                MessageBox.Show(sachctsv.Sua(sachct, idwhenClick));
                LoadttSachct();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSach.Text = "";
            txtTieude.Text = "";
            txtSoLuong.Text = "";
            txtTap.Text = "";
            txtSotrang.Text = "";
            txtGia.Text = "";
            cboNxb.SelectedIndex = -1;
            cboTheLoai.SelectedIndex = -1;
            pictureSach.Image = null;
        }

        private void btnQR_Click(object sender, EventArgs e)
        {
            QRCodeGenerator qrGenernator = new QRCodeGenerator();
            QRCodeData qrCodedata = qrGenernator.CreateQrCode(txtMaSach.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodedata);
            Bitmap qrcodeImg = qrcode.GetGraphic(20);
            pictureSach.Image = qrcodeImg;
        }

        private void btnLuuQr_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog savefliedialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
            {
                if (savefliedialog.ShowDialog() == DialogResult.OK)
                {
                    pictureSach.Image.Save(savefliedialog.FileName);
                    MessageBox.Show("Tệp đã lưu");
                }
            }
        }
        public void LoadttSachct(string name)
        {
            int index = 0;
            var result = from sct in sachctsv.GetAllSachctsv()
                         join tl in theloaisv.GetAllTheLoaisv() on sct.IdtheLoai equals tl.Id
                         join nxb in Nxbsv.GetAllNXBsv() on sct.Idnxb equals nxb.Id
                         join s in sachsv.FindName(name) on sct.Idsach equals s.Id
                         select new
                         {
                             sct.Id,
                             STT = ++index,
                             sct.MaSachCt,
                             s.TieuDe,
                             sct.HinhAnh,
                             sct.SoLuong,
                             sct.Tap,
                             sct.SoTrang,
                             sct.GiaBan,
                             nxb.TenNxb,
                             tl.TenTheLoai,
                         };

            dgvDanhSachct.DataSource = result.ToList();
            dgvDanhSachct.Columns[0].Visible = false;
            dgvDanhSachct.Columns[1].HeaderText = "STT";
            dgvDanhSachct.Columns[2].HeaderText = "Mã sách";
            dgvDanhSachct.Columns[3].HeaderText = "Tiêu đề";
            dgvDanhSachct.Columns[4].HeaderText = "Hình ảnh";
            dgvDanhSachct.Columns[5].HeaderText = "Số lượng";
            dgvDanhSachct.Columns[6].HeaderText = "Tập";
            dgvDanhSachct.Columns[7].HeaderText = "Số trang";
            dgvDanhSachct.Columns[8].HeaderText = "Giá bán";
            dgvDanhSachct.Columns[9].HeaderText = "Tên nhà XB";
            dgvDanhSachct.Columns[10].HeaderText = "Tên thể loại";


            var loadtheloai = theloaisv.GetAllTheLoaisv().ToList();
            cboTheLoai.DataSource = loadtheloai;
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "Id";

            var loadnxb = Nxbsv.GetAllNXBsv().ToList();
            cboNxb.DataSource = loadnxb;
            cboNxb.DisplayMember = "TenNxb";
            cboNxb.ValueMember = "Id";

            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dgvDanhSachct.Columns[4];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void txtTimKiemkh_TextChanged(object sender, EventArgs e)
        {
            LoadttSachct(txtTimKiemkh.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formtheloai formtheloai = new formtheloai();
            formtheloai.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void SanPham_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
