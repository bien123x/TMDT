using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public class DonHangRepository : Repository<DonHang>, IDonHangRepository
    {
        private readonly DbContextApp _context;
        public DonHangRepository(DbContextApp context) : base(context)
        {
            _context = context;
        }
        public void AddListSanPham(List<SanPham> list, int idDonHang)
        {
            foreach (SanPham sanPham in list)
            {
                ChiTietDonHang ctDonHang = new ChiTietDonHang()
                {
                    Id_Don_Hang = idDonHang,
                    Id_San_Pham = sanPham.Id,
                    So_Luong = sanPham.So_Luong_Ton,
                    Gia = sanPham.Gia_Khuyen_Mai
                };
                _context.ChiTietDonHangs.Add(ctDonHang);
                _context.SaveChanges();
            }
        }


        public void AddThanhToan(ThanhToan thanhToan)
        {
            _context.ThanhToans.Add(thanhToan);
            _context.SaveChanges();
        }

        public void AddVanChuyen(VanChuyen vanChuyen)
        {
            _context.VanChuyens.Add(vanChuyen);
            _context.SaveChanges();
        }

        public bool CancelDonHang(int donHangId)
        {
            throw new NotImplementedException();
        }

        public int CreateDonHang(DonHang donHang)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DonHang> GetAllAndInfor(int? sellerId = null)
        {
            var query = _context.DonHangs
                .Include(dh => dh.TrangThaiDonHang)
                .Include(dh => dh.VanChuyens)
                    .ThenInclude(vc => vc.TrangThaiVanChuyen)
                .Include(dh => dh.ThanhToans)
                    .ThenInclude(tt => tt.TrangThaiThanhToan)
                .AsQueryable();

            if (sellerId.HasValue)
            {
                query = query.Where(dh => dh.ChiTietDonHangs.Any(ct => ct.SanPham.Id_Cua_Hang == sellerId.Value));
            }

            return query.ToList();
        }

        public List<DonHang> GetCompletedDeliveries(DateTime startDate, DateTime endDate, int page, int pageSize, out int totalItems)
        {
            var query = _context.DonHangs
                   .Include(d => d.NguoiDung)
                   .Include(d => d.TrangThaiDonHang)
                   .Include(d => d.ThanhToans)
                       .ThenInclude(t => t.TrangThaiThanhToan)
                   .Include(d => d.VanChuyens)
                       .ThenInclude(v => v.TrangThaiVanChuyen)
                   .Where(d => d.VanChuyens.Any(v => v.Trang_Thai_Id == 4 &&
                                                      v.Ngay_Cap_Nhat >= startDate &&
                                                      v.Ngay_Cap_Nhat <= endDate))
                   .OrderByDescending(d => d.Ngay_Tao);

            totalItems = query.Count();

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public DonHang? GetDonHangById(int id)
        {
            throw new NotImplementedException();
        }

        public List<DonHang> GetDonHangByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<DonHang> GetDonHangForDelivery(int? trangThaiId, int page, int pageSize, out int totalItems)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> GetDonHangStatsByTrangThai()
        {
            throw new NotImplementedException();
        }

        public DonHang? GetDonHangWithDetails(int id)
        {
            return _context.DonHangs
                    .Include(d => d.NguoiDung)
                    .Include(d => d.TrangThaiDonHang)
                    .Include(d => d.ChiTietDonHangs)
                        .ThenInclude(ct => ct.SanPham)
                            .ThenInclude(sp => sp.CuaHang)
                    .Include(d => d.ThanhToans)
                        .ThenInclude(t => t.TrangThaiThanhToan)
                    .Include(d => d.VanChuyens)
                        .ThenInclude(v => v.TrangThaiVanChuyen)
                    .FirstOrDefault(d => d.Id == id);
        }

        public List<DonHang> GetDonHangChoVanChuyen()
        {
            return _context.DonHangs
                .Include(d => d.NguoiDung)
                .Include(d => d.TrangThaiDonHang)
                .Include(d => d.ThanhToans)
                    .ThenInclude(t => t.TrangThaiThanhToan)
               
                .ToList();
        }

        public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTrangThaiDonHang(int donHangId, int trangThaiId)
        {
            var donHang = _context.DonHangs.Find(donHangId);
            if (donHang == null)
                return false;

            donHang.Trang_Thai_Id = trangThaiId;
            donHang.Ngay_Tao = DateTime.Now;

            _context.SaveChanges();
            return true;
        }


        public List<DonHang> GetDonHangByIds(List<int> donHangIds)
        {
            return _context.DonHangs
                .Include(d => d.NguoiDung)
                .Include(d => d.TrangThaiDonHang)
                .Include(d => d.ThanhToans)
                    .ThenInclude(t => t.TrangThaiThanhToan)
                .Where(d => donHangIds.Contains(d.Id))
                .ToList();
        }
        
    }
}
