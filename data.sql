
-- Tạo tài khoản người bán cho các cửa hàng
INSERT INTO nguoi_dung (ho_ten, email, mat_khau, so_dien_thoai, vai_tro_id, ngay_tao, trang_thai)
VALUES
-- Người bán Thức Ăn (vai_tro_id = 2)
(N'Nguyễn Văn An', 'an.food@example.com', '123', '0901234567', 2, GETDATE(), 1), -- Mật khẩu: 123456 (đã hash MD5)
(N'Trần Thị Bình', 'binh.bakery@example.com', '123', '0912345678', 2, GETDATE(), 1),
(N'Phạm Văn Cao', 'cao.drink@example.com', '123', '0923456789', 2, GETDATE(), 1),
(N'Lê Thị Dung', 'dung.snack@example.com', '123', '0934567890', 2, GETDATE(), 1),

-- Người bán Mỹ Phẩm (vai_tro_id = 2)
(N'Hoàng Thị Nga', 'nga.beauty@example.com', '123', '0945678901', 2, GETDATE(), 1),
(N'Vũ Văn Hùng', 'hung.cosmetic@example.com', '123', '0956789012', 2, GETDATE(), 1),
(N'Đỗ Thị Lan', 'lan.skincare@example.com', '123', '0967890123', 2, GETDATE(), 1),
(N'Bùi Văn Minh', 'minh.makeup@example.com', '123', '0978901234', 2, GETDATE(), 1),

-- Người bán Đồ Gia Dụng (vai_tro_id = 2)
(N'Ngô Thị Hạnh', 'hanh.kitchen@example.com', '123', '0989012345', 2, GETDATE(), 1),
(N'Đặng Văn Phúc', 'phuc.homewares@example.com', '123', '0990123456', 2, GETDATE(), 1),
(N'Phan Thị Quỳnh', 'quynh.electric@example.com', '123', '0801234567', 2, GETDATE(), 1),
(N'Trịnh Văn Sơn', 'son.furniture@example.com', '123', '0812345678', 2, GETDATE(), 1),

-- Người bán Đồ Chơi (vai_tro_id = 2)
(N'Mai Thị Thùy', 'thuy.toys@example.com', '123', '0823456789', 2, GETDATE(), 1),
(N'Lý Văn Tuấn', 'tuan.games@example.com', '123', '0834567890', 2, GETDATE(), 1),
(N'Huỳnh Thị Vân', 'van.childrentoys@example.com', '123', '0845678901', 2, GETDATE(), 1),
(N'Đinh Văn Xuân', 'xuan.educationtoys@example.com', '123', '0856789012', 2, GETDATE(), 1);

-- Thêm dữ liệu vào bảng cửa hàng
INSERT INTO cua_hang (id_nguoi_ban, ten_cua_hang, mo_ta, ngay_tao)
VALUES
-- Cửa hàng Thức Ăn
((SELECT id FROM nguoi_dung WHERE email = 'an.food@example.com'), 
 N'Nhà Hàng An Phát', 
 N'Chuyên cung cấp các món ăn nhanh, đồ ăn vặt và thức ăn ngon với giá cả phải chăng. Đảm bảo vệ sinh an toàn thực phẩm.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'binh.bakery@example.com'), 
 N'Tiệm Bánh Bình An', 
 N'Tiệm bánh với đa dạng các loại bánh ngọt, bánh mì tươi được làm thủ công mỗi ngày. Chuyên nhận đặt bánh sinh nhật, bánh sự kiện.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'cao.drink@example.com'), 
 N'Cafe & Trà Sữa Cao Nguyên', 
 N'Cung cấp các loại đồ uống từ cafe, trà sữa đến nước ép trái cây tươi nguyên chất. Không sử dụng hương liệu nhân tạo.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'dung.snack@example.com'), 
 N'Đồ Ăn Vặt Dung Dung', 
 N'Thiên đường đồ ăn vặt với hàng trăm loại snack trong và ngoài nước. Luôn có khuyến mãi hấp dẫn mỗi ngày.',
 GETDATE()),

-- Cửa hàng Mỹ Phẩm
((SELECT id FROM nguoi_dung WHERE email = 'nga.beauty@example.com'), 
 N'Nga Beauty - Mỹ Phẩm Chính Hãng', 
 N'Phân phối độc quyền các thương hiệu mỹ phẩm nổi tiếng từ Hàn Quốc, Nhật Bản và Pháp. Cam kết 100% hàng chính hãng.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'hung.cosmetic@example.com'), 
 N'Hùng Cosmetics Official', 
 N'Chuyên các sản phẩm mỹ phẩm, trang điểm cao cấp với giá tốt nhất thị trường. Tư vấn và hỗ trợ khách hàng 24/7.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'lan.skincare@example.com'), 
 N'Lan Skincare Studio', 
 N'Chuyên về các sản phẩm chăm sóc da, điều trị mụn và các vấn đề về da. Có dòng sản phẩm dành riêng cho làn da nhạy cảm.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'minh.makeup@example.com'), 
 N'Minh Makeup Artist Shop', 
 N'Cung cấp các sản phẩm makeup chuyên nghiệp, dụng cụ trang điểm và phụ kiện làm đẹp. Tổ chức các khóa học makeup cơ bản.',
 GETDATE()),

-- Cửa hàng Đồ Gia Dụng
((SELECT id FROM nguoi_dung WHERE email = 'hanh.kitchen@example.com'), 
 N'Nhà Bếp Hạnh Phúc', 
 N'Đầy đủ các loại đồ dùng nhà bếp, từ nồi chảo, dao kéo đến các thiết bị làm bánh. Hàng nhập khẩu từ các thương hiệu uy tín.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'phuc.homewares@example.com'), 
 N'Phúc Homewares', 
 N'Cung cấp đồ gia dụng thông minh, tiện ích cho ngôi nhà hiện đại. Các sản phẩm tiết kiệm không gian và đa năng.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'quynh.electric@example.com'), 
 N'Quỳnh Electric House', 
 N'Chuyên các thiết bị điện gia dụng như máy xay, nồi cơm điện, bếp từ, lò vi sóng... Bảo hành chính hãng dài hạn.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'son.furniture@example.com'), 
 N'Nội Thất Gia Đình Sơn', 
 N'Cung cấp nội thất và vật dụng trang trí nhà cửa phong cách hiện đại, tối giản. Thiết kế theo yêu cầu của khách hàng.',
 GETDATE()),

