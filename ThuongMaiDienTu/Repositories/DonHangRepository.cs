﻿using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<DonHang> GetAllAndInfor()
        {
            return _context.DonHangs
            .Include(dh => dh.TrangThaiDonHang)
            .Include(dh => dh.VanChuyens)
                .ThenInclude(vc => vc.TrangThaiVanChuyen)
            .Include(dh => dh.ThanhToans)
                .ThenInclude(tt => tt.TrangThaiThanhToan)
            .ToList();
        }
    }
}
