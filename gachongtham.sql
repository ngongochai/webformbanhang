use DB_9FBCDE_hai

create database DB_9FBCDE_hai
use gachongtham
create table product(
	productid int not null identity ,
	categoryid nvarchar(100),
	productname nvarchar(200),
	imagesname nvarchar(50),
	codeproduct nvarchar(20) not null,
	price decimal,
	viewall int,
	size nvarchar(100),
)
select * from product
create table slideshow(
	slide1 nvarchar(max),
	slide2 nvarchar(max),
	slide3 nvarchar(max),
	slide4 nvarchar(max),
	slide5 nvarchar(max),

)
insert into slideshow values('b1.jpg','b2.png','b3.png','b4.png','b5.png')
drop table slideshow

create proc sp_slideshow
@ac int,
@slide nvarchar(max)
as
begin
if(ac=1)
update slideshow set slide1=@slide
if(ac=2)
update slideshow set slide1=@slide
if(ac=3)
update slideshow set slide1=@slide
if(ac=4)
update slideshow set slide1=@slide
if(ac=5)
update slideshow set slide1=@slide
end


insert into product values('1',N' Nền đỏ trái tim','nu1.jpg',N'T112',250.000,100,'1m62 x 2m x 10cm')
insert into product values('1',N' Nền đỏ trái tim','nu1.jpg',N'T112','260000',100,'1m8 x 2m x 10cm')
insert into product values('1',N' Hoa nền trắng','nu1.jpg',N'T113','250000',100,'1m62 x 2m x 10cm')
insert into product values('1',N' Hoa nền trắng','nu1.jpg',N'T113','260000',100,'1m8 x 2m x 10cm')
insert into product values('2',N' Ngựa nền hồng','nu1.jpg',N'T115','250000',100,'1m62 x 2m x 10cm')
insert into product values('2',N' Ngựa nền hồng','nu1.jpg',N'T115','260000',100,'1m8 x 2m x 10cm')
insert into product values('2',N' Máy bay xe cộ','nu1.jpg',N'T116','250000',100,'1m62 x 2m x 10cm')
insert into product values('2',N' Máy bay xe cộ','nu1.jpg',N'T116','260000',100,'1m8 x 2m x 10cm')

create table LoginUser(
	UserName varchar(50) not null,
	Password nvarchar(50) not null,
	Email varchar(100) not null,
	Name nvarchar(50),
	Sex varchar(10),
	Phone varchar(20),
)
create table LoginAdmin(
	UserName varchar(100),
	Password char(100),
)

create proc sp_checkloginAdmin(
@username varchar(100),
@password varchar(100))
as
begin
declare @set int;
if exists(select UserName from LoginAdmin where UserName=@username and Password=@password COLLATE SQL_Latin1_General_CP1_CS_AS)
begin
set @set=1;
return @set;
end
else
begin
set @set=0;
return @set;
end
end
create proc sp_checkloginUser(
@username varchar(100),
@password varchar(100))
as
begin
if exists(select UserName from LoginUser where UserName=@username and Password=@password COLLATE SQL_Latin1_General_CP1_CS_AS)
select * from LoginUser where UserName=@username
end
exec sp_checkloginUser 'user','123123'
drop proc sp_checkloginUser
select * from LoginUser
delete from LoginUser 

create proc sp_signup(
@username varchar(100),
@password varchar(100),
@email varchar(100),
@name nvarchar(MAX)
)
as
begin
declare @set int;
if exists(select *from LoginUser where UserName=@username)
begin
set @set=1;
return @set;
end
else if exists(select *from LoginUser where Email=@email)
begin
set @set=2;
return @set;
end
else
insert into LoginUser values (@username,@password,@email,@name,'','')
end
drop proc sp_signup
select * from LoginUser
exec sp_signup 'adm4in','123','adff','sdfsdfds'
-------SEARCH PRODUCT ------
create proc sp_searchproduct(
@keywork nvarchar(100)
)
as
begin
select * from product where (productname like '%'+@keywork+'%') or(codeproduct like '%'+@keywork+'%') or(size like '%'+@keywork+'%') or(categoryid like '%'+@keywork+'%')
end
------CART-----
create proc sp_cart(
@productid int
)
as
begin
select * from product where productid=@productid
end
----ManagerProduct--------

create proc sp_managerproduct(
@imgname nvarchar(100),
@productname nvarchar(100),
@price decimal,
@codeproduct nvarchar(10),
@categoryid nvarchar(100),
@size nvarchar(100)
)
as
begin
declare @set int
	if exists(select @codeproduct,@size,@productname from product where @codeproduct=codeproduct and size=@size and @productname=productname)
	begin
		set @set=0;
		return @set;
	end
	else
	begin
	insert into product values(@categoryid,@productname,@imgname,@codeproduct,@price,0,@size)
	set @set=1;
	return @set;
	end
