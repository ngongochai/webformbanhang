create database haymuasi
use haymuasi
create table product(
	productid int not null identity ,
	categoryid nvarchar(10),
	productname nvarchar(200),
	imagesname nvarchar(50),
	codeproduct nvarchar(20) not null,
	price decimal,
	viewall int
)
select * from product order by categoryid asc
alter table product add constraint p_ct2e primary key(productid)
create Table details(
codeproduct nvarchar(20) not null,
mota nvarchar(MAX),
)


alter table details add constraint f_c3t foreign key (codeproduct) references product(codeproduct)
alter table details add constraint p_cte primary key(codeproduct)
drop table details
create table category(
categoryid nvarchar(10) not null,
tenloai nvarchar(50),
)
insert into details values ('HM01','Mota san pham 1')
insert into details values ('HM02','Mota san pham 2')
insert into details values ('HM03','Mota san pham 3')
insert into details values ('HM04','Mota san pham 4')
insert into details values ('HM05','Mota san pham 5')
insert into details values ('HM06','Mota san pham 6')
insert into details values ('HM07','Mota san pham 7')
insert into details values ('HM08','Mota san pham 8')
insert into details values ('HM09','Mota san pham 9')
insert into details values ('HM10','Mota san pham 10')
SELECT * FROM details t1,product t2 where t1.codeproduct=t2.codeproduct and productid=1
select * from product
alter table category add constraint p_ct primary key(categoryid)
alter table product add constraint p_pd primary key (codeproduct)
alter table details add constraint p_dt primary key (productid)
alter table details add constraint f_pddt foreign key(productid) references product(productid)
alter table product add constraint f_ct foreign key (categoryid) references category(categoryid)
create table producttopic(
	topic nvarchar(100)
)
create table slideshow(
	slide nvarchar(50),
	stt nvarchar(5),
	slide1 nvarchar(50)
)
create table menu(
	id nvarchar (10),
		parent_id nvarchar(10),
	menucontent nvarchar(100),
	nameimg nvarchar(100),
	submenu nvarchar(100)
)
 select * from menu where productid=19

 insert into menu values ('1','0',N'Thời trang nam','nam.png',N'Áo thun nam cá sấu')
 insert into menu values('1','','','',N'Đồ bộ nam')
  insert into menu values('1','','','',N'Áo thun nam')
   insert into menu values('1','','','',N'Áo khoác nam')
    insert into menu values('1','','','',N'Áo sơ mi nam')
	 insert into menu values('1','','','',N'Áo nam')

insert into menu values ('4','0',N'Áo thun cá sấu','casau.png',N'Áo thun nữ cá sấu')
insert into menu values('4','','','',N'Áo thun Tommy nam')
insert into menu values('4','','','',N'Áo thun Tommy nữ')
insert into menu values('4','','','',N'Áo thun Polo nam')
insert into menu values('4','','','',N'Áo thun Polo nữ')
insert into menu values('4','','','',N'Áo thun Burberry nam')
insert into menu values('4','','','',N'Áo thun Burberry nữ')
insert into menu values('4','','','',N'Áo thun Holiter nam')
insert into menu values('4','','','',N'Áo thun Holiter nữ')

insert into menu values ('3','0',N'Thời trang Cặp','cap.png',N'Áo khoác cặp')
insert into menu values('3','','','',N'Áo thun cặp')
insert into menu values('3','','','',N'Áo sơ mi cặp')
insert into menu values('3','','','',N'Đồ bộ cặp')

insert into menu values ('2','0',N'Thời Trang Nữ','nu.png',N'Áo thun nữ cá sấu')
insert into menu values('2','','','',N'Áo thun nữ')
insert into menu values('2','','','',N'Áo khoác nữ')
insert into menu values('2','','','',N'Áo sơ mi nữ')
insert into menu values('2','','','',N'Áo vest nữ')
insert into menu values('2','','','',N'Áo kiểu nữ')
insert into menu values('2','','','',N'Đầm công sở')
insert into menu values('2','','','',N'Đồ thể thao nữ')
insert into menu values('2','','','',N'Đồ bộ nữ')

