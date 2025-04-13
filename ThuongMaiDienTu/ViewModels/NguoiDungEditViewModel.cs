
// NguoiDungEditViewModel.cs
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ThuongMaiDienTu.ViewModels;
public class NguoiDungEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Họ tên không được để trống")]
    [StringLength(255)]
    public string Ho_Ten { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(255, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    public string? Mat_Khau { get; set; } // Nullable vì có thể không thay đổi

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [StringLength(20)]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    public string So_Dien_Thoai { get; set; }

    [Required(ErrorMessage = "Vai trò không được để trống")]
    public int Vai_Tro_Id { get; set; }

    public bool Trang_Thai { get; set; }
    [BindNever]
    [ValidateNever]
    public IEnumerable<SelectListItem> VaiTroList { get; set; }
    public string? Mo_Ta { get; set; }
}