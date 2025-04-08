using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("don_hang")]
    public class DonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Khóa chính

        [Required]
        public int? Id_Nguoi_Mua { get; set; } // Mã người mua
        [ForeignKey("Id_Nguoi_Mua")]
        public virtual NguoiDung? NguoiDung { get; set; }

        [Required]
        public int Trang_Thai_Id { get; set; } // Trạng thái đơn hàng
        [ForeignKey("Trang_Thai_Id")]
        public virtual TrangThaiDonHang? TrangThaiDonHang { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Tong_Tien { get; set; } // Tổng tiền đơn hàng

        [Required]
        public DateTime Ngay_Tao = DateTime.Now; // Ngày tạo đơn hàng

        public virtual ICollection<VanChuyen>? VanChuyens { get; set; }
        public virtual ICollection<ThanhToan>? ThanhToans { get; set; }
        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; } // Danh sách chi tiết đơn hàng

    }
}
