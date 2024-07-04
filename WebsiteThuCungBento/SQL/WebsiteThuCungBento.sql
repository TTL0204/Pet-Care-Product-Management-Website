Create database WebsiteThuCungBento
Go
Use WebsiteThuCungBento
Go
--------------------------------------------------TABLE--------------------------------------------------
Create table NhanVien(
	MaNV		int identity(1,1) not null,
	HoTen		nvarchar(50) not null,
	DiaChi		nvarchar(200),
	SDT			varchar(11) not null,
	TenLoai		nvarchar(100) not null,
	TenDN		varchar(50) not null,	
	MatKhau		varchar(50) not null,
	Avatar		varchar(50) not null,
	Email		varchar(100) not null,
);

Create table ChucNang_Quyen(
	MaCN	varchar(50) not null,
	TenCN	nvarchar(100),
);

Create table PhanQuyen(
	MaPQ	int identity(1,1) not null,
	MaNV	int not null,
	MaCN	varchar(50) not null,
);

Create table KhachHang(
	MaKH			int identity(1,1) not null,
	HoTen			nvarchar(50) not null,
	SDT				varchar(11) not null,
	DiaChi			nvarchar(200),
	TenDN			varchar(50) not null,
	MatKhau			varchar(50) not null,
	Email			varchar(100),
	NgaySinh		datetime check(NgaySinh < getdate()),
	HinhAnh			varchar(100),
	KhoiPhucMatKhau varchar(50),
);

Create table ChamSocKhachHang(
	MaNV	int not null,
	MaKH	int not null,
);

Create table ThuongHieu(
	MaTH	int identity(1,1) not null,
	TenTH	nvarchar(100) not null,
	Logo	varchar(100) not null,
	AnHien	bit, 
);

Create table MauSac(
	MaMauSac	int identity(1,1) not null,
	TenMauSac	nvarchar(50) not null,
	AnHien		bit,
);

Create table Loai(
	MaLoai	int identity(1,1) not null,
	TenLoai nvarchar(100) not null,
);

Create table SanPham(
	MaSP		int identity(1,1) not null,
	TenSP		nvarchar(200) not null,
	DonGiaMua	decimal(18, 2) check(DonGiaMua > 0),
	DonGiaBan	decimal(18, 2) check(DonGiaBan > 0),
	MaTH		int not null,
	MaLoai		int not null,
	MaMauSac	int not null,
	SoLuong		int not null check(SoLuong > 0),
	HinhAnh		varchar(100),
	MoTa		nvarchar(max),
	ThanhToanOn	nvarchar(200),
	AnHien		bit, 
);

Create table DonHang(
	MaDH			int identity(1,1) not null,
	MaKH			int not null,
	NgayDat			datetime not null,
	NgayGiao		datetime,
	TinhTrang		bit,
	DaThanhToan		bit,
	TongTien		decimal(18, 2),
	MaNV			int,
	check (NgayDat <= NgayGiao)
);

Create table CTDH(
	MaDH		int not null,
	MaSP		int not null,
	SoLuong		int check(SoLuong > 0) not null,
	DonGia		decimal(18, 2) check(DonGia > 0) not null,
	ThanhTien	decimal(18, 2) check(ThanhTien > 0),
);

Create table Hinh(
	MaHinh	int identity(1,1) not null,
	MaSP	int not null,
	Hinh1	varchar(100),
	AnHien	bit,
);

Create table KichThuoc(
	MaKT	int identity(1,1) not null,
	MaSP	int not null,
	TenKT	int not null,
	AnHien	bit,
);

Create table GiamGia(
	MaGiamGia		int identity(1,1) not null,
	MaSP			int not null,
	PhanTramGiam	int not null,
	AnHien			bit,
);

Create table PhieuNhapKho(
	MaPhieuNK	int identity(1,1) not null,
	NgayNK		datetime not null,
	MaSP		int not null,
	SoLuong		int check(SoLuong > 0) not null,
);
--------------------------------------------------PRIMARY KEY--------------------------------------------------
Alter table NhanVien add constraint PK_Admin primary key(MaNV);
Alter table ChucNang_Quyen add constraint PK_ChucNang_Quyen primary key(MaCN);
Alter table PhanQuyen add constraint PK_PhanQuyen primary key(MaPQ, MaNV, MaCN);
Alter table KhachHang add constraint PK_KhachHang primary key(MaKH);
Alter table ChamSocKhachHang add constraint PK_CSKH primary key(MaNV, MaKH);
Alter table ThuongHieu add constraint PK_ThuongHieu primary key(MaTH);
Alter table MauSac add constraint PK_MauSac primary key(MaMauSac);
Alter table Loai add constraint PK_Loai primary key(MaLoai);
Alter table SanPham add constraint PK_SanPham primary key(MaSP);
Alter table DonHang add constraint PK_DonHang primary key(MaDH);
Alter table CTDH add constraint PK_CTDH primary key(MaDH, MaSP);
Alter table Hinh add constraint PK_HINH primary key(MaHinh);
Alter table KichThuoc add constraint PK_KichThuoc primary key(MaKT);
Alter table GiamGia add constraint PK_GiamGia primary key(MaGiamGia);
Alter table PhieuNhapKho add constraint PK_PhieuNhapKho primary key(MaPhieuNK);
--------------------------------------------------FOREIGN KEY--------------------------------------------------
Alter table PhanQuyen add constraint FK_PQ_AD foreign key(MaNV) references NhanVien(MaNV),
						  constraint FK_PQ_CN foreign key(MaCN) references ChucNang_Quyen(MaCN);
Alter table ChamSocKhachHang add constraint FK_CSKH_NV foreign key(MaNV) references NhanVien(MaNV),
								 constraint FK_CSKH_KH foreign key(MaKH) references KhachHang(MaKH);
Alter table SanPham add constraint FK_SP_TH foreign key(MaTH) references ThuongHieu(MaTH),
					    constraint FK_SP_Loai foreign key(MaLoai) references Loai(MaLoai),
						constraint FK_SP_MauSac foreign key(MaMauSac) references MauSac(MaMauSac);
Alter table DonHang add constraint FK_DH_KH foreign key(MaKH) references KhachHang(MaKH),
						constraint FK_DH_NV foreign key(MaNV) references NhanVien(MaNV);
Alter table CTDH add constraint FK_CTDH_DH foreign key(MaDH) references DonHang(MaDH),
					 constraint FK_CTDH_SP foreign key(MaSP) references SanPham(MaSP);
Alter table Hinh add constraint FK_Hinh_SP foreign key(MaSP) references SanPham(MaSP);
Alter table KichThuoc add constraint FK_KT_SP foreign key(MaSP) references SanPham(MaSP);
Alter table GiamGia add constraint FK_GG_SP foreign key(MaSP) references SanPham(MaSP);
Alter table PhieuNhapKho add constraint FK_PNK_SP foreign key(MaSP) references SanPham(MaSP);
--------------------------------------------------TRIGGER--------------------------------------------------
--1. Cập nhật thành tiền(ThanhTien) trong bảng chi tiết đơn hàng(CTDH)
Go
Create trigger trg_CapNhatThanhTien on CTDH
for insert, update
as
begin
	update CTDH
	set ThanhTien = inserted.SoLuong * inserted.DonGia from CTDH
	inner join inserted on CTDH.MaDH = inserted.MaDH and CTDH.MaSP = inserted.MaSP;
end;
Go
--select * from CTDH;

--2. Cập nhật tổng tiền(TongTien) trong bảng đơn hàng(DonHang)
Go
Create trigger trg_CapNhatTongTien on CTDH
for insert, update, delete
as
begin
	--update DonHang
	--set TongTien = (select sum(ThanhTien) from CTDH where MaDH = inserted.MaDH)
	--from DonHang
	--inner join inserted on DonHang.MaDH = inserted.MaDH;
	if exists (select * from inserted)
    begin
        update DonHang
        set TongTien = (select sum(ThanhTien) from CTDH where MaDH = inserted.MaDH)
        from DonHang
        inner join inserted on DonHang.MaDH = inserted.MaDH;
    end
    if exists (select * from deleted)
    begin
        update DonHang
        set TongTien = (select sum(ThanhTien) from CTDH where MaDH = deleted.MaDH)
        from DonHang
        inner join deleted on DonHang.MaDH = deleted.MaDH;
    end
end;
Go
--select * from CTDH;
--select * from DonHang;

--3. Thêm dữ liệu vào bảng chăm sóc khách hàng(ChamSocKhachHang)
Go
Create trigger trg_ChamSocKhachHang on KhachHang
for insert
as
begin
    --Duyệt qua từng khách hàng mới
    declare @MaKH int;
    declare KhachHang_Cursor cursor for select MaKH from inserted;
	open KhachHang_Cursor;
	fetch next from KhachHang_Cursor into @MaKH;
	while @@FETCH_STATUS = 0
	begin
		--Tìm nhân viên(không phải admin) quản lý ít khách hàng nhất
		declare @MaNV int;
		select top 1 @MaNV = MaNV
		from NhanVien
		where TenLoai = N'Nhân viên'
		order by (select count(*) from ChamSocKhachHang where MaNV = NhanVien.MaNV);

		--Gán nhân viên được trigger chọn cho khách hàng mới đó
		insert into ChamSocKhachHang(MaNV, MaKH)
		values (@MaNV, @MaKH);

		--Tìm admin
        declare @MaAdmin int;
        select top 1 @MaAdmin = MaNV
        from NhanVien
        where TenLoai = N'Admin';

		--Gán admin cho khách hàng mới
		insert into ChamSocKhachHang(MaNV, MaKH)
		values (@MaAdmin, @MaKH);

		fetch next from KhachHang_Cursor into @MaKH;
	end;
	close KhachHang_Cursor;
    deallocate KhachHang_Cursor;
