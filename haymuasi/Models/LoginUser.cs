using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace haymuasi.Models
{
    public class User
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập Email")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập Mật khẩu")]
        [StringLength(20,MinimumLength = 6,ErrorMessage="Mật khẩu ít nhất 6 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage="Xác nhận mật khẩu không đúng")]
        public string Confirm { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Tên")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

         [Display(Name = "Giới tính")]
        public string Sex { get; set; }

        [Display(Name="Điện thoại")]
        public string Phone { get; set; }
    }
}