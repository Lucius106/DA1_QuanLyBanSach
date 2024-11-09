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

namespace WF.Form_Chức_Năng.Form_Chức_Năng___NhanVien
{
    public partial class DonHang : Form
    {
        public DonHang()
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
        private void DonHang_Load(object sender, EventArgs e)
        {
            vcsv = new VoucherService();
            khachhangsv = new KhachHangService();
            nhanviensv = new NhanVienService();
            hoadonsv = new HoaDonService();
            sachsv = new SachService();
            sachctsv = new SachCtService();
            hoadonctsv = new HoaDonCtService();
            LoadHD();
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
                if (nv != null && kh != null && item.TrangThai == "Đã thanh toán")
                {
                    dgvHoaDon.Rows.Add(item.Id, STT++, item.MaHd, nv.HoTenNv, kh.TenKhachHang, item.NgayMuaHang, item.TrangThai, kh.MaKhachHang);
                }
            }
        }

        private void dgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                int index = 0;
                var selectedHoaDonId = int.Parse(dgvHoaDon.Rows[rowIndex].Cells[0].Value.ToString());
                var result = from hdct in hoadonctsv.GetAllHoaDonCtsv()
                             join hd in hoadonsv.GetAllHoaDonrv() on hdct.IdhoaDon equals hd.Id
                             where hdct.IdhoaDon == selectedHoaDonId
                             select new
                             {
                                 hdct.Id,
                                 STT = ++index,
                                 hdct.MaHdct,
                                 hdct.MaSpct,
                                 hdct.TenSp,
                                 hdct.SoLuongMua,
                                 hdct.GiaBan,
                                 hdct.ThanhTien,
                                 hdct.IdhoaDon
                             };

                dgvHoaDonChiTiet.DataSource = result.ToList();
                dgvHoaDonChiTiet.Columns[0].Visible = false;
                dgvHoaDonChiTiet.Columns[1].HeaderText = "STT";
                dgvHoaDonChiTiet.Columns[2].HeaderText = "MaHDCT";
                dgvHoaDonChiTiet.Columns[3].HeaderText = "MaSP";
                dgvHoaDonChiTiet.Columns[4].HeaderText = "Tên sách";
                dgvHoaDonChiTiet.Columns[5].HeaderText = "Số Lượng";
                dgvHoaDonChiTiet.Columns[6].HeaderText = "Giá ";
                dgvHoaDonChiTiet.Columns[7].HeaderText = "Thành Tiền";
                dgvHoaDonChiTiet.Columns[8].HeaderText = "IDHD";
                dgvHoaDonChiTiet.Columns[8].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dtptungay.Value.Date;
            DateTime ngayKetThuc = dtpdenngay.Value.Date.AddDays(1).AddSeconds(-1);
            LoadHD(ngayBatDau, ngayKetThuc);
        }
        public void LoadHD(DateTime ngayBatDau, DateTime ngayKetThuc)
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

            var query = from hd in hoadonsv.GetAllHoaDonrv()
                        join nv in nhanviensv.GetAllNhanViensv() on hd.Idnhanvien equals nv.Id
                        join kh in khachhangsv.GetAllKhachHangsv() on hd.Idkhachhang equals kh.Id
                        where hd.TrangThai == "Đã thanh toán" && hd.NgayMuaHang >= ngayBatDau && hd.NgayMuaHang <= ngayKetThuc
                        select new
                        {
                            hd.Id,
                            hd.MaHd,
                            nv.HoTenNv,
                            kh.TenKhachHang,
                            hd.NgayMuaHang,
                            hd.TrangThai,
                            kh.MaKhachHang
                        };

            foreach (var item in query)
            {
                dgvHoaDon.Rows.Add(item.Id, STT++, item.MaHd, item.HoTenNv, item.TenKhachHang, item.NgayMuaHang, item.TrangThai, item.MaKhachHang);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadHD();
        }
    }
}