-- Cửa hàng Đồ Chơi
((SELECT id FROM nguoi_dung WHERE email = 'thuy.toys@example.com'), 
 N'Đồ Chơi Thủy Tinh', 
 N'Các loại đồ chơi an toàn cho bé từ 0-6 tuổi. Đồ chơi thông minh giúp phát triển kỹ năng vận động và tư duy.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'tuan.games@example.com'), 
 N'Game Zone Tuấn', 
 N'Cung cấp các loại boardgame, đồ chơi giải trí cho mọi lứa tuổi. Có khu vực thử nghiệm trò chơi trước khi mua.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'van.childrentoys@example.com'), 
 N'Vân Kids - Đồ Chơi Trẻ Em', 
 N'Chuyên đồ chơi ngoài trời, trong nhà cho trẻ em. Đảm bảo an toàn theo tiêu chuẩn quốc tế, không chứa chất độc hại.',
 GETDATE()),

((SELECT id FROM nguoi_dung WHERE email = 'xuan.educationtoys@example.com'), 
 N'Xuân Education Toys', 
 N'Đồ chơi giáo dục, phát triển tư duy STEM, lắp ráp robot và học lập trình cho trẻ em từ 3-15 tuổi.',
 GETDATE());

 -- DANH MỤC: THỨC ĂN (ID: 2)
-- Tạo sản phẩm cho các cửa hàng thức ăn

-- Nhà Hàng An Phát (ID: 5)
INSERT INTO san_pham (id_cua_hang, id_danh_muc, ten_san_pham, mo_ta, gia_goc, giam_gia, so_luong_ton, ngay_tao, hinh_anh)
VALUES 
(5, 2, N'Cơm Rang Thập Cẩm', N'Cơm rang với thịt gà, xúc xích, trứng và nhiều loại rau củ tươi ngon. Vị đậm đà, phần ăn no cho 1 người.', 55000, 5000, 50, GETDATE(), N'/images/sanpham/com-rang-thap-cam.jpg'),
(5, 2, N'Mì Xào Hải Sản', N'Mì trứng xào với tôm, mực, nghêu và các loại rau. Hương vị đậm đà với nước xốt bí truyền của nhà hàng.', 70000, 0, 40, GETDATE(), N'/images/sanpham/mi-xao-hai-san.jpg'),
(5, 2, N'Gà Rán Sốt Cay', N'Gà rán giòn với sốt cay đặc trưng. Phần ăn gồm 3 miếng đùi gà, khoai tây chiên và salad.', 85000, 10000, 30, GETDATE(), N'/images/sanpham/ga-ran-sot-cay.jpg'),

-- Tiệm Bánh Bình An (ID: 6)
(6, 2, N'Bánh Mì Thịt Nướng', N'Bánh mì giòn với thịt heo nướng, rau thơm, đồ chua và sốt tương ớt đặc biệt. Món ăn sáng hoặc trưa lý tưởng.', 30000, 0, 100, GETDATE(), N'/images/sanpham/banh-mi-thit-nuong.jpg'),
(6, 2, N'Bánh Bông Lan Trứng Muối', N'Bánh bông lan mềm xốp với nhân trứng muối tan chảy bên trong. Sản phẩm bestseller của tiệm.', 45000, 5000, 60, GETDATE(), N'/images/sanpham/banh-bong-lan-trung-muoi.jpg'),
(6, 2, N'Bánh Tiramisu', N'Bánh tiramisu với lớp phô mai mềm mịn và hương vị cafe đậm đà. Phủ lớp bột cacao thơm ngon.', 50000, 0, 40, GETDATE(), N'/images/sanpham/banh-tiramisu.jpg'),

-- Cafe & Trà Sữa Cao Nguyên (ID: 7)
(7, 2, N'Cà Phê Đen Đá', N'Cà phê nguyên chất Việt Nam, rang xay tại chỗ mỗi ngày. Vị đắng đậm đà với hương thơm quyến rũ.', 25000, 0, 200, GETDATE(), N'/images/sanpham/ca-phe-den-da.jpg'),
(7, 2, N'Trà Sữa Trân Châu Đường Nâu', N'Trà sữa thơm béo với trân châu đường nâu dẻo thơm. Topping được làm tại chỗ mỗi ngày.', 40000, 5000, 150, GETDATE(), N'/images/sanpham/tra-sua-tran-chau.jpg'),
(7, 2, N'Sinh Tố Bơ', N'Sinh tố từ quả bơ tươi, sữa đặc và sữa tươi. Thức uống bổ dưỡng, thơm ngon.', 45000, 0, 80, GETDATE(), N'/images/sanpham/sinh-to-bo.jpg'),

-- Đồ Ăn Vặt Dung Dung (ID: 8)
(8, 2, N'Bánh Tráng Trộn', N'Bánh tráng trộn với đầy đủ phụ gia: bò khô, trứng cút, rau răm, đậu phộng và sốt me đặc biệt.', 35000, 0, 100, GETDATE(), N'/images/sanpham/banh-trang-tron.jpg'),
(8, 2, N'Takoyaki', N'Bánh bạch tuộc kiểu Nhật với nhân bạch tuộc tươi, sốt mayonnaise và bột rong biển.', 40000, 5000, 60, GETDATE(), N'/images/sanpham/takoyaki.jpg'),
(8, 2, N'Khoai Tây Lắc Phô Mai', N'Khoai tây chiên giòn lắc với bột phô mai thơm béo. Ăn vặt hoàn hảo cho buổi xem phim.', 30000, 0, 120, GETDATE(), N'/images/sanpham/khoai-tay-lac-pho-mai.jpg');

