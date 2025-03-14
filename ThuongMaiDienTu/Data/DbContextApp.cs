using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Data
{
    public class DbContextApp : DbContext
    {
        public DbContextApp(DbContextOptions<DbContextApp> options) : base(options) { }

        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<CuaHang> CuaHangs { get; set; }
    }
}
