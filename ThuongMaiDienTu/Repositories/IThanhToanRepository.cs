using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IThanhToanRepository
    {
        /// <summary>
        /// Lấy tất cả lịch sử thanh toán của một đơn hàng
        /// </summary>
        /// <param name="donHangId">ID của đơn hàng</param>
        /// <returns>Danh sách lịch sử thanh toán</returns>
        List<ThanhToan> GetThanhToanByDonHangId(int donHangId);

        /// <summary>
        /// Lấy thông tin thanh toán mới nhất của một đơn hàng
        /// </summary>
        /// <param name="donHangId">ID của đơn hàng</param>
        /// <returns>Thông tin thanh toán mới nhất, null nếu không tìm thấy</returns>
        ThanhToan? GetLatestThanhToanByDonHangId(int donHangId);

        /// <summary>
        /// Thêm mới một bản ghi thanh toán
        /// </summary>
        /// <param name="thanhToan">Thông tin thanh toán cần thêm</param>
        /// <returns>ID của bản ghi thanh toán vừa thêm</returns>
        int AddThanhToan(ThanhToan thanhToan);

        /// <summary>
        /// Cập nhật thông tin của một bản ghi thanh toán
        /// </summary>
        /// <param name="thanhToan">Thông tin thanh toán cần cập nhật</param>
        /// <returns>true nếu cập nhật thành công, false nếu không</returns>
        bool UpdateThanhToan(ThanhToan thanhToan);

        /// <summary>
        /// Xóa một bản ghi thanh toán
        /// </summary>
        /// <param name="id">ID của bản ghi thanh toán cần xóa</param>
        /// <returns>true nếu xóa thành công, false nếu không</returns>
        bool DeleteThanhToan(int id);

        /// <summary>
        /// Kiểm tra xem đơn hàng đã được thanh toán hay chưa
        /// </summary>
        /// <param name="donHangId">ID của đơn hàng</param>
        /// <returns>true nếu đã thanh toán, false nếu chưa</returns>
        bool IsPaid(int donHangId);

        /// <summary>
        /// Lấy danh sách thanh toán theo khoảng thời gian
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns>Danh sách thanh toán trong khoảng thời gian</returns>
        List<ThanhToan> GetThanhToanByDateRange(DateTime startDate, DateTime endDate);

        
    }
}