delete Menu where id='4'

 insert into menu values ('5','0',N'Mẹ Và Bé','mevabe.png',N'Thời trang bé trai')
 insert into menu values('5','','','',N'Thời trang bé gái')
  insert into menu values('5','','','',N'Thời trang gia đình')
   insert into menu values('5','','','',N'Bộ đôi cho mẹ và bé')
    insert into menu values('5','','','',N'Thời trang cho mẹ')

	
 insert into menu values ('6','0',N'Đồng hồ','dongho.png',N'Đồng hồ nam')
 insert into menu values('6','','','',N'Đồng hồ nữ')
  insert into menu values('6','','','',N'Đồng hồ cặp')
   insert into menu values('6','','','',N'Đồng hồ cao cấp')


   insert into menu values ('7','0',N'Mỹ Phẩm','mypham.png','')

      insert into menu values ('8','0',N'Phụ kiến','phukien.png',N'Mắt kính')
	   insert into menu values('8','','','',N'Dây nịt')
  insert into menu values('8','','','',N'Giầy dép')
   insert into menu values('8','','','',N'Túi xách')

insert into slideshow values('b1.jpg','1','b2.png')
insert into slideshow values('b2.png','2','b3.png')
insert into slideshow values('b3.png','3','b4.png')
insert into slideshow values('b4.png','4','')
select * from slideshow
drop table slideshow


select * from product,details,category where product.productid='1' and details.productid='1' and category.categoryid='1'
select * from category

insert into category values('1',N'có thể bạn quan tâm')
insert into category values('nam',N'có thể bạn quan tâm')
insert into category values('nu',N'có thể bạn quan tâm')
insert into category values('mvb',N'có thể bạn quan tâm')

insert into product values('1',N'Áo Thun Nữ','nu1.jpg',N'HM01','100000',1000)
insert into product values('1',N'Áo khoác nam','n1.jpg',N'HM02','150000',2000)
insert into product values('1',N'Quần jeen Nam','nam1.jpg',N'HM03','105000',1030)
insert into product values('1',N'Áo Cặp Nam Nữ','nn1.jpg',N'HM04','190000',1000)
insert into product values('1',N'Áo sơ mi nam','nam7.jpg',N'HM05','200000',4000)
insert into product values('1',N'Váy nữ','nu12.jpg',N'HM06','200000',140)
insert into product values('1',N'Thời trang nữ','nu2.jpg',N'HM07','900000',1000)
insert into product values('1',N'Thời trang nam','nam8.jpg',N'HM08','200000',400)
insert into product values('1',N'Quần nữ','nu9.jpg',N'HM09','200000',500)

insert into product values('nam',N'Thời tran nam','n1.jpg',N'HM10','500000',100)
insert into product values('nam',N'Áo khoác nam','nam1.jpg',N'HM11','200000',1020)
insert into product values('nam',N'Quần jeen nam','nam2.jpg',N'HM12','100000',130)
insert into product values('nam',N'Thời Trang nam','nam3.jpg',N'HM13','400000',100)
insert into product values('nam',N'Quần kaki nam','nam4.jpg',N'HM14','50000',300)
insert into product values('nam',N'Áo thun nam','nam5.jpg',N'HM15','200000',400)
insert into product values('nam',N'Áo jeen nam','nam6.jpg',N'HM16','600000',1000)
insert into product values('nam',N'Quần thời trang','nam7.jpg',N'HM17','800000',1000)
insert into product values('nam',N'Quần đùi nam','nam8.jpg',N'HM18','900000',1000)
insert into product values('nu',N'Áo Thun Nữ','nu1.jpg',N'HM19','100000',1000)
insert into product values('nu',N'Áo jeen Nữ','nu2.jpg',N'HM20','100000',1000)
insert into product values('nu',N'Quần Nữ','nu3.jpg',N'HM21','100000',1000)
insert into product values('nu',N'Quân thun Nữ','nu4.jpg',N'HM22','100000',1000)
insert into product values('nu',N'Đồ bộ Nữ','nu5.jpg',N'HM23','100000',1000)
insert into product values('nu',N'Áo Thun Nữ','nu6.jpg',N'HM24','100000',1000)
insert into product values('nu',N'Áo Thun Nữ','nu7.jpg',N'HM25','100000',1000)
insert into product values('nu',N'Áo Thun Nữ','nu8.jpg',N'HM26','100000',1000)
insert into product values('nu',N'Áo Thun Nữ','nu9.jpg',N'HM27','100000',1000)

