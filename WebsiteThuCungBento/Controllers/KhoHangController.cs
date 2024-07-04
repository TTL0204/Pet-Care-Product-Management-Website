using System.Linq;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;
using System;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_KhoSanPham")]
    public class KhoHangController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: Kho Hang
        [AdminPhanQuyen(MaCN = "QL_KhachHang")]
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kho = from k in db.PhieuNhapKhos select k;
                return View(kho);
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kho = from k in db.PhieuNhapKhos where k.MaPhieuNK == id select k;
                return View(kho.Single());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(PhieuNhapKho kho)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
                db.PhieuNhapKhos.InsertOnSubmit(kho);
                SanPham sp = db.SanPhams.Single(n => n.MaSP == kho.MaSP);
                sp.SoLuong = sp.SoLuong + kho.SoLuong;
                db.SubmitChanges();
                return RedirectToAction("Index", "KhoHang");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                //var kho = from k in db.PhieuNhapKhos where k.MaPhieuNK == id select k;
                //ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
                var kho = db.PhieuNhapKhos.SingleOrDefault(n => n.MaPhieuNK == id);
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP", kho.MaSP);
                return View(kho);
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
                PhieuNhapKho kho = db.PhieuNhapKhos.SingleOrDefault(n => n.MaPhieuNK == id);
                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == kho.MaSP);
                var lst = from k in db.PhieuNhapKhos where k.MaSP == sp.MaSP select k;
                UpdateModel(kho);
                sp.SoLuong = 0;
                foreach (var item in lst)
                {
                    sp.SoLuong = sp.SoLuong + item.SoLuong;
                }
                db.SubmitChanges();
                return RedirectToAction("Index", "KhoHang");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kho = from k in db.PhieuNhapKhos where k.MaPhieuNK == id select k;
                return View(kho.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                PhieuNhapKho kho = db.PhieuNhapKhos.SingleOrDefault(n => n.MaPhieuNK == id);
                db.PhieuNhapKhos.DeleteOnSubmit(kho);
                SanPham sp = db.SanPhams.Single(n => n.MaSP == kho.MaSP);
                if (sp.SoLuong > kho.SoLuong)
                {
                    sp.SoLuong = sp.SoLuong - kho.SoLuong;
                }
                else
                {
                    return RedirectToAction("ThongBao", "KhoHang");
                }
                db.SubmitChanges();
                return RedirectToAction("Index", "KhoHang");
            }
        }

        public ActionResult ThongBao()
        {
            return View();
        }
    }
}