using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class VanChuyenController : Controller
    {
        private readonly IDonHangRepository _donHangRepository;
        private readonly IVanChuyenRepository _vanChuyenRepository;
        private readonly IThanhToanRepository _thanhToanRepository;
        private readonly DbContextApp _context;

        public VanChuyenController(
            IDonHangRepository donHangRepository,
            IVanChuyenRepository vanChuyenRepository,
            IThanhToanRepository thanhToanRepository, DbContextApp context)
        {
            _donHangRepository = donHangRepository;
            _vanChuyenRepository = vanChuyenRepository;
            _thanhToanRepository = thanhToanRepository;
            _context = context;
        }

        // Kiểm tra quyền vận chuyển
        private void CheckDeliveryPermission()
        {
            var isDelivery = HttpContext.Session.GetInt32("IsDelivery");
            if (isDelivery == null || isDelivery != 4)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền truy cập chức năng này.");
            }
        }
        // GET: VanChuyenController
        public ActionResult Index()
        {
            try
            {
                CheckDeliveryPermission();

                // Chỉ lấy những đơn hàng đang chờ vận chuyển
                var donHangList = _donHangRepository.GetDonHangChoVanChuyen();

                // Get all transport statuses for the dropdown
                var trangThaiVanChuyens = _context.TrangThaiVanChuyens.ToList();
                ViewBag.TrangThaiVanChuyens = trangThaiVanChuyens;

                var trangThaiThanhToans = _context.TrangThaiThanhToans.ToList();
                ViewBag.TrangThaiThanhToans = trangThaiThanhToans;
                return View(donHangList);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        /// <summary>
        /// Xem chi tiết đơn hàng (được gọi qua AJAX để hiển thị trong modal)
        /// </summary>
        /// <param name="id">Id của đơn hàng cần xem chi tiết</param>
        /// <returns>Partial view chứa thông tin chi tiết đơn hàng</returns>
        // public IActionResult ChiTietDonHang(int id)
        // {
        //     try
        //     {
        //         CheckDeliveryPermission();

        //         // Lấy thông tin đơn hàng với đầy đủ chi tiết từ repository
        //         var donHang = _donHangRepository.GetDonHangWithDetails(id);

        //         if (donHang == null)
        //         {
        //             return Content("<div class='alert alert-danger'><i class='fas fa-exclamation-circle me-2'></i>Không tìm thấy đơn hàng</div>");
        //         }

        //         // Lấy thông tin vận chuyển mới nhất của đơn hàng (nếu có)
        //         var vanChuyenHienTai = _context.VanChuyens
        //             .Where(vc => vc.Id_Don_Hang == id)
        //             .OrderByDescending(vc => vc.Ngay_Cap_Nhat)
        //             .FirstOrDefault();

        //         // Truyền dữ liệu qua ViewBag
        //         ViewBag.VanChuyenHienTai = vanChuyenHienTai;

        //         // Trả về partial view để hiển thị trong modal
        //         return PartialView("_ChiTietDonHang", donHang);
        //     }
        //     catch (UnauthorizedAccessException)
        //     {
        //         return Content("<div class='alert alert-danger'><i class='fas fa-lock me-2'></i>Bạn không có quyền xem thông tin này</div>");
        //     }
        //     catch (Exception ex)
        //     {
        //         return Content($"<div class='alert alert-danger'><i class='fas fa-exclamation-triangle me-2'></i>Đã xảy ra lỗi: {ex.Message}</div>");
        //     }
        }

    }

