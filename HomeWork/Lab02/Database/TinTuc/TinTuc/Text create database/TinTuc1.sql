--Tao Database
Create Database Tintuc
--Tao cac Table
use Tintuc
Create Table Theloaitin
(
	IDLoai int identity primary key,
	Tentheloai nvarchar(100)
)
Go
Create Table Tintuc
(
	IdTin int identity Primary key,
	IDLoai int references Theloaitin(IDLoai),
	Tieudetin nvarchar(100),
	Noidungtin nText
)
Go
--Nhap lieu
Insert into Theloaitin(Tentheloai) values(N'Thể thao')
Insert into Theloaitin(Tentheloai) values(N'Kinh tế')
Insert into Theloaitin(Tentheloai) values(N'Thế giới')
Insert into Tintuc(IDLoai, Tieudetin, Noidungtin) values(2,N'Khủng hoảng
kinh tế trong năm 2012',N'Tình hình khủng hoảng kinh tế năm 2012 được các
chuyên gia kinh tế đánh giá . . .' )
Insert into Tintuc(IDLoai, Tieudetin, Noidungtin) values(1,N'Tranh chấp
trên biển đông',N'Trên các diễn đàn quân sự đang nóng dẫn về tinh hình biển
đông . . .' )