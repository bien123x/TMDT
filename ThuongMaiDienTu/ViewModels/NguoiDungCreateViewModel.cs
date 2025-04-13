
// NguoiDungCreateViewModel.cs
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ThuongMaiDienTu.ViewModels;
public class NguoiDungCreateViewModel
{
    [Required(ErrorMessage = "Họ tên không được để trống")]
    [StringLength(255)]
    public string Ho_Ten { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [StringLength(255)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    public string Mat_Khau { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [StringLength(20)]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    public string So_Dien_Thoai { get; set; }

    [Required(ErrorMessage = "Vai trò không được để trống")]
    public int Vai_Tro_Id { get; set; }

    public bool Trang_Thai { get; set; } = true;
    [BindNever]
    [ValidateNever] // thuộc tính này sẽ không được kiểm tra khi gửi dữ liệu từ form
    public IEnumerable<SelectListItem> VaiTroList { get; set; }
    // cung cấp dữ liệu cho dropdown list
    [StringLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
    public string? Mo_Ta { get; set; }
}
