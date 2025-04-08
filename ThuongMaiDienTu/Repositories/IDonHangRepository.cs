using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IDonHangRepository : IRepository<DonHang>
    {
        void AddListSanPham(List<SanPham> list, int idDonHang);
        void AddVanChuyen(VanChuyen vanChuyen);
        void AddThanhToan(ThanhToan thanhToan);
        IEnumerable<DonHang> GetAllAndInfor(int? sellerId = null);
    }
}
