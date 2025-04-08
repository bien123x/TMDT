using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("chi_tiet_don_hang")]
    public class ChiTietDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        public int Id_Don_Hang { get; set; }  // Khóa ngoại đến bảng DonHang
        // Thiết lập quan hệ với DonHang
        [ForeignKey("Id_Don_Hang")]
        public virtual DonHang? DonHang { get; set; }

        [Required]
        public int Id_San_Pham { get; set; }  // Khóa ngoại đến bảng SanPham
        // Thiết lập quan hệ với SanPham
        [ForeignKey("Id_San_Pham")]
        public virtual SanPham? SanPham { get; set; }

        [Required]
        public int So_Luong { get; set; }  // Số lượng sản phẩm trong đơn hàng

        [Required]
        [Precision(10, 2)]
        public decimal? Gia { get; set; }  // Giá tại thời điểm đặt hàng

    }
}