end;
Go
--select * from NhanVien;
--select * from ChamSocKhachHang;
--select * from KhachHang;

--4. Cập nhật nhân viên phụ trách phụ trách đơn hàng của khách hàng tương ứng đó
Go
Create trigger trg_DonHang_CapNhatMaNV on DonHang
for insert, update
as
begin
    --Duyệt qua từng đơn hàng mới
    declare @MaDH int, @MaKH int;
    declare DonHang_Cursor cursor for select MaDH, MaKH from inserted;
	open DonHang_Cursor;
	fetch next from DonHang_Cursor into @MaDH, @MaKH;
	while @@FETCH_STATUS = 0
	begin
		--Tìm nhân viên(không phải admin) quản lý khách hàng này
		declare @MaNV int;
		select top 1 @MaNV = MaNV
		from ChamSocKhachHang
		where MaKH = @MaKH and MaNV not in (select MaNV from NhanVien where TenLoai = N'Admin');

		--Cập nhật MaNV trong DonHang sao cho tương ứng với mã nhân viên đã phụ trách quản lý khách hàng
		update DonHang
		set MaNV = @MaNV
		where MaDH = @MaDH;

		fetch next from DonHang_Cursor into @MaDH, @MaKH;
	end;
	close DonHang_Cursor;
    deallocate DonHang_Cursor;
end;
Go
--select * from NhanVien;
--select * from ChamSocKhachHang;
--select * from DonHang;

--------------------------------------------------FUNCTION--------------------------------------------------
--1. Thống kê đơn hàng
Go
Create function fn_ThongKeDonHang()
returns int
as
begin
    declare @SoLuong int;
    select @SoLuong = count(*) from DonHang;
    return @SoLuong;
end;
Go
--select dbo.fn_ThongKeDonHang() as N'Tổng đơn hàng';

--2. Thống kê sản phẩm
Go
Create function fn_ThongKeSanPham()
returns int
as
begin
    declare @SoLuong int;
    select @SoLuong = count(*) from SanPham;
    return @SoLuong;
end;
Go
--select dbo.fn_ThongKeSanPham() as N'Tổng sản phẩm';

--3. Thống kê khách hàng
Go
Create function fn_ThongKeKhachHang()
returns int
as
begin
    declare @SoLuong int;
    select @SoLuong = count(*) from KhachHang;
    return @SoLuong;
end;
Go
--select dbo.fn_ThongKeKhachHang() as N'Tổng khách hàng';

--4. Thống kê nhân viên
Go
Create function fn_ThongKeNhanVien()
returns int
as
begin
    declare @SoLuong int;
    select @SoLuong = count(*) from NhanVien;
    return @SoLuong;
end;
Go
--select dbo.fn_ThongKeNhanVien() as N'Tổng nhân viên';

--5. Thống kê tổng doanh thu
Go
Create function fn_ThongKeDoanhThu()
returns decimal(18, 2)
as
begin
    declare @TongDoanhThu decimal(18, 2);
    select @TongDoanhThu = sum(SoLuong * DonGia) from CTDH;
    return @TongDoanhThu;
end;
Go
--select dbo.fn_ThongKeDoanhThu() as N'Tổng doanh thu';

--6. Thống kê doanh thu theo tháng
Go
Create function fn_ThongKeDoanhThuThang(@thang int, @nam int)
returns decimal(18, 2)
as
begin
    declare @TongDoanhThu decimal(18, 2);
    select @TongDoanhThu = sum(SoLuong * DonGia)
    from DonHang
    inner join CTDH on DonHang.MaDH = CTDH.MaDH
    where month(DonHang.NgayDat) = @thang and year(DonHang.NgayDat) = @nam;
    return @TongDoanhThu;
end;
Go
--declare @doanhthu decimal(18, 2);
--select @doanhthu = dbo.fn_ThongKeDoanhThuThang(7, 2023);
--print 'Doanh thu tháng 7 năm 2023 là: ' + cast(@doanhthu as varchar(20));

--7. Thống kê doanh thu theo mặt hàng
Go
Create function fn_ThongKeDoanhThuMatHang(@MaSP int)
returns decimal(18, 2)
as
begin
    declare @DoanhThu decimal(18, 2);
	select @DoanhThu = sum(CTDH.SoLuong * DonGia) from CTDH
	inner join DonHang on CTDH.MaDH = DonHang.MaDH
	where CTDH.MaSP = @MaSP
    return @DoanhThu;
end;
Go
--select dbo.fn_ThongKeDoanhThuMatHang(4);
--drop function dbo.fn_ThongKeDoanhThuMatHang;
--select * from SanPham;
--select * from CTDH;

--8. Thống kê doanh thu theo nhân viên bán hàng
Go
Create function fn_ThongKeDoanhThuNhanVien(@MaNV int)
returns decimal(18, 2)
as
begin
    declare @DoanhThu decimal(18, 2);
    select @DoanhThu = sum(SoLuong * DonGia) from CTDH 
	inner join DonHang on CTDH.MaDH = DonHang.MaDH where DonHang.MaNV = @MaNV;
    return @DoanhThu;
end;
Go
--select dbo.fn_ThongKeDoanhThuNhanVien(3);
--select * from DonHang;

--9 Thống kê doanh thu theo loại hàng
Go
Create function fn_ThongKeDoanhThuLoaiHang(@MaLoai int)
returns decimal(18, 2)
as
begin
    declare @DoanhThu decimal(18, 2);
    select @DoanhThu = sum(CTDH.SoLuong * DonGia) from CTDH 
	inner join SanPham on CTDH.MaSP = SanPham.MaSP where SanPham.MaLoai = @MaLoai;
    return @DoanhThu;
end;
Go
--select dbo.fn_ThongKeDoanhThuLoaiHang(1);
--select * from Loai;

--------------------------------------------------PROCEDURE--------------------------------------------------
--1. Thêm sản phẩm
Go
Create procedure sp_ThemSanPham
    @TenSP nvarchar(200),
    @DonGiaMua decimal(18, 2),
    @DonGiaBan decimal(18, 2),
    @MaTH int,
    @MaLoai int,
    @MaMauSac int,
    @SoLuong int,
    @HinhAnh varchar(100),
    @MoTa nvarchar(max),
    @ThanhToanOn nvarchar(200),
    @AnHien bit
as
begin
    insert into SanPham
    values (@TenSP, @DonGiaMua, @DonGiaBan, @MaTH, @MaLoai, @MaMauSac, @SoLuong, @HinhAnh, @MoTa, @ThanhToanOn, @AnHien);
end;
Go

--2. Xoá sản phẩm
Go
Create procedure sp_XoaSanPham
    @MaSP int
as
begin
	--Bắt đầu khối try. Mọi câu lệnh trong khối try sẽ được thực thi
	begin try
		begin transaction;
			delete from PhieuNhapKho where MaSP = @MaSP;
			delete from Hinh where MaSP = @MaSP;
			delete from SanPham where MaSP = @MaSP;
		--Kết thúc transaction và lưu tất cả các thay đổi đã thực hiện trong transaction
		commit transaction;
	end try
	
	--Bắt đầu khối catch
	begin catch
		-- Hủy bỏ transaction và không lưu bất kỳ thay đổi nào đã thực hiện trong transaction
		rollback transaction;
		throw;
	end catch
end;
Go
--execute sp_XoaSanPham 1;
--drop procedure sp_XoaSanPham;
--select * from SanPham;
--select * from Hinh;
--select * from PhieuNhapKho;

--3. Cập nhật sản phẩm
Go
Create procedure sp_CapNhatSanPham
    @MaSP int,
    @TenSP nvarchar(200),
    @DonGiaMua decimal(18, 2),
    @DonGiaBan decimal(18, 2),
    @MaTH int,
    @MaLoai int,
    @MaMauSac int,
    @SoLuong int,
    @HinhAnh varchar(100),
    @MoTa nvarchar(max),
    @ThanhToanOn nvarchar(200),
    @AnHien bit
as
begin
    update SanPham
    set TenSP = @TenSP,
        DonGiaMua = @DonGiaMua,
        DonGiaBan = @DonGiaBan,
        MaTH = @MaTH,
        MaLoai = @MaLoai,
        MaMauSac = @MaMauSac,
        SoLuong = @SoLuong,
        HinhAnh = @HinhAnh,
        MoTa = @MoTa,
        ThanhToanOn = @ThanhToanOn,
        AnHien = @AnHien
    where MaSP = @MaSP;
