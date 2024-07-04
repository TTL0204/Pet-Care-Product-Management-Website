using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_PhanQuyen")]
    public class AdminPQController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: AdminPQ
        #region PHÂN QUYỀN
        public ActionResult DSPhanQuyen()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                var ad = from admin in db.NhanViens select admin;
                return View(ad);
            }
        }

        public ActionResult ChiTietDSPhanQuyen(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                var ad = from admin in db.PhanQuyens where admin.MaNV == id select admin;
                return View(ad);
            }
        }

        public ActionResult DSQuyen()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                var ad = from admin in db.PhanQuyens select admin;
                return View(ad);
            }
        }

        public ActionResult Create()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaCN = new SelectList(db.ChucNang_Quyens.ToList().OrderBy(n => n.TenCN), "MaCN", "TenCN");
                ViewBag.MaNV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.HoTen), "MaNV", "HoTen");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(PhanQuyen kt)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaCN = new SelectList(db.ChucNang_Quyens.ToList().OrderBy(n => n.TenCN), "MaCN", "TenCN");
                ViewBag.MaNV = new SelectList(db.NhanViens.ToList().OrderBy(n => n.HoTen), "MaNV", "HoTen");
                db.PhanQuyens.InsertOnSubmit(kt);
                db.SubmitChanges();
                return RedirectToAction("DSPhanQuyen", "AdminPQ");

            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaCN = new SelectList(db.ChucNang_Quyens.ToList().OrderBy(n => n.TenCN), "MaCN", "TenCN");
                var mau = from m in db.PhanQuyens where m.MaPQ == id select m;
                return View(mau.Single());
            }
        }
        
        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaCN = new SelectList(db.ChucNang_Quyens.ToList().OrderBy(n => n.TenCN), "MaCN", "TenCN");
                PhanQuyen mau = db.PhanQuyens.SingleOrDefault(n => n.MaPQ == id);
                UpdateModel(mau);
                db.SubmitChanges();
                return RedirectToAction("DSPhanQuyen", "AdminPQ");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.PhanQuyens where m.MaPQ == id select m;
                return View(mau.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                PhanQuyen mau = db.PhanQuyens.SingleOrDefault(n => n.MaPQ == id);
                db.PhanQuyens.DeleteOnSubmit(mau);
                db.SubmitChanges();
                return RedirectToAction("DSPhanQuyen", "AdminPQ");
            }
        }
        #endregion

        #region CHỨC NĂNG QUYỀN
        public ActionResult DSChucNang()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                var ad = from admin in db.ChucNang_Quyens select admin;
                return View(ad);
            }
        }

        public ActionResult CreateCN()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateCN(ChucNang_Quyen chucnang)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                db.ChucNang_Quyens.InsertOnSubmit(chucnang);
                db.SubmitChanges();
                return RedirectToAction("DSChucNang", "AdminPQ");
            }
        }

        [HttpGet]
        public ActionResult EditCN(string id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.ChucNang_Quyens where m.MaCN == id select m;
                return View(mau.Single());
            }
        }

        [HttpPost, ActionName("EditCN")]
        public ActionResult CapnhatCN(string id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ChucNang_Quyen mau = db.ChucNang_Quyens.SingleOrDefault(n => n.MaCN == id);
                UpdateModel(mau);
                db.SubmitChanges();
                return RedirectToAction("DSChucNang", "AdminPQ");
            }
        }

        [HttpGet]
        public ActionResult DeleteCN(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.PhanQuyens where m.MaPQ == id select m;
                return View(mau.Single());
            }
        }

        [HttpPost, ActionName("DeleteCN")]
        public ActionResult XoaCN(string id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                PhanQuyen mau = db.PhanQuyens.SingleOrDefault(n => n.MaCN == id);
                db.PhanQuyens.DeleteOnSubmit(mau);
                db.SubmitChanges();
                return RedirectToAction("DSPhanQuyen", "AdminPQ");
            }
        }
        #endregion
    }
}