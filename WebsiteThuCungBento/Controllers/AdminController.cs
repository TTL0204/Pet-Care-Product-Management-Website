using WebsiteThuCungBento.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;

namespace WebsiteThuCungBento.Controllers
{
    public class AdminController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
                return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(DangNhapModel model)
        {
            if (ModelState.IsValid)
            {
                NhanVien ad = db.NhanViens.SingleOrDefault(n => n.TenDN == model.TenDN && n.MatKhau == model.MatKhau);
                if (ad != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoanAdmin"] = ad;

                    var quyen = db.PhanQuyens.Where(pq => pq.MaNV == ad.MaNV).Select(pq => pq.MaCN).ToList();
                    Session["Quyen"] = quyen;

                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View(model);
        }

        public ActionResult ThongTinAdmin()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Admin");
        }

        [AdminPhanQuyen(MaCN = "QL_QuanTriVien")]
        public ActionResult ListAdmin()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                var ad = from admin in db.NhanViens select admin;
                return View(ad) ;
            }
        }

        [HttpGet]
        [AdminPhanQuyen(MaCN = "QL_QuanTriVien")]
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
        public ActionResult Create(NhanVien admin, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
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
                        admin.Avatar = fileName;
                        db.NhanViens.InsertOnSubmit(admin);
                        db.SubmitChanges();
                    }
           
                return RedirectToAction("ListAdmin", "Admin");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var admin = from ad in db.NhanViens where ad.MaNV == id select ad;
                return View(admin.Single());
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
                NhanVien ad = db.NhanViens.SingleOrDefault(n => n.MaNV == id);
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
                    ad.Avatar = fileName;
                    UpdateModel(ad);
                    db.SubmitChanges();
                    return RedirectToAction("ListAdmin", "Admin");
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
                var ad = from adm in db.NhanViens where adm.MaNV == id select adm;
                return View(ad.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                NhanVien ad = db.NhanViens.SingleOrDefault(n => n.MaNV == id);
                db.NhanViens.DeleteOnSubmit(ad);
                db.SubmitChanges();
                return RedirectToAction("ListAdmin", "Admin");
            }
        }

        [HttpGet]
        public ActionResult DoiMK(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                DoiMKadmin model = new DoiMKadmin();               
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMK(int id, DoiMKadmin reset)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                NhanVien ad = db.NhanViens.SingleOrDefault(n => n.MaNV == id && n.MatKhau == reset.CheckPassword); 
                if(ad != null)
                {
                    ad.MatKhau = reset.NewPassword;
                    UpdateModel(ad);
                    Session["TaiKhoanAdmin"] = ad;
                    db.SubmitChanges();
                    message = "Cập nhật mật khẩu mới thành công";
                    return RedirectToAction("ThongTinAdmin", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Mật khẩu cũ không đúng";
                }
            }
            else
            {
                message = "Điều gì đó không hợp lệ";
            }
            ViewBag.Message = message;
            return View(reset);
        }
    }
}