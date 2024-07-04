using WebsiteThuCungBento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace WebsiteThuCungBento.Controllers
{
    public class UserController : Controller
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        // GET: User
        public ActionResult Index()
        {
            var allacc = (from a in db.SanPhams
                          join b in db.ThuongHieus on a.MaTH equals b.MaTH
                          join c in db.Loais on a.MaLoai equals c.MaLoai
                          join d in db.MauSacs on a.MaMauSac equals d.MaMauSac
                          select new ProductViewModel
                          {
                              MaSP = a.MaSP,
                              TenSP = a.TenSP,
                              DonGiaBan = a.DonGiaBan,
                              HinhAnh = a.HinhAnh,
                              MaTH = a.MaTH,
                              MaLoai = a.MaLoai,
                              TenTH = b.TenTH,
                              TenLoai = c.TenLoai,
                              SoLuong = a.SoLuong,
                              MoTa = a.MoTa,
                              TenMauSac = d.TenMauSac,
                              Logo = b.Logo
                          }).OrderBy(x => x.MaSP).Take(count: 6).ToList();
            return View(allacc);
        }

        public ActionResult Blogs()
        {
            return View();
        }

        #region Lấy sản phẩm
        public ActionResult SanPham(int? page)
        {
            if (page == null) page = 1;
            int pagesize = 9;
            int pagenum = (page ?? 1);
            var allacc = (from a in db.SanPhams
                          join b in db.ThuongHieus on a.MaTH equals b.MaTH
                          join c in db.Loais on a.MaLoai equals c.MaLoai
                          join d in db.MauSacs on a.MaMauSac equals d.MaMauSac
                          select new ProductViewModel
                          {
                              MaSP = a.MaSP,
                              TenSP = a.TenSP,
                              DonGiaBan = a.DonGiaBan,
                              HinhAnh = a.HinhAnh,
                              MaTH = a.MaTH,
                              MaLoai = a.MaLoai,
                              TenTH = b.TenTH,
                              TenLoai = c.TenLoai,
                              SoLuong = a.SoLuong,
                              MoTa = a.MoTa,
                              TenMauSac = d.TenMauSac,

                          }).OrderBy(x => x.MaSP);
            return View(allacc.ToPagedList(pagenum, pagesize));
        }
        #endregion

        public ActionResult HinhAnh(int id)
        {
            var lst = from Hinh in db.Hinhs where Hinh.MaSP == id select Hinh;
            return PartialView(lst);
        }

        public ActionResult ListHinhAnhNho(int id)
        {
            var lst = from Hinh in db.Hinhs where Hinh.MaSP == id select Hinh;
            return PartialView(lst);
        }

        public ActionResult ListHinhAnhNhoDuoi(int id)
        {
            var lst = from Hinh in db.Hinhs where Hinh.MaSP == id select Hinh;
            return PartialView(lst);
        }

        #region Lấy chi tiết sản phẩm
        public ActionResult ChiTiet(int id)
        {
            var detail = from a in db.SanPhams
                         join b in db.ThuongHieus on a.MaTH equals b.MaTH
                         join c in db.Loais on a.MaLoai equals c.MaLoai
                         join d in db.MauSacs on a.MaMauSac equals d.MaMauSac
                         join h in db.Hinhs on a.MaSP equals h.MaSP
                         where a.MaSP == id
                         select new ProductViewModel
                         {
                             MaSP = a.MaSP,
                             TenSP = a.TenSP,
                             DonGiaBan = a.DonGiaBan,
                             HinhAnh = a.HinhAnh,
                             MaTH = a.MaTH,
                             MaLoai = a.MaLoai,
                             TenTH = b.TenTH,
                             TenLoai = c.TenLoai,
                             SoLuong = a.SoLuong,
                             MoTa = a.MoTa,
                             TenMauSac = d.TenMauSac,
                             Hinh1 = h.Hinh1,
                             ThanhToanOn = a.ThanhToanOn
                         };
            return View(detail.SingleOrDefault());
        }
        #endregion

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult Loai()
        {
            var lst = from Loai in db.Loais select Loai;
            return PartialView(lst);
        }

        #region Lấy kích thước sản phẩm
        public ActionResult KichThuoc()
        {
            var lst = from KichThuoc in db.KichThuocs select KichThuoc;
            return PartialView(lst);
        }
        #endregion

        #region Lấy thương hiệu sản phẩm
        public ActionResult ThuongHieu()
        {
            var lst = from ThuongHieu in db.ThuongHieus select ThuongHieu;
            return PartialView(lst);
        }
        #endregion

        #region Lấy hình thương hiệu
        public ActionResult HinhThuongHieu()
        {
            var lst = from ThuongHieu in db.ThuongHieus select ThuongHieu;
            return PartialView(lst);
        }
        #endregion

        #region Lấy sản phẩm theo loại sản phẩm
        public ActionResult SPTheoLoai(int id)
        {
            var sp = from SanPham in db.SanPhams where SanPham.MaLoai == id select SanPham;
            return View(sp);
        }
        #endregion


        #region Lấy sản phẩm theo thương hiệu
        public ActionResult SPTheoThuongHieu(int id)
        {
            var sp = from SanPham in db.SanPhams where SanPham.MaTH == id select SanPham;
            return View(sp);
        }
        #endregion

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        public bool KiemTraTenDN(string tendn)
        {
            return db.KhachHangs.Count(x => x.TenDN == tendn) > 0;
        }

        public bool KiemTraEmail(string Email)
        {
            return db.KhachHangs.Count(x => x.Email == Email) > 0;
        }

        #region Đăng ký tài khoản người dùng
        [HttpPost]
        public ActionResult DangKy(DangKyModel model)
        {
            if (ModelState.IsValid)
            {
                if (KiemTraTenDN(model.TenDN))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (KiemTraEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var maHoaMK = MahoaMD5.GetMD5(model.MatKhau);
                    var kh = new KhachHang();
                    kh.HoTen = model.HoTen;
                    kh.TenDN = model.TenDN;
                    kh.MatKhau = maHoaMK;
                    kh.Email = model.Email;
                    kh.DiaChi = model.DiaChi;
                    kh.SDT = model.DienThoai;
                    kh.HinhAnh = model.HinhAnh;
                    kh.NgaySinh = model.NgaySinh;
                    db.KhachHangs.InsertOnSubmit(kh);
                    db.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
            }
            return View(model);
        }
        #endregion

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        #region Đăng nhập tài khoản người dùng
        [HttpPost]
        public ActionResult DangNhap(DangNhapModel model)
        {
            var maHoaMK = MahoaMD5.GetMD5(model.MatKhau);
            if (ModelState.IsValid)
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TenDN == model.TenDN && n.MatKhau == maHoaMK);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "User");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            else
            {
            }
            return View(model);
        }
        #endregion

        #region Kiểm tra thông tin cá nhân của tài khoản đăng nhập
        public ActionResult ThongTinCaNhan()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        #endregion

        #region Sửa thông tin cá nhân khách hàng
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Index", "User");
            else
            {
                var thongtin = from tt in db.KhachHangs where tt.MaKH == id select tt;
                return View(thongtin.Single());
            }
        }
        #endregion

        #region Cập nhật thông tin cá nhân người dùng
        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Index", "User");
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
                    Session["TaiKhoan"] = kh;
                    UpdateModel(kh);
                    db.SubmitChanges();
                    return RedirectToAction("ThongTinCaNhan", "User");
                }
            }
        }
        #endregion

        #region Đăng xuất tài khoản
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "User");
        }
        #endregion

        #region Tìm kiếm sản phẩm
        public ActionResult KetQuaTimKiem(string searchString)
        {
            //var links = from l in db.SanPhams select l;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    links = links.Where(s => s.TenSP.Contains(searchString));
            //}
            var links = db.sp_TimKiemSanPham(searchString);
            return View(links);
        }
        #endregion

        #region Lấy kích thước sản phẩm theo mã sản phẩm
        public ActionResult KTtheoMaSP(int id)
        {
            var kt = from KichThuoc in db.KichThuocs where KichThuoc.MaSP == id select KichThuoc;
            return PartialView(kt);
        }
        #endregion

        #region Gửi liên kết xác minh thay đổi mật khẩu về mail khách hàng yêu cầu
        [NonAction]
        public void SendVerificationLinkEmail(string EmailId, string activationCode, string EmailFor = "ResetPassword")
        {
            var verifyUrl = "/User/" + EmailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("websitethucungbento@outlook.com.vn", "Trần Thành Luân");
            var toEmail = new MailAddress(EmailId);
            var fromEmailPassword = "khongcomatkhau123";
            string subject = "";
            string body = "";
            if (EmailFor == "ResetPassword")
            {
                subject = "Đặt lại mật khẩu";
                body = "<b>Xin chào bạn</b>,<br/><br/> Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu của bạn. Vui lòng nhấp vào liên kết dưới đây để thiết lập mật khẩu mới cho tài khoản của bạn " + "<br/><br/><a href=" + link + ">Link đặt lại mật khẩu</a>";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }) smtp.Send(message);
        }
        #endregion

        [HttpGet]
        public ActionResult QuenMK()
        {
            return View();
        }

        #region Chức năng quên mật khẩu
        [HttpPost]
        public ActionResult QuenMK(QuenMKModel quenMK)
        {
            if (ModelState.IsValid)
            {
                var account = db.KhachHangs.SingleOrDefault(n => n.Email == quenMK.Email);
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                    account.KhoiPhucMatKhau = resetCode;
                    db.SubmitChanges();
                    return RedirectToAction("QuenMKXN", "User");
                }
                else
                {
                    ViewBag.Message = "Email không đúng";
                }
            }
            else
            {
            }
            return View(quenMK);
        }
        #endregion

        public ActionResult ResetPassword(string id)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.KhoiPhucMatKhau == id);
            if (kh != null)
            {
                ResetPassword model = new ResetPassword();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        #region Thay đổi mật khẩu thành công
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.KhoiPhucMatKhau == model.ResetCode);
                if (kh != null)
                {
                    //kh.MatKhau = model.NewPassword;
                    kh.MatKhau = MahoaMD5.GetMD5(model.NewPassword);
                    kh.KhoiPhucMatKhau = "";
                    UpdateModel(kh);
                    db.SubmitChanges();
                    message = "Cập nhật mật khẩu mới thành công";
                    return RedirectToAction("DangNhap", "User");
                }
            }
            else
            {
                message = "Điều gì đó không hợp lệ";
            }
            ViewBag.Message = message;
            return View(model);
        }
        #endregion

        public ActionResult QuenMKXN()
        {
            return View();
        }

        [NonAction]
        public void SendContact(string Name, string Email, string Subject, string Content)
        {
            KhachHang kh = new KhachHang();
            var fromEmail = new MailAddress("websitethucungbento@outlook.com.vn");
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "khongcomatkhau123";
            string subject = Subject;
            string body = "<br/> Họ tên: " + Name + "<br/><br/> Email: " + " " + Email + "<br/><br/> Nội dung: " + Content;

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }) smtp.Send(message);
        }

        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LienHe(LienHeModel lh)
        {
            if (ModelState.IsValid)
            {
                SendContact(lh.Name, lh.Email, lh.Subject, lh.Message);
                return RedirectToAction("ThongBaoLienHe", "User");
            }
            else
            {
            }
            return View(lh);
        }

        public ActionResult ThongBaoLienHe()
        {
            return View();
        }
    }
}