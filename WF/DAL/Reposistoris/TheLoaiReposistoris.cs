﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.DAL.Models;

namespace WF.DAL.Reposistoris
{
    public class TheLoaiReposistoris
    {
        QuanLyBanSachContext db = new QuanLyBanSachContext();
        public List<TheLoai> GetAllTheLoaistr()
        {
            return db.TheLoais.ToList();
        }

        public bool Them(TheLoai tl)
        {
            db.TheLoais.Add(tl);
            db.SaveChanges();
            return true;    
        }

        public bool Sua(TheLoai tl , int id)
        {
            var sua = db.TheLoais.FirstOrDefault(x => x.Id == id);
            sua.MaTheLoai = tl.MaTheLoai;
            sua.TenTheLoai = tl.TenTheLoai;
            db.TheLoais.Update(sua);
            db.SaveChanges();
            return true;
        }
        public List<TheLoai> FindName(string name)
        {
            return db.TheLoais.Where(x => x.TenTheLoai.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}