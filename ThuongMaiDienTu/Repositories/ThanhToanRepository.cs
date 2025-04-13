using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public class ThanhToanRepository : Repository<ThanhToan>, IThanhToanRepository
    {
        private readonly DbContextApp _context;

        public ThanhToanRepository(DbContextApp context) : base(context)
        {
            _context = context;
        }

        public List<ThanhToan> GetThanhToanByDonHangId(int donHangId)
        {
            return _context.ThanhToans
                .Include(t => t.TrangThaiThanhToan)
                .Where(t => t.Id_Don_Hang == donHangId)
                .OrderByDescending(t => t.Ngay_Tao)
                .ToList();
        }

        public int AddThanhToan(ThanhToan thanhToan)
        {
            _context.ThanhToans.Add(thanhToan);
            _context.SaveChanges();
            return thanhToan.Id;
        }

        public bool UpdateThanhToan(ThanhToan thanhToan)
        {
            try
            {
                _context.ThanhToans.Update(thanhToan);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ThanhToan? GetLatestThanhToanByDonHangId(int donHangId)
        {
            return _context.ThanhToans
                .Include(t => t.TrangThaiThanhToan)
                .Where(t => t.Id_Don_Hang == donHangId)
                .OrderByDescending(t => t.Ngay_Tao)
                .FirstOrDefault();
        }

        public bool IsPaid(int donHangId)
        {
            var latestThanhToan = GetLatestThanhToanByDonHangId(donHangId);
            // Giả sử 2 là mã trạng thái "Đã thanh toán"
            return latestThanhToan != null && latestThanhToan.Trang_Thai_Id == 2;
        }

        public List<ThanhToan> GetThanhToanByTrangThai(int trangThaiId)
        {
            return _context.ThanhToans
                .Include(t => t.DonHang)
                .Include(t => t.TrangThaiThanhToan)
                .Where(t => t.Trang_Thai_Id == trangThaiId)
                .OrderByDescending(t => t.Ngay_Tao)
                .ToList();
        }

        public bool DeleteThanhToan(int id)
        {
            try
            {
                var thanhToan = _context.ThanhToans.Find(id);
                if (thanhToan == null)
                    return false;

                _context.ThanhToans.Remove(thanhToan);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ThanhToan> GetThanhToanByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}