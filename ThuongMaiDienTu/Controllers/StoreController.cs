using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class StoreController : Controller
    {
        private IRepository<CuaHang> _repository;

        public StoreController(IRepository<CuaHang> repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.SellerId = HttpContext.Session.GetInt32("SellerId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CuaHang cuaHang)
        {
            if (ModelState.IsValid) {
                _repository.Add(cuaHang);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Profile()
        {
            // Lấy UserId từ Session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // Lấy thông tin người dùng từ database
            var store = _repository.GetAll().Where(ch => ch.Id_Nguoi_Ban == userId).FirstOrDefault();
            if (store == null)
            {
                return NotFound(); // Nếu không tìm thấy người dùng
            }

            return View(store);
        }
    }
}