-- DANH MỤC: MỸ PHẨM (ID: 3)
-- Tạo sản phẩm cho các cửa hàng mỹ phẩm

-- Nga Beauty - Mỹ Phẩm Chính Hãng (ID: 9)
INSERT INTO san_pham (id_cua_hang, id_danh_muc, ten_san_pham, mo_ta, gia_goc, giam_gia, so_luong_ton, ngay_tao, hinh_anh)
VALUES 
(9, 3, N'Kem Chống Nắng Innisfree', N'Kem chống nắng Innisfree SPF50+ PA++++ không gây nhờn rít, thích hợp cho da dầu và da hỗn hợp.', 250000, 30000, 50, GETDATE(), N'/images/sanpham/kem-chong-nang-innisfree.jpg'),
(9, 3, N'Nước Tẩy Trang Bioderma', N'Nước tẩy trang Bioderma Sensibio H2O dành cho da nhạy cảm, làm sạch sâu mà không gây kích ứng.', 350000, 0, 40, GETDATE(), N'/images/sanpham/nuoc-tay-trang-bioderma.jpg'),
(9, 3, N'Phấn Nước Laneige Neo', N'Phấn nước Laneige Neo Cushion với độ che phủ hoàn hảo, kiểm soát dầu tốt và cấp ẩm cho da.', 650000, 100000, 30, GETDATE(), N'/images/sanpham/phan-nuoc-laneige.jpg'),

-- Hùng Cosmetics Official (ID: 10)
(10, 3, N'Son Môi MAC Ruby Woo', N'Son môi MAC Ruby Woo tông đỏ thuần kinh điển. Chất son lì, lâu trôi, không gây khô môi.', 500000, 50000, 60, GETDATE(), N'/images/sanpham/son-mac-ruby-woo.jpg'),
(10, 3, N'Phấn Mắt Urban Decay', N'Bảng phấn mắt Urban Decay Naked với 12 màu từ nhũ đến lì, dễ phối và lên màu chuẩn.', 950000, 150000, 25, GETDATE(), N'/images/sanpham/phan-mat-urban-decay.jpg'),
(10, 3, N'Mascara Maybelline', N'Mascara Maybelline Lash Sensational cho hàng mi cong vút, dày và dài tự nhiên.', 220000, 20000, 70, GETDATE(), N'/images/sanpham/mascara-maybelline.jpg'),

-- Lan Skincare Studio (ID: 11)
(11, 3, N'Serum Vitamin C Melano CC', N'Serum Vitamin C Melano CC Rohto giúp làm mờ thâm nám, sáng da hiệu quả sau 4 tuần sử dụng.', 280000, 0, 80, GETDATE(), N'/images/sanpham/serum-melano-cc.jpg'),
(11, 3, N'Mặt Nạ Ngủ Laneige', N'Mặt nạ ngủ Laneige Water Sleeping Mask cấp ẩm toàn diện, phục hồi da trong khi ngủ.', 550000, 50000, 40, GETDATE(), N'/images/sanpham/mat-na-ngu-laneige.jpg'),
(11, 3, N'Sữa Rửa Mặt Cosrx', N'Sữa rửa mặt Cosrx Low pH Good Morning Gel Cleanser với độ pH thấp, làm sạch nhẹ nhàng.', 180000, 0, 100, GETDATE(), N'/images/sanpham/sua-rua-mat-cosrx.jpg'),

-- Minh Makeup Artist Shop (ID: 12)
(12, 3, N'Bộ Cọ Trang Điểm 15 Cây', N'Bộ cọ trang điểm cao cấp 15 cây đầy đủ chức năng, lông cọ mềm mịn, cán cọ chắc chắn.', 750000, 150000, 20, GETDATE(), N'/images/sanpham/bo-co-trang-diem.jpg'),
(12, 3, N'Phấn Phủ Innisfree No Sebum', N'Phấn phủ kiềm dầu Innisfree No Sebum Mineral Powder, giữ lớp makeup lâu trôi suốt ngày dài.', 150000, 0, 90, GETDATE(), N'/images/sanpham/phan-phu-innisfree.jpg'),
(12, 3, N'Kẹp Mi Shiseido', N'Kẹp mi Shiseido chất liệu cao cấp, uốn mi cong tự nhiên, không gây gãy mi.', 350000, 50000, 30, GETDATE(), N'/images/sanpham/kep-mi-shiseido.jpg');

-- DANH MỤC: ĐỒ GIA DỤNG (ID: 8)
-- Tạo sản phẩm cho các cửa hàng đồ gia dụng

-- Nhà Bếp Hạnh Phúc (ID: 13)
INSERT INTO san_pham (id_cua_hang, id_danh_muc, ten_san_pham, mo_ta, gia_goc, giam_gia, so_luong_ton, ngay_tao, hinh_anh)
VALUES 
(13, 8, N'Bộ Nồi Inox 5 Món Lock&Lock', N'Bộ nồi inox 5 món Lock&Lock với lớp đáy 3 lớp, sử dụng được trên mọi loại bếp, kể cả bếp từ.', 1800000, 300000, 15, GETDATE(), N'/images/sanpham/bo-noi-inox-lock-lock.jpg'),
(13, 8, N'Chảo Chống Dính Tefal', N'Chảo chống dính Tefal 26cm với công nghệ Thermo-Spot báo nhiệt, an toàn cho sức khỏe.', 550000, 50000, 30, GETDATE(), N'/images/sanpham/chao-chong-dinh-tefal.jpg'),
(13, 8, N'Bộ Dao Nhà Bếp Zwilling', N'Bộ dao nhà bếp Zwilling 3 món được làm từ thép không gỉ cao cấp, sắc bén và bền bỉ.', 1500000, 200000, 10, GETDATE(), N'/images/sanpham/bo-dao-zwilling.jpg'),

