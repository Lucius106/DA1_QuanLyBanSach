using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.DAL.Models;

namespace WF.DAL.Reposistoris
{
    public class HoaDonCtReposistoris
    {
        QuanLyBanSachContext db = new QuanLyBanSachContext();
        public List<HoaDonCt> GetAllHoaDonct()
        {
            return db.HoaDonCts.ToList();
        }
        public bool Them(HoaDonCt hoadonct)
        {
            db.HoaDonCts.Add(hoadonct);
            db.SaveChanges();
            return true;
        }
        public bool Xoa(int id)
        {
            var XemayDelete = db.HoaDonCts.FirstOrDefault(x => x.Id == id);
            if (XemayDelete == null)
            {
                return false;
            }
            db.HoaDonCts.Remove(XemayDelete);
            db.SaveChanges();
            return true;
        }

    }
}
