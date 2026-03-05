CREATE DATABASE QLKTX
USE QLKTX;


-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL UNIQUE,
    TenNV NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    DiaChi NVARCHAR(100),
    SoDienThoai NVARCHAR(20)
);

-- Tạo bảng Day
CREATE TABLE Day (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaDay NVARCHAR(50) NOT NULL UNIQUE,
    TenDay NVARCHAR(50) NOT NULL,
    QuanLy INT NOT NULL,
    TrangThai NVARCHAR(50),
    CONSTRAINT FK_Day_NhanVien FOREIGN KEY (QuanLy) REFERENCES NhanVien(MaNV)
);

-- Tạo bảng Phong
CREATE TABLE Phong (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong NVARCHAR(50) NOT NULL UNIQUE,
    TenPhong NVARCHAR(50) NOT NULL,
    SoSV INT,
    SoSVToiDa INT,
    TinhTrang NVARCHAR(50),
    LoaiPhong NVARCHAR(50),
    MaDay NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Phong_Day FOREIGN KEY (MaDay) REFERENCES Day(MaDay)
);

-- Tạo bảng SinhVien
CREATE TABLE SinhVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaSV INT NOT NULL UNIQUE,
    TenSV NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
	NgayDK DATE,
    QueQuan NVARCHAR(100),
    Khoa NVARCHAR(50),
    Lop NVARCHAR(50),
    MaPhong NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_SinhVien_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);

-- Tạo bảng NguoiDung
CREATE TABLE NguoiDung (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(50) NOT NULL,
    VaiTro NVARCHAR(50)
);

CREATE TABLE HopDong (
    MaHopDong NVARCHAR(50) PRIMARY KEY, 
    MaPhong NVARCHAR(50) NOT NULL, 
    MaSV INT NOT NULL UNIQUE,
    NgayBatDau DATE NOT NULL, 
    NgayKetThuc DATE NOT NULL, 
    TienPhong DECIMAL(18,2) NOT NULL, 
    DonGiaDien DECIMAL(18,2) NOT NULL, 
    DonGiaNuoc DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_HopDong_Phong FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong), 
    CONSTRAINT FK_HopDong_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV) 
);

CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY, 
    MaHoaDon NVARCHAR(50) NOT NULL UNIQUE, 
    MaHopDong NVARCHAR(50) NOT NULL, 
    MaNhanVien INT, 
    Ngay DATE NOT NULL, 
    TinhTrang NVARCHAR(50) NOT NULL, 
    SoDien INT NOT NULL, 
    SoNuoc INT NOT NULL, 
    CONSTRAINT FK_HoaDon_HopDong FOREIGN KEY (MaHopDong) REFERENCES HopDong(MaHopDong), 
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNV) 
);

SELECT 
    H.ID,
    H.MaHoaDon,
    H.MaHopDong,
    H.MaNhanVien,
    H.Ngay,
    HD.TienPhong,
    H.SoDien,
    H.SoNuoc,
    HD.DonGiaDien,
    HD.DonGiaNuoc,
    (H.SoDien * HD.DonGiaDien) AS TongTienDien,
    (H.SoNuoc * HD.DonGiaNuoc) AS TongTienNuoc,
    (HD.TienPhong + (H.SoDien * HD.DonGiaDien) + (H.SoNuoc * HD.DonGiaNuoc)) AS TongTien,
    H.TinhTrang
FROM HoaDon H
INNER JOIN HopDong HD ON H.MaHopDong = HD.MaHopDong;


SELECT * FROM NguoiDung;
SELECT * FROM NhanVien;
SELECT * FROM Day;
SELECT * FROM Phong;
SELECT * FROM SinhVien;
SELECT * FROM HopDong;
SELECT * FROM HoaDon;

DROP TABLE NguoiDung;
DROP TABLE NhanVien;
DROP TABLE Day;
DROP TABLE Phong;
DROP TABLE SinhVien	;
DROP TABLE HoaDon;
DROP TABLE HopDong;

INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro)
VALUES 
('admin1', 'password1', 'Admin'),
('admin2', 'password2', 'Admin'),
('admin3', 'password3', 'Admin'),
('admin4', 'password4', 'Admin'),
('admin5', 'password5', 'Admin'),
('admin6', 'password6', 'Admin'),
('admin7', 'password7', 'Admin'),
('admin8', 'password8', 'Admin'),
('admin9', 'password9', 'Admin'),
('admin10', 'password10', 'Admin'),
('staff1', 'password11', 'Staff'),
('staff2', 'password12', 'Staff'),
('staff3', 'password13', 'Staff'),
('staff4', 'password14', 'Staff'),
('staff5', 'password15', 'Staff'),
('staff6', 'password16', 'Staff'),
('staff7', 'password17', 'Staff'),
('staff8', 'password18', 'Staff'),
('staff9', 'password19', 'Staff'),
('staff10', 'password20', 'Staff');

INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DiaChi, SoDienThoai)
VALUES
(1001, N'Nguyễn Văn A', N'Nam', '1990-01-15', N'123 Đường ABC, Hà Nội', '0987654321'),
(1002, N'Trần Thị B', N'Nữ', '1992-02-20', N'456 Đường XYZ, TP HCM', '0987654322'),
(1003, N'Phạm Văn C', N'Nam', '1993-03-10', N'789 Đường DEF, Đà Nẵng', '0987654323'),
(1004, N'Lê Thị D', N'Nữ', '1994-04-25', N'123 Đường GHI, Cần Thơ', '0987654324'),
(1005, N'Hoàng Văn E', N'Nam', '1988-05-30', N'456 Đường JKL, Hải Phòng', '0987654325'),
(1006, N'Võ Thị F', N'Nữ', '1991-06-15', N'789 Đường MNO, Huế', '0987654326'),
(1007, N'Nguyễn Văn G', N'Nam', '1990-07-20', N'123 Đường PQR, Vinh', '0987654327'),
(1008, N'Trần Thị H', N'Nữ', '1992-08-25', N'456 Đường STU, Quy Nhơn', '0987654328'),
(1009, N'Phạm Văn I', N'Nam', '1993-09-10', N'789 Đường VWX, Nha Trang', '0987654329'),
(1010, N'Lê Thị K', N'Nữ', '1994-10-05', N'123 Đường YZA, Buôn Ma Thuột', '0987654330'),
(1011, N'Hoàng Văn L', N'Nam', '1988-11-15', N'456 Đường BCD, Hải Dương', '0987654331'),
(1012, N'Võ Thị M', N'Nữ', '1991-12-20', N'789 Đường EFG, Bắc Giang', '0987654332'),
(1013, N'Nguyễn Văn N', N'Nam', '1989-01-25', N'123 Đường HIJ, Nam Định', '0987654333'),
(1014, N'Trần Thị O', N'Nữ', '1993-02-15', N'456 Đường KLM, Bắc Ninh', '0987654334'),
(1015, N'Phạm Văn P', N'Nam', '1994-03-10', N'789 Đường NOP, Thái Bình', '0987654335'),
(1016, N'Lê Thị Q', N'Nữ', '1992-04-25', N'123 Đường QRS, Thanh Hóa', '0987654336'),
(1017, N'Hoàng Văn R', N'Nam', '1987-05-15', N'456 Đường TUV, Nghệ An', '0987654337'),
(1018, N'Võ Thị S', N'Nữ', '1990-06-10', N'789 Đường WXY, Quảng Ninh', '0987654338'),
(1019, N'Nguyễn Văn T', N'Nam', '1991-07-05', N'123 Đường ZAB, Bình Định', '0987654339'),
(1020, N'Trần Thị U', N'Nữ', '1993-08-25', N'456 Đường CDE, Long An', '0987654340'),
(1021, N'Phạm Văn V', N'Nam', '1989-09-15', N'789 Đường FGH, Bình Dương', '0987654341'),
(1022, N'Lê Thị W', N'Nữ', '1992-10-05', N'123 Đường IJK, Đồng Nai', '0987654342'),
(1023, N'Hoàng Văn X', N'Nam', '1994-11-20', N'456 Đường LMN, An Giang', '0987654343'),
(1024, N'Võ Thị Y', N'Nữ', '1988-12-15', N'789 Đường OPQ, Kiên Giang', '0987654344'),
(1025, N'Nguyễn Văn Z', N'Nam', '1990-01-10', N'123 Đường RST, Tây Ninh', '0987654345'),
(1026, N'Trần Thị A1', N'Nữ', '1991-02-05', N'456 Đường UVW, Tiền Giang', '0987654346'),
(1027, N'Phạm Văn B1', N'Nam', '1992-03-15', N'789 Đường XYZ, Đồng Tháp', '0987654347'),
(1028, N'Lê Thị C1', N'Nữ', '1993-04-10', N'123 Đường ABC, Bến Tre', '0987654348'),
(1029, N'Hoàng Văn D1', N'Nam', '1994-05-05', N'456 Đường DEF, Hậu Giang', '0987654349'),
(1030, N'Võ Thị E1', N'Nữ', '1988-06-15', N'789 Đường GHI, Sóc Trăng', '0987654350');


