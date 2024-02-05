use UNIVER;

select FACULTY.FACULTY, PULPIT.PULPIT, PROFESSION.PROFESSION_NAME
	from FACULTY, PULPIT, PROFESSION
	where FACULTY.FACULTY = PULPIT.FACULTY and FACULTY.FACULTY = PROFESSION.FACULTY
		and PROFESSION.PROFESSION_NAME In 
			(select PROFESSION.PROFESSION_NAME
			from PROFESSION
				where (PROFESSION_NAME Like '%технология%' or PROFESSION_NAME Like '%технологии%'))

select FACULTY.FACULTY, PULPIT.PULPIT, PROFESSION.PROFESSION_NAME
	from FACULTY
	Inner Join PROFESSION on PROFESSION.FACULTY = FACULTY.FACULTY
	Inner Join PULPIT on PULPIT.FACULTY = FACULTY.FACULTY
		where PROFESSION.PROFESSION_NAME In
			(select PROFESSION.PROFESSION_NAME
			from PROFESSION
				where (PROFESSION_NAME Like '%технология%' or PROFESSION_NAME Like '%технологии%'))

select FACULTY.FACULTY, PULPIT.PULPIT, PROFESSION.PROFESSION_NAME
	from FACULTY
	Inner Join PROFESSION on PROFESSION.FACULTY = FACULTY.FACULTY
	Inner Join PULPIT on PULPIT.FACULTY = FACULTY.FACULTY
		where (PROFESSION_NAME Like '%технология%' or PROFESSION_NAME Like '%технологии%')

select AUDITORIUM_TYPE, AUDITORIUM_CAPACITY
	from AUDITORIUM a
		where AUDITORIUM = (select top(1) AUDITORIUM from AUDITORIUM aa
			where aa.AUDITORIUM_TYPE = a.AUDITORIUM_TYPE
				order by AUDITORIUM_CAPACITY desc)
	order by AUDITORIUM_CAPACITY desc

select FACULTY.FACULTY
	from FACULTY
		where not exists (select * from PULPIT
			where PULPIT.FACULTY = FACULTY.FACULTY)

select top 1
	(select avg(NOTE) from PROGRESS
		where SUBJECT like 'ОАиП') [ОАиП],
	(select avg(NOTE) from PROGRESS
		where SUBJECT like 'КГ') [КГ],
	(select avg(NOTE) from PROGRESS
		where SUBJECT like 'СУБД') [СУБД],
	(select avg(NOTE) from PROGRESS
		where SUBJECT like 'БД') [БД]
from PROGRESS

select STUDENT.IDSTUDENT, STUDENT.NAME, PROGRESS.NOTE
	from PROGRESS Inner Join STUDENT
	on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
		where NOTE >all (select NOTE from PROGRESS
			where SUBJECT like 'ОАиП')

select STUDENT.IDSTUDENT, STUDENT.NAME, PROGRESS.NOTE
	from PROGRESS Inner Join STUDENT
	on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
		where NOTE =any (select NOTE from PROGRESS
			where SUBJECT like 'ОАиП')

SELECT S1.NAME, S2.BDAY
	FROM STUDENT S1
		INNER JOIN STUDENT S2 ON S1.BDAY = S2.BDAY
	WHERE S1.IDSTUDENT <> S2.IDSTUDENT
	ORDER BY S1.BDAY;


use B_MyBase;

select Orders.order_detail, Orders.order_date, Products.price
	from Orders, Products
	where Orders.order_detail =  Products.detail_name
		and order_supplier in (select supplier from Suppliers
		where (sup_address like 'Brest%'))

select Orders.order_detail, Orders.order_date, Products.price
	from Orders Inner Join Products
	on orders.order_detail = Products.detail_name
	where order_supplier in (select supplier from Suppliers
		where (sup_address like 'Brest%'))


select Orders.order_detail, Orders.order_date, Products.price
	from Orders 
	Inner Join Products on orders.order_detail = Products.detail_name
	Inner Join Suppliers on orders.order_supplier = Suppliers.supplier
		where sup_address like 'Brest'

select order_detail, order_price
	from Orders o1
	where order_supplier = (select top 1 order_supplier from Orders o2
		where o2.order_detail = o1.order_detail
		order by order_price desc)
	order by order_price desc

select detail_name from Products
	where exists (select * from Orders
		where Orders.order_detail = Products.detail_name)

select top 1
	(select avg(order_quantity) from Orders
		where order_detail like 'detail1' ) [деталь 1]
	from Orders

select order_detail, order_quantity	
	from Orders
	where order_quantity >all (select order_quantity from Orders
		where order_detail like 'detail1')

select order_detail, order_quantity	
	from Orders
	where order_quantity <any (select order_quantity from Orders
		where order_detail like 'detail1')