-- Phúc Homewares (ID: 14)
(14, 8, N'Máy Xay Sinh Tố Philips HR2195', N'Máy xay sinh tố Philips HR2195 công suất 900W, lưỡi dao ProBlend 6, xay mịn mọi loại nguyên liệu.', 1600000, 200000, 20, GETDATE(), N'/images/sanpham/may-xay-sinh-to-philips.jpg'),
(14, 8, N'Nồi Chiên Không Dầu Philips', N'Nồi chiên không dầu Philips HD9252 dung tích 4.1L, công nghệ Rapid Air cho món ăn giòn bên ngoài, mềm bên trong.', 2500000, 300000, 15, GETDATE(), N'/images/sanpham/noi-chien-khong-dau-philips.jpg'),
(14, 8, N'Máy Ép Trái Cây Tốc Độ Chậm', N'Máy ép trái cây tốc độ chậm Hurom HP-LBF17 giữ nguyên vitamin và dưỡng chất, giảm oxy hóa.', 4500000, 500000, 8, GETDATE(), N'/images/sanpham/may-ep-trai-cay-hurom.jpg'),

-- Quỳnh Electric House (ID: 15)
(15, 8, N'Nồi Cơm Điện Tử Cuckoo', N'Nồi cơm điện tử Cuckoo CRP-PK1000S công nghệ nấu áp suất, dung tích 1.8L, thích hợp cho gia đình 4-6 người.', 3200000, 200000, 12, GETDATE(), N'/images/sanpham/noi-com-dien-cuckoo.jpg'),
(15, 8, N'Máy Lọc Không Khí Samsung', N'Máy lọc không khí Samsung AX60R5080WD lọc bụi mịn PM2.5, diệt khuẩn và khử mùi hiệu quả.', 5500000, 500000, 5, GETDATE(), N'/images/sanpham/may-loc-khong-khi-samsung.jpg'),
(15, 8, N'Bếp Từ Đôi Electrolux', N'Bếp từ đôi Electrolux EHI7280BA mặt kính Schott Ceran, 9 mức công suất, hẹn giờ thông minh.', 8500000, 1000000, 3, GETDATE(), N'/images/sanpham/bep-tu-doi-electrolux.jpg'),

-- Nội Thất Gia Đình Sơn (ID: 16)
(16, 8, N'Ghế Sofa Văng Da', N'Ghế sofa văng da 3 chỗ ngồi thiết kế hiện đại, chân inox chắc chắn, da nhập khẩu cao cấp.', 12500000, 1500000, 2, GETDATE(), N'/images/sanpham/ghe-sofa-vang-da.jpg'),
(16, 8, N'Bàn Ăn Mặt Đá 6 Ghế', N'Bàn ăn mặt đá cẩm thạch tự nhiên, chân sắt sơn tĩnh điện, kèm 6 ghế bọc da PU cao cấp.', 15000000, 2000000, 3, GETDATE(), N'/images/sanpham/ban-an-mat-da.jpg'),
(16, 8, N'Tủ Quần Áo 4 Cánh', N'Tủ quần áo 4 cánh làm từ gỗ công nghiệp MDF chống ẩm, phủ melamine cao cấp, bền đẹp.', 7500000, 500000, 5, GETDATE(), N'/images/sanpham/tu-quan-ao-4-canh.jpg');

-- DANH MỤC: ĐỒ CHƠI (ID: 9)
-- Tạo sản phẩm cho các cửa hàng đồ chơi

-- Đồ Chơi Thủy Tinh (ID: 17)
INSERT INTO san_pham (id_cua_hang, id_danh_muc, ten_san_pham, mo_ta, gia_goc, giam_gia, so_luong_ton, ngay_tao, hinh_anh)
VALUES 
(17, 9, N'Đồ Chơi Xếp Hình Duplo', N'Bộ đồ chơi xếp hình Duplo 120 chi tiết phát triển khả năng sáng tạo và tư duy không gian cho bé từ 2-5 tuổi.', 850000, 100000, 20, GETDATE(), N'/images/sanpham/do-choi-xep-hinh-duplo.jpg'),
(17, 9, N'Thảm Chơi Nhiều Màu', N'Thảm chơi nhiều màu có nhạc và đèn, kích thích thị giác và thính giác cho bé từ 0-12 tháng.', 550000, 50000, 30, GETDATE(), N'/images/sanpham/tham-choi-nhieu-mau.jpg'),
(17, 9, N'Xe Đẩy Tập Đi Hình Gấu', N'Xe đẩy tập đi hình gấu có nhạc và đồ chơi, giúp bé phát triển kỹ năng vận động.', 680000, 80000, 15, GETDATE(), N'/images/sanpham/xe-day-tap-di.jpg'),

-- Game Zone Tuấn (ID: 18)
(18, 9, N'Boardgame Catan', N'Boardgame Catan phiên bản tiếng Việt, trò chơi chiến thuật dành cho 3-4 người chơi, độ tuổi từ 10+.', 750000, 0, 25, GETDATE(), N'/images/sanpham/boardgame-catan.jpg'),
(18, 9, N'Uno Card Game', N'Bộ bài Uno cổ điển, trò chơi vui nhộn cho cả gia đình với nhiều thẻ đặc biệt hấp dẫn.', 150000, 0, 50, GETDATE(), N'/images/sanpham/uno-card-game.jpg'),
(18, 9, N'Cờ Vua Gỗ Cao Cấp', N'Bộ cờ vua làm từ gỗ tự nhiên, quân cờ được chạm khắc tinh xảo, phù hợp mọi lứa tuổi.', 450000, 50000, 15, GETDATE(), N'/images/sanpham/co-vua-go.jpg'),

-- Vân Kids - Đồ Chơi Trẻ Em (ID: 19)
(19, 9, N'Xe Đạp Trẻ Em Size 16', N'Xe đạp trẻ em size 16 inch thiết kế an toàn với bánh phụ, phù hợp cho bé 4-7 tuổi.', 1500000, 200000, 10, GETDATE(), N'/images/sanpham/xe-dap-tre-em.jpg'),
(19, 9, N'Bể Bơi Phao 3 Tầng', N'Bể bơi phao 3 tầng cho trẻ em, chất liệu nhựa PVC an toàn, dễ lắp đặt và cất giữ.', 450000, 50000, 20, GETDATE(), N'/images/sanpham/be-boi-phao.jpg'),
(19, 9, N'Cầu Trượt Trong Nhà', N'Cầu trượt trong nhà cho bé từ 1-5 tuổi, thiết kế an toàn với tay vịn và bậc thang rộng.', 950000, 100000, 8, GETDATE(), N'/images/sanpham/cau-truot-trong-nha.jpg'),