end;
Go

--4. Thêm khách hàng
Go
Create procedure sp_ThemKhachHang
    @HoTen nvarchar(50),
    @SDT char(11),
    @DiaChi nvarchar(200),
    @TenDN varchar(50),
    @MatKhau varchar(50),
    @Email varchar(100),
    @NgaySinh datetime,
    @HinhAnh varchar(100),
    @KhoiPhucMatKhau varchar(50)
as
begin
    insert into KhachHang
    values (@HoTen, @SDT, @DiaChi, @TenDN, @MatKhau, @Email, @NgaySinh, @HinhAnh, @KhoiPhucMatKhau);
end;
Go

--5. Xoá khách hàng
Go
Create procedure sp_XoaKhachHang
    @MaKH int
as
begin
	begin try
		begin transaction;
			delete from CTDH where MaDH in (select MaDH from DonHang where MaKH = @MaKH);
			delete from DonHang where MaKH = @MaKH;
			delete from ChamSocKhachHang where MaKH = @MaKH;
			delete from KhachHang where MaKH = @MaKH;
		commit transaction;
	end try
	begin catch
		rollback transaction;
		throw;
	end catch
end;
Go
--execute sp_XoaKhachHang 1;
--drop procedure sp_XoaKhachHang;
--select * from KhachHang;
--select * from ChamSocKhachHang;
--select * from DonHang;
--select * from CTDH;

--6. Cập nhật khách hàng
Go
Create procedure sp_CapNhatKhachHang
    @MaKH int,
    @HoTen nvarchar(50),
    @SDT char(11),
    @DiaChi nvarchar(200),
    @TenDN varchar(50),
    @MatKhau varchar(50),
    @Email varchar(100),
    @NgaySinh datetime,
    @HinhAnh varchar(100),
    @KhoiPhucMatKhau varchar(50)
as
begin
    update KhachHang
    set HoTen = @HoTen,
        SDT = @SDT,
        DiaChi = @DiaChi,
        TenDN = @TenDN,
        MatKhau = @MatKhau,
        Email = @Email,
        NgaySinh = @NgaySinh,
        HinhAnh = @HinhAnh,
        KhoiPhucMatKhau = @KhoiPhucMatKhau
    where MaKH = @MaKH;
end;
Go

--7. Xoá nhân viên
Go
Create procedure sp_XoaNhanVien
    @MaNV int
as
begin
	begin try
		begin transaction;
			delete from CTDH where MaDH in (select MaDH from DonHang where MaNV = @MaNV);
			delete from DonHang where MaNV = @MaNV;
			delete from ChamSocKhachHang where MaNV = @MaNV;
			delete from PhanQuyen where MaNV = @MaNV;
			delete from NhanVien where MaNV = @MaNV;
		commit transaction;
	end try
	begin catch
		rollback transaction;
		throw;
	end catch
end;
Go
--execute sp_XoaNhanVien 2;
--drop procedure sp_XoaNhanVien;
--select * from NhanVien;
--select * from PhanQuyen;
--select * from ChamSocKhachHang;
--select * from DonHang;

--8. Xoá thương hiệu
Go
Create procedure sp_XoaThuongHieu
	@MaTH int
as
begin
	begin try
		begin transaction;
			delete from CTDH where MaSP in (select MaSP from SanPham where MaTH = @MaTH);
			delete from GiamGia where MaSP in (select MaSP from SanPham where MaTH = @MaTH);
			delete from PhieuNhapKho where MaSP in (select MaSP from SanPham where MaTH = @MaTH);
			delete from Hinh where MaSP in (select MaSP from SanPham where MaTH = @MaTH);
			delete from SanPham where MaTH = @MaTH;	
			delete from ThuongHieu where MaTH = @MaTH;
		commit transaction;
	end try
	begin catch
		rollback transaction;
		throw;
	end catch
end;
Go
--execute sp_XoaThuongHieu 5;
--drop procedure sp_XoaThuongHieu;
--select * from ThuongHieu;
--select * from SanPham;
--select * from Hinh;
--select * from PhieuNhapKho;
--select * from GiamGia;
--select * from CTDH;

--9. Xoá loại sản phẩm
Go
Create procedure sp_XoaLoaiSanPham
    @MaLoai int
as
begin
    begin try
        begin transaction;
			delete from KichThuoc where MaSP in (select MaSP from SanPham where MaLoai = @MaLoai);
			delete from CTDH where MaSP in (select MaSP from SanPham where MaLoai = @MaLoai);
			delete from PhieuNhapKho where MaSP in (select MaSP from SanPham where MaLoai = @MaLoai);
            delete from Hinh where MaSP in (select MaSP from SanPham where MaLoai = @MaLoai);
            delete from SanPham where MaLoai = @MaLoai;
            delete from Loai where MaLoai = @MaLoai;
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw;
    end catch
end;
Go
--execute sp_XoaLoaiSanPham 1;
--drop procedure sp_XoaLoaiSanPham;
--select * from Loai;
--select * from SanPham;
--select * from Hinh;
--select * from PhieuNhapKho;
--select * from CTDH;
--select * from KichThuoc;

--10. Xoá màu sắc
Go
Create procedure sp_XoaMauSac
    @MaMauSac int
as
begin
    begin try
        begin transaction;
			delete from KichThuoc where MaSP in (select MaSP from SanPham where MaMauSac = @MaMauSac);
			delete from CTDH where MaSP in (select MaSP from SanPham where MaMauSac = @MaMauSac);
			delete from PhieuNhapKho where MaSP in (select MaSP from SanPham where MaMauSac = @MaMauSac);
			delete from Hinh where MaSP in (select MaSP from SanPham where MaMauSac = @MaMauSac);
            delete from SanPham where MaMauSac = @MaMauSac;
            delete from MauSac where MaMauSac = @MaMauSac;
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw;
    end catch
end;
Go
--execute sp_XoaMauSac 2;
--drop procedure sp_XoaMauSac;
--select * from MauSac;
--select * from SanPham;
--select * from Hinh;
--select * from PhieuNhapKho;
--select * from CTDH;
--select * from KichThuoc;

--11. Cập nhật màu sắc
Go
Create procedure sp_CapNhatMauSac
    @MaMauSac int,
    @TenMauSac nvarchar(50)
as
begin
    update MauSac
    set TenMauSac = @TenMauSac
    where MaMauSac = @MaMauSac;
end;
Go

--12. Xoá đơn hàng
Go
Create procedure sp_XoaDonHang
    @MaDH int
as
begin
    begin try
        begin transaction;
            delete from CTDH where MaDH = @MaDH;
            delete from DonHang where MaDH = @MaDH;
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw;
    end catch
end;
Go
--execute sp_XoaDonHang 1;
--drop procedure sp_XoaDonHang;
--select * from DonHang;
--select * from CTDH;

--13. Xoá một CTDH của một hoá đơn cụ thể
Go
Create procedure sp_XoaMotCTDH
    @MaDH int,
    @MaSP int
as
begin
    begin try
        begin transaction;
            delete from CTDH where MaDH = @MaDH and MaSP = @MaSP;
        commit transaction;
    end try
    begin catch
        rollback transaction;
        throw;
    end catch
end;
Go
--execute sp_XoaMotCTDH 1,5;
--drop procedure sp_XoaMotCTDH;
--select * from CTDH;
--select * from DonHang;

--14. Danh sách sản phẩm bán chạy nhất
Go
Create procedure sp_ListSPBanChayNhat
as
begin
    select top 3 SanPham.TenSP, sum(CTDH.SoLuong) as SoLuongDaBan
    from SanPham
    inner join CTDH on SanPham.MaSP = CTDH.MaSP
    group by SanPham.TenSP
    order by SoLuongDaBan desc;
end;
Go
--execute sp_ListSPBanChayNhat;
--drop procedure sp_ListSPBanChayNhat;
--select * from CTDH;

--15. Danh sách đơn hàng
Go
Create procedure sp_ListDSDonHang
as
begin
	select MaDH, HoTen, NgayGiao, DiaChi, TongTien
	from DonHang
	inner join KhachHang on DonHang.MaKH = KhachHang.MaKH;
end;
Go
--execute sp_ListDSDonHang;
--drop procedure sp_ListDSDonHang;

--16. Danh sách khách hàng được quản lý theo mã nhân viên
Go
Create procedure sp_ListDSKhachHangTheoNV
	@MaNV int
as
begin
	select KhachHang.*, NhanVien.HoTen as TenNV
	from KhachHang
	inner join ChamSocKhachHang on KhachHang.MaKH = ChamSocKhachHang.MaKH
	inner join NhanVien on ChamSocKhachHang.MaNV = NhanVien.MaNV
	where ChamSocKhachHang.MaNV = @MaNV;
end;
Go
--execute sp_ListDSKhachHangTheoNV 2;
--drop procedure sp_ListDSKhachHangTheoNV;
--select * from NhanVien;
--select * from KhachHang;
--select * from ChamSocKhachHang;

