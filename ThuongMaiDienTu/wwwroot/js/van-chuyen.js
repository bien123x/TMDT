/**
 * Tải chi tiết đơn hàng vào modal
 * @param {number} orderId - ID của đơn hàng cần xem chi tiết
 */
function loadOrderDetails(orderId) {
  document.getElementById("orderIdDetail").textContent = orderId;

  fetch(`/VanChuyen/ChiTietDonHang?id=${orderId}`)
    .then((response) => response.text())
    .then((html) => {
      document.getElementById("orderDetailsContent").innerHTML = html;
    })
    .catch((error) => {
      document.getElementById(
        "orderDetailsContent"
      ).innerHTML = `<div class="alert alert-danger">Lỗi khi tải chi tiết đơn hàng: ${error.message}</div>`;
    });
}

/**
 * Chuẩn bị modal cập nhật trạng thái
 * @param {number} orderId - ID của đơn hàng cần cập nhật
 */
function prepareUpdateStatus(orderId) {
  document.getElementById("donHangId").value = orderId;
}


