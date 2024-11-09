using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WF.DAL.Models;

namespace WF.DAL.Reposistoris
{
    public class SachReposistoris
    {
        QuanLyBanSachContext db = new QuanLyBanSachContext();
        public List<Sach> GetAllSachstr()
        {
            return db.Saches.ToList();
        }
        public bool Them(Sach sach)
        {
            db.Saches.Add(sach); 
            db.SaveChanges();
            return true;
        }

        public bool Sua(Sach sach , int id)
        {
            var sua = db.Saches.FirstOrDefault(s => s.Id == id);
            sua.MaSach = sach.MaSach;
            sua.TieuDe = sach.TieuDe;
            sua.MoTa = sach.MoTa;
            sua.TrangThai = sach.TrangThai;
            sua.NgonNgu = sach.NgonNgu;
            sua.TacGia = sach.TacGia;
            db.Saches.Update(sua);
            db.SaveChanges();
            return true;
        }

        public List<Sach> FindName(string name)
        {
            return db.Saches.Where(x => x.TieuDe.ToLower().Contains(name.ToLower())).ToList();  
        }
    }
}