INSERT INTO Day (MaDay, TenDay, QuanLy, TrangThai)
VALUES
('D001', N'Dãy A', 1001, N'Hoạt động'),
('D002', N'Dãy B', 1002, N'Hoạt động'),
('D003', N'Dãy C', 1003, N'Hoạt động'),
('D004', N'Dãy D', 1004, N'Hoạt động'),
('D005', N'Dãy E', 1005, N'Còn trống'),
('D006', N'Dãy F', 1006, N'Còn trống'),
('D007', N'Dãy G', 1007, N'Còn trống'),
('D008', N'Dãy H', 1008, N'Còn trống'),
('D009', N'Dãy I', 1009, N'Bảo trì'),
('D010', N'Dãy J', 1010, N'Bảo trì'),
('D011', N'Dãy K', 1011, N'Bảo trì'),
('D012', N'Dãy L', 1012, N'Bảo trì'),
('D013', N'Dãy M', 1013, N'Hoạt động'),
('D014', N'Dãy N', 1014, N'Hoạt động'),
('D015', N'Dãy O', 1015, N'Còn trống'),
('D016', N'Dãy P', 1016, N'Còn trống'),
('D017', N'Dãy Q', 1017, N'Bảo trì'),
('D018', N'Dãy R', 1018, N'Bảo trì'),
('D019', N'Dãy S', 1019, N'Hoạt động'),
('D020', N'Dãy T', 1020, N'Hoạt động');

INSERT INTO Phong (MaPhong, TenPhong, SoSV, SoSVToiDa, TinhTrang, LoaiPhong, MaDay)
VALUES
('P001', N'Phòng A1', 3, 4, N'Thiếu', N'Nam', 'D001'),
('P002', N'Phòng A2', 4, 4, N'Đủ', N'Nữ', 'D001'),
('P003', N'Phòng B1', 2, 4, N'Thiếu', N'Nam', 'D002'),
('P004', N'Phòng B2', 0, 4, N'Thiếu', N'Nữ', 'D002'),
('P005', N'Phòng C1', 3, 3, N'Đủ', N'Nam', 'D003'),
('P006', N'Phòng C2', 2, 3, N'Thiếu', N'Nữ', 'D003'),
('P007', N'Phòng D1', 4, 4, N'Đủ', N'Nam', 'D004'),
('P008', N'Phòng D2', 1, 4, N'Thiếu', N'Nữ', 'D004'),
('P009', N'Phòng E1', 0, 4, N'Thiếu', N'Nữ', 'D005'),
('P010', N'Phòng E2', 3, 4, N'Thiếu', N'Nam', 'D005'),
('P011', N'Phòng F1', 4, 4, N'Đủ', N'Nam', 'D006'),
('P012', N'Phòng F2', 3, 4, N'Thiếu', N'Nữ', 'D006'),
('P013', N'Phòng G1', 1, 4, N'Thiếu', N'Nam', 'D007'),
('P014', N'Phòng G2', 0, 4, N'Thiếu', N'Nữ', 'D007'),
('P015', N'Phòng H1', 4, 4, N'Đủ', N'Nữ', 'D008'),
('P016', N'Phòng H2', 2, 4, N'Thiếu', N'Nam', 'D008'),
('P017', N'Phòng I1', 3, 3, N'Đủ', N'Nữ', 'D009'),
('P018', N'Phòng I2', 1, 3, N'Thiếu', N'Nam', 'D009'),
('P019', N'Phòng J1', 0, 4, N'Thiếu', N'Nữ', 'D010'),
('P020', N'Phòng J2', 4, 4, N'Đủ', N'Nam', 'D010'),
('P021', N'Phòng K1', 3, 4, N'Thiếu', N'Nữ', 'D011'),
('P022', N'Phòng K2', 4, 4, N'Đủ', N'Nam', 'D011'),
('P023', N'Phòng L1', 2, 3, N'Thiếu', N'Nữ', 'D012'),
('P024', N'Phòng L2', 3, 3, N'Đủ', N'Nam', 'D012'),
('P025', N'Phòng M1', 0, 4, N'Thiếu', N'Nữ', 'D013'),
('P026', N'Phòng M2', 2, 4, N'Thiếu', N'Nam', 'D013'),
('P027', N'Phòng N1', 4, 4, N'Đủ', N'Nữ', 'D014'),
('P028', N'Phòng N2', 1, 4, N'Thiếu', N'Nam', 'D014'),
('P029', N'Phòng O1', 3, 4, N'Thiếu', N'Nữ', 'D015'),
('P030', N'Phòng O2', 4, 4, N'Đủ', N'Nam', 'D015');

