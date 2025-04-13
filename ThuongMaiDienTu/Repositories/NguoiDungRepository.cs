using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.ViewModels;

public class NguoiDungRepository : Repository<NguoiDung>, INguoiDungRepository
{
    private readonly DbContextApp _context;

    public NguoiDungRepository(DbContextApp context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<NguoiDungViewModel> GetAllWithDetails()
    {
        // Sử dụng một truy vấn JOIN duy nhất
        var nguoiDungs = _context.NguoiDungs
            .Include(n => n.VaiTro)
            .GroupJoin(
                _context.CuaHangs,
                nguoiDung => nguoiDung.Id,
                cuaHang => cuaHang.Id_Nguoi_Ban,
                (nguoiDung, cuaHangs) => new { NguoiDung = nguoiDung, CuaHangs = cuaHangs }
            )
            .SelectMany(
                x => x.CuaHangs.DefaultIfEmpty(),
                (nguoiDungGroup, cuaHang) => new NguoiDungViewModel
                {
                    Id = nguoiDungGroup.NguoiDung.Id,
                    Ho_Ten = nguoiDungGroup.NguoiDung.Ho_Ten,
                    Email = nguoiDungGroup.NguoiDung.Email,
                    So_Dien_Thoai = nguoiDungGroup.NguoiDung.So_Dien_Thoai,
                    Ten_Vai_Tro = nguoiDungGroup.NguoiDung.VaiTro != null ? nguoiDungGroup.NguoiDung.VaiTro.Ten_vai_Tro : "Không xác định",
                    Vai_Tro_Id = nguoiDungGroup.NguoiDung.Vai_Tro_Id,
                    Ngay_Tao = nguoiDungGroup.NguoiDung.Ngay_Tao,
                    Trang_Thai = nguoiDungGroup.NguoiDung.Trang_Thai,
                    HasStore = cuaHang != null,
                    StoreId = cuaHang != null ? cuaHang.Id : 0,
                    Mo_Ta = nguoiDungGroup.NguoiDung.Mo_Ta
                }
            )
            .ToList();

        return nguoiDungs;
    }
    
    public NguoiDungViewModel GetNguoiDungViewModelById(int id)
    {
        return _context.NguoiDungs
        .Where(n => n.Id == id)
        .Include(n => n.VaiTro)
        .GroupJoin(
            _context.CuaHangs,
            n => n.Id,
            c => c.Id_Nguoi_Ban,
            (nguoiDung, cuaHangs) => new { NguoiDung = nguoiDung, CuaHang = cuaHangs.FirstOrDefault() }
        )
        .Select(x => new NguoiDungViewModel
        {
            Id = x.NguoiDung.Id,
            Ho_Ten = x.NguoiDung.Ho_Ten,
            Email = x.NguoiDung.Email,
            So_Dien_Thoai = x.NguoiDung.So_Dien_Thoai,
            Ten_Vai_Tro = x.NguoiDung.VaiTro != null ? x.NguoiDung.VaiTro.Ten_vai_Tro : "Không xác định",
            Vai_Tro_Id = x.NguoiDung.Vai_Tro_Id,
            Ngay_Tao = x.NguoiDung.Ngay_Tao,
            Trang_Thai = x.NguoiDung.Trang_Thai,
            HasStore = x.CuaHang != null,
            StoreId = x.CuaHang != null ? x.CuaHang.Id : 0
        })
        .FirstOrDefault();
        
    }

    public NguoiDung GetNguoiDungWithVaiTro(int id)
    {
        return _context.NguoiDungs
            .Include(n => n.VaiTro)
            .FirstOrDefault(n => n.Id == id);
    }

    public IEnumerable<NguoiDung> GetNguoiDungByVaiTro(int vaiTroId)
    {
        return _context.NguoiDungs
            .Include(n => n.VaiTro)
            .Where(n => n.Vai_Tro_Id == vaiTroId)
            .ToList();
    }
    
    public CuaHang GetCuaHangByNguoiDungId(int nguoiDungId)
    {
        return _context.CuaHangs
            .FirstOrDefault(c => c.Id_Nguoi_Ban == nguoiDungId);
    }

    public bool ToggleTrangThai(int id)
    {
        var nguoiDung = _context.NguoiDungs.Find(id);
        if (nguoiDung != null)
        {
            nguoiDung.Trang_Thai = !nguoiDung.Trang_Thai;
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool ChangeVaiTro(int id, int vaiTroId)
    {
        var nguoiDung = _context.NguoiDungs.Find(id);
        if (nguoiDung != null)
        {
            nguoiDung.Vai_Tro_Id = vaiTroId;
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    // Ghi đè các phương thức của Repository<NguoiDung>
    // Sử dụng từ khóa new để tránh cảnh báo


    public new NguoiDung GetById(int id)
    {
        return base.GetById(id);
    }

    public new void Add(NguoiDung entity)
    {
        base.Add(entity);
    }

    public new void Update(NguoiDung entity)
    {
        base.Update(entity);
    }

    public new void Delete(int id)
    {
        base.Delete(id);
    }
}