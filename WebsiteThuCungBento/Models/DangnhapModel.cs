using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThuCungBento.Models
{
    public class DangNhapModel
    {
        [Display(Name = "Tên đăng nhập:")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string TenDN { set; get; }


        [Display(Name = "Mật khẩu:")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Độ dài mật khẩu ít nhất 3 - 20 ký tự.")]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string MatKhau { set; get; }
    }
}