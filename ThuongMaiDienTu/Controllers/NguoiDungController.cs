using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.ViewModels;


public class NguoiDungController : Controller
{
    private readonly INguoiDungRepository _nguoiDungRepository;
    private readonly IRepository<VaiTro> _vaiTroRepository;
    private readonly Microsoft.AspNetCore.Identity.PasswordHasher<NguoiDung> _passwordHasher = new();
    private readonly DbContextApp _context;

    public NguoiDungController(
        INguoiDungRepository nguoiDungRepository,
        IRepository<VaiTro> vaiTroRepository,
        DbContextApp context)
    {
        _nguoiDungRepository = nguoiDungRepository;
        _vaiTroRepository = vaiTroRepository;
        _context = context;
    }


    // Hiển thị danh sách người dùng
    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        // Kiểm tra người dùng đăng nhập có quyền admin
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin != 1)
        {
            return RedirectToAction("NotAllow", "Home");
        }

        var nguoiDungs = _nguoiDungRepository.GetAllWithDetails();


        // Thông tin phân trang
        int totalItems = nguoiDungs.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        // Đảm bảo page nằm trong khoảng hợp lệ
        page = Math.Max(1, Math.Min(page, totalPages));

        // Thực hiện phân trang
        var pagedNguoiDungs = nguoiDungs
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Truyền thông tin phân trang cho view
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalItems;

        return View(pagedNguoiDungs);
    }

    public IActionResult DSNguoiDung(int page = 1, int pageSize = 10)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("NotAllow", "Home");
        }
        var nguoiDungs = _nguoiDungRepository.GetAllWithDetails();
        // Thông tin phân trang
        int totalItems = nguoiDungs.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        // Đảm bảo page nằm trong khoảng hợp lệ
        page = Math.Max(1, Math.Min(page, totalPages));

        // Thực hiện phân trang
        var pagedNguoiDungs = nguoiDungs
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Truyền thông tin phân trang cho view
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalItems;

        return View(pagedNguoiDungs);

    }

    // Xem chi tiết người dùng
    public IActionResult Details(int id)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("NotAllow", "Home");
        }

        var viewModel = _nguoiDungRepository.GetNguoiDungViewModelById(id);
        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    // Thêm người dùng mới - GET
    public IActionResult Create()
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.VaiTros = _vaiTroRepository.GetAll()
        .Select(vt => new SelectListItem
        {
            Value = vt.Id.ToString(),
            Text = vt.Ten_vai_Tro
        }).ToList();

        var viewModel = new NguoiDungCreateViewModel
        {
            Trang_Thai = true,
            VaiTroList = _vaiTroRepository.GetAll()
            .Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Ten_vai_Tro
            })
        };

        return View(viewModel);
    }

    // Thêm người dùng mới - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NguoiDungCreateViewModel viewModel)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
        }
        //Khởi tạo danh sách vai trò cho dropdown
        viewModel.VaiTroList = LoadVaiTroList(viewModel.Vai_Tro_Id);

        // Kiểm tra tính hợp lệ của dữ liệu 
        ValidationNguoiDungCreate(viewModel);

        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Thêm thông báo tổng quát
            TempData["ErrorMessage"] = $"Vui lòng kiểm tra lại thông tin đã nhập: {string.Join(", ", errorMessages)}";

            return View(viewModel);
        }

        try
        {
            // Tạo đối tượng NguoiDung từ viewModel
            var nguoiDung = new NguoiDung
            {
                Ho_Ten = viewModel.Ho_Ten.Trim(),
                Email = viewModel.Email.Trim(),
                // Mã hóa mật khẩu trước khi lưu
                Mat_Khau = BCrypt.Net.BCrypt.HashPassword(viewModel.Mat_Khau),
                So_Dien_Thoai = viewModel.So_Dien_Thoai?.Trim(),
                Vai_Tro_Id = viewModel.Vai_Tro_Id,
                Ngay_Tao = DateTime.Now,
                Mo_Ta =  viewModel.Mo_Ta,
                Trang_Thai = viewModel.Trang_Thai
            };

            // Lưu vào cơ sở dữ liệu
            _nguoiDungRepository.Add(nguoiDung);
            TempData["StatusMessage"] = "Thêm người dùng thành công!";
            return RedirectToAction("DSNguoiDung");
            // Thông báo thành công
            // TempData["StatusMessage"] = "Tạo tài khoản người dùng thành công!";
            // return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.VaiTros = _vaiTroRepository.GetAll()
            .Select(vt => new SelectListItem
            {
                Value = vt.Id.ToString(),
                Text = vt.Ten_vai_Tro
            }).ToList();
            // Xử lý ngoại lệ và ghi log
            ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
            return View(viewModel);
        }
    }

    private void ValidationNguoiDungCreate(NguoiDungCreateViewModel viewModel)
    {
        //Kiểm tra mật khẩu 
        if (viewModel.Mat_Khau == null || viewModel.Mat_Khau.Length < 6)
        {
            ModelState.AddModelError("Mat_Khau", "Mật khẩu phải có ít nhất 6 ký tự");
        }

        // Kiểm tra định dạng email hợp lệ
        if (!string.IsNullOrEmpty(viewModel.Email) && !IsValidEmail(viewModel.Email))
        {
            ModelState.AddModelError("Email", "Email không đúng định dạng");
        }

        // Kiểm tra định dạng số điện thoại
        if (!string.IsNullOrEmpty(viewModel.So_Dien_Thoai) && !IsValidPhoneNumber(viewModel.So_Dien_Thoai))
        {
            ModelState.AddModelError("So_Dien_Thoai", "Số điện thoại không đúng định dạng");
        }

        // Kiểm tra email đã tồn tại
        if (!string.IsNullOrEmpty(viewModel.Email) && _context.NguoiDungs.Any(n => n.Email == viewModel.Email))
        {
            ModelState.AddModelError("Email", "Email đã được sử dụng, vui lòng chọn email khác");
        }

        // Kiểm tra số điện thoại đã tồn tại
        if (!string.IsNullOrEmpty(viewModel.So_Dien_Thoai) && _context.NguoiDungs.Any(n => n.So_Dien_Thoai == viewModel.So_Dien_Thoai))
        {
            ModelState.AddModelError("So_Dien_Thoai", "Số điện thoại đã được sử dụng, vui lòng nhập số khác");
        }

        // Kiểm tra vai trò hợp lệ
        if (viewModel.Vai_Tro_Id <= 0 || !_context.VaiTros.Any(v => v.Id == viewModel.Vai_Tro_Id))
        {
            ModelState.AddModelError("Vai_Tro_Id", "Vui lòng chọn vai trò hợp lệ");
        }
    }
    // Phương thức xác thực cho Edit
    private void ValidateNguoiDungEdit(NguoiDungEditViewModel viewModel, int id)
    {
        // Kiểm tra mật khẩu thỏa mãn độ phức tạp nếu được nhập
        if (!string.IsNullOrEmpty(viewModel.Mat_Khau) && viewModel.Mat_Khau.Length < 6)
        {
            ModelState.AddModelError("Mat_Khau", "Mật khẩu phải có ít nhất 6 ký tự");
        }

        // Kiểm tra định dạng email hợp lệ
        if (!string.IsNullOrEmpty(viewModel.Email) && !IsValidEmail(viewModel.Email))
        {
            ModelState.AddModelError("Email", "Email không đúng định dạng");
        }

        // Kiểm tra định dạng số điện thoại
        if (!string.IsNullOrEmpty(viewModel.So_Dien_Thoai) && !IsValidPhoneNumber(viewModel.So_Dien_Thoai))
        {
            ModelState.AddModelError("So_Dien_Thoai", "Số điện thoại không đúng định dạng");
        }

        // Kiểm tra email đã tồn tại (trừ người dùng hiện tại)
        if (!string.IsNullOrEmpty(viewModel.Email) && _context.NguoiDungs.Any(n => n.Email == viewModel.Email && n.Id != id))
        {
            ModelState.AddModelError("Email", "Email đã được sử dụng, vui lòng chọn email khác");
        }

        // Kiểm tra số điện thoại đã tồn tại (trừ người dùng hiện tại)
        if (!string.IsNullOrEmpty(viewModel.So_Dien_Thoai) && _context.NguoiDungs.Any(n => n.So_Dien_Thoai == viewModel.So_Dien_Thoai && n.Id != id))
        {
            ModelState.AddModelError("So_Dien_Thoai", "Số điện thoại đã được sử dụng, vui lòng nhập số khác");
        }

        // Kiểm tra vai trò hợp lệ
        if (viewModel.Vai_Tro_Id <= 0 || !_context.VaiTros.Any(v => v.Id == viewModel.Vai_Tro_Id))
        {
            ModelState.AddModelError("Vai_Tro_Id", "Vui lòng chọn vai trò hợp lệ");
        }
    }
    // Phương thức tải danh sách vai trò
    private IEnumerable<SelectListItem> LoadVaiTroList(int selectedId = 0)
    {
        return _vaiTroRepository.GetAll()
            .Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Ten_vai_Tro,
                Selected = v.Id == selectedId
            });
    }
    // Hàm kiểm tra email hợp lệ
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    // Hàm kiểm tra số điện thoại hợp lệ
    private bool IsValidPhoneNumber(string phone)
    {
        // Kiểm tra số điện thoại VN (10 số, bắt đầu bằng 0)
        return !string.IsNullOrEmpty(phone) && System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0\d{9}$");
    }

    // Hàm băm mật khẩu (nên sử dụng)
    private string HashPassword(string password)
    {
        var tempUser = new NguoiDung();
        return _passwordHasher.HashPassword(tempUser, password);
    }

    // Chỉnh sửa người dùng - GET
    public IActionResult Edit(int id)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var nguoiDung = _nguoiDungRepository.GetById(id);
        if (nguoiDung == null)
        {
            return NotFound();
        }

        var viewModel = new NguoiDungEditViewModel
        {
            Id = nguoiDung.Id,
            Ho_Ten = nguoiDung.Ho_Ten,
            Email = nguoiDung.Email,
            So_Dien_Thoai = nguoiDung.So_Dien_Thoai,
            Vai_Tro_Id = nguoiDung.Vai_Tro_Id,
            Trang_Thai = nguoiDung.Trang_Thai,
            VaiTroList = _vaiTroRepository.GetAll()
                .Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Ten_vai_Tro,
                    Selected = v.Id == nguoiDung.Vai_Tro_Id
                })
        };

        return View(viewModel);
    }

    // Chỉnh sửa người dùng - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, NguoiDungEditViewModel viewModel)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
        }

        if (id != viewModel.Id)
        {
            return NotFound();
        }

        // Luôn khởi tạo danh sách vai trò
        viewModel.VaiTroList = LoadVaiTroList(viewModel.Vai_Tro_Id);
        // Áp dụng xác thực
        ValidateNguoiDungEdit(viewModel, id);

        if (ModelState.IsValid)
        {
            try
            {
                var nguoiDung = _nguoiDungRepository.GetById(id);
                if (nguoiDung == null)
                {
                    return NotFound();
                }

                nguoiDung.Ho_Ten = viewModel.Ho_Ten;
                nguoiDung.Email = viewModel.Email;
                nguoiDung.So_Dien_Thoai = viewModel.So_Dien_Thoai;
                nguoiDung.Vai_Tro_Id = viewModel.Vai_Tro_Id;
                nguoiDung.Trang_Thai = viewModel.Trang_Thai;

                // Nếu nhập mật khẩu mới thì mã hóa và cập nhật
                if (!string.IsNullOrEmpty(viewModel.Mat_Khau))
                {
                    nguoiDung.Mat_Khau = BCrypt.Net.BCrypt.HashPassword(viewModel.Mat_Khau);
                }

                _nguoiDungRepository.Update(nguoiDung);

                TempData["StatusMessage"] = "Cập nhật thông tin người dùng thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.NguoiDungs.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
            }
        }
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            if (errorMessages.Any())
            {
                TempData["ErrorMessage"] = $"Vui lòng kiểm tra lại thông tin đã nhập: {string.Join(", ", errorMessages)}";
            }
        }

        return View(viewModel);
    }

    // Thay đổi trạng thái người dùng - GET
    [HttpGet]
    public IActionResult ToggleStatus(int id)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = _nguoiDungRepository.ToggleTrangThai(id);
        if (result)
        {
            TempData["StatusMessage"] = "Đã thay đổi trạng thái người dùng thành công.";
        }
        else
        {
            TempData["ErrorMessage"] = "Không thể thay đổi trạng thái người dùng.";
        }

        return RedirectToAction(nameof(Index));
    }

    // AJAX - Thay đổi vai trò người dùng
    [HttpPost]
    public IActionResult ChangeRole(int id, int vaiTroId)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return Json(new { success = false, message = "Không có quyền thực hiện." });
        }

        var result = _nguoiDungRepository.ChangeVaiTro(id, vaiTroId);
        if (result)
        {
            return Json(new { success = true, message = "Đã thay đổi vai trò người dùng thành công." });
        }
        else
        {
            return Json(new { success = false, message = "Không thể thay đổi vai trò người dùng." });
        }

    }
    [HttpGet("NguoiDung/GetUserData/{id}")]
    public IActionResult GetUserData(int id)
    {
        var user = _nguoiDungRepository.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Json(new
        {
            id = user.Id,
            ho_Ten = user.Ho_Ten,
            email = user.Email,
            so_Dien_Thoai = user.So_Dien_Thoai,
            Ngay_Tao = user.Ngay_Tao.ToString("dd/MM/yyyy"),
            Vai_Tro = _vaiTroRepository.GetById(user.Vai_Tro_Id)?.Ten_vai_Tro,
            mo_Ta = user.Mo_Ta,
            trang_Thai = user.Trang_Thai
        });
    }
    [HttpPatch]
    [Route("NguoiDung/EditND/{id}")]
    public IActionResult EditND(int id, [FromBody] NguoiDungEditViewModel nguoiDungSua)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return Json(new { success = false, message = "Không có quyền thực hiện" });
        }
        ModelState.Clear();
        TryValidateModel(nguoiDungSua, nameof(nguoiDungSua.Ho_Ten));
        TryValidateModel(nguoiDungSua, nameof(nguoiDungSua.Email));
        TryValidateModel(nguoiDungSua, nameof(nguoiDungSua.So_Dien_Thoai));
        // Kiểm tra email và số điện thoại trùng lặp (trừ người dùng hiện tại)
        if (!string.IsNullOrEmpty(nguoiDungSua.Email) && _context.NguoiDungs.Any(n => n.Email == nguoiDungSua.Email && n.Id != id))
        {
            ModelState.AddModelError("Email", "Email đã được sử dụng, vui lòng chọn email khác");
        }

        if (!string.IsNullOrEmpty(nguoiDungSua.So_Dien_Thoai) && _context.NguoiDungs.Any(n => n.So_Dien_Thoai == nguoiDungSua.So_Dien_Thoai && n.Id != id))
        {
            ModelState.AddModelError("So_Dien_Thoai", "Số điện thoại đã được sử dụng, vui lòng nhập số khác");
        }
        if (ModelState.IsValid)
        {
            var nguoiDung = _nguoiDungRepository.GetById(id);
            if (nguoiDung == null)
            {
                return Json(new { success = false, message = "Không tìm thấy người dùng" });
            }

            // Cập nhật thông tin người dùng
            nguoiDung.Ho_Ten = nguoiDungSua.Ho_Ten;
            nguoiDung.Email = nguoiDungSua.Email;
            nguoiDung.So_Dien_Thoai = nguoiDungSua.So_Dien_Thoai;
            nguoiDung.Mo_Ta = nguoiDungSua.Mo_Ta;
            if (!string.IsNullOrEmpty(nguoiDungSua.Mat_Khau))
            {
                nguoiDung.Mat_Khau = BCrypt.Net.BCrypt.HashPassword(nguoiDungSua.Mat_Khau);
            }

            _nguoiDungRepository.Update(nguoiDung);
            return Json(new { success = true, message = "Cập nhật thông tin người dùng thành công!" });
        }

        return Json(new { success = false, message = "Cập nhật thất bại", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
    }
    [HttpPost]
    [Route("NguoiDung/ToggleUserStatus/{id}")]
    public IActionResult ToggleUserStatus(int id)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin != 1)
        {
            return Json(new { success = false, message = "Không có quyền thực hiện" });
        }

        var nguoiDung = _nguoiDungRepository.GetById(id);
        if (nguoiDung == null)
        {
            return Json(new { success = false, message = "Không tìm thấy người dùng" });
        }

        // Đảo ngược trạng thái
        nguoiDung.Trang_Thai = !nguoiDung.Trang_Thai;
        _nguoiDungRepository.Update(nguoiDung);

        string message = nguoiDung.Trang_Thai
            ? "Đã mở khóa người dùng thành công"
            : "Đã khóa người dùng thành công";

        return Json(new
        {
            success = true,
            message = message,
            newStatus = nguoiDung.Trang_Thai
        });
    }
}