using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IVanChuyenRepository : IRepository<VanChuyen>
    {
        /// <summary>
        /// Lấy tất cả lịch sử vận chuyển của một đơn hàng
        /// </summary>
        /// <param name="donHangId">ID của đơn hàng</param>
        /// <returns>Danh sách lịch sử vận chuyển</returns>
        List<VanChuyen> GetVanChuyenByDonHangId(int donHangId);

        /// <summary>
        /// Lấy thông tin vận chuyển mới nhất của một đơn hàng
        /// </summary>
        /// <param name="donHangId">ID của đơn hàng</param>
        /// <returns>Thông tin vận chuyển mới nhất, null nếu không tìm thấy</returns>
        VanChuyen? GetLatestVanChuyenByDonHangId(int donHangId);

        /// <summary>
        /// Lấy danh sách vận chuyển theo trạng thái
        /// </summary>
        /// <param name="trangThaiId">ID của trạng thái vận chuyển</param>
        /// <returns>Danh sách vận chuyển theo trạng thái</returns>
        List<VanChuyen> GetVanChuyenByTrangThai(int trangThaiId);

        /// <summary>
        /// Lấy danh sách vận chuyển theo khoảng thời gian
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns>Danh sách vận chuyển trong khoảng thời gian</returns>
        List<VanChuyen> GetVanChuyenByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Đếm số lượng đơn hàng theo trạng thái vận chuyển
        /// </summary>
        /// <param name="trangThaiId">ID của trạng thái vận chuyển</param>
        /// <returns>Số lượng đơn hàng</returns>
        int CountVanChuyenByTrangThai(int trangThaiId);

        /// <summary>
        /// Lấy danh sách đơn hàng cần vận chuyển (trạng thái 2: chờ vận chuyển)
        /// </summary>
        /// <returns>Danh sách đơn hàng cần vận chuyển</returns>
        List<DonHang> GetDonHangCanVanChuyen();

        /// <summary>
        /// Lấy danh sách đơn hàng đang vận chuyển (trạng thái 3: đang vận chuyển)
        /// </summary>
        /// <returns>Danh sách đơn hàng đang vận chuyển</returns>
        List<DonHang> GetDonHangDangVanChuyen();

        /// <summary>
        /// Lấy thống kê vận chuyển theo ngày
        /// </summary>
        /// <param name="date">Ngày cần thống kê</param>
        /// <returns>Dictionary với key là trạng thái, value là số lượng</returns>
        Dictionary<string, int> GetVanChuyenStatsByDate(DateTime date);
    }
}