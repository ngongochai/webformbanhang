using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
    public class ChangePassword
    {
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string Confirm { get; set; }
    }
}