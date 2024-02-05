---------- 1 задание ----------

use UNIVER;

create view [Преподаватель]
	as select t.TEACHER[Имя преподавателя], t.GENDER[Пол], p.PULPIT[Кафедра] 
		from TEACHER as t 
		inner join PULPIT as p
			on t.PULPIT = p.PULPIT
go

select * from [Преподаватель]

drop view [Преподаватель];
go

---------- 2 задание ----------

create view [Количество кафедр]
	as select f.FACULTY_NAME[Факультет], count(p.PULPIT)[Количество кафедр]
		from FACULTY as f
		inner join PULPIT as p
			on f.FACULTY = p.FACULTY
		group by f.FACULTY_NAME, f.FACULTY_NAME
go
select * from [Количество кафедр]

drop view [Количество кафедр];
go
---------- 3 задание ----------

create view [Аудитории](Код, [Наименование аудитории])
	as select a.AUDITORIUM[Код], a.AUDITORIUM_TYPE[Наименование аудитории]
		from AUDITORIUM as a
			where a.AUDITORIUM_TYPE like 'ЛК%'

select * from [Аудитории]

insert [Аудитории] values('123', 'ЛК')
update [Аудитории] set Код = '321' where Код like '123'
delete [Аудитории] where Код like '321'

drop view [Аудитории]
go

---------- 4 задание ----------

create view [Лекционные аудитории]
 as select a.AUDITORIUM[Код], a.AUDITORIUM_TYPE[Наименование аудитории]
		from AUDITORIUM as a
			where a.AUDITORIUM_TYPE like 'ЛК%'

select * from [Лекционные аудитории]

drop view [Лекционные аудитории]

---------- 5 задание ----------

create view [Дисциплины]
	as select top 100 s.SUBJECT[Код], s.SUBJECT_NAME[Наименование дисциплины], s.PULPIT[Кафедра]
		from SUBJECT as s
			order by s.SUBJECT_NAME

select * from [Дисциплины]

drop view [Дисциплины]

---------- 6 задание ----------

alter view [Количество кафедр] with schemabinding
	as select f.FACULTY_NAME[Факультет], count(p.PULPIT)[Количество кафедр] 
		from dbo.FACULTY f inner join dbo.PULPIT p
			on f.FACULTY = p.FACULTY
		group by f.FACULTY_NAME

select * from [Количество кафедр]

drop view [Количество кафедр]