-- Xuân Education Toys (ID: 20)
(20, 9, N'Robot Lập Trình STEM', N'Robot lập trình STEM cho trẻ từ 8 tuổi, giúp bé học lập trình cơ bản qua các hoạt động thú vị.', 1200000, 0, 15, GETDATE(), N'/images/sanpham/robot-lap-trinh.jpg'),
(20, 9, N'Kính Hiển Vi Trẻ Em', N'Kính hiển vi dành cho trẻ em với độ phóng đại 100x, kèm bộ mẫu vật và hướng dẫn sử dụng.', 550000, 50000, 12, GETDATE(), N'/images/sanpham/kinh-hien-vi-tre-em.jpg'),
(20, 9, N'Bộ Thí Nghiệm Khoa Học', N'Bộ thí nghiệm khoa học 50+ thí nghiệm an toàn, giúp bé khám phá thế giới khoa học một cách thú vị.', 650000, 50000, 10, GETDATE(), N'/images/sanpham/bo-thi-nghiem-khoa-hoc.jpg');


-- Tạo dữ liệu đánh giá cho các sản phẩm
INSERT INTO danh_gia (id_san_pham, id_nguoi_mua, so_sao, noi_dung, hinh_anh, ngay_danh_gia)
VALUES
-- DANH MỤC THỨC ĂN - Nhà Hàng An Phát (ID: 5)
(81, 1, 5, N'Món cơm rang thật sự rất ngon, đầy đủ nguyên liệu tươi ngon. Đặc biệt thịt gà mềm, không bị khô. Sẽ đặt lại.', '/images/reviews/review-com-rang-01.jpg', DATEADD(DAY, -5, GETDATE())),
(81, 3, 4, N'Cơm rang thập cẩm khá ngon, phần ăn đủ no cho 1 người. Tuy nhiên giao hàng hơi chậm.', '/images/reviews/review-com-rang-02.jpg', DATEADD(DAY, -2, GETDATE())),
(81, 5, 5, N'Quá ngon! Đã đặt nhiều lần và chất lượng luôn ổn định. Cơm không bị nhão, đồ ăn tươi ngon.', NULL, DATEADD(DAY, -10, GETDATE())),

(82, 2, 5, N'Mì xào hải sản tươi ngon, đặc biệt tôm rất tươi và ngọt. Sốt đậm đà, rất vừa miệng.', '/images/reviews/review-mi-xao-01.jpg', DATEADD(DAY, -7, GETDATE())),
(82, 4, 3, N'Mì ngon nhưng hôm nay hơi ít hải sản so với những lần trước đặt. Mong cửa hàng cải thiện.', NULL, DATEADD(DAY, -3, GETDATE())),

(83, 1, 4, N'Gà rán sốt cay đúng chuẩn vị Hàn Quốc. Giòn bên ngoài, mềm bên trong. Sốt cay vừa phải, dễ ăn.', '/images/reviews/review-ga-ran-01.jpg', DATEADD(DAY, -5, GETDATE())),
(83, 3, 5, N'Phần gà rán khá nhiều, sốt cay đậm đà. Khoai tây chiên giòn tan. Rất đáng tiền!', '/images/reviews/review-ga-ran-02.jpg', DATEADD(DAY, -8, GETDATE())),

-- DANH MỤC THỨC ĂN - Tiệm Bánh Bình An (ID: 6)
(84, 3, 5, N'Bánh mì thịt nướng thơm ngon, vỏ bánh giòn rụm. Thịt nướng đậm đà, rau sống tươi. Sẽ tiếp tục ủng hộ.', '/images/reviews/review-banh-mi-01.jpg', DATEADD(DAY, -4, GETDATE())),
(84, 5, 4, N'Bánh mì ngon, thịt nướng thơm. Chỉ tiếc là đồ chua hơi ít, nếu nhiều hơn thì tuyệt vời.', NULL, DATEADD(DAY, -6, GETDATE())),

(85, 1, 5, N'Bánh bông lan trứng muối ngon tuyệt! Phần nhân trứng muối béo, mặn vừa phải, bánh mềm xốp.', '/images/reviews/review-banh-bong-lan-01.jpg', DATEADD(DAY, -8, GETDATE())),
(85, 2, 5, N'Mình là tín đồ của bánh bông lan trứng muối và phải nói rằng đây là một trong những tiệm làm ngon nhất mình từng ăn.', '/images/reviews/review-banh-bong-lan-02.jpg', DATEADD(DAY, -5, GETDATE())),

(86, 3, 5, N'Bánh tiramisu chuẩn vị Ý, lớp phô mai mềm mịn, thơm mùi cafe. Ngọt vừa phải, rất hợp khẩu vị người Việt.', '/images/reviews/review-tiramisu-01.jpg', DATEADD(DAY, -7, GETDATE())),
(86, 4, 4, N'Bánh tiramisu ngon, làm rất cẩn thận. Chỉ tiếc là phần rượu rum hơi nhẹ so với khẩu vị của mình.', NULL, DATEADD(DAY, -3, GETDATE())),

-- DANH MỤC THỨC ĂN - Cafe & Trà Sữa Cao Nguyên (ID: 7)
(87, 4, 4, N'Cà phê đen đá đậm đà, hương vị rất Việt Nam. Cảm giác như uống cà phê ở Sài Gòn vậy.', NULL, DATEADD(DAY, -3, GETDATE())),
(87, 1, 5, N'Cà phê nguyên chất, thơm nồng, vị đắng vừa phải. Mỗi sáng đều đặt một ly để tỉnh táo làm việc.', NULL, DATEADD(DAY, -1, GETDATE())),

