using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThuCungBento.Models
{
    public class ProductViewModel
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal? DonGiaBan { get; set; }
        public string HinhAnh { get; set; }
        public int MaTH { get; set; }
        public int MaLoai { get; set; }
        public string TenTH { get; set; }
        public string TenLoai { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }
        public string TenMauSac { get; set; }
        public int TenKichThuoc { get; set; }
        public string Hinh1 { get; set; }
        public string Logo { get; set; }
        public string ThanhToanOn { get; set; }
        public int GiamGia { get; set; }
    }
}