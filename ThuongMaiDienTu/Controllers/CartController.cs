using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ThuongMaiDienTu.Helpers;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Repositories;

namespace ThuongMaiDienTu.Controllers
{
    public class CartController : Controller
    {
        private ISanPhamRepository _sanPhamRepository;
        public CartController(ISanPhamRepository sanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
        }
        public IActionResult Index()
        {
            var cart = CartHelper.GetCart(HttpContext.Session);
            ViewBag.CoSanPham = cart.Count > 0;
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] Dictionary<string, int> data)
        {
            int id = data["id"];
            int soLuongSanPham = data["soLuongSanPham"];
            var cart = CartHelper.GetCart(HttpContext.Session);

            var item = cart.Find(p => p.Id == id);
            
            if (item != null)
            {
                item.So_Luong_Ton+= soLuongSanPham;
            }
            else
            {
                var sanPham = _sanPhamRepository.GetById(id);
                sanPham.So_Luong_Ton = soLuongSanPham;
                cart.Add(sanPham);
            }
            CartHelper.SaveCart(HttpContext.Session, cart);
            var count = CartHelper.GetCartCount(HttpContext.Session);
            HttpContext.Session.SetInt32("cart_count", count);
            return Json(new {soLuong = count.ToString() });
        }

        [HttpPost]
        public IActionResult UpdateCart([FromBody] Dictionary<string, int> data)
        {
            int id = data["id"];
            int soLuongSanPham = data["soLuong"];
            var cart = CartHelper.GetCart(HttpContext.Session);

            var item = cart.Find(p => p.Id == id);

            if (soLuongSanPham > _sanPhamRepository.GetById(id).So_Luong_Ton)
            {
                return Json(new {success = false});
            }
            else
            {
                item.So_Luong_Ton = soLuongSanPham;

                CartHelper.SaveCart(HttpContext.Session, cart);

                var count = CartHelper.GetCartCount(HttpContext.Session);
                HttpContext.Session.SetInt32("cart_count", count);
                return Json(new { success = true, soLuong = count});
            }
        }

        [HttpDelete]
        public IActionResult DeleteCart([FromBody]int id)
        {
            var cart = CartHelper.GetCart(HttpContext.Session);
            var sp = cart.FirstOrDefault(p => p.Id == id);
            cart.Remove(sp);
            CartHelper.SaveCart(HttpContext.Session, cart);

            var count = CartHelper.GetCartCount(HttpContext.Session);
            HttpContext.Session.SetInt32("cart_count", count);

            return Json(new {soLuong = count });
        }
    }
}
