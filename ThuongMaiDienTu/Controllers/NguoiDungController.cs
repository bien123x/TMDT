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
    public IActionResult Index()
    {
        // Kiểm tra người dùng đăng nhập có quyền admin
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var nguoiDungs = _nguoiDungRepository.GetAllWithDetails();
        return View(nguoiDungs);
    }

    // Xem chi tiết người dùng
    public IActionResult Details(int id)
    {
        var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
        if (isAdmin == null)
        {
            return RedirectToAction("Login", "Account");
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

        // Nếu có lỗi hoặc ModelState không hợp lệ, hiển thị lại form với thông báo lỗi
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
                Mat_Khau = viewModel.Mat_Khau, 
                So_Dien_Thoai = viewModel.So_Dien_Thoai?.Trim(),
                Vai_Tro_Id = viewModel.Vai_Tro_Id,
                Ngay_Tao = DateTime.Now,
                Trang_Thai = viewModel.Trang_Thai
            };

            // Lưu vào cơ sở dữ liệu
            _nguoiDungRepository.Add(nguoiDung);

            // Thông báo thành công
            TempData["StatusMessage"] = "Tạo tài khoản người dùng thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
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

                // Nếu nhập mật khẩu mới thì cập nhật
                if (!string.IsNullOrEmpty(viewModel.Mat_Khau))
                {
                    nguoiDung.Mat_Khau = viewModel.Mat_Khau; // Trong thực tế nên mã hóa mật khẩu
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
            catch(Exception ex)
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
}