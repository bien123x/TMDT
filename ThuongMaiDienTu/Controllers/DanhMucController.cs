using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly IDanhMucRepository _danhMucRepository;

        public DanhMucController(IDanhMucRepository danhMucRepository)
        {
            _danhMucRepository = danhMucRepository;
        }

        // GET: DanhMuc
        public ActionResult Index()
        {
            var danhMucs = _danhMucRepository.GetAllDanhMucs();
            return View(danhMucs);
        }

        //GET: DanhMuc/Details/5
        // public async Task<ActionResult> Details(int id)
        // {
        //     var danhMucs = await _danhMucRepository.GetDanhMucByIdAsync(id);
        //     if (danhMucs == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(danhMucs);
        // }

        // GET: DanhMuc/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: DanhMuc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten_Danh_Muc")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                await _danhMucRepository.AddDanhMucAsync(danhMuc);
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }


        // GET: DanhMuc/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }
        // POST: DanhMuc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten_Danh_Muc")] DanhMuc danhMuc)
        {
            if (id != danhMuc.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _danhMucRepository.UpdateDanhMucAsync(danhMuc);
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }

        //GET: DanhMuc/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        //POST: DanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _danhMucRepository.DeleteDanhMucAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //GET: DanhMuc/UpdateTrangThai/5
        public async Task<ActionResult> UpdateTrangThai(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }
        //POST: DanhMuc/UpdateTrangThai/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTrangThaiCF(int id)
        {
            await _danhMucRepository.UpdateTrangThai(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: DanhMuc/ToggleStatus/5
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            await _danhMucRepository.UpdateTrangThai(id);

            // Hiển thị thông báo thành công
            string message = danhMuc.Trang_Thai ?
                $"Đã khóa danh mục {danhMuc.Ten_Danh_Muc}" :
                $"Đã mở khóa danh mục {danhMuc.Ten_Danh_Muc}";

            TempData["StatusMessage"] = message;

            return RedirectToAction(nameof(Index));
        }
        // Thêm phương thức CreateAjax
        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors });
            }

            try
            {
                // Đặt trạng thái mặc định là true (hoạt động)
                danhMuc.Trang_Thai = true;

                // Thêm danh mục mới
                await _danhMucRepository.AddDanhMucAsync(danhMuc);

                return Json(new
                {
                    success = true,
                    message = "Thêm danh mục thành công!",
                    data = new
                    {
                        id = danhMuc.Id,
                        ten_Danh_Muc = danhMuc.Ten_Danh_Muc,
                        trang_Thai = danhMuc.Trang_Thai
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi khi thêm danh mục: " + ex.Message
                });
            }
        }
        // Thêm các phương thức Ajax mới sau CreateAjax

        [HttpPut]
        public async Task<IActionResult> EditAjax([FromBody] DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors });
            }

            try
            {
                var existingDanhMuc = await _danhMucRepository.GetDanhMucByIdAsync(danhMuc.Id);
                if (existingDanhMuc == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy danh mục" });
                }

                // Cập nhật thông tin
                existingDanhMuc.Ten_Danh_Muc = danhMuc.Ten_Danh_Muc;
                // Giữ nguyên trạng thái

                // Cập nhật danh mục
                await _danhMucRepository.UpdateDanhMucAsync(existingDanhMuc);

                return Json(new
                {
                    success = true,
                    message = "Cập nhật danh mục thành công!",
                    data = new
                    {
                        id = existingDanhMuc.Id,
                        ten_Danh_Muc = existingDanhMuc.Ten_Danh_Muc,
                        trang_Thai = existingDanhMuc.Trang_Thai
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi khi cập nhật danh mục: " + ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            try
            {
                var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
                if (danhMuc == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy danh mục" });
                }

                // Xóa danh mục
                await _danhMucRepository.DeleteDanhMucAsync(id);

                return Json(new
                {
                    success = true,
                    message = $"Đã xóa danh mục '{danhMuc.Ten_Danh_Muc}' thành công!"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi khi xóa danh mục: " + ex.Message
                });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ToggleStatusAjax(int id)
        {
            try
            {
                var danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);
                if (danhMuc == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy danh mục" });
                }

                // Lưu trạng thái trước khi cập nhật để hiển thị thông báo
                bool oldStatus = danhMuc.Trang_Thai;

                // Đổi trạng thái
                await _danhMucRepository.UpdateTrangThai(id);

                // Lấy danh mục sau khi cập nhật trạng thái
                danhMuc = await _danhMucRepository.GetDanhMucByIdAsync(id);

                string action = oldStatus ? "khóa" : "mở khóa";

                return Json(new
                {
                    success = true,
                    message = $"Đã {action} danh mục '{danhMuc.Ten_Danh_Muc}' thành công!",
                    data = new
                    {
                        id = danhMuc.Id,
                        ten_Danh_Muc = danhMuc.Ten_Danh_Muc,
                        trang_Thai = danhMuc.Trang_Thai
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi khi thay đổi trạng thái danh mục: " + ex.Message
                });
            }
        }
    }
    

    
}
