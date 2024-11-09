using System;
using System.Collections.Generic;

namespace WF.DAL.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            SachChiTiets = new HashSet<SachChiTiet>();
        }

        public int Id { get; set; }
        public string MaTheLoai { get; set; } = null!;
        public string TenTheLoai { get; set; } = null!;

        public virtual ICollection<SachChiTiet> SachChiTiets { get; set; }
    }
}
