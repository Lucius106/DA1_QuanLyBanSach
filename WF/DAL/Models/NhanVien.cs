using System;
using System.Collections.Generic;

namespace WF.DAL.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int Id { get; set; }
        public string MaNv { get; set; } = null!;
        public string HoTenNv { get; set; } = null!;
        public byte[]? Hinh { get; set; }
        public string TenTk { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string? Email { get; set; }
        public string Cccd { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? Sđt { get; set; }
        public string TrangThai { get; set; } = null!;
        public string VaiTro { get; set; } = null!;

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
