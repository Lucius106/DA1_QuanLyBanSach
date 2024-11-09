using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.BLL.Service;
using WF.DAL.Models;
using WF.Form_Chức_Năng.Form_Chức_Năng___ADMIN;
using WF.GUI.View;
using ZXing;
using static QRCoder.PayloadGenerator;
using static QRCoder.PayloadGenerator.SwissQrCode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WF.Form_Chức_Năng.Form_Chức_Năng___NhanVien
{
    public partial class BanHang : Form
    {
        public BanHang()
        {
            InitializeComponent();
        }
        VoucherService vcsv;
        KhachHangService khachhangsv;
        NhanVienService nhanviensv;
        SachService sachsv;
        SachCtService sachctsv;
        HoaDonService hoadonsv;
        HoaDonCtService hoadonctsv;
        int idwhenClick = new int();
        private bool isDoubleClick = false;
        private void BanHang_Load(object sender, EventArgs e)
        {
            vcsv = new VoucherService();
            khachhangsv = new KhachHangService();
            nhanviensv = new NhanVienService();
            hoadonsv = new HoaDonService();
            sachsv = new SachService();
            sachctsv = new SachCtService();
            hoadonctsv = new HoaDonCtService();
            LoadSp();
            LoadGiamGia();
            LoadHD();
            camCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in camCollection)
            {
                cbocame.Items.Add(item.Name);
            }
            if (camCollection.Count > 0)
            {
                cbocame.SelectedIndex = 0;
            }
        }

        public void LoadSp()
        {
            int index = 0;
            var result = from sct in sachctsv.GetAllSachctsv()
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
                         };

            dgvDanhSachSP.DataSource = result.ToList();
            dgvDanhSachSP.Columns[0].Visible = false;
            dgvDanhSachSP.Columns[1].HeaderText = "STT";
            dgvDanhSachSP.Columns[2].HeaderText = "Mã sách";
            dgvDanhSachSP.Columns[3].HeaderText = "Tiêu đề";
            dgvDanhSachSP.Columns[4].HeaderText = "Hình ảnh";
            dgvDanhSachSP.Columns[5].HeaderText = "Số lượng";
            dgvDanhSachSP.Columns[6].HeaderText = "Tập";
            dgvDanhSachSP.Columns[7].HeaderText = "Số trang";
            dgvDanhSachSP.Columns[8].HeaderText = "Giá bán";

            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dgvDanhSachSP.Columns[4];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        public void LoadHD()
        {
            int STT = 1;
            dgvHoaDon.Rows.Clear();
            dgvHoaDon.ColumnCount = 8;
            dgvHoaDon.Columns[0].Name = "ID";
            dgvHoaDon.Columns[1].Name = "STT";
            dgvHoaDon.Columns[2].Name = "Mã HD";
            dgvHoaDon.Columns[3].Name = "Tên NV";
            dgvHoaDon.Columns[4].Name = "Tên KH";
            dgvHoaDon.Columns[5].Name = "Ngày tạo";
            dgvHoaDon.Columns[6].Name = "Trạng thái";
            dgvHoaDon.Columns[7].Name = "Mã KH";

            dgvHoaDon.Columns[7].Visible = false;
            dgvHoaDon.Columns[0].Visible = false;

            foreach (var item in hoadonsv.GetAllHoaDonrv())
            {
                var nv = nhanviensv.GetAllNhanViensv().FirstOrDefault(x => x.Id == item.Idnhanvien);
                var kh = khachhangsv.GetAllKhachHangsv().FirstOrDefault(x => x.Id == item.Idkhachhang);
                if (nv != null && kh != null && item.TrangThai == "Chưa thanh toán")
                {
                    dgvHoaDon.Rows.Add(item.Id, STT++, item.MaHd, nv.HoTenNv, kh.TenKhachHang, item.NgayMuaHang, item.TrangThai, kh.MaKhachHang);
                }
            }

            var loadkh = khachhangsv.GetAllKhachHangsv().ToList();
            cboid.DataSource = loadkh;
            cboid.ValueMember = "Id";

        }
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            string maHD;
            var allHoaDon = hoadonsv.GetAllHoaDonrv();
            int count = allHoaDon.Count + 1;
            maHD = "HĐ" + count;
            while (allHoaDon.Any(x => x.MaHd == maHD))
            {
                count++;
                maHD = "HĐ" + count;
            }

            hd.MaHd = maHD;
            hd.Idnhanvien = 1;
            hd.Idkhachhang = 4;
            hd.NgayMuaHang = DateTime.Now;
            hd.TrangThai = "Chưa thanh toán";
            hoadonsv.Them(hd);
            LoadHD();
            txtMaKh.Text = "";
            txtTenKH.Text = "";
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idwhenClick = int.Parse(dgvHoaDon.CurrentRow.Cells[0].Value.ToString());
            txtMaKh.Text = dgvHoaDon.CurrentRow.Cells[7].Value.ToString();
            txtTenKH.Text = dgvHoaDon.CurrentRow.Cells[4].Value.ToString();
            txtidmax.Text = dgvHoaDon.CurrentRow.Cells[0].Value.ToString();
        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            ThemKhachHang frm = new ThemKhachHang();
            frm.Show();
        }
        public void CapNhatThongTinKhachHang(string maKhachHang, string tenKhachHang, int id)
        {
            cboid.Text = id.ToString();
            txtMaKh.Text = maKhachHang;
            txtTenKH.Text = tenKhachHang;
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.Idkhachhang = int.Parse(cboid.SelectedValue.ToString());
            hoadonsv.Sua(hd, idwhenClick);
            LoadHD();
        }

        private void lbTienThua_TextChanged(object sender, EventArgs e)
        {
            double kt, tt;
            if (double.TryParse(txtTienKhachdua.Text, out kt) && double.TryParse(lbThanhToan.Text, out tt))
            {
                double tienthua = kt - tt;
                lbTienThua.Text = tienthua.ToString();
            }
        }
        private void TinhTongTien()
        {
            double tongTien = 0;
            foreach (DataGridViewRow row in dgvDsHoaDonChiTiet.Rows)
            {
                double thanhTien;
                if (row.Cells[7].Value != null && double.TryParse(row.Cells[7].Value.ToString(), out thanhTien))
                {
                    tongTien += thanhTien;
                }
                lbTongTien.Text = tongTien.ToString();
                LoadGiamGia();
            }

        }
        public void Loadhdct()
        {
            int STT = 1;
            dgvDsHoaDonChiTiet.Rows.Clear();
            dgvDsHoaDonChiTiet.ColumnCount = 9;
            dgvDsHoaDonChiTiet.Columns[0].Name = "ID";
            dgvDsHoaDonChiTiet.Columns[1].Name = "STT";
            dgvDsHoaDonChiTiet.Columns[2].Name = "MaHDCT";
            dgvDsHoaDonChiTiet.Columns[3].Name = "MaSP";
            dgvDsHoaDonChiTiet.Columns[4].Name = "Tên sách";
            dgvDsHoaDonChiTiet.Columns[5].Name = "Số Lượng";
            dgvDsHoaDonChiTiet.Columns[6].Name = "Giá ";
            dgvDsHoaDonChiTiet.Columns[7].Name = "Thành Tiền";
            dgvDsHoaDonChiTiet.Columns[8].Name = "IDHD";
            dgvDsHoaDonChiTiet.Columns[0].Visible = false;
            dgvDsHoaDonChiTiet.Columns[8].Visible = false;

            foreach (var item in hoaDonCts)
            {
                dgvDsHoaDonChiTiet.Rows.Add(item.Id, STT++, item.MaHdct, item.MaSpct, item.TenSp, item.SoLuongMua, item.GiaBan, item.ThanhTien, int.Parse(txtidmax.Text));
            }
            TinhTongTien();
        }

        private void txtTienKhachdua_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtTienKhachdua.Text, out double tienKhachDua))
            {
                double tongTienSauGiam = Convert.ToDouble(lbThanhToan.Text);
                double tienThua = Math.Max(tienKhachDua - tongTienSauGiam, 0);
                lbTienThua.Text = tienThua.ToString();
            }
            else
            {
                lbTienThua.Text = "0";
            }
        }
        private void dgvDsHoaDonChiTiet_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDsHoaDonChiTiet.Columns[7].Index)
            {
                TinhTongTien();
            }
            else if (e.ColumnIndex == dgvDsHoaDonChiTiet.Columns[5].Index)
            {
                int sl = Convert.ToInt32(dgvDsHoaDonChiTiet.Rows[e.RowIndex].Cells[5].Value);
                double gia = Convert.ToDouble(dgvDsHoaDonChiTiet.Rows[e.RowIndex].Cells[6].Value);
                dgvDsHoaDonChiTiet.Rows[e.RowIndex].Cells[7].Value = sl * gia;
                TinhTongTien();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            int mahd = int.Parse(txtmahd.Text);
            var removedItem = hoadonctsv.GetAllHoaDonCtsv().FirstOrDefault(x => x.Id == mahd);
            int removedQuantity = removedItem != null ? removedItem.SoLuongMua : 0;
            var sachct = sachctsv.GetAllSachctsv().Find(x => x.MaSachCt == txtMas.Text);
            int slcon = sachct.SoLuong;
            int newSL = slcon + removedQuantity;
            string mess = hoadonctsv.Xoa(mahd);
            MessageBox.Show(mess, "Thông báo");
            sachctsv.updateSL(txtMas.Text, newSL);
            LoadSp();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDsHoaDonChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm sản phẩm vào hóa đơn trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                HoaDon hd = new HoaDon();
                hd.TrangThai = "Đã thanh toán";
                hoadonsv.suatt(hd, int.Parse(txtidmax.Text));
                hoaDonCts.Clear();
                dgvDsHoaDonChiTiet.Rows.Clear();
                LoadHD();
                lbTongTien.Text = "";
                cboGiamGia.Text = "";
                txttiengiam.Text = "";
                lbThanhToan.Text = "";
                txtTienKhachdua.Text = "";
                lbTienThua.Text = "";
                txtGhiChu.Text = "";
                txtMaKh.Text = "";
                txtTenKH.Text = "";
                MessageBox.Show("Thanh toán thành công");

                if (cboGiamGia.SelectedItem != null)
                {
                    KeyValuePair<string, string> selectedItem = (KeyValuePair<string, string>)cboGiamGia.SelectedItem;

                    // Lấy giá trị key và value của mục đã chọn
                    string maGiamGia = selectedItem.Key;
                    var voucher = vcsv.GetAllVouchersv().FirstOrDefault(x => x.MaVoucher == maGiamGia);
                    if (voucher != null)
                    {
                        // Trừ số lượng của mã giảm giá đó đi 1
                        if (voucher.SoLuong > 0)
                        {
                            voucher.SoLuong--;
                            vcsv.UpdateMaGiamGia(voucher);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã giảm giá không tồn tại.");
                    }
                }
                else
                {
                    // Thông báo nếu không có mục nào được chọn
                    MessageBox.Show("Vui lòng chọn một mục từ ComboBox.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void cboGiamGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGiamGia.SelectedItem != null && cboGiamGia.SelectedItem is KeyValuePair<string, string> selectedItem)
            {
                string maGiamGia = selectedItem.Key;
                double tongTien;
                if (double.TryParse(lbTongTien.Text, out tongTien))
                {
                    double tiLeGiamGia = vcsv.GetTiLeGiamGia(maGiamGia);
                    double giamToiDa = vcsv.GetGiamToiDa(maGiamGia); // Lấy giá trị GiamToiDa từ mã giảm giá

                    if (tiLeGiamGia > 0)
                    {
                        double tienGiam = tongTien * (tiLeGiamGia / 100.0);

                        // Nếu số tiền giảm vượt quá số tiền giảm tối đa, chỉ sử dụng số tiền giảm tối đa
                        if (tienGiam > giamToiDa)
                        {
                            tienGiam = giamToiDa;
                        }

                        double tongTienSauGiam = tongTien - tienGiam;

                        txttiengiam.Text = tienGiam.ToString();
                        lbThanhToan.Text = tongTienSauGiam.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi: Không thể chuyển đổi tổng tiền thành số.");
                }
            }
        }
        public void LoadGiamGia()
        {
            cboGiamGia.Items.Clear();
            foreach (var magiamgia in vcsv.GetAllVouchersv())
            {
                if (magiamgia.NgayKetThuc > DateTime.Now && magiamgia.DonHangToiThieu <= GetTongTien())
                {
                    string displayText = $"{magiamgia.TiLeGiam}% - {magiamgia.GiamToiDa}đ";
                    cboGiamGia.Items.Add(new KeyValuePair<string, string>(magiamgia.MaVoucher, displayText));
                }
            }
        }
        private double GetTongTien()
        {
            double tongTien;
            if (double.TryParse(lbTongTien.Text, out tongTien))
            {
                return tongTien;
            }
            return 0;
        }
        private void dgvDanhSachSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDanhSachSP.Rows[e.RowIndex];
                string mahdct;
                var allHoaDonct = hoadonctsv.GetAllHoaDonCtsv();
                int count = allHoaDonct.Count + 1;
                mahdct = "HDCT" + count;
                while (allHoaDonct.Any(x => x.MaHdct == mahdct))
                {
                    count++;
                    mahdct = "HDCT" + count;
                }
                string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập số lượng:", "Nhập số lượng");
                int soLuong;
                if (!int.TryParse(input, out soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng lớn hơn 0");
                    return;
                }
                int soluongkho = int.Parse(selectedRow.Cells[5].Value.ToString());
                if (soLuong > soluongkho)
                {
                    MessageBox.Show("Số lượng không đủ trong kho");
                    return;
                }
                int IdSp = int.Parse(selectedRow.Cells[0].Value.ToString());
                int tong = soluongkho - soLuong;
                SachChiTiet sachct = new SachChiTiet();
                sachct.SoLuong = tong;

                var success = sachctsv.UpdateSP(IdSp, sachct);
                if (success != null)
                {
                    selectedRow.Cells[5].Value = tong;
                }

                HoaDonCt hdct = new HoaDonCt();
                hdct.MaHdct = mahdct;
                hdct.MaSpct = selectedRow.Cells[2].Value.ToString();
                hdct.TenSp = selectedRow.Cells[3].Value.ToString();
                hdct.SoLuongMua = soLuong;
                hdct.GiaBan = double.Parse(selectedRow.Cells[8].Value.ToString());
                hdct.ThanhTien = hdct.GiaBan * hdct.SoLuongMua;
                hdct.IdhoaDon = int.Parse(txtidmax.Text);
                hoadonctsv.Them(hdct);


                var existingHdct = hoaDonCts.FirstOrDefault(x => x.MaSpct == hdct.MaSpct);
                if (existingHdct != null)
                {
                    existingHdct.SoLuongMua++;
                    existingHdct.ThanhTien = existingHdct.GiaBan * existingHdct.SoLuongMua;
                    
                }
                else
                {
                    hoaDonCts.Add(hdct);
                }
                LoadSp();
                Loadhdct();
            }
        }

        private void dgvDsHoaDonChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmahd.Text = dgvDsHoaDonChiTiet.CurrentRow.Cells[0].Value.ToString();
            txtMas.Text = dgvDsHoaDonChiTiet.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lưu hóa đơn thành cônng");
            hoaDonCts.Clear();
            dgvDsHoaDonChiTiet.Rows.Clear();
            LoadHD();
            lbTongTien.Text = "";
            cboGiamGia.Text = "";
            txttiengiam.Text = "";
            lbThanhToan.Text = "";
            txtTienKhachdua.Text = "";
            lbTienThua.Text = "";
            txtGhiChu.Text = "";
            txtMaKh.Text = "";
            txtTenKH.Text = "";
        }
        public void LoadSp(string name)
        {
            int index = 0;
            var result = sachsv.FindName(txtTimKiem.Text)
                               .Join(sachctsv.GetAllSachctsv(),
                                     s => s.Id,
                                     sct => sct.Idsach,
                                     (s, sct) => new
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
                                     })
                               .ToList();

            dgvDanhSachSP.DataSource = result;
            dgvDanhSachSP.Columns[0].Visible = false;
            dgvDanhSachSP.Columns[1].HeaderText = "STT";
            dgvDanhSachSP.Columns[2].HeaderText = "Mã sách";
            dgvDanhSachSP.Columns[3].HeaderText = "Tiêu đề";
            dgvDanhSachSP.Columns[4].HeaderText = "Hình ảnh";
            dgvDanhSachSP.Columns[5].HeaderText = "Số lượng";
            dgvDanhSachSP.Columns[6].HeaderText = "Tập";
            dgvDanhSachSP.Columns[7].HeaderText = "Số trang";
            dgvDanhSachSP.Columns[8].HeaderText = "Giá bán";

            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dgvDanhSachSP.Columns[4];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadSp(txtTimKiem.Text);
        }
        List<HoaDonCt> hoaDonCts = new List<HoaDonCt>();
        public void Loadhdct(int selectedHoaDonId)
        {
            int STT = 1;
            dgvDsHoaDonChiTiet.Rows.Clear();
            dgvDsHoaDonChiTiet.ColumnCount = 9;
            dgvDsHoaDonChiTiet.Columns[0].Name = "ID";
            dgvDsHoaDonChiTiet.Columns[1].Name = "STT";
            dgvDsHoaDonChiTiet.Columns[2].Name = "MaHDCT";
            dgvDsHoaDonChiTiet.Columns[3].Name = "MaSP";
            dgvDsHoaDonChiTiet.Columns[4].Name = "Tên sách";
            dgvDsHoaDonChiTiet.Columns[5].Name = "Số Lượng";
            dgvDsHoaDonChiTiet.Columns[6].Name = "Giá ";
            dgvDsHoaDonChiTiet.Columns[7].Name = "Thành Tiền";
            dgvDsHoaDonChiTiet.Columns[8].Name = "IDHD";
            dgvDsHoaDonChiTiet.Columns[0].Visible = false;
            dgvDsHoaDonChiTiet.Columns[8].Visible = false;

            var result = from hdct in hoadonctsv.GetAllHoaDonCtsv()
                         join hd in hoadonsv.GetAllHoaDonrv() on hdct.IdhoaDon equals hd.Id
                         where hdct.IdhoaDon == selectedHoaDonId
                         select new
                         {
                             hdct.Id,
                             STT = ++STT,
                             hdct.MaHdct,
                             hdct.MaSpct,
                             hdct.TenSp,
                             hdct.SoLuongMua,
                             hdct.GiaBan,
                             hdct.ThanhTien,
                             hdct.IdhoaDon
                         };

            foreach (var item in result)
            {
                dgvDsHoaDonChiTiet.Rows.Add(item.Id, item.STT, item.MaHdct, item.MaSpct, item.TenSp, item.SoLuongMua, item.GiaBan, item.ThanhTien, item.IdhoaDon);
            }
            TinhTongTien();
        }
        private void dgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string idhd = dgvHoaDon.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtidmax.Text = idhd;
                int selectedHoaDonId = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells[0].Value);
                Loadhdct(selectedHoaDonId);
            }
        }
        FilterInfoCollection camCollection;
        VideoCaptureDevice videoCaptureDevice;
        bool scanning = false;
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (scanning)
            {
                pictureQR.Image = (Bitmap)eventArgs.Frame.Clone();
            }
        }

        private void BanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (scanning && pictureQR.Image != null)
            {
                BarcodeReader reader = new BarcodeReader();
                Result result = reader.Decode((Bitmap)pictureQR.Image);
                if (result != null)
                {
                    string decoded = result.Text.Trim();
                    MessageBox.Show(decoded, "Kết quả");
                    timer1.Stop(); // Dừng timer sau khi quét thành công
                    scanning = false; // Đặt biến scanning về false để ngăn quét tiếp
                }
            }
        }

        private void btnstart_Click_1(object sender, EventArgs e)
        {
            if (!scanning)
            {
                videoCaptureDevice = new VideoCaptureDevice(camCollection[cbocame.SelectedIndex].MonikerString);
                videoCaptureDevice.NewFrame += new AForge.Video.NewFrameEventHandler(VideoCaptureDevice_NewFrame);
                videoCaptureDevice.Start();
                scanning = true;
                timer1.Start();
            }
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoCaptureDevice != null)
                {
                    if (videoCaptureDevice.IsRunning)
                    {
                        videoCaptureDevice.SignalToStop();
                        videoCaptureDevice.WaitForStop();
                        pictureQR.Image = null;
                        videoCaptureDevice = null;
                        scanning = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi dừng camera: " + ex.Message);
            }
        }
    }
}
