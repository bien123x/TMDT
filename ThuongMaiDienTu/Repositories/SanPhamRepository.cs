﻿using System;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories;

public class SanPhamRepository : Repository<SanPham> , ISanPhamRepository
{
    private readonly DbContextApp _context;
    public SanPhamRepository(DbContextApp context) : base(context)
    {
        _context = context;
    }
    public IEnumerable<SanPham> GetSanPhamsByDanhMucId(int danhMucId)
    {
        return _context.SanPhams.Where(s => s.Id_Danh_Muc == danhMucId).ToList();
    }
    public List<SanPham> GetAllSanPhams() // Phải khớp với ISanPhamRepository
    {
        return _context.SanPhams.ToList();
    }

    public List<SanPham> TimKiemSanPham(string tuKhoa)
    {
        return _context.SanPhams
            .Where(s => s.Ten_San_Pham.Contains(tuKhoa) || s.Mo_Ta.Contains(tuKhoa))
            .ToList();
    }

}

