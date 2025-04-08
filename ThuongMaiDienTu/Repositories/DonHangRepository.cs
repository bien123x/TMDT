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
    }
}
