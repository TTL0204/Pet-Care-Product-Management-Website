using PagedList;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_HinhMoTa")]
    public class HinhController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: Hinh
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                if (page == null) page = 1;
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                var hinh = from h in db.Hinhs select h;
                return View(hinh.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var hinh = from h in db.Hinhs where h.MaHinh == id select h;
                if (hinh == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(hinh.Single());
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
        public ActionResult Create(Hinh hinh, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
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
                    hinh.Hinh1 = fileName;
                    db.Hinhs.InsertOnSubmit(hinh);
                    db.SubmitChanges();
                    return RedirectToAction("Index", "Hinh");
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
                var hinh = from h in db.Hinhs where h.MaHinh == id select h;
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
                return View(hinh.Single());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                Hinh hinh = db.Hinhs.SingleOrDefault(g => g.MaHinh == id);
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
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
                    hinh.Hinh1 = fileName;
                    UpdateModel(hinh);
                    db.SubmitChanges();
                    return RedirectToAction("Index", "Hinh");
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
                var hinh = from h in db.Hinhs where h.MaHinh == id select h;
                return View(hinh.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                Hinh hinh = db.Hinhs.SingleOrDefault(g => g.MaHinh == id);
                db.Hinhs.DeleteOnSubmit(hinh);
                db.SubmitChanges();
                return RedirectToAction("Index", "Hinh");
            }
        }
    }
}