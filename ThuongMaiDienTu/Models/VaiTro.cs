using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThuongMaiDienTu.Models
{
    [Table("vai_tro")]
    public class VaiTro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Không tự động tăng
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Ten_vai_Tro { get; set; }

        public ICollection<NguoiDung> NguoiDungs { get; set; }
    } 
}
