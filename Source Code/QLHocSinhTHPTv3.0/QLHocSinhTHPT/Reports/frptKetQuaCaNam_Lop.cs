using System;
using System.Text;
using System.Drawing;
using QLHocSinhTHPT.Bussiness;
using QLHocSinhTHPT.Component;
using QLHocSinhTHPT.Controller;
using DevComponents.DotNetBar;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

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
            IList<KQCaNamTongHopInfo> ketqua = KQCaNamTongHopCtrl.LayDsKQCaNamTongHop(cmbLop.SelectedValue.ToString(),cmbNamHoc.SelectedValue.ToString());
            List<KetQuaHocLucInfo> lst = new List<KetQuaHocLucInfo>();
            foreach(KQCaNamTongHopInfo kq in ketqua)
            {
                lst.Add(new KetQuaHocLucInfo(kq.HocSinh.MaHocSinh, kq.HocSinh.HoTen, kq.HocSinh.GioiTinh,kq.HocSinh.NgaySinh, kq.DTBCaNam, kq.HocLuc.TenHocLuc,kq.HanhKiem.TenHanhKiem));
            }
            this.bSKQCNTH.DataSource = lst;
            gvHocLuc.DataSource = bSKQCNTH;
        }
        #endregion
    }
}