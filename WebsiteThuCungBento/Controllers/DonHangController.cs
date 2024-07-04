using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_DonHang")]
    public class DonHangController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: QLDonDatHang
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DonDatHang()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                var hang = from h in db.DonHangs select h;
                return View(hang);
            }
        }

        public ActionResult ChiTietDonHang(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }    
            else
            { 
                var CTDH = (from c in db.CTDHs where c.MaDH == id select c).ToList();
                return View(CTDH);
            }
        }

        [HttpGet]
        public ActionResult XoaDonHang(int id)
        {
            //DonHang hang = db.DonHangs.SingleOrDefault(h => h.MaDH == id);
            db.sp_XoaDonHang(id);
            return RedirectToAction("DonDatHang", "DonHang");
        }

        [HttpGet]
        public ActionResult XoaMotCTDH(int id)
        {
            int MaDH = db.CTDHs.Where(c => c.MaSP == id).Select(c => c.MaDH).FirstOrDefault();
            db.sp_XoaMotCTDH(MaDH, id);
            return RedirectToAction("ChiTietDonHang", new { id =  MaDH});
        }
    }
}