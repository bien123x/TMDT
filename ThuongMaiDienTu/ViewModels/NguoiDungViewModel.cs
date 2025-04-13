
namespace ThuongMaiDienTu.ViewModels;
public class NguoiDungViewModel
{
    public int Id { get; set; }
    public string Ho_Ten { get; set; }
    public string Email { get; set; }
    public string So_Dien_Thoai { get; set; }
    public string Ten_Vai_Tro { get; set; }
    public int Vai_Tro_Id { get; set; }
    public DateTime Ngay_Tao { get; set; }
    public bool Trang_Thai { get; set; }
    public bool HasStore { get; set; }
    public int? StoreId { get; set; }

    public string? Mo_Ta { get; set; }
}
