-- Bảng vai trò người dùng
CREATE TABLE [vai_tro] (
  [id] INT PRIMARY KEY,
  [ten_vai_tro] VARCHAR(20) UNIQUE
);
GO

-- Chèn dữ liệu vào bảng vai trò
INSERT INTO [vai_tro] (id, ten_vai_tro) VALUES
(1, 'nguoi_mua'),
(2, 'nguoi_ban'),
(3, 'quan_tri');
GO

-- Bảng người dùng
CREATE TABLE [nguoi_dung] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [ho_ten] VARCHAR(255),
  [email] VARCHAR(255) UNIQUE,
  [mat_khau] VARCHAR(255),
  [so_dien_thoai] VARCHAR(20),
  [vai_tro_id] INT FOREIGN KEY REFERENCES [vai_tro]([id]),
  [ngay_tao] DATETIME DEFAULT GETDATE()
);
GO
ALTER TABLE nguoi_dung
ALTER COLUMN ho_ten NVARCHAR(255);

-- Thêm cột Trang_Thai vào bảng nguoi_dung
ALTER TABLE nguoi_dung
ADD Trang_Thai BIT NOT NULL DEFAULT 1;

-- Bảng cửa hàng
CREATE TABLE [cua_hang] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_nguoi_ban] INT UNIQUE NOT NULL FOREIGN KEY REFERENCES [nguoi_dung] ([id]),
  [ten_cua_hang] VARCHAR(255),
  [mo_ta] VARCHAR(MAX),
  [ngay_tao] DATETIME DEFAULT GETDATE()
);
GO
ALTER TABLE cua_hang
ALTER COLUMN ten_cua_hang NVARCHAR(255);

ALTER TABLE cua_hang
ALTER COLUMN mo_ta NVARCHAR(MAX);


-- Bảng danh mục sản phẩm
CREATE TABLE [danh_muc] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [ten_danh_muc] VARCHAR(255)
);
GO
ALTER TABLE danh_muc
ADD trang_thai BIT NOT NULL DEFAULT 1; -- 1: Hoạt động (mở), 0: Khóa
ALTER TABLE danh_muc
ALTER COLUMN ten_danh_muc NVARCHAR(100)


-- Bảng sản phẩm
CREATE TABLE [san_pham] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_cua_hang] INT NOT NULL FOREIGN KEY REFERENCES [cua_hang]([id]),
  [id_danh_muc] INT NOT NULL FOREIGN KEY REFERENCES [danh_muc]([id]),
  [ten_san_pham] VARCHAR(255),
  [mo_ta] VARCHAR(MAX),
  [gia_goc] DECIMAL(10,2),
  [giam_gia] DECIMAL(10,2) DEFAULT 0,
  [gia_khuyen_mai] AS (gia_goc - giam_gia) PERSISTED, 
  [so_luong_ton] INT,
  [ngay_tao] DATETIME DEFAULT GETDATE()
);
GO
ALTER TABLE san_pham ADD hinh_anh NVARCHAR(255) NULL;
ALTER TABLE san_pham
ALTER COLUMN ten_san_pham NVARCHAR(255);


-- Bảng trạng thái đơn hàng (thay cho ENUM)
CREATE TABLE [trang_thai_don_hang] (
  [id] INT PRIMARY KEY,
  [mo_ta] VARCHAR(50) UNIQUE
);
GO

-- Chèn dữ liệu trạng thái đơn hàng
INSERT INTO [trang_thai_don_hang] (id, mo_ta) VALUES
(1, 'cho_xac_nhan'),
(2, 'da_xac_nhan'),
(3, 'dang_giao'),
(4, 'hoan_tat'),
(5, 'da_huy');
GO

