using Microsoft.Win32.SafeHandles;
using PagedList;
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
    [AdminPhanQuyen(MaCN = "QL_SanPham")]
    public class SanPhamController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: SanPham
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                if (page == null) page = 1;
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                var sp = from g in db.SanPhams select g;
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var sp = from g in db.SanPhams where g.MaSP == id select g;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp.Single());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                ViewBag.MaTH = new SelectList(db.ThuongHieus.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
                ViewBag.MaMauSac = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMauSac), "MaMauSac", "TenMauSac");
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, SanPham sp, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                ViewBag.MaTH = new SelectList(db.ThuongHieus.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
                ViewBag.MaMauSac = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMauSac), "MaMauSac", "TenMauSac");
                if (fileUpload == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sp.HinhAnh = fileName;
                    db.SanPhams.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    return RedirectToAction("Index", "SanPham");
                }

            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var sp = from g in db.SanPhams where g.MaSP == id select g;
                ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                ViewBag.MaTH = new SelectList(db.ThuongHieus.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
                ViewBag.MaMauSac = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMauSac), "MaMauSac", "TenMauSac");
                return View(sp.Single());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                SanPham sp = db.SanPhams.SingleOrDefault(g => g.MaSP == id);
                ViewBag.MaLoai = new SelectList(db.Loais.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                ViewBag.MaTH = new SelectList(db.ThuongHieus.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
                ViewBag.MaMauSac = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMauSac), "MaMauSac", "TenMauSac");
                if (fileUpload == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                    fileUpload.SaveAs(path);
                    sp.HinhAnh = fileName;
                    UpdateModel(sp);
                    db.SubmitChanges();
                    return RedirectToAction("Index", "SanPham");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var sp = from g in db.SanPhams where g.MaSP == id select g;
                return View(sp.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                SanPham sp = db.SanPhams.SingleOrDefault(g => g.MaSP == id);
                var kichthuoc = from kt in db.KichThuocs where kt.MaSP == id select kt;
                var hinh = from h in db.Hinhs where h.MaSP == id select h;
                var kho = from pnk in db.PhieuNhapKhos where pnk.MaSP == id select pnk;
                var dathang = from ctdh in db.CTDHs where ctdh.MaSP == id select ctdh;
                var dondathang = from dh in db.DonHangs select dh;
                foreach (var item in dathang)
                {
                    db.CTDHs.DeleteOnSubmit(item);
                }
                foreach (var item in hinh)
                {
                    db.Hinhs.DeleteOnSubmit(item);
                }
                foreach (var item in kho)
                {
                    db.PhieuNhapKhos.DeleteOnSubmit(item);
                }
                foreach (var item in kichthuoc)
                {
                    db.KichThuocs.DeleteOnSubmit(item);
                }
                db.SanPhams.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Index", "SanPham");
            }
        }

        #region UpdateProduct
        [HttpPost]
        public void UpdateProduct(int id)
        {
            var _sp = (from s in db.SanPhams where s.MaSP == id select s).SingleOrDefault();
            string _Hinh = "";
            if (_sp.AnHien == true)
            {
                _sp.AnHien = false;
                _Hinh = "/Content/Images/icon_An.png";
            }
            else
            {
                _sp.AnHien = true;
                _Hinh = "/Content/Images/icon_Hien.png";
            }
            UpdateModel(_sp);
            db.SubmitChanges();
            Response.Write(_Hinh);
        }
        #endregion
    }
}