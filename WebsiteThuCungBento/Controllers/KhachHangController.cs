using WebsiteThuCungBento.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using System.Reflection;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_KhachHang")]
    public class KhachHangController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: ThuongHieu
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var nv = (NhanVien)Session["TaiKhoanAdmin"];
                var manv = nv.MaNV;
                if (nv.TenLoai == "Admin")
                {
                    var kh = db.sp_ListDSKhachHangVaNhanVienPhuTrach().ToList();
                    return View("Index", kh);
                }
                else
                {
                    var kh = db.sp_ListDSKhachHangTheoNV(manv).ToList();
                    return View("Index1", kh);
                }
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kh = from l in db.KhachHangs where l.MaKH == id select l;
                return View(kh.Single());
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
        public ActionResult Create(KhachHang kh, HttpPostedFileBase fileUpload)
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
                        kh.HinhAnh = fileName;
                        //db.KhachHangs.InsertOnSubmit(kh);
                        //db.SubmitChanges();
                        var maHoaMK = MahoaMD5.GetMD5(kh.MatKhau);
                        db.sp_ThemKhachHang(kh.HoTen, kh.SDT, kh.DiaChi, kh.TenDN, maHoaMK, kh.Email, kh.NgaySinh, kh.HinhAnh, kh.KhoiPhucMatKhau);
                    }
                }
                return RedirectToAction("Index", "KhachHang");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kh = from l in db.KhachHangs where l.MaKH == id select l;
                return View(kh.Single());
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
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == id);
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
                    kh.HinhAnh = fileName;
                    //UpdateModel(kh);
                    //db.SubmitChanges();
                    db.sp_CapNhatKhachHang(kh.MaKH, kh.HoTen, kh.SDT, kh.DiaChi, kh.TenDN, kh.MatKhau, kh.Email, kh.NgaySinh, kh.HinhAnh, kh.KhoiPhucMatKhau);
                    return RedirectToAction("Index", "KhachHang");
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
                var kh = from nxb in db.KhachHangs where nxb.MaKH == id select nxb;
                return View(kh.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                //KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == id);
                //db.KhachHangs.DeleteOnSubmit(kh);
                //db.SubmitChanges();
                db.sp_XoaKhachHang(id);
                return RedirectToAction("Index", "KhachHang");
            }
        }
    }
}