using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThuCungBento.Models
{
    public class DangKyModel
    {
        [Display(Name = "Tên đăng nhập:")]
        [Required(ErrorMessage = "Tên đang nhập không được để trống.")]
        public string TenDN { set; get; }


        [Display(Name = "Mật khẩu:")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Độ dài mật khẩu ít nhất 3 - 20 ký tự.")]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string MatKhau { set; get; }



        [Display(Name = "Xác nhận mật khẩu:")]
        [Compare("MatKhau", ErrorMessage = " Xác nhận mật khẩu không đúng.")]
        [Required(ErrorMessage = " mật khẩu không được để trống.")]
        public string XacNhanMatKhau { set; get; }



        [Display(Name = "Họ và tên: ")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Họ tên không hợp lệ.")]
        [Required(ErrorMessage = " Họ tên không được để trống.")]
        public string HoTen { set; get; }



        [Display(Name = "Số điện thoại:")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Required(ErrorMessage = " Số điện thoại không được để trống.")]
        public string DienThoai { set; get; }



        [Display(Name = "Email:")]
        [Required(ErrorMessage = " Email không được để trống.")]
        public string Email { set; get; }



        [Display(Name = "Hình ảnh:")]
        public string HinhAnh { set; get; }



        [Display(Name = "Ngày sinh:")]
        public DateTime NgaySinh { set; get; }


        [Display(Name = "Địa chỉ:")]
        public string DiaChi { set; get; }
    }
}