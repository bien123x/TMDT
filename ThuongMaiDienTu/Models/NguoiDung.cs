using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("nguoi_dung")]
    public class NguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Ho_Ten { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Mat_Khau { get; set; } 

        [StringLength(20)]
        [Phone]
        public string So_Dien_Thoai { get; set; }

        public int Vai_Tro_Id { get; set; }

        [ForeignKey("Vai_Tro_Id")]
        public VaiTro? VaiTro { get; set; } // Navigation Property

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Ngay_Tao { get; set; } = DateTime.Now;
    }
}
