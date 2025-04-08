using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Helpers;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class DonHangController : Controller
    {
        private readonly IDonHangRepository _repository;
        private readonly DbContextApp _context;
        public DonHangController(IDonHangRepository repository, DbContextApp context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var isSeller = HttpContext.Session.GetInt32("IsSeller");
            ViewBag.IsSeller = isSeller;

            IEnumerable<DonHang> donHangs;
            if (isSeller.HasValue && isSeller.Value == 1)
            {
                donHangs = _repository.GetAllAndInfor(userId);
            }
            else
            {
                donHangs = _repository.GetAllAndInfor();
            }

            return View(donHangs);
        }
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var listSanPham = CartHelper.GetCart(HttpContext.Session);

            var groupedByStore = listSanPham.GroupBy(sp => sp.Id_Cua_Hang);

            foreach (var group in groupedByStore)
            {
                DonHang donHang = new DonHang();
                donHang.Id_Nguoi_Mua = userId;
                donHang.Tong_Tien = group.Sum(sp => (decimal) sp.Gia_Khuyen_Mai * sp.So_Luong_Ton);
                donHang.Trang_Thai_Id = 1;
                donHang.Ngay_Tao = DateTime.Now;

                _repository.Add(donHang);

                _repository.AddListSanPham(group.ToList(), donHang.Id);

                var vanChuyen = new VanChuyen();
                vanChuyen.Id_Don_Hang = donHang.Id;
                vanChuyen.Trang_Thai_Id = 1;
                vanChuyen.Ngay_Cap_Nhat = DateTime.Now;
                _repository.AddVanChuyen(vanChuyen);

                var thanhToan = new ThanhToan();
                thanhToan.Id_Don_Hang = donHang.Id;
                thanhToan.Phuong_Thuc_Id = 1;
                thanhToan.Trang_Thai_Id = 1;
                thanhToan.Ngay_Tao = DateTime.Now;
                _repository.AddThanhToan(thanhToan);
            }

            return RedirectToAction("Index", "DonHang");
        }

        public IActionResult Details(int id)
        {
            var chiTietDonHangs = _context.ChiTietDonHangs
                .Where(ct => ct.Id_Don_Hang == id)
                .Include(ct => ct.SanPham)
                .ToList();

            return View(chiTietDonHangs);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]int orderId)
        {
            var donHang = _repository.GetById(orderId);

            var chitietDonHang = _context.ChiTietDonHangs.Where(ct => ct.Id_Don_Hang == orderId);
            _context.ChiTietDonHangs.RemoveRange(chitietDonHang);

            var vanChuyens = _context.VanChuyens.Where(vc => vc.Id_Don_Hang == orderId);
            _context.VanChuyens.RemoveRange(vanChuyens);

            var thanhToans = _context.ThanhToans.Where(tt => tt.Id_Don_Hang == orderId);
            _context.ThanhToans.RemoveRange(thanhToans);

            _context.DonHangs.Remove(donHang);

            _context.SaveChanges();
            return Json(new { message = "Xóa Thành Công" });
        }

        [HttpPost]
        public IActionResult UpdateStatus([FromBody] int id)
        {
            var donHang = _repository.GetById(id);
            if (donHang == null)
            {
                return NotFound();
            }

            donHang.Trang_Thai_Id = 2;
            _repository.Update(donHang);

            return Json(new { message = "Cập nhật trạng thái thành công" });
        }
    }
}
