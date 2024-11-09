CREATE DATABASE QuanLyBanSach;
GO

USE QuanLyBanSach;
GO

-- bảng sach
CREATE TABLE Sach (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaSach VARCHAR(10) NOT NULL,
    TieuDe NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(100) NULL,
	NgonNgu nvarchar(20) not null,
	TacGia nvarchar(200) not null,
    TrangThai NVARCHAR(50) NOT NULL,
);
-- bảng NhaXuatBan 
CREATE TABLE NhaXuatBan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaNXB VARCHAR(10) NOT NULL,
    TenNXB NVARCHAR(50) NOT NULL,
    SĐT VARCHAR(10) NULL,
    DiaChi NVARCHAR(50) NULL,
    NamXB datetime NULL,
	TrangThai nvarchar(30) null
);
-- bảng thể loại
CREATE TABLE TheLoai (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaTheLoai VARCHAR(10) NOT NULL,
    TenTheLoai NVARCHAR(50) NOT NULL
);
-- bảng khách hàng
CREATE TABLE KhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang VARCHAR(10) NOT NULL,
    TenKhachHang NVARCHAR(50) NULL,
    SĐT VARCHAR(10) NULL,
    DiaChi NVARCHAR(50) NULL,
	GioiTinh nvarchar(10) null
);
-- bảng voucher
CREATE TABLE Voucher (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaVoucher VARCHAR(10) NOT NULL,
    NgayBatDau datetime NOT NULL,
    NgayKetThuc datetime NOT NULL,
    TiLeGiam float NOT NULL,
    DonHangToiThieu float NOT NULL,
    GiamToiDa float NOT NULL,
	SoLuong int not null
);
-- bảng Nhân viên
CREATE TABLE NhanVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaNV VARCHAR(10) NOT NULL,
    HoTenNV NVARCHAR(50) NOT NULL,
	Hinh varbinary(Max) null,
    TenTK VARCHAR(20) NOT NULL,
    MatKhau VARCHAR(20) NOT NULL,
    Email VARCHAR(50) NULL,
    CCCD VARCHAR(20) NOT NULL,
    NgaySinh DATETIME NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    DiaChi NVARCHAR(50) NULL,
    SĐT VARCHAR(10) NULL,
    TrangThai NVARCHAR(50) NOT NULL,
    VaiTro nvarchar(30) not null
);
-- bảng SachChiTiet
CREATE TABLE SachChiTiet (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MaSachCt VARCHAR(10) NOT NULL,
	Tap nvarchar(100) NOT NULL,
    SoLuong INT NOT NULL,
    SoTrang INT NOT NULL,
    GiaBan FLOAT NOT NULL,
    HinhAnh varbinary(Max) NOT NULL,
    IDSach INT REFERENCES Sach(ID),
	IDNXB INT REFERENCES NhaXuatBan(ID),
    IDTheLoai INT REFERENCES TheLoai(ID),
);

-- bảng hóa đơn
CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TrangThai NVARCHAR(50) NOT NULL,
    NgayMuaHang datetime NOT NULL,
    IDNhanvien INT REFERENCES NhanVien(ID),
    IDKhachhang INT REFERENCES KhachHang(ID),
    IDVoucher INT REFERENCES Voucher(ID),
	MaHd varchar(10) null
);
-- bảng hóa đơn chi tiết
CREATE TABLE HoaDonCt (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    SoLuongMua INT NOT NULL,
    GiaBan FLOAT NOT NULL,
    IDSachCt INT REFERENCES SachChiTiet(ID),
    IDHoaDon INT REFERENCES HoaDon(ID),
	MaHdct varchar(10) null,
	ThanhTien Float null,
	MaSpct varchar(10) null,
	TenSp nvarchar(100) null
);


