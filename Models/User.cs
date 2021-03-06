//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComTam_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Tên Đăng nhập")]
        public string TenDangNhap { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Tên người dùng")]
        public string TenUser { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp ")]

        public string ConfirmPassword { get; set; }
        public Nullable<int> RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual Role Role { get; set; }
    }
}

