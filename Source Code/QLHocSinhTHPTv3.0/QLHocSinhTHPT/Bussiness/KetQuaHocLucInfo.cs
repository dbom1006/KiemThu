using System;
using System.Collections.Generic;
using System.Text;

namespace QLHocSinhTHPT.Bussiness
{
    class KetQuaHocLucInfo
    {
        public string MaHocSinh { get; set; }
        public string HoTen { get; set; }
        public float DTBCaNam { get; set; }
        public string TenHocLuc { get; set; }

        public KetQuaHocLucInfo(string ma,string ten,float dtb,string hocluc)
        {
            MaHocSinh = ma;
            HoTen = ten;
            DTBCaNam = dtb;
            TenHocLuc = hocluc;
        }
    }
}
