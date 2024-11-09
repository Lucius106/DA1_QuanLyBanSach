using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.DAL.Reposistoris;

namespace WF.DAL.Models
{
    public class TaikhoanService
    {
        taikhoanReposistoris Taikhoan = new taikhoanReposistoris();
        public bool Taikhoans(string taikhoan, string matkhau, out NhanVien user)
        {
            user = Taikhoan.TaiKhoan(taikhoan, matkhau);
            return user != null;
        }
    }
}
