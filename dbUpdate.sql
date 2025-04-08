-- Thêm cột Trang_Thai vào bảng nguoi_dung
ALTER TABLE nguoi_dung
ADD Trang_Thai BIT NOT NULL DEFAULT 1;
ALTER TABLE danh_muc
ADD trang_thai BIT NOT NULL DEFAULT 1; -- 1: Hoạt động (mở), 0: Khóa
ALTER TABLE san_pham ADD hinh_anh NVARCHAR(255) NULL;
ALTER TABLE Don_Hang  -- tên bảng thực tế của bạn
ALTER COLUMN Tong_Tien DECIMAL(18, 2);  -- tăng kích thước phần nguyên

ALTER TABLE danh_muc
ALTER COLUMN ten_danh_muc NVARCHAR(100)

ALTER TABLE banner
ALTER COLUMN tieu_de NVARCHAR(255);

ALTER TABLE cua_hang
ALTER COLUMN ten_cua_hang NVARCHAR(255);

ALTER TABLE cua_hang
ALTER COLUMN mo_ta NVARCHAR(MAX);

ALTER TABLE danh_gia
ALTER COLUMN noi_dung NVARCHAR(MAX);

ALTER TABLE nguoi_dung
ALTER COLUMN ho_ten NVARCHAR(255);

ALTER TABLE san_pham
ALTER COLUMN ten_san_pham NVARCHAR(255);
ALTER TABLE san_pham
ALTER COLUMN mo_ta NVARCHAR(255);

ALTER TABLE cua_hang
ADD trang_thai BIT NOT NULL DEFAULT 1;
