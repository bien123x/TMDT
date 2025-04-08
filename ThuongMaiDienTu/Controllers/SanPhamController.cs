using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThuongMaiDienTu.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly DbContextApp _context;
        public SanPhamController(ISanPhamRepository sanPhamRepository, DbContextApp context)
        {
            _sanPhamRepository = sanPhamRepository;
            _context = context;
        }
    
        // GET: SanPhamController
        public IActionResult Index(int? storeId)
        {
            if (storeId.HasValue)
            {
                // Kiểm tra xem cửa hàng có tồn tại không
                var store = _context.CuaHangs.FirstOrDefault(ch => ch.Id == storeId.Value);
                if (store == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy cửa hàng
                }

                // Lấy thông tin cửa hàng và đổ vào ViewBag để hiển thị trên view
                ViewBag.StoreName = store.Ten_Cua_Hang;
                ViewBag.StoreId = store.Id;
                ViewBag.StoreDescription = store.Mo_Ta;

                // Lấy danh sách sản phẩm của cửa hàng
                var sanPhamCuaHang = _sanPhamRepository.GetAll()
                    .Where(sp => sp.Id_Cua_Hang == storeId.Value)
                    .ToList();

                return View(sanPhamCuaHang);
            }
            var isSeller = HttpContext.Session.GetInt32("IsSeller");
            var userId = HttpContext.Session.GetInt32("UserId");
            var cuaHangId = HttpContext.Session.GetInt32("StoreId");
            if (isSeller == 1 && cuaHangId.HasValue) {
                // Lấy thông tin cửa hàng của người bán để hiển thị
                var store = _context.CuaHangs.FirstOrDefault(ch => ch.Id == cuaHangId.Value);
                if (store != null)
                {
                    ViewBag.StoreName = store.Ten_Cua_Hang;
                    ViewBag.StoreId = store.Id;
                    ViewBag.StoreDescription = store.Mo_Ta;
                }
                
                var sanPham = _sanPhamRepository.GetAll().Where(sp => sp.Id_Cua_Hang == cuaHangId).ToList();
                return View(sanPham);
            }
            var sanPhams =  _sanPhamRepository.GetAll().ToList();
            return View(sanPhams);
        }
        public SelectList GetListDanhMuc()
        {
            var danhMuc = _context.DanhMucs.ToList();
            var danhMucList = danhMuc.Select(dm => new SelectListItem
            {
                Value = dm.Id.ToString(),
                Text = dm.Ten_Danh_Muc
            });
            return new SelectList(danhMucList, "Value", "Text");
        }
        public IActionResult Create()
        {
            ViewBag.DanhMuc = GetListDanhMuc();
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.Id_Cua_Hang = (int) HttpContext.Session.GetInt32("StoreId");

                if (sanPham.HinhAnhFile != null && sanPham.HinhAnhFile.Length > 0)
                {
                    string fileName = Path.GetFileName(sanPham.HinhAnhFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\SanPham", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        sanPham.HinhAnhFile.CopyTo(stream);
                    }

                    sanPham.Hinh_Anh = "/images/SanPham/" + fileName;
                }

                _sanPhamRepository.Add(sanPham);

                return RedirectToAction("Index", "SanPham");
            }
            ViewBag.DanhMuc = GetListDanhMuc();
            return View(sanPham);
        }
        public IActionResult Edit(int id)
        {
            var sanPham =  _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
        [HttpPost]
        public IActionResult Edit(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                 _sanPhamRepository.Update(sanPham);
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            _sanPhamRepository.Delete(id);
            return Json(new {message = "Xóa Thành Công"});
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _sanPhamRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult ChiTietSanPham(int id)
        {
            var sanPham = _sanPhamRepository.GetSanPhamWithDanhGia(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            // ✅ Lấy danh sách đánh giá
            var danhGiaList = sanPham.DanhGias.ToList();
            double trungBinhSao = danhGiaList.Any() ? danhGiaList.Average(d => d.So_Sao) : 0;
            int tongDanhGia = danhGiaList.Count();

            ViewBag.TrungBinhSao = trungBinhSao;
            ViewBag.TongDanhGia = tongDanhGia;
            ViewBag.DanhGias = danhGiaList;

            return View(sanPham);
        }

    }
}