insert into product values('mvb',N'Váy cặp mẹ và bé','m1.jpg',N'HM28','100000',1000)
insert into product values('mvb',N'Áo khoác bé','m2.jpg',N'HM29','100000',1000)
insert into product values('mvb',N'Áo thun','m3.jpg',N'HM30','100000',1000)
insert into product values('mvb',N'Áo khoác','m4.jpg',N'HM31','100000',1000)
FORMAT() function. e.g. '#,##.000'




insert into producttopic values('Thoi Trang Gia Si')
insert into producttopic values('Quan Ao Gia Si')
insert into producttopic values('Thoi Trang Gia Si Tphcm')
insert into producttopic values('Bo Si Quan Ao')
insert into producttopic values('Mua Quan Ao Gia Si')
insert into producttopic values('Ban Si Quan Ao')
insert into producttopic values('Bo Si Quan Ao')
insert into producttopic values('Quan Ao Si')
insert into producttopic values('Lay Si Quan Ao')
insert into producttopic values('Chuyen Si Quan Ao')
insert into producttopic values('Thoi Trang Nam Gia Si')
insert into producttopic values('Thoi Trang Nu Gia Si')
insert into producttopic values('Quan Ao Nu Gia Si')
insert into producttopic values('Quan Ao Nam Gia Si')



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
insert into LoginAdmin values('admin','admin')
select * from LoginAdmin
select * from LoginUser
drop table LoginUser
select * from LoginUser
alter table LoginUser add constraint p_lrg primary key(UserName,Email)
insert into LoginUser values('1','admin','123','Admin')
insert into LoginUser values('2','ngochai','ngongochai',N'Ngô Ngọc Hải')

select * from LoginUser where UserName='admin' and Password=123

use haymuasi
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
drop proc  sp_checkloginAdmin
---00000000--------------
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
select * from product where (productname like '%'+@keywork+'%') or(codeproduct like '%'+@keywork+'%')
end
exec sp_searchproduct 'hm02'
drop proc sp_searchproduct
------CART-----
create proc sp_cart(
@productid int
)
as
begin
select * from product where productid=@productid
end
drop proc sp_cart
exec sp_cart '1'

----ManagerProduct--------

create proc sp_managerproduct(
@imgname varchar(100),
@productname nvarchar(100),
@price decimal,
@codeproduct varchar(10),
@categoryid varchar(100)
)
as
begin
declare @set int
	if exists(select @codeproduct from product where @codeproduct=codeproduct)
	begin
		set @set=0;
		return @set;
	end
	else
	begin
	insert into product values(@categoryid,@productname,@imgname,@codeproduct,@price,0)
	set @set=1;
	return @set;
	end
end
select * from product
drop proc sp_managerproduct	
exec sp_managerproduct '100','2','3','4','5','addproduct','5'
--------xoa product------
create proc sp_deleteproduct(
@productid int
)
as
begin
	delete from product where @productid=productid
end

create proc sp_editproduct
@productid int,
@imgname varchar(100),
@productname nvarchar(100),
@price decimal,
@codeproduct varchar(10),
@categoryid nvarchar(100),
@action char(1)
as begin
	declare @set int;
	if(@action='0')
	begin
	alter table details nocheck constraint all
	update product set imagesname=@imgname,productname=@productname,price=@price,categoryid=@categoryid where productid=@productid
	alter table details check constraint all
	end
	else
	begin
		if exists(select @codeproduct from product where @codeproduct=codeproduct)
		begin
			set @set=1;
		return @set;
		end
		else
		alter table details nocheck constraint all
		update product set imagesname=@imgname,productname=@productname,codeproduct=@codeproduct,price=@price,categoryid=@categoryid where productid=@productid
		alter table details check constraint all
	end
end
exec sp_editproduct 43,'nam.jpg','quan jeea',100,'hm01','sản phẩm nổi bật','0'
drop proc sp_editproduct
select * from product

update product set imagesname='nam.jpg',productname='quan jean dep',codeproduct='hm000001',price='20',categoryid='nam' where productid=19

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
exec sp_searchcode '1','2'
drop proc sp_searchcode

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

drop trigger trg_update_codeproduct