(88, 2, 5, N'Trà sữa thơm ngon, trân châu dẻo, vừa miệng. Ngọt vừa phải, rất hợp khẩu vị.', '/images/reviews/review-tra-sua-01.jpg', DATEADD(DAY, -2, GETDATE())),
(88, 3, 3, N'Trà sữa ổn nhưng hôm nay hơi ngọt. Trân châu đường nâu dẻo và thơm.', NULL, DATEADD(DAY, -5, GETDATE())),

(89, 5, 5, N'Sinh tố bơ đặc và thơm, vị bơ tự nhiên không bị ngọt quá. Phục vụ nhanh và chu đáo.', '/images/reviews/review-sinh-to-bo-01.jpg', DATEADD(DAY, -6, GETDATE())),
(89, 2, 4, N'Sinh tố bơ thơm ngon, vị béo đặc trưng của bơ kết hợp với sữa tươi. Chỉ tiếc là hơi ít.', NULL, DATEADD(DAY, -4, GETDATE())),

-- DANH MỤC THỨC ĂN - Đồ Ăn Vặt Dung Dung (ID: 8)
(90, 1, 5, N'Bánh tráng trộn đúng vị Sài Gòn, đầy đủ vị chua cay mặn ngọt. Phần lượng rất nhiều, ăn no luôn.', '/images/reviews/review-banh-trang-tron-01.jpg', DATEADD(DAY, -3, GETDATE())),
(90, 4, 4, N'Bánh tráng trộn ngon, gia vị đậm đà. Có điều bò khô hơi ít, mong shop cải thiện.', NULL, DATEADD(DAY, -5, GETDATE())),

(91, 3, 5, N'Takoyaki chuẩn vị Nhật, bên ngoài giòn, bên trong mềm. Bạch tuộc tươi, không bị dai.', '/images/reviews/review-takoyaki-01.jpg', DATEADD(DAY, -7, GETDATE())),
(91, 5, 4, N'Takoyaki ngon, bánh mềm và nhân bạch tuộc tươi. Chỉ tiếc là lần này hơi ít sốt mayonnaise.', NULL, DATEADD(DAY, -2, GETDATE())),

(92, 2, 5, N'Khoai tây lắc phô mai giòn rụm, vị phô mai thơm nồng. Ăn vặt cực kỳ đã miệng!', '/images/reviews/review-khoai-tay-lac-01.jpg', DATEADD(DAY, -4, GETDATE())),
(92, 1, 4, N'Khoai tây giòn, lắc phô mai vừa đủ. Phần ăn khá nhiều, 2 người ăn vẫn thấy no.', NULL, DATEADD(DAY, -6, GETDATE())),

-- DANH MỤC MỸ PHẨM - Nga Beauty (ID: 9)
(93, 5, 5, N'Kem chống nắng Innisfree thấm nhanh, không gây nhờn rít. Dùng makeup đè lên không bị vón cục. Rất thích!', '/images/reviews/review-kem-chong-nang-01.jpg', DATEADD(DAY, -15, GETDATE())),
(93, 2, 4, N'Kem chống nắng tốt, thích hợp cho da dầu. Chỉ có điều mùi hương hơi nồng, nhưng chất lượng thì không phàn nàn gì.', NULL, DATEADD(DAY, -7, GETDATE())),

(94, 3, 5, N'Nước tẩy trang Bioderma quá tuyệt vời! Làm sạch hiệu quả kể cả với makeup lâu trôi mà không làm khô da.', '/images/reviews/review-nuoc-tay-trang-01.jpg', DATEADD(DAY, -10, GETDATE())),
(94, 1, 5, N'Đã dùng rất nhiều loại nước tẩy trang nhưng Bioderma vẫn là số 1. Nhẹ dịu với da nhạy cảm, không gây kích ứng.', NULL, DATEADD(DAY, -12, GETDATE())),

(95, 4, 4, N'Phấn nước Laneige có độ che phủ tốt, kiểm soát dầu hiệu quả. Tiếc là hơi khô với da mình vào mùa đông.', '/images/reviews/review-phan-nuoc-01.jpg', DATEADD(DAY, -20, GETDATE())),
(95, 5, 5, N'Phấn nước Laneige Neo là best cushion mình từng dùng! Che phủ tốt, không trôi suốt ngày dài, da dầu dùng rất hợp.', NULL, DATEADD(DAY, -18, GETDATE())),

-- DANH MỤC MỸ PHẨM - Hùng Cosmetics (ID: 10)
(96, 4, 5, N'Son MAC Ruby Woo lên môi đẹp xuất sắc! Chất son lì nhưng không khô môi, giữ màu cả ngày.', '/images/reviews/review-son-mac-01.jpg', DATEADD(DAY, -8, GETDATE())),
(96, 5, 4, N'Màu son rất đẹp, lên chuẩn màu. Chỉ có điều hơi khô môi nếu không thoa son dưỡng trước.', '/images/reviews/review-son-mac-02.jpg', DATEADD(DAY, -5, GETDATE())),

(97, 2, 5, N'Bảng phấn mắt Urban Decay quá đỉnh! Màu lên chuẩn, dễ blend và giữ màu tốt.', '/images/reviews/review-phan-mat-01.jpg', DATEADD(DAY, -20, GETDATE())),
(97, 3, 5, N'Đã dùng rất nhiều bảng mắt nhưng Urban Decay vẫn là ưu tiên số 1. Màu sắc đa dạng, chất phấn mịn.', NULL, DATEADD(DAY, -15, GETDATE())),

(98, 1, 4, N'Mascara Maybelline làm mi dài và cong vút, không vón cục. Tiếc là hơi khó tẩy trang.', '/images/reviews/review-mascara-01.jpg', DATEADD(DAY, -12, GETDATE())),
(98, 5, 5, N'Mascara giá rẻ mà chất lượng đỉnh! Mi cong vút, dày và không lem trong cả ngày dài.', NULL, DATEADD(DAY, -8, GETDATE())),

