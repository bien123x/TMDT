using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ThuongMaiDienTu.Data;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Middleware
{
    public class CookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DbContextApp dbContext)
        {
            // Kiểm tra xem người dùng đã đăng nhập qua cookie nhưng chưa có session
            if (context.User.Identity.IsAuthenticated && context.Session.GetInt32("UserId") == null)
            {
                // Lấy UserId từ claims
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var roleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;

                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                {
                    // Tìm người dùng trong database
                    var user = dbContext.NguoiDungs.Find(userId);
                    if (user != null && user.Trang_Thai == true)
                    {
                        // Thiết lập session giống như khi đăng nhập thông thường
                        context.Session.SetInt32("UserId", userId);
                        context.Session.SetInt32("VaiTroId", user.Vai_Tro_Id);

                        // Thiết lập các session khác tùy theo vai trò
                        if (user.Vai_Tro_Id == 2) // Seller
                        {
                            context.Session.SetInt32("IsSeller", 1);
                            var cuaHang = dbContext.CuaHangs.FirstOrDefault(ch => ch.Id_Nguoi_Ban == userId);
                            if (cuaHang != null)
                            {
                                context.Session.SetInt32("StoreId", cuaHang.Id);
                            }
                        }
                        else if (user.Vai_Tro_Id == 3) // Admin
                        {
                            context.Session.SetInt32("IsAdmin", 1);
                        }
                        else if (user.Vai_Tro_Id == 4) // Delivery
                        {
                            context.Session.SetInt32("IsDelivery", 4);
                        }
                    }
                }
            }

            await _next(context);
        }
    }

    // Extension method để dễ dàng sử dụng middleware
    public static class CookieAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCookieAuthentication(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CookieAuthenticationMiddleware>();
        }
    }
}