use UNIVER;

SELECT AUDITORIUM, AUDITORIUM_TYPE.AUDITORIUM_TYPE
	from AUDITORIUM Inner Join AUDITORIUM_TYPE
	on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE

SELECT AUDITORIUM, AUDITORIUM_TYPE.AUDITORIUM_TYPE, AUDITORIUM_TYPE.AUDITORIUM_TYPENAME
	from AUDITORIUM Inner Join AUDITORIUM_TYPE
	on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE
		And AUDITORIUM_TYPE.AUDITORIUM_TYPENAME Like '%компьютер%'

SELECT  
	FACULTY.FACULTY AS факультет, 
	PULPIT.PULPIT AS кафедра,
	GROUPS.PROFESSION AS специальность, 
	SUBJECT.SUBJECT AS дисциплина,
	STUDENT.NAME AS ФИО,
	Case
		when PROGRESS.NOTE = 6 then 'шесть'
		when PROGRESS.NOTE = 7 then 'семь'
		when PROGRESS.NOTE = 8 then 'восемь'
	End AS оценка
	
	From PROGRESS
	Inner Join STUDENT on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
	Inner Join GROUPS on STUDENT.IDGROUP = GROUPS.IDGROUP
	Inner Join FACULTY on GROUPS.FACULTY = FACULTY.FACULTY
	Inner Join PULPIT on FACULTY.FACULTY = PULPIT.FACULTY
	Inner Join SUBJECT on PROGRESS.SUBJECT = SUBJECT.SUBJECT

	Where 
		PROGRESS.NOTE between 6 and 8

	Order by
		PROGRESS.NOTE asc;

Select isnull (TEACHER.TEACHER_NAME, '***') [препод],
	PULPIT.PULPIT [кафедра]
	From PULPIT Left Outer Join TEACHER
		on PULPIT.PULPIT = TEACHER.PULPIT


select PULPIT.FACULTY, PULPIT.PULPIT, PULPIT.PULPIT_NAME
	from PULPIT full outer join TEACHER
	on PULPIT.PULPIT = TEACHER.PULPIT
	where TEACHER.TEACHER is null

select TEACHER.TEACHER_NAME, TEACHER.TEACHER, TEACHER.PULPIT,TEACHER.GENDER
	from PULPIT full outer join TEACHER
	on PULPIT.PULPIT = TEACHER.PULPIT
	where PULPIT.PULPIT is not null

select * 
	from PULPIT full outer join TEACHER
	on PULPIT.PULPIT = TEACHER.PULPIT

Select AUDITORIUM, AUDITORIUM_TYPE.AUDITORIUM_TYPE
	from AUDITORIUM Cross Join AUDITORIUM_TYPE
		where AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM.AUDITORIUM_TYPE

Use B_MyBase;

SELECT Products.detail_name, Products.price, Orders.order_price
	from Orders Inner Join Products
	on Orders.order_detail = Products.detail_name
		
Select Orders.order_detail, Products.price, Orders.order_price
	from Orders Inner Join Products
	on Orders.order_detail = Products.detail_name 
		and Orders.order_detail Like '%1%'

Select 
	Products.detail_name as наименование, 
	Orders.order_date as [дата поставки],
	Case
		when Orders.order_price between 1 and 500 then 'цена < 500'
		when Orders.order_price between 500 and 1000 then 'цена от 500 до 1000'
	End [диапазон цен]
	from Products inner join Orders
	on Products.detail_name = Orders.order_detail
		Order by Orders.order_date desc

select isnull (Orders.order_detail, '***') [Товар],
	Products.quantity_in_stock
	from Products Left Outer Join Orders
		on Products.detail_name = Orders.order_detail

select Suppliers.phone_number, Suppliers.sup_address, Suppliers.supplier, Suppliers.supplier_code
	from Suppliers full outer join Orders
	on Suppliers.supplier_code = Orders.order_supplier
	where Orders.order_supplier is null

