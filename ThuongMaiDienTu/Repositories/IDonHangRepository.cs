using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Repositories
{
    public interface IDonHangRepository : IRepository<DonHang>
    {
        /// <summary>
        /// Lấy danh sách đơn hàng theo danh sách IDs
        /// </summary>
        /// <param name="donHangIds">Danh sách ID của đơn hàng</param>
        /// <returns>Danh sách đơn hàng</returns>
        List<DonHang> GetDonHangByIds(List<int> donHangIds);
        List<DonHang> GetDonHangChoVanChuyen();
        /// <summary>
        /// Lấy tất cả đơn hàng cần vận chuyển với phân trang
        /// </summary>
        /// <param name="trangThaiId">ID trạng thái đơn hàng (null để lấy tất cả)</param>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Số đơn hàng mỗi trang</param>
        /// <param name="totalItems">Tổng số đơn hàng thỏa điều kiện</param>
        /// <returns>Danh sách đơn hàng cần vận chuyển</returns>
        List<DonHang> GetDonHangForDelivery(int? trangThaiId, int page, int pageSize, out int totalItems);
        /// <summary>
        /// Lấy danh sách đơn hàng đã hoàn thành vận chuyển theo khoảng thời gian
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Số đơn hàng mỗi trang</param>
        /// <param name="totalItems">Tổng số đơn hàng thỏa điều kiện</param>
        /// <returns>Danh sách đơn hàng đã hoàn thành</returns>
        List<DonHang> GetCompletedDeliveries(DateTime startDate, DateTime endDate, int page, int pageSize, out int totalItems);
        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        /// <param name="donHangId">ID đơn hàng</param>
        /// <param name="trangThaiId">ID trạng thái mới</param>
        /// <returns>true nếu cập nhật thành công, false nếu thất bại</returns>
        bool UpdateTrangThaiDonHang(int donHangId, int trangThaiId);
        /// <summary>
        /// Lấy tất cả đơn hàng của một người dùng
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <returns>Danh sách đơn hàng</returns>
        List<DonHang> GetDonHangByUserId(int userId);

        /// <summary>
        /// Lấy đơn hàng theo ID
        /// </summary>
        /// <param name="id">ID đơn hàng</param>
        /// <returns>Thông tin đơn hàng</returns>
        DonHang? GetDonHangById(int id);

        /// <summary>
        /// Tạo mới đơn hàng
        /// </summary>
        /// <param name="donHang">Thông tin đơn hàng</param>
        /// <returns>ID đơn hàng mới</returns>
        int CreateDonHang(DonHang donHang);

        /// <summary>
        /// Hủy đơn hàng
        /// </summary>
        /// <param name="donHangId">ID đơn hàng</param>
        /// <returns>true nếu hủy thành công, false nếu thất bại</returns>
        bool CancelDonHang(int donHangId);

        /// <summary>
        /// Thống kê số lượng đơn hàng theo trạng thái
        /// </summary>
        /// <returns>Dictionary với key là trạng thái, value là số lượng</returns>
        Dictionary<string, int> GetDonHangStatsByTrangThai();

        /// <summary>
        /// Thống kê doanh thu theo khoảng thời gian
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns>Tổng doanh thu</returns>
        decimal GetTotalRevenue(DateTime startDate, DateTime endDate);
        void AddListSanPham(List<SanPham> list, int idDonHang);
        void AddVanChuyen(VanChuyen vanChuyen);
        void AddThanhToan(ThanhToan thanhToan);
        IEnumerable<DonHang> GetAllAndInfor(int? sellerId = null);

        /// <summary>
        /// Lấy chi tiết đơn hàng bao gồm thông tin về sản phẩm, người mua, thanh toán và vận chuyển
        /// </summary>
        /// <param name="id">ID đơn hàng</param>
        /// <returns>Thông tin chi tiết đơn hàng</returns>
        DonHang? GetDonHangWithDetails(int id);
    }
}