--17. Danh sách khách hàng được quản lý theo mã nhân viên(Trừ Admin)
Go
Create procedure sp_ListDSKhachHangVaNhanVienPhuTrach
as
begin
	select KhachHang.*, NhanVien.HoTen as TenNV
	from KhachHang
	inner join ChamSocKhachHang on KhachHang.MaKH = ChamSocKhachHang.MaKH
	inner join NhanVien on ChamSocKhachHang.MaNV = NhanVien.MaNV
	where NhanVien.TenLoai = N'Nhân viên';
end;
Go
--execute sp_ListDSKhachHangVaNhanVienPhuTrach;
--drop procedure sp_ListDSKhachHangVaNhanVienPhuTrach;

--18. Tìm kiếm sản phẩm
Go
Create procedure sp_TimKiemSanPham
	@searchString nvarchar(100)
as
begin
	select * from SanPham where TenSP like '%' + @searchString + '%';
end
Go
--execute sp_TimKiemSanPham N'Whiskas';
--drop procedure sp_TimKiemSanPham;

--------------------------------------------------DATA--------------------------------------------------
Insert into NhanVien values
(N'Trần Thành Luân', N'TP.Biên Hoà', '0367512498', N'Admin', 'admin', '123', 'TL.png', 'websitethucungbento@outlook.com.vn'),
(N'Nguyễn Hoàng Tuấn', N'Củ Chi', '0367512498', N'Nhân viên', 'staff1', '123', 'HT.jpg', 'nguyenhoangtuan@gmail.com'),
(N'Trần Lâu Bình', N'TP.Biên Hoà', '0367512498', N'Nhân viên', 'staff2', '123', 'LB.jpg', 'tranlaubinh@gmail.com');

Insert into ChucNang_Quyen values 
('QL_ChucNang', N'Quản lý chức năng quyền'),
('QL_DonHang', N'Quản lý đơn đặt hàng'),
('QL_HinhMoTa', N'Quản lý hình mô tả'),
('QL_KhachHang', N'Quản lý khách hàng'),
('QL_KhoSanPham', N'Quản lý kho sản phẩm'),
('QL_KichThuoc', N'Quản lý kích thước'),
('QL_LoaiSanPham', N'Quản lý loại sản phẩm'),
('QL_MauSac', N'Quản lý màu sắc'),
('QL_PhanQuyen', N'Quản lý phân quyền'),
('QL_QuanTriVien', N'Quản lý quản trị viên'),
('QL_SanPham', N'Quản lý sản phẩm'),
('QL_ThuongHieu', N'Quản lý thương hiệu');

Insert into PhanQuyen values
(1, 'QL_ChucNang'),
(1, 'QL_DonHang'),
(1, 'QL_HinhMoTa'),
(1, 'QL_KhachHang'),
(1, 'QL_KhoSanPham'),
(1, 'QL_KichThuoc'),
(1, 'QL_LoaiSanPham'),
(1, 'QL_MauSac'),
(1, 'QL_PhanQuyen'),
(1, 'QL_QuanTriVien'),
(1, 'QL_SanPham'),
(1, 'QL_ThuongHieu'),
(2, 'QL_DonHang'),
(2, 'QL_KhachHang'),
(2, 'QL_KhoSanPham'),
(2, 'QL_SanPham'),
(3, 'QL_DonHang'),
(3, 'QL_KhachHang'),
(3, 'QL_KhoSanPham'),
(3, 'QL_SanPham');

Set dateformat dmy;
Insert into KhachHang values
(N'Thành Luân', '0367512498', N'TP.Biên Hoà', 'thanhluan', '202cb962ac59075b964b07152d234b70', 'tranthanhluan02042003@gmail.com', '02/04/2003', 'account.png', null),
(N'Ngọc Thành', '0396105269', N'TP.Biên Hoà', 'ngocthanh', '202cb962ac59075b964b07152d234b70', 'dinhngocthanh@gmail.com', '01/07/2003', 'account.png', null),
(N'Hoàng Phúc', '0766471654', N'TP.Biên Hoà', 'hoangphuc', '202cb962ac59075b964b07152d234b70', 'nguyenhoangphuc@gmail.com', '12/03/2003', 'account.png', null),
(N'Văn Lộc', '0585711612', N'TP.Biên Hoà', 'vanloc', '202cb962ac59075b964b07152d234b70', 'levanloc@gmail.com', '04/07/2003', 'account.png', null),
(N'Xuân Hương', '0976032507', N'TP.Biên Hoà', 'xuanhuong', '202cb962ac59075b964b07152d234b70', 'nguyenthixuanhuong@gmail.com', '07/12/2003', 'account.png', null),
(N'Minh Trang', '0650810679', N'TP.Biên Hoà', 'minhtrang', '202cb962ac59075b964b07152d234b70', 'nguyenminhtrang@gmail.com', '24/12/2003', 'account.png', null);

Insert into ThuongHieu values
(N'Sanicat (Tây Ban Nha)', 'logo/LogoSP/Sanicat.jpg', null),
(N'Catsrang', 'logo/LogoSP/Catsrang.jpg', null),
(N'Flexi (Đức)', 'logo/LogoSP/Flexi.png', null),
(N'Iskhan (Hàn Quốc)', 'logo/LogoSP/Iskhan.png', null),
(N'Bio-Pharmachemie', 'logo/LogoSP/Bio-Pharmachemie.png', null),
(N'Ciao (Nhật Bản)', 'logo/LogoSP/Ciao.jpg', null),
(N'Royal Canin (Pháp)', 'logo/LogoSP/RoyalCanin.jpg', null),
(N'NutriSource (Mỹ)', 'logo/LogoSP/NutriSource.png', null),
(N'Cature ( Hàn Quốc)', 'logo/LogoSP/Cature.png', null),
(N'SmartHeart', 'logo/LogoSP/SmartHeart.png', null),
(N'Whiskas', 'logo/LogoSP/Whiskas.png', null),
(N'Khác', 'logo/LogoSP/petshop.jpg', null);

Insert into MauSac values
(N'Đen', null),
(N'Trắng', null),
(N'Đỏ', null),
(N'Xanh', null);

Insert into Loai values
(N'Thức ăn cho mèo'),
(N'Thức ăn cho chó'),
(N'Quần áo cho chó'),
(N'Quần áo cho mèo'),
(N'Phụ kiện vệ sinh'),
(N'Sức khỏe thú y'),
(N'Đồ chơi'),
(N'Khác');

