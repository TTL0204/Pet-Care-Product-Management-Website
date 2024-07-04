using WebsiteThuCungBento.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThuCungBento.Controllers
{
    public class GioHangController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst == null)
            {
                lst = new List<GioHang>();
                Session["GioHang"] = lst;
            }
            return lst;
        }

        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            List<GioHang> lst = LayGioHang();
            GioHang sp = lst.Find(n => n.iMaSP == iMaSP);
            if (sp == null)
            {
                sp = new GioHang(iMaSP);
                lst.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.iSoLuong++;
                return Redirect(strURL);
            }
        }

        public ActionResult GioHang()
        {
            List<GioHang> lst = LayGioHang();
            if (lst.Count == 0)
            {
                return RedirectToAction("Index", "User");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lst);
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                iTongSoLuong = lst.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        private decimal TongTien()
        {
            decimal dTongTien = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                dTongTien = lst.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            List<GioHang> lst = LayGioHang();
            GioHang sp = lst.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang(int iMaSP)
        {
            List<GioHang> lst = LayGioHang();
            GioHang sp = lst.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sp != null)
            {
                lst.RemoveAll(n => n.iMaSP == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lst.Count == 0)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lst = LayGioHang();
            lst.Clear();
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        #region Thêm đơn đặt hàng mới
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            if (collection["NgayGiao"].Equals(""))
            {
                DateTime aDateTime = DateTime.Now;
                DateTime newTime = aDateTime.AddDays(7);
                dh.NgayGiao = newTime;
            }
            else
            {
                var NgayGiao = String.Format("{0:dd/MM/yyyy}", collection["NgayGiao"]);
                dh.NgayGiao = DateTime.Parse(NgayGiao);
            }

            dh.TinhTrang = false;
            int HTTH = int.Parse(collection["sl_ThanhToan"]);
            if (HTTH == 0)
                dh.DaThanhToan = false;
            else
                dh.DaThanhToan = true;

            dh.TongTien = (decimal)TongTien();
            db.DonHangs.InsertOnSubmit(dh);
            db.SubmitChanges();
            foreach (var item in gh)
            {
                SanPham sp = db.SanPhams.Single(n => n.MaSP == item.iMaSP);
                if (sp.SoLuong >= item.iSoLuong)
                {
                    CTDH ctdh = new CTDH();
                    ctdh.MaDH = dh.MaDH;
                    ctdh.MaSP = item.iMaSP;
                    ctdh.SoLuong = item.iSoLuong;
                    ctdh.DonGia = (int)item.dDonGia;
                    ctdh.ThanhTien = (decimal)item.dThanhTien;
                    db.CTDHs.InsertOnSubmit(ctdh);
                    sp.SoLuong = sp.SoLuong - item.iSoLuong;
                    db.SubmitChanges();
                    Session["GioHang"] = null;
                }
                else
                {
                    return RedirectToAction("ThongBao", "GioHang");
                }
            }
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        #endregion

        public ActionResult ThongBao()
        {
            return View();
        }
            
        public ActionResult XacNhanDonHang()
        {
            var dh = from d in db.DonHangs select d.NgayGiao;
            return View(dh);
        }

        #region Lấy hình thương hiệu
        public ActionResult HinhThuongHieu()
        {
            var lst = from ThuongHieu in db.ThuongHieus select ThuongHieu;
            return PartialView(lst);
        }
        #endregion

        public ActionResult XoaSanPham(int id)
        {
            List<GioHang> lst = LayGioHang();
            GioHang sp = lst.SingleOrDefault(n => n.iMaSP == id);
            if (sp != null)
            {
                lst.RemoveAll(n => n.iMaSP == id);
            }
            return RedirectToAction("ChiTietDonHang", "DonHang");
        }
    }
}