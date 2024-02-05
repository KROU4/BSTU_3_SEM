use UNIVER;

select AUDITORIUM_TYPENAME, 
	max(AUDITORIUM_CAPACITY) [max capacity],
	min(AUDITORIUM_CAPACITY) [min capacity],
	avg(AUDITORIUM_CAPACITY) [avg capacity],
	sum(AUDITORIUM_CAPACITY) [sum capacity],
	count(*) [total]
from AUDITORIUM Inner Join AUDITORIUM_TYPE
	on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE
	Group by AUDITORIUM_TYPENAME
----
select *
from (select Case when (Note between 1 and 3) then 'failed'
	when (Note between 4 and 7) then 'passed'
	else 'avtomat'
	end [exam results], count(*) [amount]
from PROGRESS Group by Case
	when (Note between 1 and 3) then 'failed'
	when (Note between 4 and 7) then 'passed'
	else 'avtomat'
	end ) as P
	Order by Case [exam results]
		when 'failed' then 3
		when 'passed' then 2
		when 'avtomat' then 1
		else 0
		end
----
select a.FACULTY,
       G.PROFESSION,
       (2014 - G.YEAR_FIRST ) [course],
       round(avg(cast(NOTE as float(4))), 2) as [avg mark]
from FACULTY a
         Inner Join GROUPS G on a.FACULTY = G.FACULTY
         Inner Join STUDENT S on G.IDGROUP = S.IDGROUP
         Inner Join PROGRESS P on S.IDSTUDENT = P.IDSTUDENT
group by a.FACULTY, G.PROFESSION, G.YEAR_FIRST
order by [avg mark] desc
----
select a.FACULTY,
       G.PROFESSION,
       (2014 - G.YEAR_FIRST ) [course],
       round(avg(cast(NOTE as float(4))), 2) as [avg mark]
from FACULTY a
         Inner Join GROUPS G on a.FACULTY = G.FACULTY
         Inner Join STUDENT S on G.IDGROUP = S.IDGROUP
         Inner Join PROGRESS P on S.IDSTUDENT = P.IDSTUDENT
		where P.SUBJECT = 'ясад' or P.SUBJECT = 'нюХо' 
group by a.FACULTY, G.PROFESSION, G.YEAR_FIRST
order by [avg mark] desc
----
select g.PROFESSION,
	   p.SUBJECT,
	    round(avg(cast(NOTE as float(4))), 2) as [avg mark]
from FACULTY a
         Inner Join GROUPS g on a.FACULTY = g.FACULTY
         Inner Join STUDENT s on g.IDGROUP = S.IDGROUP
         Inner Join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
		where a.FACULTY = 'хр' 
group by g.PROFESSION, p.SUBJECT
order by [avg mark] asc
----
select p1.NOTE, p1.SUBJECT,
	(select count(*) from PROGRESS p2
	where p2.NOTE = p1.NOTE 
	and p2.SUBJECT = p1.SUBJECT) [amount]
from PROGRESS p1
	group by p1.NOTE, p1.SUBJECT
	having NOTE = 8 or NOTE = 9


use B_MyBase;

select detail_name,
	max(price) [max price],
	count(*) [amount order's details]
from Orders Inner Join Products
	on Orders.order_detail = Products.detail_name
	and Products.price > 400
group by detail_name
----
select *
from (select Case when order_price between 100 and 300 then 'mala'
	when order_price between 300 and 600 then 'norm'
	else 'mnoga'
	end [price rating], count (*) [amount]
from Orders group by case		
	when order_price between 100 and 300 then 'mala'
	when order_price between 300 and 600 then 'norm'
	else 'mnoga'
	end ) as T
		order by case [price rating]
			when 'mala' then 3
			when 'norm' then 2
			when 'mnoga' then 1
			else 0
			end
----
select o.order_detail,
	   s.supplier,
	   p.price,
	   round(avg(cast(o.order_price as float(4))),2) [avg price]
from Products p Inner Join Orders o
	on p.detail_name = o.order_detail
	Inner Join Suppliers s
	on s.supplier = o.order_supplier
where p.price > 400
group by o.order_detail,
		 s.supplier,
		 p.price
----
select order_detail,
		order_price,	
		sum(order_quantity) [amount]
from Orders
where order_detail in ('detail1','detail9')
group by order_detail,
		order_price
----
select p1.order_detail, p1.order_price,
	(select count(*) from Orders p2
	where p2.order_detail = p1.order_detail
	and p2.order_price = p1.order_price) [amount]
from Orders p1
group by p1.order_detail, p1.order_price
having order_price <300 or order_price > 800 