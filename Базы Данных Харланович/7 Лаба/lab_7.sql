use UNIVER;
----1
select g.PROFESSION,
	   p.SUBJECT,
	   a.FACULTY,
	    round(avg(cast(NOTE as float(4))), 2) as [avg mark]
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where a.FACULTY = 'ИТ' 
group by ROLLUP (a.FACULTY, g.PROFESSION, p.SUBJECT)
----2
select g.PROFESSION,
	   p.SUBJECT,
	    round(avg(cast(NOTE as float(4))), 2) as [avg mark]
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where a.FACULTY = 'ИТ' 
	group by CUBE (g.PROFESSION, p.SUBJECT)
----3
select g.PROFESSION,
	   p.SUBJECT, 
	   p.NOTE
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where p.NOTE > 8
group by g.PROFESSION, p.SUBJECT, p.NOTE
	UNION
select g.PROFESSION,
	   p.SUBJECT, 
	   p.NOTE
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where p.NOTE <= 8 
group by g.PROFESSION, p.SUBJECT, p.NOTE
----4
select g.PROFESSION,
	   p.SUBJECT, 
	   p.NOTE
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where s.IDSTUDENT between 1001 and 1025
group by g.PROFESSION, p.SUBJECT, p.NOTE
	INTERSECT
select g.PROFESSION,
	   p.SUBJECT, 
	   p.NOTE
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where  s.IDSTUDENT between 1001 and 1014
group by g.PROFESSION, p.SUBJECT, p.NOTE
----5
select g.PROFESSION,
	   p.SUBJECT, 
	   p.NOTE
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where s.IDSTUDENT between 1001 and 1025
group by g.PROFESSION, p.SUBJECT, p.NOTE
	EXCEPT
select g.PROFESSION,
	   p.SUBJECT, 
	   p.NOTE
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where  s.IDSTUDENT between 1001 and 1014
group by g.PROFESSION, p.SUBJECT, p.NOTE
----6
use B_MyBase;

select order_detail, order_price, SUM(order_quantity) [amount]
	from Orders 
	Where order_detail in ('detail1', 'detail9')
	group by ROLLUP (order_detail,order_price);
----
select order_detail, order_price, SUM(order_quantity) [amount]
	from Orders 
	Where order_detail in ('detail1', 'detail9')
	group by CUBE (order_detail,order_price);
----
select order_detail, sum(order_quantity) [amount]
	 from Orders
	 where order_supplier = 'sup4'
	 group by order_detail 
	UNION
select order_detail, sum(order_quantity) [amount]
	 from Orders
	 where order_supplier = 'sup5'
	 group by order_detail
----
select order_detail, sum(order_quantity) [amount]
	 from Orders
	 where order_supplier = 'sup4'
	 group by order_detail 
	INTERSECT
select order_detail, sum(order_quantity) [amount]
	 from Orders
	 where order_supplier = 'sup5'
	 group by order_detail
----
select order_detail, sum(order_quantity) [amount]
	 from Orders
	 where order_supplier = 'sup4'
	 group by order_detail 
	EXCEPT
select order_detail, sum(order_quantity) [amount]
	 from Orders
	 where order_supplier = 'sup5'
	 group by order_detail
----7
use UNIVER;

--Подсчитать количество студентов в каждой группе, на каждом факультете и всего в университете одним запросом. 
SELECT
    a.FACULTY,
    g.IDGROUP,
    COUNT(*) as count_students
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
GROUP BY ROLLUP (a.FACULTY, g.IDGROUP);

--Подсчитать количество аудиторий по типам и суммарной вместимости в корпусах и всего одним запросом.
select
	a.AUDITORIUM_TYPE,
	count(*) as amount,
	sum(AUDITORIUM_CAPACITY) as total
from AUDITORIUM a
	Inner Join AUDITORIUM_TYPE b on a.AUDITORIUM_TYPE = b.AUDITORIUM_TYPE
GROUP BY ROLLUP (a.AUDITORIUM_TYPE)






