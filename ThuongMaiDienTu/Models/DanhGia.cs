using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("danh_gia")]
    public class DanhGia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        public int Id_San_Pham { get; set; }  // Khóa ngoại đến bảng SanPham
        [ForeignKey("Id_San_Pham")]
        public virtual SanPham? SanPham { get; set; }

        [Required]
        public int Id_Nguoi_Mua { get; set; }  // Khóa ngoại đến bảng NguoiDung
        [ForeignKey("Id_Nguoi_Mua")]
        public virtual NguoiDung? NguoiDung { get; set; }

        [Required]
        [Range(1, 5)]
        public int So_Sao { get; set; }  // Số sao đánh giá (1-5)

        [StringLength(1000)]
        public string? Noi_Dung { get; set; }  // Nội dung đánh giá

        public string? Hinh_Anh { get; set; }  // Đường dẫn hình ảnh (nếu có)

        [Required]
        public DateTime Ngay_Danh_Gia { get; set; } = DateTime.Now;  // Ngày đánh giá (mặc định là thời gian hiện tại)

        // Thêm thuộc tính này để xử lý upload file
        [NotMapped] // Không ánh xạ vào cơ sở dữ liệu
        public IFormFile? HinhAnhFile { get; set; }  // File hình ảnh từ form
    }
}
