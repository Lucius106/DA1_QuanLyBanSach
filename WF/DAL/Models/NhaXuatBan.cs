using System;
using System.Collections.Generic;

namespace WF.DAL.Models
{
    public partial class NhaXuatBan
    {
        public NhaXuatBan()
        {
            SachChiTiets = new HashSet<SachChiTiet>();
        }

        public int Id { get; set; }
        public string MaNxb { get; set; } = null!;
        public string TenNxb { get; set; } = null!;
        public string? Sđt { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NamXb { get; set; }
        public string? TrangThai { get; set; }

        public virtual ICollection<SachChiTiet> SachChiTiets { get; set; }
    }
}
