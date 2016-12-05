using System;
using System.Collections.Generic;
using System.Text;

namespace QLHocSinhTHPT.Bussiness
{
    class KetQuaHocLucInfo
    {
        public string MaHocSinh { get; set; }
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public float DTBCaNam { get; set; }
        public string TenHocLuc { get; set; }
        public string TenHanhKiem { get; set; }

        public KetQuaHocLucInfo(string ma,string ten,bool gt,DateTime ns,float dtb,string hocluc,string hanhkiem)
        {
            MaHocSinh = ma;
            HoTen = ten;
            GioiTinh = gt;
            NgaySinh = ns;
            DTBCaNam = dtb;
            TenHocLuc = hocluc;
            TenHanhKiem = hanhkiem;
        }
    }
}
