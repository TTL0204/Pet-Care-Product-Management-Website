using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_LoaiSanPham")]
    public class LoaiController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: Loai
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from l in db.Loais select l;
                return View(loai);
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from l in db.Loais where l.MaLoai == id select l;
                return View(loai.Single());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(Loai loai)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                db.Loais.InsertOnSubmit(loai);
                db.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from l in db.Loais where l.MaLoai == id select l;
                return View(loai.Single());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                Loai loai = db.Loais.SingleOrDefault(n => n.MaLoai == id);
                UpdateModel(loai);
                db.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from nxb in db.Loais where nxb.MaLoai == id select nxb;
                return View(loai.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                //Loai nhaxuatban = db.Loais.SingleOrDefault(n => n.MaLoai == id);
                //db.Loais.DeleteOnSubmit(nhaxuatban);
                //db.SubmitChanges();
                db.sp_XoaLoaiSanPham(id);
                return RedirectToAction("Index", "Loai");
            }
        }
    }
}