using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingMobile.Models.ViewModel
{
    public class User
    {
        [Required(ErrorMessage ="Không được để trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string UserPassword { get; set; }
        public int Role { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name ="Họ và tên")]
        [DataType(DataType.Text,ErrorMessage ="Chỉ được nhập chữ")]
        public string FullName { get; set; }
    }
}