Insert into SanPham values
(N'Bánh Thưởng Pedigree Dentastix Sạch Răng', 23500, 32000, 5, 2, 1, 100, 'product-image/thucancho/21.png', N'', NULL, NULL),
(N'THỨC ĂN ƯỚT ROYAL CANIN MAXI PUPPY', 100000, 159000, 7, 2, 1, 99, 'product-image/thucancho/11.png', N'ROYAL CANIN - MINI PUPPY CHO CHÓ DƯỚI 10 THÁNG Những chú chó của giống chó nhỏ chỉ cần những loại thức ăn có thiết kế nhỏ, tuy nhiên chúng lại cần nhiều năng lượng hơn giống chó lớn vì chúng có thời kỳ tăng trưởng ngắn hơn và mạnh mẽ hơn. Thêm vào đó, tuổi thọ trung bình của giống chó nhỏ cao hơn giống chó lớn, chính vì vậy chúng khá khắc khe trong việc ăn uống. Công thức của ROYAL CANIN MINI được thiết kế để...', NULL, NULL),
(N'THỨC ĂN CAO CẤP CHO CHÓ CON EARTHBORN HOLISTIC DOG PUPPY VANTAGE', 500000, 595000, 5, 2, 1, 99, 'product-image/thucancho/15.png', N'', NULL, NULL),
(N'Smartheart - Pate lon vị bò cho chó 400gr', 40000, 45000, 10, 2, 1, 97, 'product-image/thucancho/22.png', N'- Có thể cho cún con dùng trực tiếp hoặc trộn chung với các loại rau, củ, quả, hạt tùy thích.', NULL, NULL),
(N'Smartheart - treat healthy hip happy joint 100g', 10000, 19000, 10, 2, 1, 98, 'product-image/thucancho/23.png', N'Bánh Xương giúp xương chắc khỏe năng động - Smartheart treat healthy', NULL, NULL),
(N'Sốt Gà Cá Ngừ Whiskas cho mèo 85g', 8000, 15000, 11, 1, 1, 100, 'product-image/thucanmeo/1.png', N'Bánh Xương giúp xương chắc khỏe năng động - Smartheart treat healthy', NULL, NULL),
(N'ÁO LIỀN QUẦN PIKACHU', 90000, 100000, 3, 3, 1, 100, 'product-image/quanao/22.png', N'Giúp cho thú cưng hóa trang thành những con vật thật ngộ nghĩnh và dễ thương, chất liệu êm ái, thoải mái khi mặc.', NULL, NULL),
(N'Whiskas - Tuna 1,2kg', 100000, 119000, 11, 1, 1, 97, 'product-image/thucanmeo/2.png', N'Thức ăn cho mèo lớn Whiskas vị cá ngừ 1,2kg được lựa chọn từ những thành phần thịt, cá tươi ngon nhất trong chế biến, giàu protein, các vitamin và khoáng chất thiết yếu và không có chất bảo quản, mang đến tác dụng cân bằng dinh dưỡng hàng ngày cho thú cưng của bạn.', NULL, NULL),
(N'Whiskas - Pate Ocean Fish 400g (lon)', 35000, 45000, 11, 1, 1, 97, 'product-image/thucanmeo/4.png', N'Thức ăn cho mèo lớn Whiskas vị cá ngừ 1,2kg được lựa chọn từ những thành phần thịt, cá tươi ngon nhất trong chế biến, giàu protein, các vitamin và khoáng chất thiết yếu và không có chất bảo quản, mang đến tác dụng cân bằng dinh dưỡng hàng ngày cho thú cưng của bạn.', NULL, NULL),
(N'Whiskas - Vị cá thu (mackarel) 1,2kg', 100000, 130000, 11, 1, 2, 100, 'product-image/thucanmeo/6.png', N'Thức ăn cho mèo lớn Whiskas vị cá ngừ 1,2kg được lựa chọn từ những thành phần thịt, cá tươi ngon nhất trong chế biến, giàu protein, các vitamin và khoáng chất thiết yếu và không có chất bảo quản, mang đến tác dụng cân bằng dinh dưỡng hàng ngày cho thú cưng của bạn.', NULL, NULL),
(N'Whiskas - Pate Vị cá thu cho mèo lớn (Mackarel)85g', 8000, 15000, 11, 1, 2, 198, 'product-image/thucanmeo/5.png', N'Thức ăn cho mèo lớn Whiskas vị cá ngừ 1,2kg được lựa chọn từ những thành phần thịt, cá tươi ngon nhất trong chế biến, giàu protein, các vitamin và khoáng chất thiết yếu và không có chất bảo quản, mang đến tác dụng cân bằng dinh dưỡng hàng ngày cho thú cưng của bạn.', NULL, NULL),
(N'Whiskas - Pate Tuna mèo con junior 85g', 2000000, 3500000, 11, 1, 2, 100, 'product-image/thucanmeo/7.png', N'Thức ăn cho mèo lớn Whiskas vị cá ngừ 1,2kg được lựa chọn từ những thành phần thịt, cá tươi ngon nhất trong chế biến, giàu protein, các vitamin và khoáng chất thiết yếu và không có chất bảo quản, mang đến tác dụng cân bằng dinh dưỡng hàng ngày cho thú cưng của bạn.', NULL, NULL),
(N'Ciao - Creamy vị cá ngừ xông khói 14g', 5000, 10000, 6, 1, 2, 100, 'product-image/thucanmeo/8.png', N'Thành phần', NULL, NULL),
(N'Ciao - Creamy vị cá ngừ và sò điệp 14g', 5000, 10000, 6, 1, 1, 98, 'product-image/thucanmeo/9.png', N'Thành phần', NULL, NULL),
(N'Ciao - Creamy vị cá ngừ trắng 14g', 5000, 10000, 6, 1, 1, 100, 'product-image/thucanmeo/10.png', N'Thành phần ', NULL, NULL),
(N'Ciao - Creamy mix vị gà chicken 14g- Hộp 50 thanh', 400000, 438000, 6, 1, 3, 99, 'product-image/thucanmeo/11.png', N'Ciao Churu là một trong những món bánh thưởng hấp dẫn nhất đối với các cô cậu mèo. Dạng sốt siêu mịn cùng mùi vị hấp dẫn, đảm bảo bé mèo nhà bạn không thể rời khỏi tuýp Ciao Churu 1 phút nào.', NULL, NULL),
(N'Ciao - Creamy mix vị cá ngừ tuna 14g- Hộp 50 thanh', 400000, 438000, 6, 1, 3, 98, 'product-image/thucanmeo/12.png', N'Ciao Churu là một trong những món bánh thưởng hấp dẫn nhất đối với các cô cậu mèo. Dạng sốt siêu mịn cùng mùi vị hấp dẫn, đảm bảo bé mèo nhà bạn không thể rời khỏi tuýp Ciao Churu 1 phút nào.', NULL, NULL),
(N'Ciao - Creamy mix vị cá ngừ collagen chống búi lông 14g- Hộp 50 thanh', 400000, 438000, 6, 1, 2, 98, 'product-image/thucanmeo/13.png', N'Ciao Churu là một trong những món bánh thưởng hấp dẫn nhất đối với các cô cậu mèo. Dạng sốt siêu mịn cùng mùi vị hấp dẫn, đảm bảo bé mèo nhà bạn không thể rời khỏi tuýp Ciao Churu 1 phút nào.', NULL, NULL),
(N'SmartHeart Creamy Dog Treats Chicken & Spinach 15gx4', 30000, 40000, 10, 2, 2, 99, 'product-image/thucancho/2.png', N'Với công thức vượt trội giúp ', NULL, NULL),
(N'SmartHeart- Bánh quy Dog Treats Grilled Chicken Flavor 100g', 15000, 25000, 10, 2, 1, 100, 'product-image/thucancho/4.png', N'Với công thức vượt trội giúp ', NULL, NULL),
(N'SmartHeart- Bánh quy Dog Treats Fruit & Veggie Flavor 100g', 15000, 25000, 10, 2, 1, 100, 'product-image/thucancho/3.png', N'Với công thức vượt trội giúp ', NULL, NULL),
(N'Smartheart - Puppy Power Pack 3kg', 200000, 279000, 10, 2, 1, 100, 'product-image/thucancho/5.png', N'Với công thức vượt trội giúp ', NULL, NULL),
(N'Smartheart - Pate vị bò cho chó lớn 130gr', 10000, 19000, 10, 2, 1, 100, 'product-image/thucancho/6.png', N'Pate Smartheart  vị bò  cho chó lớn 130gr là sản phẩm được nghiên cứu sản xuất để phù hợp với nhu cầu dinh dưỡng của chú cún cưng nhà bạn. Với đầy đủ các dưỡng chất thiết yếu cùng với hương vị thịt bò và thịt cừu đầy hấp dẫn, Thức Ăn Cho Chó Pate Smartheart  vị bò  cho chó 130gr chắc hẳn sẽ là lựa chọn hàng đầu giúp cún cưng của bạn phát triển khỏe mạnh.', NULL, NULL),
(N'SmartHeart Creamy Dog Treats Strawberry Flavor 15gx4', 30000, 40000, 10, 2, 2, 100, 'product-image/thucancho/7.png', N'Pate Smartheart  vị bò  cho chó lớn 130gr là sản phẩm được nghiên cứu sản xuất để phù hợp với nhu cầu dinh dưỡng của chú cún cưng nhà bạn. Với đầy đủ các dưỡng chất thiết yếu cùng với hương vị thịt bò và thịt cừu đầy hấp dẫn, Thức Ăn Cho Chó Pate Smartheart  vị bò  cho chó 130gr chắc hẳn sẽ là lựa chọn hàng đầu giúp cún cưng của bạn phát triển khỏe mạnh.', NULL, NULL),
(N'SmartHeart Creamy Dog Treats Chicken & Pumpkin 15gx4', 30000, 40000, 10, 2, 2, 100, 'product-image/thucancho/8.png', N'Pate Smartheart  vị bò  cho chó lớn 130gr là sản phẩm được nghiên cứu sản xuất để phù hợp với nhu cầu dinh dưỡng của chú cún cưng nhà bạn. Với đầy đủ các dưỡng chất thiết yếu cùng với hương vị thịt bò và thịt cừu đầy hấp dẫn, Thức Ăn Cho Chó Pate Smartheart  vị bò  cho chó 130gr chắc hẳn sẽ là lựa chọn hàng đầu giúp cún cưng của bạn phát triển khỏe mạnh.', NULL, NULL),
(N'Áo khoác khủng long', 70000, 120000, 12, 3, 2, 99, 'product-image/quanao/21.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nỉ họa tiết thỏ hồng nhạt L', 35000, 45000, 12, 3, 2, 100, 'product-image/quanao/5.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo hoodie comic L', 90000, 120000, 12, 3, 2, 100, 'product-image/quanao/6.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo Phao Cho Chó Cưng Cỡ S/M/L/XL/XXL, Áo Phao, Bảo Quản An Toàn Khi Bơi Thiết Kế Cánh Phản Quang', 80000, 110000, 12, 3, 2, 100, 'product-image/quanao/18.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nỉ họa tiết dứa xanh M', 40000, 60000, 12, 3, 2, 100, 'product-image/quanao/2.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nỉ bông xanh đậm L', 40000, 60000, 12, 3, 2, 100, 'product-image/quanao/1.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo khoác phao cho thú cưng', 70000, 100000, 12, 3, 2, 95, 'product-image/quanao/15.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nỉ bông xanh nhạt XL', 40000, 60000, 12, 3, 2, 200, 'product-image/quanao/8.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nón bò sữa', 70000, 120000, 12, 4, 2, 99, 'product-image/quanao/19.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo Superman size 7', 35000, 45000, 12, 4, 2, 100, 'product-image/quanao/4.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nỉ họa tiết thỏ hồng nhạt L', 35000, 45000, 12, 4, 2, 100, 'product-image/quanao/5.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo len có mũ xỏ hai chân trang trí tai voi hoạt hình dành cho thú cưng', 70000, 100000, 12, 4, 2, 99, 'product-image/quanao/17.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'DOGGYDOLLY CAT - ÁO HÌNH ẾCH CHO MÈO', 100000, 130000, 12, 4, 2, 100, 'product-image/quanao/12.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Combo 4 áo size S', 100000, 160000, 12, 4, 2, 100, 'product-image/quanao/13.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo xanh bò sữa', 30000, 50000, 12, 4, 2, 100, 'product-image/quanao/14.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo + nón hình vịt', 50000, 80000, 12, 4, 2, 47, 'product-image/quanao/16.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Áo nỉ bông xanh nhạt XL', 40000, 60000, 12, 4, 2, 20, 'product-image/quanao/8.png', N'+ Chất liệu mềm mại, êm ái, không kích ứng da, phù hợp cho cún yêu nhà bạn', NULL, NULL),
(N'Cature- Hạt khử mùi mèo Fresh Scent Deodoriser- Bathing ocean Smell lọ 200v', 90000, 120000, 12, 5, 1, 100, 'product-image/dungcuvesinh/1.png', N'- Chỉ cần rắc ít lên cát vệ sinh! Chiến đấu chống lại mùi hôi! Không còn mùi chứ không chỉ ác mùi.', NULL, NULL),
(N'Cát vệ sinh Kit cat 10 lít cát vón (xanh)', 100000, 159000, 12, 5, 1, 100, 'product-image/dungcuvesinh/2.png', N'Đặc điểm nổi bật của Cát vệ sinh cho mèo:', NULL, NULL),
(N'Cát vệ sinh Kit cat 10 lít cát vón (xanh lá)', 100000, 159000, 12, 5, 1, 100, 'product-image/dungcuvesinh/3.png', N'Đặc điểm nổi bật của Cát vệ sinh cho mèo:', NULL, NULL),
(N'Cát vệ sinh Kit cat 10 lít cát vón (tím)', 100000, 159000, 12, 5, 1, 100, 'product-image/dungcuvesinh/4.png', N'Đặc điểm nổi bật của Cát vệ sinh cho mèo:', NULL, NULL),
(N'Bioline - Xịt vệ sinh đúng chỗ', 40000, 55000, 12, 5, 1, 100, 'product-image/dungcuvesinh/5.png', N'Đặc điểm nổi bật của Cát vệ sinh cho mèo:', NULL, NULL),
(N'Găng tay chải lông - không hộp', 25000, 35000, 12, 5, 1, 100, 'product-image/dungcuvesinh/6.png', N'Đặc điểm nổi bật của Cát vệ sinh cho mèo:', NULL, NULL),
(N'Natural Pet - Bổ sung dinh dưỡng da lông 60v', 550000, 595000, 12, 6, 1, 100, 'product-image/suckhoe/1.png', N'Skin & Coat Supplement Tablet là viên nhai bổ sung dinh dưỡng thiết yếu về da lông cho chó mèo và các loài động vật nhỏ của Natural Pet, Singapore.', NULL, NULL),
(N'SS - Đồ chơi mèo hình tròn gắm lông vũ', 110000, 130000, 12, 7, 1, 100, 'product-image/dochoi/1.png', N'Đồ chơi cho mèo ngộ nghĩnh, dễ thương, với phần lông vũ buộc vào đầu cây gậy, bạn sẽ tha hồ đùa giỡn với chú mèo của mình', NULL, NULL),
(N'TOY CAT - Đồ chơi mèo banh lục lạc', 15000, 29000, 12, 7, 1, 100, 'product-image/dochoi/2.png', N'Đồ chơi mèo banh lục lạc CityZoo được làm bằng chất liệu nhựa cao cấp, sẽ là món quà ý nghĩa của bạn dành cho thú cưng của mình.', NULL, NULL),
(N'Bàn lò xo mèo tập tát đế vuông', 70000, 89000, 12, 7, 1, 100, 'product-image/dochoi/3.png', N'Bàn lò xo mèo tập tát với thiết kế độc đáo gồm thân bàn giữ thăng bằng và lo xo lắc lư qua lại, đầu trang trí bằng chuột bông và bông như thật sẽ khiến Mèo nhà bạn sẽ không thể dời mắt khỏi chúng .', NULL, NULL),
(N'Bàn cào móng hình ngói', 110000, 130000, 12, 7, 1, 100, 'product-image/dochoi/4.png', N'Bàn cào móng hình ngói', NULL, NULL),
(N'CTCBio - Mài móng mèo', 200000, 249000, 12, 7, 1, 100, 'product-image/dochoi/5.png', N'Đồ chơi cho mèo ngộ nghĩnh, dễ thương, với phần lông vũ buộc vào đầu cây gậy, bạn sẽ tha hồ đùa giỡn với chú mèo của mình', NULL, NULL),
(N'CTCBio - Đồ chơi bóng đá Soccerball', 40000, 50000, 12, 7, 1, 100, 'product-image/dochoi/6.png', N'Đồ chơi cho mèo ngộ nghĩnh, dễ thương, với phần lông vũ buộc vào đầu cây gậy, bạn sẽ tha hồ đùa giỡn với chú mèo của mình', NULL, NULL),
(N'Nhà Quả dưa hấu nhỏ ABC', 200000, 250000, 12, 8, 1, 100, 'product-image/khac/1.png', N'Nhà hình quả dưa hấu nhỏ ABC xanh 32 x 41 x 26cm', NULL, NULL),
(N'Nhà lều vịt vàng ABC', 350000, 399000, 12, 8, 1, 100, 'product-image/khac/2.png', N'Nhà hình quả dưa hấu nhỏ ABC xanh 32 x 41 x 26cm', NULL, NULL),
(N'Nhà lều ếch xanh ABC', 350000, 399000, 12, 8, 1, 100, 'product-image/khac/3.png', N'- Ngôi nhà được làm từ chất liệu cotton thông thoáng, dễ vệ sinh và phù hợp với khí hậu nóng ẩm ở Việt Nam.', NULL, NULL),
(N'Balo phi hành da nhựa trong tai mèo', 200000, 250000, 12, 8, 1, 100, 'product-image/khac/4.png', N'Đồ chơi cho mèo ngộ nghĩnh, dễ thương, với phần lông vũ buộc vào đầu cây gậy, bạn sẽ tha hồ đùa giỡn với chú mèo của mình', NULL, NULL),
(N'Nệm pikachu ABC số 4', 400000, 435000, 12, 8, 1, 100, 'product-image/khac/5.png', N'Với nhiều người, chó mèo không chỉ là động vật giúp giữ nhà, đuổi chuột mà còn là người bạn nhỏ thân thiết và rất trung thành. Do đó, hãy thể hiện tình yêu thương của bạn dành cho “người bạn nhỏ” đáng yêu này bằng cách trang bị cho chúng một chiếc nệm nằm êm ái và xinh xắn của ABC để thú cưng có một nơi thư giãn thật thoải mái.', NULL, NULL),
(N'Balo di chuyển phi hành gia ( nhựa trong)', 350000, 399000, 12, 8, 1, 100, 'product-image/khac/6.png', N'Đồ chơi cho mèo ngộ nghĩnh, dễ thương, với phần lông vũ buộc vào đầu cây gậy, bạn sẽ tha hồ đùa giỡn với chú mèo của mình', NULL, NULL),
(N'Bát ăn đôi lớn', 450000, 499000, 12, 8, 1, 100, 'product-image/khac/9.png', N'Với nhiều người, chó mèo không chỉ là động vật giúp giữ nhà, đuổi chuột mà còn là người bạn nhỏ thân thiết và rất trung thành. Do đó, hãy thể hiện tình yêu thương của bạn dành cho “người bạn nhỏ” đáng yêu này bằng cách trang bị cho chúng một chiếc nệm nằm êm ái và xinh xắn của ABC để thú cưng có một nơi thư giãn thật thoải mái.', NULL, NULL),
(N'Bộ ăn uống tự động (800gr và 400ml)', 550000, 595000, 12, 8, 1, 100, 'product-image/khac/8.png', N'Với nhiều người, chó mèo không chỉ là động vật giúp giữ nhà, đuổi chuột mà còn là người bạn nhỏ thân thiết và rất trung thành. Do đó, hãy thể hiện tình yêu thương của bạn dành cho “người bạn nhỏ” đáng yêu này bằng cách trang bị cho chúng một chiếc nệm nằm êm ái và xinh xắn của ABC để thú cưng có một nơi thư giãn thật thoải mái.', NULL, NULL),
(N'Bát bobo 3062, 15x7cm', 45000, 68000, 12, 8, 1, 100, 'product-image/khac/10.png', N'Với nhiều người, chó mèo không chỉ là động vật giúp giữ nhà, đuổi chuột mà còn là người bạn nhỏ thân thiết và rất trung thành. Do đó, hãy thể hiện tình yêu thương của bạn dành cho “người bạn nhỏ” đáng yêu này bằng cách trang bị cho chúng một chiếc nệm nằm êm ái và xinh xắn của ABC để thú cưng có một nơi thư giãn thật thoải mái.', NULL, NULL),
(N'Cây mèo leo Roller CLASSIC COMFORT', 650000, 750000, 12, 7, 1, 100, 'product-image/khac/11.png', N'Với các nhà thiết kế đầy tính sáng tạo giàu kinh nghiệm, nhân viên chuyên nghiệp và đội ngũ quản lý có năng lực, AFP và Pawise đang khẳng định vị thế của mình trong lĩnh vực chăm sóc thú cưng, mong muốn mang đến những sản phẩm an toàn, đáng tin cậy cùng dịch vụ hiệu quả.', NULL, NULL),
(N'Cây mèo leo 3 tầng VINTAGE CAT', 4800000, 4950000, 12, 7, 1, 100, 'product-image/khac/12.png', N'Cây mèo leo 3 tầng sẽ giúp bạn tiết kiệm không gian và cho mèo một khu sinh hoạt đầy đủ nhất. Mèo sẽ leo lên cao khi vui chơi và nghỉ ngơi ở dưới khi cần ngủ.', NULL, NULL),
(N'Bóng lật đật', 30000, 40000, 12, 7, 1, 100, 'product-image/khac/13.png', N'Cây mèo leo 3 tầng sẽ giúp bạn tiết kiệm không gian và cho mèo một khu sinh hoạt đầy đủ nhất. Mèo sẽ leo lên cao khi vui chơi và nghỉ ngơi ở dưới khi cần ngủ.', NULL, NULL),
(N'Nhỏ gáy ngăn ngừa ve cho mèo con', 30000, 45000, 12, 6, 1, 100, 'product-image/suckhoe/2.png', N'Nhỏ gáy ngăn ngừa ve cho mèo con', NULL, NULL),
(N'Vòng cổ ngăn ngừa ve cho chó', 150000, 177000, 12, 6, 1, 100, 'product-image/suckhoe/3.png', N'ETOpure Vòng cổ ngăn ngừa ve cho chó được sản xuất bằng công nghệ đảm bảo vòng cổ liên tục tiết ra các chiết xuất tự nhiên từ Hoa oải hương và Margosa dưới lông của thú cưng, đẩy lùi hiệu quả ve bọ mà không ảnh hưởng đến sức khỏe của thú cưng và chủ nuôi cùng những đặc tính nổi bật.', NULL, NULL),
(N'Canxi phospho 50 viên', 280000, 305000, 12, 6, 1, 100, 'product-image/suckhoe/4.png', N'Canxi phospho 50 viên', NULL, NULL),
(N'Nước rửa khóe mắt', 150000, 193000, 12, 6, 1, 100, 'product-image/suckhoe/5.png', N'Canxi phospho 50 viên', NULL, NULL),
(N'THỨC ĂN CATSRANG CHO MÈO MỌI LỨA TUỔI', 50000, 60000, 2, 1, 1, 100, 'product-image/thucanmeo/14.png', N'THỨC ĂN CHO MÈO CATSRANG CHO MỌI LỨA TUỔI (Indoor/Outdoor) 1. Đặc điểm nổi bật Catsrang là thức ăn dành cho mọi giống mèo, mọi lứa tuổi. Catsrang là sản phẩm thức ăn hạt cao cấp đã được nghiên cứu công thức kỹ lưỡng nhằm đảm bảo cho mèo ở mọi lứa tuổi dinh dưỡng đầy đủ nhất Sử dụng protein cao cấp rất tốt...', NULL, NULL),
(N'THỨC ĂN CATSRANG KITTEN CHO MÈO CON ', 60000, 75000, 2, 1, 1, 96, 'product-image/thucanmeo/15.png', N'THỨC ĂN CATSRANG KITTEN CHO MÈO CON 1. Đặc điểm nổi bật Thức ăn hạt dành cho mèo con dưới 12 tháng tuổi -Ngăn ngừa búi lông ở mèo -Tốt cho sức khỏe -Tăng cường hệ miễn dịch -Tăng cường sức khỏe răng miệng -Giảm thiểu mùi hôi phân, nước tiểu Khối...', NULL, NULL),
(N'Thức Ăn Hạt Cho Mèo Trưởng Thành Catsrang Adult', 70000, 80000, 2, 1, 1, 100, 'product-image/thucanmeo/16.png', N'Thức ăn CATSRANG KITTEN là thức ăn khô dành cho mèo con từ 2-10 tháng tuổi.', NULL, NULL),
(N'Dây Dắt Chó Tự Động Flexi New Comfort (L) – Đức', 1000000, 1070000, 3, 8, 1, 99, 'product-image/khac/14.png', N'- Dây dắt được sản xuất 100% tại Đức và được nhập khẩu về Việt Nam theo đường chính ngạch.', NULL, NULL),
(N'Dây Dắt Chó Tự Động Flexi New Comfort (L) – Đức', 1000000, 1070000, 3, 8, 1, 100, 'product-image/khac/1.jpg', N'- Dây dắt được sản xuất 100% tại Đức và được nhập khẩu về Việt Nam theo đường chính ngạch.', NULL, NULL),
(N'Dây Dắt Chó Tự Động Flexi New Comfort (M) – Đức', 700000, 802000, 3, 8, 1, 100, 'product-image/khac/2.jpg', N'- Dây dắt được sản xuất 100% tại Đức và được nhập khẩu về Việt Nam theo đường chính ngạch.', NULL, NULL),
(N'Flexi Fun dây dắt tự động', 350000, 394000, 3, 8, 1, 100, 'product-image/khac/15.png', N'- Dây dắt được sản xuất 100% tại Đức và được nhập khẩu về Việt Nam theo đường chính ngạch.', NULL, NULL),
(N'NUTRISOURCE PUREVITA CHÓ - SALMON&PEAS 2.3KG', 350000, 385000, 8, 2, 1, 100, 'product-image/thucancho/10.png', N'Duy trì sức đề kháng cho chó con: chất chống oxy hóa CELT. Hỗ trợ hệ tiêu hóa hoạt động ổn định: L.I.P, đường FOS. Cung cấp dinh dưỡng toàn diện cho chó: chế biến theo công thức cung cấp năng lượng cao.', NULL, NULL),
(N'Thức ăn cho chó trưởng thành cỡ nhỏ ROYAL CANIN Mini Adult', 135000, 155000, 7, 2, 1, 100, 'product-image/thucancho/12.png', N'ROYAL CANIN Mini Adult được nghiên cứu để cung cấp dinh dưỡng theo nhu cầu thực tế của chó. Cung cấp năng lượng theo nhu cầu dinh dưỡng của chó. Duy trì da và lông khỏe mạnh: EPA, DHA. Kiểm soát cân nặng lý tưởng cho chó: L-Carnitine', NULL, NULL),
(N'Cature- Cát gỗ tự nhiên smart pellet cho mèo 2.4kg', 135000, 155000, 9, 5, 1, 99, 'product-image/dungcuvesinh/10.png', N'Được làm từ 100% từ gỗ cùng với hạt than hoạt tính.', NULL, NULL),
(N'Thức ăn cho mèo con Royal Canin Kitten 2kg', 135000, 155000, 7, 2, 1, 100, 'product-image/thucanmeo/18.png', N'ROYAL CANIN Mini Adult được nghiên cứu để cung cấp dinh dưỡng theo nhu cầu thực tế của chó. Cung cấp năng lượng theo nhu cầu dinh dưỡng của chó. Duy trì da và lông khỏe mạnh: EPA, DHA. Kiểm soát cân nặng lý tưởng cho chó: L-Carnitine', NULL, NULL),
(N'Cát Hạt Thủy Tinh Cho Mèo Sanicat Silica Gel Hương Lô Hội, 5L', 100000, 129000, 1, 5, 1, 100, 'product-image/dungcuvesinh/8.png', N'Cát thuỷ tinh Sanicat Silica Gel oải hương hỗ trợ vệ sinh cho mèo với công thức hương thơm đặc biệt. Kiểu hạt cát dễ dàng sử dụng và chăm sóc tối ưu, hạn chế mùi, hạn chế vi khuẩn.', NULL, NULL),
(N'SANICAT BENTONITE - CÁT VỆ SINH HẠT TRẮNG 8 L', 100000, 129000, 1, 5, 1, 100, 'product-image/dungcuvesinh/7.png', N'Thành phần 100% Bentonite (đất sét) trắng tự nhiên, cát vệ sinh cho mèo Sanicat thế hệ mới mang đến tính năng khử mùi vượt trội, kiểm soát vi khuẩn và vón cục tốt với các hạt cát siêu trắng.', NULL, NULL),
(N'Cát Thuỷ Tinh Sanicat Silica Gel Oải Hương, 15L', 350000, 379000, 1, 5, 1, 100, 'product-image/dungcuvesinh/9.png', N'Cát thuỷ tinh Sanicat Silica Gel oải hương hỗ trợ vệ sinh cho mèo với công thức hương thơm đặc biệt. Kiểu hạt cát dễ dàng sử dụng và chăm sóc tối ưu, hạn chế mùi, hạn chế vi khuẩn.', NULL, NULL),
(N'THỨC ĂN DÀNH CHO CHÓ VIÊM DA VÀ DỊ ỨNG ALLERGY FREE 1,2KG | ISKHAN 5', 250000, 294000, 4, 2, 1, 100, 'product-image/thucancho/16.png', N'Iskhan Allergy free 1,2kg-Thức ăn cho chó trưởng thành biếng ăn là một loại thực phẩm dành riêng cho những chú chó trưởng thành nhưng gặp vấn đề về đường ruột dẫn đến khả năng hấp thu kém. Sản phẩm bao gồm các thành phần nguyên liệu dinh dưỡng cao và an toàn nhất, để thúc đẩy quá trình phục hồi sức khỏe cho chó.', NULL, NULL),
(N'Thức ăn khô Iskhan Sensitive', 135000, 155000, 5, 7, 1, 97, 'product-image/thucancho/4.png', N'Thức ăn khô Iskhan Sensitive là dòng sản phẩm thức ăn hạt dành cho thú cưng. Với công thức vượt trội không chứa thành phần ngũ cốc, chất bảo quản hay sản phẩm làm thay đổi gen.', NULL, NULL),
(N'Royal canin mini adult', 135000, 155000, 5, 7, 1, 98, 'product-image/thucanmeo/4.png', N'ROYAL CANIN Mini Adult được nghiên cứu để cung cấp dinh dưỡng theo nhu cầu thực tế của chó. Cung cấp năng lượng theo nhu cầu dinh dưỡng của chó. Duy trì da và lông khỏe mạnh: EPA, DHA. Kiểm soát cân nặng lý tưởng cho chó: L-Carnitine', NULL, NULL);

Insert into DonHang values
(1, '08/07/2023', '09/07/2023', 0, 0, null, null),
(2, '08/07/2023', '10/07/2023', 0, 1, null, null),
(2, '08/07/2023', '09/07/2023', 0, 0, null, null),
(3, '08/07/2023', '10/07/2023', 0, 0, null, null),
(4, '08/07/2023', '11/07/2023', 0, 1, null, null),
(6, '09/07/2023', '10/07/2023', 0, 0, null, null);

Insert into CTDH values
(1, 3, 1, 595000, null),
(1, 4, 4, 45000, null),
(1, 5, 1, 19000, null),
(1, 9, 1, 45000, null),
(2, 2, 1, 159000, null),
(2, 4, 3, 45000, null),
(2, 8, 1, 119000, null),
(2, 11, 1, 15000, null),
(2, 26, 1, 120000, null),
(2, 37, 1, 100000, null),
(3, 8, 1, 119000, null),
(3, 9, 1, 45000, null),
(3, 11, 1, 15000, null),
(3, 14, 1, 10000, null),
(3, 17, 1, 438000, null),
(4, 4, 2, 45000, null),
(4, 5, 1, 19000, null),
(4, 9, 1, 45000, null),
(4, 14, 1, 10000, null),
(4, 18, 1, 438000, null),
(4, 19, 1, 40000, null),
(4, 73, 4, 75000, null),
(5, 8, 1, 119000, null),
(5, 16, 1, 438000, null),
(5, 17, 1, 438000, null),
(5, 18, 1, 438000, null),
(5, 34, 1, 120000, null),
(5, 41, 1, 80000, null),
(6, 87, 3, 155000, null),
(6, 88, 2, 155000, null);

Insert into Hinh values
(1, N'product-image/thucancho/21-1.png', null),
(2, N'product-image/3-1.png', null),
(3, N'product-image/3-1.png', null),
(4, N'product-image/3-1.png', null),
(5, N'product-image/3-1.png', null),
(6, N'product-image/3-1.png', null),
(7, N'product-image/bangsize.png', null),
(8, N'product-image/3-1.png', null),
(9, N'product-image/3-1.png', null),
(10, N'product-image/3-1.png', null),
(11, N'product-image/3-1.png', null),
(12, N'product-image/3-1.png', null),
(13, N'product-image/3-1.png', null),
(14, N'product-image/3-1.png', null),
(15, N'product-image/3-1.png', null),
(16, N'product-image/3-1.png', null),
(17, N'product-image/3-1.png', null),
(18, N'product-image/3-1.png', null),
(19, N'product-image/3-1.png', null),
(20, N'product-image/3-1.png', null),
(21, N'product-image/3-1.png', null),
(22, N'product-image/3-1.png', null),
(23, N'product-image/3-1.png', null),
(24, N'product-image/3-1.png', null),
(25, N'product-image/3-1.png', null),
(26, N'product-image/bangsize.png', null),
(27, N'product-image/bangsize.png', null),
(28, N'product-image/bangsize.png', null),
(29, N'product-image/bangsize.png', null),
(30, N'product-image/bangsize.png', null),
(31, N'product-image/bangsize.png', null),
(32, N'product-image/bangsize.png', null),
(33, N'product-image/bangsize.png', null),
(34, N'product-image/bangsize.png', null),
(35, N'product-image/bangsize.png', null),
(36, N'product-image/bangsize.png', null),
(37, N'product-image/bangsize.png', null),
(38, N'product-image/bangsize.png', null),
(39, N'product-image/bangsize.png', null),
(40, N'product-image/bangsize.png', null),
(41, N'product-image/bangsize.png', null),
(42, N'product-image/bangsize.png', null),
(43, N'product-image/dungcuvesinh/1.png', null),
(44, N'product-image/dungcuvesinh/2.png', null),
(45, N'product-image/dungcuvesinh/3.png', null),
(46, N'product-image/dungcuvesinh/4.png', null),
(47, N'product-image/dungcuvesinh/5.png', null),
(48, N'product-image/dungcuvesinh/6.png', null),
(49, N'product-image/suckhoe/1.png', null),
(50, N'product-image/dochoi/1.png', null),
(51, N'product-image/dochoi/2.png', null),
(52, N'product-image/dochoi/3.png', null),
(53, N'product-image/dochoi/4.png', null),
(54, N'product-image/dochoi/5.png', null),
(55, N'product-image/dochoi/6.png', null),
(56, N'product-image/khac/1.png', null),
(57, N'product-image/khac/2.png', null),
(58, N'product-image/khac/3.png', null),
(59, N'product-image/khac/4.png', null),
(60, N'product-image/khac/5.png', null),
(61, N'product-image/khac/6.png', null),
(62, N'product-image/khac/9.png', null),
(63, N'product-image/khac/8.png', null),
(64, N'product-image/khac/10.png', null),
(65, N'product-image/khac/11.png', null),
(66, N'product-image/khac/12.png', null),
(67, N'product-image/khac/13.png', null),
(68, N'product-image/suckhoe/2.png', null),
(69, N'product-image/suckhoe/3.png', null),
(70, N'product-image/suckhoe/4.png', null),
(71, N'product-image/suckhoe/5.png', null),
(72, N'product-image/thucanmeo/14.png', null),
(73, N'product-image/thucanmeo/15.png', null),
(74, N'product-image/thucanmeo/16.png', null),
(75, N'product-image/khac/14.png', null),
(76, N'product-image/khac/1.png', null),
(77, N'product-image/khac/2.png', null),
(78, N'product-image/khac/15.png', null),
(79, N'product-image/thucancho/10.png', null),
(80, N'product-image/thucancho/12.png', null),
(81, N'product-image/dungcuvesinh/10.png', null),
(82, N'product-image/thucanmeo/18.png', null),
(83, N'product-image/dungcuvesinh/8.png', null),
(84, N'product-image/dungcuvesinh/7.png', null),
(85, N'product-image/dungcuvesinh/9.png', null),
(86, N'product-image/thucancho/16.png', null),
(87, N'product-image/thucancho/4.png', null),
(88, N'product-image/thucanmeo/4.png', null);

Insert into KichThuoc values
(41, 0, null),
(41, 1, null),
(28, 2, null);

Insert into PhieuNhapKho values
('07/07/2023', 1, 100),
('07/07/2023', 2, 100),
('07/07/2023', 3, 100),
('07/07/2023', 4, 100),
('07/07/2023', 5, 100),
('07/07/2023', 6, 100),
('07/07/2023', 8, 100),
('07/09/2023', 7, 100),
('07/07/2023', 9, 100),
('07/07/2023', 10, 100),
('07/07/2023', 11, 100),
('07/07/2023', 12, 100),
('07/07/2023', 13, 100),
('07/07/2023', 14, 100),
('07/07/2023', 15, 100),
('07/07/2023', 16, 100),
('07/07/2023', 17, 100),
('07/07/2023', 18, 100),
('07/07/2023', 19, 100),
('07/07/2023', 20, 100),
('07/07/2023', 21, 100),
('07/07/2023', 22, 100),
('07/07/2023', 23, 100),
('07/07/2023', 24, 100),
('07/07/2023', 25, 100),
('07/07/2023', 26, 100),
('07/07/2023', 27, 100),
('07/07/2023', 28, 100),
('07/07/2023', 29, 100),
('07/07/2023', 30, 100),
('07/07/2023', 31, 100),
('07/07/2023', 86, 100),
('07/07/2023', 87, 100),
('07/07/2023', 88, 100);