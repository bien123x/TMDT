using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Repositories;
using ThuongMaiDienTu.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace ThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly ILogger<HomeController> _logger;



        public HomeController(ISanPhamRepository sanPhamRepository, IBannerRepository bannerRepository, IDanhMucRepository danhMucRepository, ILogger<HomeController> logger)
        {
            _sanPhamRepository = sanPhamRepository;
            _bannerRepository = bannerRepository;
            _danhMucRepository = danhMucRepository;
            _logger = logger;
            _sanPhamRepository = sanPhamRepository;
        }

        public IActionResult Index(int? danhMucId, int page = 1)
        {
            const int pageSize = 10; // Số sản phẩm mỗi trang
            List<SanPham> sanPhams;

            if (danhMucId.HasValue && danhMucId > 0)
            {
                sanPhams = _sanPhamRepository.GetSanPhamByDanhMuc(danhMucId.Value);
            }
            else
            {
                sanPhams = _sanPhamRepository.GetAllSanPhams();
            }

            int totalItems = sanPhams.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));
            sanPhams = sanPhams.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            List<DanhMuc> danhMucs = _danhMucRepository.GetAllDanhMucs();
            List<Banner> banners = _bannerRepository.GetAllBanners();

            ViewBag.DanhMucs = danhMucs;
            ViewBag.Banners = banners;
            ViewBag.SelectedDanhMuc = danhMucId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(sanPhams);
        }

        // Action mới để trả về JSON cho phân trang
        [HttpGet]
        public IActionResult GetProducts(int? danhMucId, int page = 1)
        {
            const int pageSize = 10; // Số sản phẩm mỗi trang
            List<SanPham> sanPhams;

            if (danhMucId.HasValue && danhMucId > 0)
            {
                sanPhams = _sanPhamRepository.GetSanPhamByDanhMuc(danhMucId.Value);
            }
            else
            {
                sanPhams = _sanPhamRepository.GetAllSanPhams();
            }

            int totalItems = sanPhams.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));
            sanPhams = sanPhams.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new
            {
                Products = sanPhams.Select(sp => new
                {
                    sp.Id,
                    sp.Ten_San_Pham,
                    Hinh_Anh = sp.Hinh_Anh != null ? Url.Content(sp.Hinh_Anh) : Url.Content("~/images/logo.png"),
                    sp.Gia_Goc,
                    sp.Giam_Gia
                }),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return Json(result);
        }

        [HttpGet]
        public IActionResult TimKiem(string tuKhoa, int page = 1)
        {
            const int pageSize = 10; // Số sản phẩm mỗi trang
            List<SanPham> sanPhams = _sanPhamRepository.TimKiemSanPham(tuKhoa);

            int totalItems = sanPhams.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));
            sanPhams = sanPhams.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TuKhoa = tuKhoa;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(sanPhams);
        }

        [HttpGet]
        public IActionResult GetSearchProducts(string tuKhoa, int page = 1)
        {
            const int pageSize = 10; // Số sản phẩm mỗi trang
            List<SanPham> sanPhams = _sanPhamRepository.TimKiemSanPham(tuKhoa);

            int totalItems = sanPhams.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));
            sanPhams = sanPhams.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new
            {
                Products = sanPhams.Select(sp => new
                {
                    sp.Id,
                    sp.Ten_San_Pham,
                    Hinh_Anh = sp.Hinh_Anh != null ? Url.Content(sp.Hinh_Anh) : Url.Content("~/images/logo.png"),
                    sp.Gia_Goc,
                    sp.Giam_Gia
                }),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return Json(result);
        }



        public IActionResult ChiTietSanPham(int id)
        {
            var sanPham = _sanPhamRepository.GetById(id); // ⚙️ Dùng đúng tên biến
            if (sanPham == null)
                return NotFound();
            return View(sanPham); // View ChiTietSanPham.cshtml
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
