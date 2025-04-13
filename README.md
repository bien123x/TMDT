Link DB: https://dbdiagram.io/d/dbTTS-67c7e780263d6cf9a0428562 
✅ A01:2021 – Broken Access Control
Kiểm tra quyền admin trong các controller (isAdmin != 1)
Chuyển hướng người dùng không có quyền đến trang NotAllow
Phân quyền người dùng (Admin, Seller, Delivery)
✅ A02:2021 – Cryptographic Failures (một phần)
Sử dụng BCrypt để mã hóa mật khẩu, một thuật toán băm mạnh
Chức năng mã hóa mật khẩu tài khoản hiện có
✅ A03:2021 – Injection (một phần)
Sử dụng Entity Framework giúp ngăn chặn SQL Injection
Sử dụng các tham số trong truy vấn
✅ A07:2021 – Identification and Authentication Failures (một phần)
Kiểm tra tài khoản bị khóa/vô hiệu hóa
Xác thực đăng nhập với các thông báo lỗi cụ thể