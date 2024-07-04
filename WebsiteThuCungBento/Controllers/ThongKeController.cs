using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    public class ThongKeController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: ThongKe
        public ActionResult Index(int? maNV, int? maSP, int? thang, int? nam, int? maLoai)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.TongDoanhThu = ThongKeTongDoanhThu();
                ViewBag.TongDonHang = ThongKeDonHang();
                ViewBag.TongSanPham = ThongKeSanPham();
                ViewBag.TongKhachHang = ThongKeKhachHang();
                ViewBag.TongAdmin = ThongKeNhanVien();

                var spBanChay = LayDanhSachSanPhamBanChayNhat();
                ViewBag.SPBanChay = spBanChay;
                ViewBag.Labels = string.Join(",", spBanChay.Select(x => $"\"{x.TenSP}\""));
                ViewBag.Data = string.Join(",", spBanChay.Select(x => x.SoLuongDaBan));

                var dsDonHang = db.sp_ListDSDonHang().ToList();
                ViewBag.DSDonHang = dsDonHang;

                ViewBag.DSNV = db.NhanViens.ToList();
                if (maNV.HasValue)
                {
                    ViewBag.DoanhThuNhanVien = ThongKeDoanhThuNhanVien(maNV.Value);
                }

                ViewBag.DSMH = db.SanPhams.ToList();
                if (maSP.HasValue)
                {
                    ViewBag.DoanhThuMatHang = ThongKeDoanhThuMatHang(maSP.Value);
                }

                if (thang.HasValue && nam.HasValue)
                {
                    ViewBag.DoanhThuThang = ThongKeDoanhThuThang(thang.Value, nam.Value);
                }

                ViewBag.DSLoai = db.Loais.ToList();
                if (maLoai.HasValue)
                {
                    ViewBag.DoanhThuLoaiHang = ThongKeDoanhThuLoaiHang(maLoai.Value);
                }
            } 
            return View();
        }

        public ActionResult GetDoanhThuNhanVien(int maNV)
        {
            var doanhThu = ThongKeDoanhThuNhanVien(maNV);
            return Content(String.Format("{0:#,##0 đ}", doanhThu));
        }

        public ActionResult GetDoanhThuThang(int? thang, int? nam)
        {
            if (thang.HasValue && nam.HasValue)
            {
                var doanhThu = ThongKeDoanhThuThang(thang.Value, nam.Value);
                return Content(String.Format("{0:#,##0 đ}", doanhThu));
            }
            else
            {
                return Content("0 đ");
            }
        }

        public ActionResult GetDoanhThuLoaiHang(int maLoai)
        {
            var doanhThu = ThongKeDoanhThuLoaiHang(maLoai);
            return Content(String.Format("{0:#,##0 đ}", doanhThu));
        }

        public int ThongKeDonHang()
        {
            //int sl = db.DonHangs.Count();
            var sl = db.fn_ThongKeDonHang();
            return (int)sl;
        }

        public int ThongKeSanPham()
        {
            //int sl = db.SanPhams.Count();
            var sl = db.fn_ThongKeSanPham();
            return (int)sl;
        }

        public int ThongKeKhachHang()
        {
            //int sl = db.KhachHangs.Count();
            var sl = db.fn_ThongKeKhachHang();
            return (int)sl;
        }

        public int ThongKeNhanVien()
        {
            //int sl = db.NhanViens.Count();
            var sl = db.fn_ThongKeNhanVien();
            return (int)sl;
        }

        public decimal ThongKeTongDoanhThu()
        {
            //decimal TongDoanhThu = decimal.Parse(db.CTDHs.Sum(n => n.SoLuong * n.DonGia).ToString());
            var TongDoanhThu = db.fn_ThongKeDoanhThu();
            return (decimal)TongDoanhThu;
        }

        public decimal ThongKeDoanhThuThang(int Thang, int Nam)
        {
            //var lst = db.DonHangs.Where(n => n.NgayDat.Month == Thang && n.NgayDat.Year == Nam);
            //decimal TongTien = 0;
            //foreach(var item in lst)
            //{
            //    TongTien += decimal.Parse(item.CTDHs.Sum(n => n.SoLuong * n.DonGia).ToString());
            //}
            var TongTien = db.fn_ThongKeDoanhThuThang(Thang, Nam);
            return (decimal)TongTien;
        }

        public ActionResult GetDoanhThuMatHang(int maSP)
        {
            var doanhThu = ThongKeDoanhThuMatHang(maSP);
            return Content(String.Format("{0:#,##0 đ}", doanhThu));
        }

        public List<SPBanChay> LayDanhSachSanPhamBanChayNhat()
        {
            var result = db.sp_ListSPBanChayNhat().Select(x => new SPBanChay
            {
                TenSP = x.TenSP,
                SoLuongDaBan = (int)x.SoLuongDaBan
            }).ToList();
            return result;
        }

        public decimal ThongKeDoanhThuMatHang(int id)
        {
            var TongTien = db.fn_ThongKeDoanhThuMatHang(id);
            return (decimal)TongTien;
        }

        public decimal ThongKeDoanhThuNhanVien(int id)
        {
            var TongTien = db.fn_ThongKeDoanhThuNhanVien(id);
            return(decimal)TongTien;
        }

        public decimal ThongKeDoanhThuLoaiHang(int id)
        {
            var TongTien = db.fn_ThongKeDoanhThuLoaiHang(id);
            return(decimal)TongTien;
        }
    }
}