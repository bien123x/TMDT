using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public class VanChuyenRepository : Repository<VanChuyen>, IVanChuyenRepository
    {
        private readonly DbContextApp _context;

        public VanChuyenRepository(DbContextApp context) : base(context)
        {
            _context = context;
        }

        public List<VanChuyen> GetVanChuyenByDonHangId(int donHangId)
        {
            return _context.VanChuyens
                .Include(v => v.TrangThaiVanChuyen)
                .Where(v => v.Id_Don_Hang == donHangId)
                .OrderByDescending(v => v.Ngay_Cap_Nhat)
                .ToList();
        }

        public VanChuyen? GetLatestVanChuyenByDonHangId(int donHangId)
        {
            return _context.VanChuyens
                .Include(v => v.TrangThaiVanChuyen)
                .Where(v => v.Id_Don_Hang == donHangId)
                .OrderByDescending(v => v.Ngay_Cap_Nhat)
                .FirstOrDefault();
        }

        public List<VanChuyen> GetVanChuyenByTrangThai(int trangThaiId)
        {
            return _context.VanChuyens
                .Include(v => v.DonHang)
                .Include(v => v.TrangThaiVanChuyen)
                .Where(v => v.Trang_Thai_Id == trangThaiId)
                .OrderByDescending(v => v.Ngay_Cap_Nhat)
                .ToList();
        }

        public int CountVanChuyenByTrangThai(int trangThaiId)
        {
            return _context.VanChuyens
                .Where(v => v.Trang_Thai_Id == trangThaiId)
                .Count();
        }

        public List<DonHang> GetDonHangCanVanChuyen()
        {
            return _context.DonHangs
        .Include(d => d.NguoiDung)
        .Include(d => d.TrangThaiDonHang)
        .Include(d => d.ThanhToans)
            .ThenInclude(t => t.TrangThaiThanhToan)
        .Where(d => d.Trang_Thai_Id == 2) // 2 = Chờ vận chuyển
        .ToList();
        }

        public List<DonHang> GetDonHangDangVanChuyen()
        {
            var donHangIds = _context.VanChuyens
                .Where(v => v.Trang_Thai_Id == 3) // 3 = Đang vận chuyển
                .Select(v => v.Id_Don_Hang)
                .Distinct()
                .ToList();

            return _context.DonHangs
                .Include(d => d.NguoiDung)
                .Include(d => d.TrangThaiDonHang)
                .Include(d => d.ThanhToans)
                    .ThenInclude(t => t.TrangThaiThanhToan)
                .Where(d => donHangIds.Contains(d.Id))
                .ToList();
        }

        public List<VanChuyen> GetVanChuyenByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.VanChuyens
                .Include(v => v.TrangThaiVanChuyen)
                .Include(v => v.DonHang)
                .Where(v => v.Ngay_Cap_Nhat >= startDate && v.Ngay_Cap_Nhat <= endDate)
                .OrderByDescending(v => v.Ngay_Cap_Nhat)
                .ToList();
        }

        public Dictionary<string, int> GetVanChuyenStatsByDate(DateTime date)
        {
            var startDate = date.Date;
            var endDate = date.Date.AddDays(1).AddSeconds(-1);

            return _context.VanChuyens
                .Where(v => v.Ngay_Cap_Nhat >= startDate && v.Ngay_Cap_Nhat <= endDate)
                .GroupBy(v => v.TrangThaiVanChuyen.Mo_Ta)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}