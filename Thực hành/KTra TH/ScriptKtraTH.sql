/*
Created		11/06/2020
Modified		11/06/2020
Project		
Model		
Company		
Author		
Version		
Database		MS SQL 7 
*/

create database KtraTH
GO
Use KtraTH
Go
Create table [Khuvuc] (
	[MaKV] Integer NOT NULL,
	[TenKV] Nchar(30) NULL,
Primary Key  ([MaKV])
) 
go

Create table [Khachhang] (
	[MaKH] Char(4) NOT NULL,
	[MaKV] Integer NOT NULL,
	[TenKH] Nchar(40) NULL,
	[Namsinh] Integer NULL,
Primary Key  ([MaKH],[MaKV])
) 
go


Alter table [Khachhang] add  foreign key([MaKV]) references [Khuvuc] ([MaKV]) 
go


Set quoted_identifier on
go

Set quoted_identifier off
go


