CREATE DATABASE QuanLiNS
GO
USE QuanLiNS

CREATE TABLE PHONGBAN
(
	MAPB CHAR(100) primary key,
	TENPB nvarchar(100)
)
GO
CREATE TABLE NHANVIEN
(
	MANV VARCHAR(100) primary key,
	TENNV NVARCHAR(30),
	NGAYSINH DATETIME,
	MAPB CHAR(100)
	  CONSTRAINT FK_maphongban FOREIGN KEY (MAPB)

    REFERENCES PHONGBAN(MAPB)
)