using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class StoreController : Controller
    {
        private IRepository<CuaHang> _repository;
        private DbContextApp _context;

        public StoreController(IRepository<CuaHang> repository, DbContextApp context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            var storeList = _repository.GetAll().ToList();
            return View(storeList);
        }

        public IActionResult Create()
        {
            ViewBag.SellerId = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(cuaHang);
                var ch = _repository.GetAll().ToList().FirstOrDefault(ch=> ch.Id_Nguoi_Ban == cuaHang.Id_Nguoi_Ban);
                HttpContext.Session.SetInt32("StoreId", ch.Id);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Edit()
        {
            var storeId = (int)HttpContext.Session.GetInt32("StoreId");
            return View(_repository.GetById(storeId));
        }

        [HttpPatch]
        [Route("Store/Edit/{id}")]
        public IActionResult Edit(int id, [FromBody] CuaHang cuaHangSua)
        {
            ModelState.Clear();
            TryValidateModel(cuaHangSua, nameof(cuaHangSua.Ten_Cua_Hang));
            TryValidateModel(cuaHangSua, nameof(cuaHangSua.Mo_Ta));
            if (ModelState.IsValid)
            {
                var cuaHang = _repository.GetById(id);
                if (cuaHang == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy cửa hàng" });
                }

                cuaHang.Ten_Cua_Hang = cuaHangSua.Ten_Cua_Hang;
                cuaHang.Mo_Ta = cuaHangSua.Mo_Ta;

                _repository.Update(cuaHang);
                return Json(new { success = true, message = "Cập nhật thành công!" });
            }

            return Json(new { success = false, message = "Cập nhật thất bại", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
        }
        [HttpGet]
        public IActionResult GetStoreData(int id)
        {
            var store = _repository.GetById(id);
            if (store == null)
            {
                return NotFound();
            }

            return Json(new
            {
                id = store.Id,
                tenCuaHang = store.Ten_Cua_Hang,
                moTa = store.Mo_Ta,
                trangThai = store.Trang_Thai
            });
        }

        [HttpPost]
        public IActionResult ToggleStoreStatus([FromBody]  int id)
        {
            var store = _repository.GetById(id);
            if (store == null)
            {
                return Json(new { success = false, message = "Không tìm thấy cửa hàng" });
            }

            // Đảo ngược trạng thái
            store.Trang_Thai = !store.Trang_Thai;
            _repository.Update(store);

            string message = store.Trang_Thai ? "Cửa hàng đã được mở khóa" : "Cửa hàng đã bị khóa";
            return Json(new { success = true, message = message, newStatus = store.Trang_Thai });
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

        [HttpGet("Store/ProfileStore/{id}")]
        public IActionResult ProfileStore(int id)
        {
            // Lấy thông tin cửa hàng từ id được truyền vào
            var store = _repository.GetById(id);

            // Kiểm tra nếu không tìm thấy cửa hàng
            if (store == null)
            {
                return NotFound(); // Trả về 404 Not Found nếu không tìm thấy
            }

            // Lấy thông tin chi tiết về người bán (chủ cửa hàng)
            var storeSeller = _context.NguoiDungs.FirstOrDefault(u => u.Id == store.Id_Nguoi_Ban);
            if (storeSeller != null)
            {
                ViewBag.StoreSeller = storeSeller;
            }

            // Lấy số lượng sản phẩm của cửa hàng
            var productCount = _context.SanPhams.Count(p => p.Id_Cua_Hang == id);
            ViewBag.ProductCount = productCount;

            // Lấy đánh giá trung bình của cửa hàng (qua các sản phẩm)
            var storeProducts = _context.SanPhams.Where(p => p.Id_Cua_Hang == id).Select(p => p.Id).ToList();
            if (storeProducts.Any())
            {
                var averageRating = _context.DanhGias
                    .Where(r => storeProducts.Contains(r.Id_San_Pham))
                    .Average(r => (double?)r.So_Sao) ?? 0;
                ViewBag.AverageRating = Math.Round(averageRating, 1);

                // Lấy tổng số đánh giá
                var reviewCount = _context.DanhGias.Count(r => storeProducts.Contains(r.Id_San_Pham));
                ViewBag.ReviewCount = reviewCount;
            }
            else
            {
                ViewBag.AverageRating = 0;
                ViewBag.ReviewCount = 0;
            }

            // Trả về view với mô hình cửa hàng
            return View(store);
        }
    }
}