-- Bảng đơn hàng
CREATE TABLE [don_hang] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_nguoi_mua] INT NOT NULL FOREIGN KEY REFERENCES [nguoi_dung]([id]),
  [tong_tien] DECIMAL(10,2),
  [trang_thai_id] INT NOT NULL FOREIGN KEY REFERENCES [trang_thai_don_hang]([id]),
  [ngay_tao] DATETIME DEFAULT GETDATE()
);
GO
ALTER TABLE Don_Hang  -- tên bảng thực tế của bạn
ALTER COLUMN Tong_Tien DECIMAL(18, 2);  -- tăng kích thước phần nguyên

-- Bảng chi tiết đơn hàng
CREATE TABLE [chi_tiet_don_hang] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_don_hang] INT NOT NULL FOREIGN KEY REFERENCES [don_hang]([id]),
  [id_san_pham] INT NOT NULL FOREIGN KEY REFERENCES [san_pham]([id]),
  [so_luong] INT,
  [gia] DECIMAL(10,2)
);
GO

-- Bảng phương thức thanh toán (thay cho ENUM)
CREATE TABLE [phuong_thuc_thanh_toan] (
  [id] INT PRIMARY KEY,
  [mo_ta] VARCHAR(50) UNIQUE
);
GO

INSERT INTO [phuong_thuc_thanh_toan] (id, mo_ta) VALUES
(1, 'COD');
GO

-- Bảng trạng thái thanh toán (thay cho ENUM)
CREATE TABLE [trang_thai_thanh_toan] (
  [id] INT PRIMARY KEY,
  [mo_ta] VARCHAR(50) UNIQUE
);
GO

INSERT INTO [trang_thai_thanh_toan] (id, mo_ta) VALUES
(1, 'cho_xu_ly'),
(2, 'hoan_tat'),
(3, 'that_bai');
GO

-- Bảng thanh toán
CREATE TABLE [thanh_toan] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_don_hang] INT NOT NULL FOREIGN KEY REFERENCES [don_hang]([id]),
  [phuong_thuc_id] INT NOT NULL FOREIGN KEY REFERENCES [phuong_thuc_thanh_toan]([id]),
  [trang_thai_id] INT NOT NULL FOREIGN KEY REFERENCES [trang_thai_thanh_toan]([id]),
  [ngay_tao] DATETIME DEFAULT GETDATE()
);
GO

-- Bảng trạng thái vận chuyển (thay cho ENUM)
CREATE TABLE [trang_thai_van_chuyen] (
  [id] INT PRIMARY KEY,
  [mo_ta] VARCHAR(50) UNIQUE
);
GO

INSERT INTO [trang_thai_van_chuyen] (id, mo_ta) VALUES
(1, 'dang_xu_ly'),
(2, 'dang_giao'),
(3, 'da_giao');
GO

-- Bảng vận chuyển
CREATE TABLE [van_chuyen] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_don_hang] INT NOT NULL FOREIGN KEY REFERENCES [don_hang]([id]),
  [trang_thai_id] INT NOT NULL FOREIGN KEY REFERENCES [trang_thai_van_chuyen]([id]),
  [ngay_cap_nhat] DATETIME DEFAULT GETDATE()
);
GO

-- Bảng đánh giá sản phẩm
CREATE TABLE [danh_gia] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [id_san_pham] INT NOT NULL FOREIGN KEY REFERENCES [san_pham]([id]),
  [id_nguoi_mua] INT NOT NULL FOREIGN KEY REFERENCES [nguoi_dung]([id]),
  [so_sao] INT NOT NULL DEFAULT 5 CHECK (so_sao BETWEEN 1 AND 5),
  [noi_dung] VARCHAR(MAX),
  [hinh_anh] VARCHAR(MAX),
  [ngay_danh_gia] DATETIME DEFAULT GETDATE()
);
GO
ALTER TABLE danh_gia
ALTER COLUMN noi_dung NVARCHAR(MAX);

-- Bảng banner
CREATE TABLE [banner] (
  [id] INT PRIMARY KEY IDENTITY(1,1),
  [tieu_de] VARCHAR(255),
  [hinh_anh] VARCHAR(255),
);
GO
ALTER TABLE banner
ALTER COLUMN tieu_de NVARCHAR(255);

