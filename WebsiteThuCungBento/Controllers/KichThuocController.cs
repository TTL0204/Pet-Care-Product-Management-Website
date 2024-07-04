using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebsiteThuCungBento.App_Start;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.Controllers
{
    [AdminPhanQuyen(MaCN = "QL_KichThuoc")]
    public class KichThuocController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: KichThuoc
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                if (page == null) page = 1;
                int pageSize = 21;
                int pageNumber = (page ?? 1);
                var kichthuoc = from kt in db.KichThuocs select kt;
                return View(kichthuoc.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kichthuoc = from kt in db.KichThuocs where kt.MaKT == id select kt;
                if (kichthuoc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(kichthuoc.Single());
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
        public ActionResult Create(KichThuoc kichthuoc)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP");
                db.KichThuocs.InsertOnSubmit(kichthuoc);
                db.SubmitChanges();
                return RedirectToAction("Index", "KichThuoc");

            }
        }

        public ActionResult Delete(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                var kichthuoc = from kt in db.KichThuocs where kt.MaKT == id select kt;
                return View(kichthuoc.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
                return RedirectToAction("DangNhap", "Admin");
            else
            {
                KichThuoc kichthuoc = db.KichThuocs.SingleOrDefault(n => n.MaKT == id);
                db.KichThuocs.DeleteOnSubmit(kichthuoc);
                db.SubmitChanges();
                return RedirectToAction("Index", "KichThuoc");
            }
        }
    }
}