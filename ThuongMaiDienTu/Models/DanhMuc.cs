using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ThuongMaiDienTu.Models
{
    [Table("danh_muc")]
    public class DanhMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(10, ErrorMessage = "Tên danh mục không được vượt quá 10 ký tự")]
        [DisplayName("Tên danh mục")]
        public string Ten_Danh_Muc { get; set; }

        [DisplayName("Trạng thái")]
        public bool Trang_Thai { get; set; } = true; // Mặc định là hoạt động

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [DisplayName("Mô tả")]
        public string? Mo_Ta { get; set; }
    }
}
