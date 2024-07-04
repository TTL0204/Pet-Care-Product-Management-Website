using System.Linq;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_MauSac")]
    public class MauSacController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: MauSac
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.MauSacs select m;
                return View(mau);
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.MauSacs where m.MaMauSac == id select m;
                return View(mau.Single());
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
        public ActionResult Create(MauSac mau)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                db.MauSacs.InsertOnSubmit(mau);
                db.SubmitChanges();
                return RedirectToAction("Index", "MauSac");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.MauSacs where m.MaMauSac == id select m;
                return View(mau.Single());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult CapNhat(int id, FormCollection collection)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                MauSac mau = db.MauSacs.SingleOrDefault(n => n.MaMauSac == id);
                //UpdateModel(mau);
                //db.SubmitChanges();
                mau.TenMauSac = collection["TenMauSac"];
                db.sp_CapNhatMauSac(mau.MaMauSac, mau.TenMauSac);
                return RedirectToAction("Index", "MauSac");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var mau = from m in db.MauSacs where m.MaMauSac == id select m;
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
                //MauSac mau = db.MauSacs.SingleOrDefault(n => n.MaMauSac == id);
                //db.MauSacs.DeleteOnSubmit(mau);
                //db.SubmitChanges();
                db.sp_XoaMauSac(id);
                return RedirectToAction("Index", "MauSac");
            }
        }
    }
}