DELETE FROM SinhVien
DELETE FROM HoaDon
DELETE FROM HopDong
INSERT INTO SinhVien (MaSV, TenSV, GioiTinh, NgaySinh, NgayDK, QueQuan, Khoa, Lop, MaPhong)
VALUES
(10001, N'Nguyễn Văn A', N'Nam', '2003-01-15', '2024-09-01', N'Hà Nội', N'Công nghệ thông tin', N'CNTT01', 'P001'),
(10002, N'Trần Thị B', N'Nữ', '2002-12-25', '2024-09-01', N'Hải Phòng', N'Kinh tế', N'KT01', 'P002'),
(10003, N'Lê Văn C', N'Nam', '2001-11-10', '2024-09-02', N'Hồ Chí Minh', N'Kỹ thuật', N'KT02', 'P003'),
(10004, N'Phạm Thị D', N'Nữ', '2003-05-08', '2024-09-02', N'Đà Nẵng', N'Y dược', N'YD01', 'P004'),
(10005, N'Nguyễn Văn E', N'Nam', '2004-03-20', '2024-09-03', N'Hà Tĩnh', N'Công nghệ thông tin', N'CNTT02', 'P005'),
(10006, N'Trần Thị F', N'Nữ', '2003-02-14', '2024-09-03', N'Quảng Bình', N'Kinh tế', N'KT02', 'P006'),
(10007, N'Lê Văn G', N'Nam', '2002-07-19', '2024-09-04', N'Hà Nội', N'Kỹ thuật', N'KT03', 'P007'),
(10008, N'Phạm Thị H', N'Nữ', '2003-06-22', '2024-09-04', N'Hải Dương', N'Y dược', N'YD02', 'P008'),
(10009, N'Nguyễn Văn I', N'Nam', '2001-09-05', '2024-09-05', N'Nam Định', N'Công nghệ thông tin', N'CNTT03', 'P009'),
(10010, N'Trần Thị K', N'Nữ', '2004-04-18', '2024-09-05', N'Thái Bình', N'Kinh tế', N'KT03', 'P010'),
(10011, N'Lê Văn L', N'Nam', '2002-12-30', '2024-09-06', N'Nghệ An', N'Kỹ thuật', N'KT04', 'P011'),
(10012, N'Phạm Thị M', N'Nữ', '2003-08-16', '2024-09-06', N'Hà Nam', N'Y dược', N'YD03', 'P012'),
(10013, N'Nguyễn Văn N', N'Nam', '2001-10-09', '2024-09-07', N'Hải Phòng', N'Công nghệ thông tin', N'CNTT04', 'P013'),
(10014, N'Trần Thị O', N'Nữ', '2003-03-28', '2024-09-07', N'Bắc Ninh', N'Kinh tế', N'KT04', 'P014'),
(10015, N'Lê Văn P', N'Nam', '2002-06-12', '2024-09-08', N'Hồ Chí Minh', N'Kỹ thuật', N'KT05', 'P015'),
(10016, N'Phạm Thị Q', N'Nữ', '2004-01-03', '2024-09-08', N'Đà Nẵng', N'Y dược', N'YD04', 'P016'),
(10017, N'Nguyễn Văn R', N'Nam', '2003-09-11', '2024-09-09', N'Hà Tĩnh', N'Công nghệ thông tin', N'CNTT05', 'P017'),
(10018, N'Trần Thị S', N'Nữ', '2001-05-14', '2024-09-09', N'Quảng Bình', N'Kinh tế', N'KT05', 'P018'),
(10019, N'Lê Văn T', N'Nam', '2002-11-02', '2024-09-10', N'Hà Nội', N'Kỹ thuật', N'KT06', 'P019'),
(10020, N'Phạm Thị U', N'Nữ', '2003-12-21', '2024-09-10', N'Hải Dương', N'Y dược', N'YD05', 'P020'),
(10021, N'Nguyễn Văn V', N'Nam', '2004-02-24', '2024-09-11', N'Nam Định', N'Công nghệ thông tin', N'CNTT06', 'P021'),
(10022, N'Trần Thị W', N'Nữ', '2001-03-16', '2024-09-11', N'Thái Bình', N'Kinh tế', N'KT06', 'P022'),
(10023, N'Lê Văn X', N'Nam', '2003-07-25', '2024-09-12', N'Nghệ An', N'Kỹ thuật', N'KT07', 'P023'),
(10024, N'Phạm Thị Y', N'Nữ', '2002-10-14', '2024-09-12', N'Hà Nam', N'Y dược', N'YD06', 'P024'),
(10025, N'Nguyễn Văn Z', N'Nam', '2004-05-04', '2024-09-13', N'Hải Phòng', N'Công nghệ thông tin', N'CNTT07', 'P025'),
(10026, N'Trần Thị AA', N'Nữ', '2003-08-27', '2024-09-13', N'Bắc Ninh', N'Kinh tế', N'KT07', 'P026'),
(10027, N'Lê Văn BB', N'Nam', '2002-09-09', '2024-09-14', N'Hồ Chí Minh', N'Kỹ thuật', N'KT08', 'P027'),
(10028, N'Phạm Thị CC', N'Nữ', '2001-04-07', '2024-09-14', N'Đà Nẵng', N'Y dược', N'YD07', 'P028'),
(10029, N'Nguyễn Văn DD', N'Nam', '2003-11-13', '2024-09-15', N'Hà Tĩnh', N'Công nghệ thông tin', N'CNTT08', 'P029'),
(10030, N'Trần Thị EE', N'Nữ', '2002-06-23', '2024-09-15', N'Quảng Bình', N'Kinh tế', N'KT08', 'P030'),
(10031, N'Nguyễn Thị A', N'Nữ', '2003-01-20', '2023-08-01', N'Hà Nội', N'Kinh tế', N'KT09', 'P030'),
(10032, N'Lê Thị B', N'Nữ', '2002-02-15', '2023-07-10', N'Nam Định', N'Công nghệ thông tin', N'CNTT09', 'P030'),
(10033, N'Phạm Văn C', N'Nam', '2001-05-30', '2023-06-05', N'Hải Phòng', N'Kỹ thuật', N'KT10', 'P018'),
(10034, N'Nguyễn Thị D', N'Nữ', '2003-04-12', '2023-09-10', N'Quảng Ninh', N'Y dược', N'YD08', 'P028'),
(10035, N'Trần Thị E', N'Nữ', '2002-03-22', '2023-11-14', N'Thái Bình', N'Kinh tế', N'KT11', 'P016'),
(10036, N'Nguyễn Thị F', N'Nữ', '2003-09-04', '2023-08-25', N'Hà Tĩnh', N'Kỹ thuật', N'KT12', 'P026'),
(10037, N'Lê Văn G', N'Nam', '2001-12-20', '2023-05-18', N'Bắc Ninh', N'Công nghệ thông tin', N'CNTT10', 'P023'),
(10038, N'Phạm Thị H', N'Nữ', '2004-01-18', '2023-07-25', N'Quảng Bình', N'Y dược', N'YD09', 'P007'),
(10039, N'Nguyễn Văn I', N'Nam', '2003-06-10', '2023-04-13', N'Hồ Chí Minh', N'Kỹ thuật', N'KT13', 'P021'),
(10040, N'Lê Thị J', N'Nữ', '2002-11-28', '2023-08-30', N'Hải Dương', N'Kinh tế', N'KT14', 'P020'),
(10041, N'Nguyễn Văn K', N'Nam', '2004-02-15', '2025-01-05', N'Hà Nội', N'Công nghệ thông tin', N'CNTT11', 'P001'),
(10042, N'Phạm Thị L', N'Nữ', '2003-09-22', '2025-01-06', N'Hải Phòng', N'Kỹ thuật', N'KT15', 'P002'),
(10043, N'Nguyễn Thị M', N'Nữ', '2002-05-10', '2025-01-07', N'Đà Nẵng', N'Y dược', N'YD10', 'P003'),
(10044, N'Lê Văn N', N'Nam', '2001-08-14', '2025-01-08', N'Hồ Chí Minh', N'Kinh tế', N'KT16', 'P004'),
(10045, N'Trần Thị O', N'Nữ', '2003-02-03', '2025-01-09', N'Hải Dương', N'Kỹ thuật', N'KT17', 'P006');


	SELECT * 
	FROM SinhVien
	WHERE YEAR(NgayDK) = 2025