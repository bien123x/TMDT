using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<NguoiDung> _repository;
        private readonly DbContextApp _context;
        public AccountController(IRepository<NguoiDung> repository, DbContextApp context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kiểm tra username có tồn tại không
            var userByCredential = _repository.GetAll().Where(nd =>
                (nd.So_Dien_Thoai == username || nd.Email == username));

            if (!userByCredential.Any())
            {
                ViewBag.Error = "Tài khoản không tồn tại!";
                return View();
            }

            // Kiểm tra trạng thái tài khoản
            var user = userByCredential.FirstOrDefault();
            if (user.Trang_Thai != true)
            {
                ViewBag.Error = "Tài khoản đã bị khóa hoặc vô hiệu hóa!";
                return View();
            }

            // Kiểm tra mật khẩu
            if (!BCrypt.Net.BCrypt.Verify(password, user.Mat_Khau))
            {
                ViewBag.Error = "Mật khẩu không chính xác!";
                return View();
            }

            // Đăng nhập thành công - giữ nguyên logic hiện tại
            var userId = user.Id;
            HttpContext.Session.SetInt32("UserId", userId);
            HttpContext.Session.SetInt32("VaiTroId", user.Vai_Tro_Id);

            var checkSeller = user.Vai_Tro_Id == 2;
            var checkAdmin = user.Vai_Tro_Id == 3;
            var checkDVVC = user.Vai_Tro_Id == 4;

            // Logic phân quyền người dùng
            if (checkSeller)
            {
                HttpContext.Session.SetInt32("IsSeller", 1);
                if (!_context.CuaHangs.Any(ch => ch.Id_Nguoi_Ban == userId))
                {
                    return RedirectToAction("Create", "Store");
                }
                else
                {
                    var cuaHang = _context.CuaHangs.FirstOrDefault(ch => ch.Id_Nguoi_Ban == userId);
                    HttpContext.Session.SetInt32("StoreId", cuaHang.Id);
                }
            }

            if (checkAdmin)
            {
                HttpContext.Session.SetInt32("IsAdmin", 1);
                return RedirectToAction("Index", "Home");
            }
            if (checkDVVC)
            {
                HttpContext.Session.SetInt32("IsDelivery", 4);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Register(NguoiDung nguoiDung)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return View(nguoiDung);
            }

            // Kiểm tra email đã tồn tại
            if (_context.NguoiDungs.Any(u => u.Email == nguoiDung.Email))
            {
                TempData["ErrorMessage"] = "Email đã được sử dụng";
                return View(nguoiDung);
            }

            // Mã hóa mật khẩu
            nguoiDung.Mat_Khau = BCrypt.Net.BCrypt.HashPassword(nguoiDung.Mat_Khau);
            nguoiDung.Ngay_Tao = DateTime.Now;

            _repository.Add(nguoiDung);

            var newUser = _context.NguoiDungs
            .Where(u => u.Email == nguoiDung.Email)
            .OrderByDescending(u => u.Id)
            .FirstOrDefault();

            if (newUser != null)
            {
                // Lưu ID vào session để sử dụng trong StoreController
                HttpContext.Session.SetInt32("UserId", newUser.Id);
                HttpContext.Session.SetInt32("VaiTroId", newUser.Vai_Tro_Id);

                if (newUser.Vai_Tro_Id == 2)
                {
                    // Lưu thêm vào TempData để đảm bảo dữ liệu có sẵn sau khi chuyển hướng
                    TempData["SellerId"] = newUser.Id;
                    HttpContext.Session.SetInt32("IsSeller", 1);
                    return RedirectToAction("Create", "Store");
                }
            }


            TempData["SuccessMessage"] = "Đăng ký thành công!";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            // Lấy UserId từ Session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // Lấy thông tin người dùng từ database
            var user = _context.NguoiDungs.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(); // Nếu không tìm thấy người dùng
            }

            var checkStore = _context.CuaHangs.Count(u => u.Id_Nguoi_Ban == userId);

            if (user.Vai_Tro_Id == 2 && checkStore == 0)
            {
                HttpContext.Session.SetInt32("SellerId", user.Id);
                ViewBag.NoStore = true;
            }

            return View(user);
        }

        public IActionResult EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var nguoiDung = _context.NguoiDungs.Find(userId);
            return View(nguoiDung);
        }

        [HttpPatch]
        public IActionResult EditProfile([FromBody] NguoiDung nguoiDung)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            var user = _context.NguoiDungs.FirstOrDefault(u => u.Id == userId);

            // Kiểm tra email đã tồn tại
            if (_context.NguoiDungs.Where(u => u.Email != nguoiDung.Email).Any(u => u.Email == nguoiDung.Email))
            {
                return Json(new { success = false, message = "Email đã được sử dụng" });
            }

            user.Ho_Ten = nguoiDung.Ho_Ten;
            user.Email = nguoiDung.Email;
            user.So_Dien_Thoai = nguoiDung.So_Dien_Thoai;

            _repository.Update(user);
            return Json(new { success = true, message = "Sửa thông tin thành công!" });
        }
        
    }
}
