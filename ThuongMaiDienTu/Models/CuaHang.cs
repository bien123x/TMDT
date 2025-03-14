using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("cua_hang")]
    public class CuaHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Người bán là bắt buộc.")]
        public int Id_Nguoi_Ban { get; set; }

        [ForeignKey("Id_Nguoi_Ban")]
        public NguoiDung? NguoiDung { get; set; }

        [Required(ErrorMessage = "Tên cửa hàng không được để trống.")]
        [StringLength(20, ErrorMessage = "Tên cửa hàng không được vượt quá 20 ký tự.")]
        public string Ten_Cua_Hang { get; set; }

        [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự.")]
        public string Mo_Ta { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Ngay_Tao { get; set; } = DateTime.Now;
    }
}
