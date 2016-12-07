using System;
using System.Text;
using System.Drawing;
using QLHocSinhTHPT.Bussiness;
using QLHocSinhTHPT.Component;
using QLHocSinhTHPT.Controller;
using DevComponents.DotNetBar;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using QLHocSinhTHPT.DataLayer;
using System.Data;

namespace QLHocSinhTHPT.Reports
{
    public partial class frptKetQuaCaNam_Lop : Office2007Form
    {
        #region Fields
        NamHocCtrl  m_NamHocCtrl    = new NamHocCtrl();
        LopCtrl     m_LopCtrl       = new LopCtrl();
        #endregion

        #region Constructor
        public frptKetQuaCaNam_Lop()
        {
            InitializeComponent();
            DataService.OpenConnection();
        }
        #endregion

        #region Load
        private void frptKetQuaCaNam_Lop_Load(object sender, EventArgs e)
        {
            m_NamHocCtrl.HienThiComboBox(cmbNamHoc);
            if (cmbNamHoc.SelectedValue != null)
                m_LopCtrl.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
        }
        #endregion

        #region SelectedIndexChanged event
        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                m_LopCtrl.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }
        #endregion

        #region Click event
        private void btnXem_Click(object sender, EventArgs e)
        {
            DanhGiaHocLucTheoNam();
            //IList<KQCaNamTongHopInfo> ketqua = KQCaNamTongHopCtrl.LayDsKQCaNamTongHop(cmbLop.SelectedValue.ToString(),cmbNamHoc.SelectedValue.ToString());
            //List<KetQuaHocLucInfo> lst = new List<KetQuaHocLucInfo>();
            //foreach(KQCaNamTongHopInfo kq in ketqua)
            //{
            //    lst.Add(new KetQuaHocLucInfo(kq.HocSinh.MaHocSinh, kq.HocSinh.HoTen, kq.DTBCaNam, kq.HocLuc.TenHocLuc));
            //}
            //this.bSKQCNTH.DataSource = lst;
            //gvHocLuc.DataSource = bSKQCNTH;
        }
        private void DanhGiaHocLucTheoNam()
        {
            List<KetQuaHocLucInfo> lstKQ = new List<KetQuaHocLucInfo>();
            //Khởi tạo các đối tượng get dữ liệu
            var maLop = cmbLop.SelectedValue.ToString();
            var namHoc = cmbNamHoc.SelectedValue.ToString();
            String[] HocLuc = { "Giỏi", "Khá", "Trung Bình", "Yếu", "Kém" };
            float[] diemCanDuoi = { 8, 6.5f, 5, 3.5f, 0};
            HocSinhData m_HocSinhData = new HocSinhData();
            MonHocData m_MonHocData = new MonHocData();
            DiemCtrl m_DiemCtrl = new DiemCtrl();
            DataTable m_DT = m_MonHocData.LayDsMonHoc(namHoc, maLop);
            List<HocSinhInfo> lstHS = m_HocSinhData.LayDSHocSinhTheoLop(namHoc, maLop);
            //Tính điểm TB và xếp loại học lực cho mỗi học sinh trong lớp
            foreach (HocSinhInfo hs in lstHS)
            {
                //Khởi tạo các biến điểm cho mỗi học sinh
                float tongDiem = 0;
                float tongDiemCacMon = 0;
                float diemTBTungMon = 0;
                int tongHeSoCacMon = 0;
                String xepLoai = "";
                float[] arrayDiemTBTungMon = new float[m_DT.Rows.Count];
                int soMonHoc = 0;
                //Tính điểm trung bình
                foreach (DataRow row in m_DT.Rows){
                    diemTBTungMon = m_DiemCtrl.DiemTrungBinhMonCaNam(hs.MaHocSinh, row["MaMonHoc"].ToString(), namHoc, maLop);
                    arrayDiemTBTungMon[soMonHoc++] = diemTBTungMon;
                    tongDiemCacMon += diemTBTungMon * Convert.ToInt32(row["HeSo"].ToString());
                    tongHeSoCacMon += Convert.ToInt32(row["HeSo"].ToString());
                }
                if (tongHeSoCacMon > 0) tongDiem = tongDiemCacMon / tongHeSoCacMon;
                else    tongDiem = 0;
                //Xếp loại học lực
                //Tìm điểm TB Môn nhỏ nhất
                float diemTBMonNhoNhat = arrayDiemTBTungMon[0];
                for (int i = 1; i < arrayDiemTBTungMon.Length - 1; i++)
                    diemTBMonNhoNhat = Math.Min(diemTBMonNhoNhat, arrayDiemTBTungMon[i]);
                //Xét học lực theo điểm TB và điểm TB môn nhỏ nhất
                for (int i = 0; i < 4; i++){
                    if (tongDiem >= diemCanDuoi[i] && diemTBMonNhoNhat >= diemCanDuoi[i + 1]){
                        xepLoai = HocLuc[i];
                        break;
                    }
                }
                if (xepLoai == "")  xepLoai = HocLuc[4].ToString();
                lstKQ.Add(new KetQuaHocLucInfo(hs.MaHocSinh, hs.HoTen, tongDiem, xepLoai));
            }
            //Hiển thị ra gridview
            bSKQCNTH.DataSource = lstKQ;
            gvHocLuc.DataSource = bSKQCNTH;
        }
        #endregion
    }
}