-- DANH MỤC MỸ PHẨM - Lan Skincare (ID: 11)
(99, 3, 5, N'Serum Vitamin C Melano CC giúp mình cải thiện đáng kể vết thâm sau mụn. Dùng 1 tháng đã thấy hiệu quả rõ rệt.', '/images/reviews/review-serum-melano-01.jpg', DATEADD(DAY, -30, GETDATE())),
(99, 4, 4, N'Serum thấm nhanh, không gây kích ứng. Mình dùng được 2 tuần và thấy da sáng hơn, vết thâm mờ dần.', NULL, DATEADD(DAY, -25, GETDATE())),

(100, 2, 5, N'Mặt nạ ngủ Laneige cấp ẩm siêu đỉnh! Sáng dậy da căng mọng, không còn khô căng như trước.', '/images/reviews/review-mat-na-ngu-01.jpg', DATEADD(DAY, -15, GETDATE())),
(100, 1, 5, N'Best sleeping mask ever! Da mình thuộc da khô và rất dễ bong tróc vào mùa đông, nhưng từ khi dùng em này thì da căng mịn hẳn.', NULL, DATEADD(DAY, -10, GETDATE())),

(101, 5, 4, N'Sữa rửa mặt Cosrx dịu nhẹ, không làm khô da. pH thấp nên rất phù hợp với da nhạy cảm của mình.', '/images/reviews/review-sua-rua-mat-01.jpg', DATEADD(DAY, -20, GETDATE())),
(101, 3, 5, N'Sữa rửa mặt Cosrx là holy grail của mình! Làm sạch dịu nhẹ, không gây kích ứng da và giá cả phải chăng.', NULL, DATEADD(DAY, -18, GETDATE())),

-- DANH MỤC MỸ PHẨM - Minh Makeup (ID: 12)
(102, 1, 5, N'Bộ cọ 15 cây chất lượng tuyệt vời, lông cọ mềm mịn. Đặc biệt cọ tán phấn mắt rất dễ blend.', '/images/reviews/review-co-trang-diem-01.jpg', DATEADD(DAY, -25, GETDATE())),
(102, 4, 4, N'Bộ cọ đầy đủ chức năng, lông cọ mềm. Chỉ tiếc là cán cọ hơi trơn, khó cầm khi tay ướt.', NULL, DATEADD(DAY, -20, GETDATE())),

(103, 2, 5, N'Phấn phủ Innisfree No Sebum kiềm dầu cực đỉnh! Dùng cho da dầu mà makeup vẫn giữ được cả ngày.', '/images/reviews/review-phan-phu-01.jpg', DATEADD(DAY, -15, GETDATE())),
(103, 5, 4, N'Phấn phủ kiềm dầu tốt, nhỏ gọn tiện mang theo. Chỉ có điều khi dặm lại nhiều lần có thể bị cakey.', NULL, DATEADD(DAY, -10, GETDATE())),

(104, 3, 5, N'Kẹp mi Shiseido chắc chắn, uốn mi cong vút. Đã thử nhiều loại nhưng vẫn quay về với em này.', '/images/reviews/review-kep-mi-01.jpg', DATEADD(DAY, -30, GETDATE())),
(104, 1, 4, N'Kẹp mi tốt, mi cong đều. Hơi khó kẹp với mắt mí lót như mình, nhưng quen rồi thì cũng ok.', NULL, DATEADD(DAY, -25, GETDATE())),

-- DANH MỤC ĐỒ GIA DỤNG - Nhà Bếp Hạnh Phúc (ID: 13)
(105, 1, 5, N'Bộ nồi Lock&Lock chất lượng tuyệt vời, đáy nồi dày, giữ nhiệt tốt. Nấu ăn rất ngon và tiết kiệm gas.', '/images/reviews/review-bo-noi-01.jpg', DATEADD(DAY, -30, GETDATE())),
(105, 4, 4, N'Bộ nồi đẹp, chắc chắn và nấu rất đều nhiệt. Chỉ tiếc là hơi nặng khi cầm nắm.', NULL, DATEADD(DAY, -25, GETDATE())),

(106, 2, 5, N'Chảo Tefal xứng đáng với giá tiền. Chống dính hiệu quả, dễ vệ sinh và bền theo thời gian.', '/images/reviews/review-chao-tefal-01.jpg', DATEADD(DAY, -18, GETDATE())),
(106, 5, 5, N'Đã dùng chảo Tefal được 6 tháng và vẫn còn rất tốt. Thermo-Spot rất tiện để biết khi nào chảo đủ nóng.', NULL, DATEADD(DAY, -22, GETDATE())),

(107, 3, 5, N'Bộ dao Zwilling sắc bén đến kinh ngạc! Cắt thịt, rau củ đều nhẹ nhàng và chính xác.', '/images/reviews/review-dao-zwilling-01.jpg', DATEADD(DAY, -20, GETDATE())),
(107, 1, 5, N'Dao Zwilling là một khoản đầu tư xứng đáng cho nhà bếp. Sắc bén, cầm chắc tay và dễ vệ sinh.', NULL, DATEADD(DAY, -15, GETDATE())),

-- DANH MỤC ĐỒ GIA DỤNG - Quỳnh Electric House (ID: 15)
(111, 3, 5, N'Nồi cơm Cuckoo nấu cơm ngon không tưởng! Hạt cơm chín đều, dẻo thơm. Nhiều chức năng hữu ích.', '/images/reviews/review-noi-com-cuckoo-01.jpg', DATEADD(DAY, -45, GETDATE())),
(111, 1, 4, N'Nồi cơm rất tốt, nhiều chức năng. Chỉ tiếc là hướng dẫn sử dụng tiếng Việt hơi khó hiểu.', NULL, DATEADD(DAY, -40, GETDATE())),

(112, 4, 5, N'Máy lọc không khí Samsung thực sự hiệu quả. Không gian sống sạch hơn, bụi ít hơn và không còn mùi khó chịu.', '/images/reviews/review-may-loc-kk-01.jpg', DATEADD(DAY, -35, GETDATE())),
(112, 2, 5, N'Từ khi dùng máy lọc không khí Samsung, tình trạng dị ứng của tôi đỡ hẳn. Máy chạy êm, hiệu quả rõ rệt.', NULL, DATEADD(DAY, -30, GETDATE())),

