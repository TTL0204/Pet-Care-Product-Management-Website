using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThuCungBento.Models
{
    public class GioHang
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        public int iMaSP { set; get; }
        public string sTenSP { set; get; }
        public string sHinhAnh { set; get; }
        public decimal dDonGia { set; get; }
        public int iSoLuong { set; get; }
        public decimal dThanhTien
        {
            get { return iSoLuong * dDonGia; }

        }
        public decimal dTongTien { set; get; }
        public GioHang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sanpham = db.SanPhams.Single(n => n.MaSP == iMaSP);
            sTenSP = sanpham.TenSP;
            sHinhAnh = sanpham.HinhAnh;
            dDonGia = decimal.Parse(sanpham.DonGiaBan.ToString());
            iSoLuong = 1;
        }
    }
}