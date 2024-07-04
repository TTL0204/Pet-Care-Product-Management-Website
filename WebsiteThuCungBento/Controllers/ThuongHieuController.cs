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
    [AdminPhanQuyen(MaCN = "QL_ThuongHieu")]
    public class ThuongHieuController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: ThuongHieu
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from l in db.ThuongHieus select l;
                return View(loai);
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from l in db.ThuongHieus where l.MaTH == id select l;
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
        [ValidateInput(false)]
        public ActionResult Create(ThuongHieu loai, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                if (fileUpload == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        loai.Logo = fileName;
                        db.ThuongHieus.InsertOnSubmit(loai);
                        db.SubmitChanges();
                    }

                }                            
                return RedirectToAction("Index", "ThuongHieu");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var loai = from l in db.ThuongHieus where l.MaTH == id select l;
                return View(loai.Single());
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ThuongHieu loai = db.ThuongHieus.SingleOrDefault(n => n.MaTH == id);
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
                    loai.Logo = fileName;
                    UpdateModel(loai);
                    db.SubmitChanges();
                    return RedirectToAction("Index", "ThuongHieu");
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
                var loai = from th in db.ThuongHieus where th.MaTH == id select th;
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
                //ThuongHieu th = db.ThuongHieus.SingleOrDefault(n => n.MaTH == id);
                //db.ThuongHieus.DeleteOnSubmit(th);
                //db.SubmitChanges();
                db.sp_XoaThuongHieu(id);
                return RedirectToAction("Index", "ThuongHieu");
            }
        }
    }
}