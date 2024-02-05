USE B_MyBase;

CREATE table Products
(	product_article nvarchar(15) primary key,
	detail_name nvarchar(20),
	price real,
	quantity_in_stock int
)on FG1;

CREATE table Suppliers
(	supplier nvarchar(20) primary key,
	supplier_code nvarchar(15),
	phone_number nvarchar(15),
	sup_address nvarchar(50)
)on FG2;

CREATE table Orders
(	order_number int primary key,
	order_date date,
	order_detail nvarchar(20),
	order_quantity int,
	order_supplier nvarchar(20) foreign key references Suppliers(supplier),
	order_phone_number nvarchar(15),
	order_price real,
	order_product_article nvarchar(15) foreign key references Products(product_article)
)on FG3;

ALTER table Orders DROP Column order_date;
ALTER table Orders ADD order_date date;

INSERT into Products(detail_name,price,quantity_in_stock,product_article) 
values 
	('detail1',100,100,'det1'),
	('detail2',200,200,'det2'),
	('detail3',300,300,'det3'),
	('detail4',400,400,'det4'),
	('detail5',500,500,'det5'),
	('detail6',600,600,'det6'),
	('detail7',700,700,'det7'),
	('detail8',800,800,'det8'),
	('detail9',900,900,'det9'),
	('detail10',1000,1000,'det10')

INSERT into Suppliers(supplier, supplier_code, phone_number,sup_address)
values
	('sup1', '1111', '3751111','Minsk'),
	('sup2', '2222','3752222','Mogilev'),
	('sup3', '3333','3753333','Grodno'),
	('sup4', '4444','3754444','Vitebsk'),
	('sup5', '5555','3755555','Gomel'),
	('sup6', '6666','3756666','Brest')

INSERT into Orders(order_number, order_date, order_detail, order_quantity, order_supplier, order_phone_number, order_price, order_product_article)
values 
	(1,'2023-01-01','detail1',10,'sup5','3755555',100,'det1'),
	(2,'2023-02-02','detail2',16,'sup3','3753333',200,'det2'),
	(3,'2023-03-03','detail3',29,'sup4','3754444',300,'det3'),
	(4,'2023-03-03','detail4',100,'sup4','3754444',400,'det4'),
	(5,'2023-05-05','detail5',125,'sup2','3752222',500,'det5'),
	(6,'2023-06-06','detail1',123,'sup1','3751111',100,'det1'),
	(7,'2023-07-07','detail7',134,'sup6','3756666',700,'det7'),
	(8,'2023-07-07','detail8',139,'sup6','3756666',800,'det8'),
	(9,'2023-07-07','detail9',138,'sup6','3756666',900,'det9'),
	(10,'2023-10-10','detail10',115,'sup5','3755555',1000,'det10')

-- SELECT * From Products;

-- SELECT detail_name, price From Products;

-- SELECT count(*) From Products;

-- SELECT detail_name [Cheap products] From Products
--	where price < 200

-- UPDATE Products set quantity_in_stock = 1; 
-- SELECT quantity_in_stock From Products;

-- UPDATE Products set price = price + 1
--	Where detail_name = 'detail1';

-- SELECT price From Products
--	Where detail_name = 'detail1';

-- DELETE from Orders
--	Where order_price = 100;

-- INSERT into Orders(order_number, order_date, order_detail, order_quantity, order_supplier, order_phone_number, order_price, order_product_article)
-- values
-- (1,'2023-01-01','detail1',10,'sup5','3755555',100,'det1'),
-- (6,'2023-06-06','detail1',123,'sup1','3751111',100,'det1')

-- SELECT * from Orders;