end
--------xoa product------
create proc sp_deleteproduct(
@productid int
)
as
begin
	delete from product where @productid=productid
end

create proc sp_deletecustomer(
@customerid int
)
as
begin
	delete from detailorder where @customerid=customerid
	delete from ordercustome where @customerid=customerid
end

create proc sp_editproduct
@productid int,
@imgname varchar(100),
@productname nvarchar(100),
@size nvarchar(100),
@price decimal,
@codeproduct varchar(10),
@categoryid nvarchar(100),
@action char(1)

as begin
	declare @set int;
	if(@action='0')
	begin
	update product set imagesname=@imgname,productname=@productname,price=@price,categoryid=@categoryid,size=@size where productid=@productid
	end
	else
	begin
		if exists(select @codeproduct,@size,@productname from product where @codeproduct=codeproduct and size=@size and productname=@productname)
		begin
			set @set=1;
		return @set;
		end
		else
		update product set imagesname=@imgname,productname=@productname,size=@size,codeproduct=@codeproduct,price=@price,categoryid=@categoryid where productid=@productid
	end
end

---000000000-------
create proc sp_searchcode(
@keywork nvarchar(100),
@action varchar(1)
)
as
begin
if(@action='1')
select * from product
else
select * from product where codeproduct like '%'+@keywork+'%'
end

-------create triger edit-----
create trigger trg_update_codeproduct
on product after update
as
begin
if(UPDATE(codeproduct))
update details
set details.codeproduct=t2.codeproduct
from details ,inserted t2,deleted t3
where t2.codeproduct=t3.codeproduct
end




-----update-viewall-----
create proc update_viewall
@one int,
@productid int
as begin
declare @sum int

update product set viewall=viewall+@one where @productid=productid
end


create table ordercustome(
	customerid int identity primary key,
	namecustomer nvarchar(100),
	phone varchar(12),
	address nvarchar(max),
	city nvarchar(100),
	email varchar(100),
	note nvarchar(max),
)
create table detailorder(
	customerid int not null,
	codeproduct nvarchar(20),
	size nvarchar(100),
	productname nvarchar(200),
	imagesname nvarchar(50),
	price decimal,
	quantity int,
	total decimal,
)
alter table detailorder add constraint f_sc3t foreign key (customerid) references ordercustome(customerid)

insert into ordercustome values('ngo ngoc hai','01696872563','thu duc','tphcm','ngongochai112@gmail.com')
insert into ordercustome values('ngo ngoc hasi','01696872563','thu duc','tphcm','ngongochai112@gmail.com')
insert into detailorder values(1,'t01','1m2','quan nam','nam.jpg','100000','2','200000')
insert into detailorder values(1,'t02','1m3','quan nu','nu.jpg','200000','2','400000')
insert into pay values(1,'600000')

select namecustomer,sum(total)as total from ordercustome t1,detailorder t2 where t1.customerid=t2.customerid group by t1.namecustomer
------order----
create proc sp_order
	@namecustomer nvarchar(100),
	@phone varchar(12),
	@address nvarchar(max),
	@city nvarchar(100),
	@email varchar(100),
	@codeproduct nvarchar(20),
	@size nvarchar(100),
	@productname nvarchar(200),
	@imagesname nvarchar(50),
	@price decimal,
	@quantity int,
	@total decimal,
	@ac int

as
begin
declare @id int
if(@ac=0)
begin
insert into ordercustome values(@namecustomer,@phone,@address,@city,@email,'')
select @id=customerid  from ordercustome where namecustomer=@namecustomer 
insert into detailorder values(@id,@codeproduct,@size,@productname,@imagesname,@price,@quantity,@total)
end
else
begin
select @id=customerid  from ordercustome where namecustomer=@namecustomer 
insert into detailorder values(@id,@codeproduct,@size,@productname,@imagesname,@price,@quantity,@total)
end
end

drop proc sp_order
select *  from ordercustome order by customerid desc select * from detailorder where customerid=2 order by customerid desc 
	select * from detailorder order by customerid desc

select * from ordercustome order by customerid desc where customerid=5
delete from detailorder where customerid=1

update ordercustome set note='mai giao' where customerid=5
create proc updatenote
@customerid int,
@note nvarchar(max)
as
begin
update ordercustome set note=@note where customerid=@customerid
end

create proc sp_changepassword
@password varchar(100)
as
begin
update LoginAdmin set Password=@password
end

select * from LoginAdmin