(113, 5, 5, N'Bếp từ Electrolux thiết kế đẹp, nhiều chức năng an toàn. Nấu nhanh và tiết kiệm điện hơn bếp gas.', '/images/reviews/review-bep-tu-01.jpg', DATEADD(DAY, -25, GETDATE())),
(113, 3, 4, N'Bếp từ chất lượng tốt, nấu nướng tiện lợi. Chỉ tiếc là cần đầu tư thêm nồi chảo phù hợp.', NULL, DATEADD(DAY, -20, GETDATE())),

-- DANH MỤC ĐỒ CHƠI - Đồ Chơi Thủy Tinh (ID: 17)
(117, 5, 5, N'Bộ xếp hình Duplo tuyệt vời cho bé 3 tuổi nhà mình. Con rất thích và chơi hàng giờ, giúp phát triển khả năng sáng tạo.', '/images/reviews/review-xep-hinh-duplo-01.jpg', DATEADD(DAY, -15, GETDATE())),
(117, 1, 5, N'Đồ chơi chất lượng cao, an toàn cho trẻ. Các mảnh ghép vừa tay bé, màu sắc bắt mắt.', NULL, DATEADD(DAY, -10, GETDATE())),

(118, 2, 4, N'Thảm chơi rất tốt, nhiều màu sắc kích thích thị giác cho bé. Nhạc và đèn đều hoạt động tốt. Chỉ tiếc là hơi khó vệ sinh.', '/images/reviews/review-tham-choi-01.jpg', DATEADD(DAY, -20, GETDATE())),
(118, 3, 5, N'Thảm chơi đa năng, âm thanh và ánh sáng vừa phải không làm bé sợ. Con mình 5 tháng tuổi rất thích nằm lên thảm.', NULL, DATEADD(DAY, -18, GETDATE())),

(119, 4, 5, N'Xe đẩy tập đi hình gấu rất dễ thương và chắc chắn. Bé nhà mình 10 tháng đã tự đẩy đi quanh nhà.', '/images/reviews/review-xe-day-tap-di-01.jpg', DATEADD(DAY, -25, GETDATE())),
(119, 1, 4, N'Xe đẩy tập đi thiết kế an toàn, bánh xe di chuyển êm. Tiếc là nhạc hơi to, không điều chỉnh được âm lượng.', NULL, DATEADD(DAY, -22, GETDATE())),

-- DANH MỤC ĐỒ CHƠI - Game Zone Tuấn (ID: 18)
(120, 4, 5, N'Boardgame Catan cực kỳ thú vị! Đã tổ chức nhiều buổi chơi cùng bạn bè và ai cũng thích. Phiên bản tiếng Việt dễ hiểu.', '/images/reviews/review-catan-01.jpg', DATEADD(DAY, -25, GETDATE())),
(120, 5, 5, N'Một trò chơi tuyệt vời để kết nối mọi người. Luật chơi dễ hiểu nhưng đầy chiến thuật. Rất đáng đồng tiền.', NULL, DATEADD(DAY, -22, GETDATE())),

(121, 1, 4, N'Bài Uno luôn là lựa chọn tuyệt vời cho các buổi tụ tập gia đình. Dễ chơi, vui nhộn và phù hợp với mọi lứa tuổi.', '/images/reviews/review-uno-01.jpg', DATEADD(DAY, -15, GETDATE())),
(121, 3, 5, N'Bài chất lượng tốt, màu sắc rõ ràng. Đã mua nhiều bộ để tặng bạn bè vì quá thích trò chơi này.', NULL, DATEADD(DAY, -12, GETDATE())),

(122, 2, 5, N'Bộ cờ vua gỗ cao cấp, đẹp và chắc chắn. Quân cờ nặng vừa phải, đứng vững trên bàn cờ.', '/images/reviews/review-co-vua-01.jpg', DATEADD(DAY, -30, GETDATE())),
(122, 4, 5, N'Cờ vua chất lượng cao, bàn cờ gập gọn tiện lợi. Đã dạy con trai 7 tuổi chơi và bé rất thích.', NULL, DATEADD(DAY, -28, GETDATE())),

-- DANH MỤC ĐỒ CHƠI - Xuân Education Toys (ID: 20)
(126, 2, 5, N'Robot lập trình STEM là món quà tuyệt vời cho con trai 10 tuổi. Bé rất thích và học được nhiều kiến thức lập trình cơ bản.', '/images/reviews/review-robot-lap-trinh-01.jpg', DATEADD(DAY, -30, GETDATE())),
(126, 4, 5, N'Đồ chơi giáo dục chất lượng cao. Con mình dành hàng giờ để khám phá và lập trình robot. Rất đáng đồng tiền.', NULL, DATEADD(DAY, -28, GETDATE())),

(127, 1, 4, N'Kính hiển vi trẻ em có độ phóng đại tốt, dễ sử dụng. Con mình 8 tuổi có thể tự khám phá thế giới vi mô.', '/images/reviews/review-kinh-hien-vi-01.jpg', DATEADD(DAY, -20, GETDATE())),
(127, 5, 5, N'Kính hiển vi chất lượng tốt với giá cả phải chăng. Bộ mẫu vật đi kèm đa dạng và thú vị.', NULL, DATEADD(DAY, -18, GETDATE())),

(128, 3, 5, N'Bộ thí nghiệm khoa học tuyệt vời giúp con khám phá thế giới tự nhiên. Các thí nghiệm an toàn và đầy thú vị.', '/images/reviews/review-thi-nghiem-khoa-hoc-01.jpg', DATEADD(DAY, -35, GETDATE())),
(128, 5, 4, N'Bộ thí nghiệm rất hay, hướng dẫn chi tiết dễ hiểu. Một số thí nghiệm cần sự giám sát của người lớn nhưng đều rất thú vị.', '/images/reviews/review-thi-nghiem-khoa-hoc-02.jpg', DATEADD(DAY, -32, GETDATE()));