select Orders.order_date, Orders.order_detail, Orders.order_number, Orders.order_phone_number, 
	Orders.order_price, Orders.order_product_article, Orders.order_quantity,Orders.order_supplier

	from Orders full outer join Suppliers
	on Orders.order_supplier = Suppliers.supplier
	where Suppliers.supplier is not null

select * 
	from Orders full outer join Suppliers
	on Orders.order_supplier = Suppliers.supplier

select Products.detail_name, Products.price, Orders.order_price
	from Orders Cross Join Products
		where Orders.order_detail = Products.detail_name

use UNIVER;
create table TIMETABLE
(
    DAY_NAME   nvarchar(2) check (DAY_NAME in ('пн', 'вт', 'ср', 'чт', 'пт', 'сб')),
    LESSON     int check (LESSON between 1 and 4),
    TEACHER		char(10) foreign key references TEACHER (TEACHER),
    AUDITORIUM char(20) foreign key references AUDITORIUM (AUDITORIUM),
    SUBJECT    char(10) foreign key references SUBJECT (SUBJECT),
    IDGROUP    int foreign key references GROUPS (IDGROUP),
)
insert into TIMETABLE
values ('пн', 1, 'СМЛВ', '313-1', 'СУБД', 2),
       ('пн', 2, 'СМЛВ', '313-1', 'ОАиП', 4),
       ('пн', 1, 'МРЗ', '324-1', 'СУБД', 6),
       ('пн', 3, 'УРБ', '324-1', 'ПИС', 4),
       ('пн', 1, 'УРБ', '206-1', 'ПИС', 10),
       ('пн', 4, 'СМЛВ', '206-1', 'ОАиП', 3),
       ('пн', 1, 'БРКВЧ', '301-1', 'СУБД', 7),
       ('пн', 4, 'БРКВЧ', '301-1', 'ОАиП', 7),
       ('пн', 2, 'БРКВЧ', '413-1', 'СУБД', 8),
       ('пн', 2, 'ДТК', '423-1', 'СУБД', 7),
       ('пн', 4, 'ДТК', '423-1', 'ОАиП', 2),
       ('вт', 1, 'СМЛВ', '313-1', 'СУБД', 2),
       ('вт', 2, 'СМЛВ', '313-1', 'ОАиП', 4),
       ('вт', 3, 'УРБ', '324-1', 'ПИС', 4),
       ('вт', 4, 'СМЛВ', '206-1', 'ОАиП', 3);


SELECT DISTINCT AUDITORIUM as 'Аудитория, свободные во вт'
	FROM TIMETABLE
	WHERE DAY_NAME = 'пн' AND AUDITORIUM NOT IN (
		SELECT DISTINCT AUDITORIUM
			FROM TIMETABLE
			WHERE DAY_NAME = 'вт' 
		);

SELECT DISTINCT AUDITORIUM as 'Аудитория, свободные во вт 2 парой'
	FROM TIMETABLE
	WHERE DAY_NAME = 'пн' and AUDITORIUM not IN (
		SELECT DISTINCT AUDITORIUM
			FROM TIMETABLE
			WHERE DAY_NAME = 'вт' and LESSON = 2
		);


select GROUPS.IDGROUP,DAY_NAME, case
           when ( count(*)= 0) then 4
           when ( count(*)= 1) then 3
           when ( count(*)= 2) then 2
           when ( count(*)= 3) then 1
           when ( count(*)= 4) then 0
           end [Кол-во окон]
FROM  GROUPS inner join TIMETABLE T on GROUPS.IDGROUP = T.IDGROUP
group by GROUPS.IDGROUP,DAY_NAME
order by GROUPS.IDGROUP


SELECT TEACHER.TEACHER_NAME, TIMETABLE.DAY_NAME, TIMETABLE.LESSON
	FROM TIMETABLE cross JOIN TEACHER
WHERE 
  TIMETABLE.TEACHER != TEACHER.TEACHER;

select GROUPS.IDGROUP,TIMETABLE.DAY_NAME, TIMETABLE.LESSON
	from TIMETABLE CROSS JOIN GROUPS
where 
  TIMETABLE.IDGROUP != GROUPS.IDGROUP;
