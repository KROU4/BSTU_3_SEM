	------ Применение триггеров -----

----- 1 задание -----

use UNIVER;

create table TR_AUDIT
(
	ID int identity, -- номер
	STMT varchar(20) check (STMT in ('INS', 'DEL', 'UPD')), -- DML оператор
	TRNAME varchar(50), -- имя триггера
	CC varchar(300) -- комментарий
)

create trigger TR_TEACHERS_INS on TEACHER after INSERT
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '';
	set @a1 = (select [TEACHER] from inserted);
	set @a2 = (select [TEACHER_NAME] from inserted);
	set @a3 = (select [GENDER] from inserted);
	set @a4 = (select [PULPIT] from inserted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Вставка значения в таблицу TEACHER : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('INS', 'TR_TEACHERS_INS', @in);
	return;
end; 

insert into TEACHER(GENDER, PULPIT, TEACHER, TEACHER_NAME)
			values('м', 'ИСиТ', 'ТНС', 'Степаненко Арина Владимировна');

select * from TR_AUDIT;

drop trigger TR_TEACHERS_INS;
delete TR_AUDIT;


----- 2 задание ----

create trigger TR_TEACHERS_DEL on TEACHER after DELETE
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '';
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = (select [TEACHER_NAME] from deleted);
	set @a3 = (select [GENDER] from deleted);
	set @a4 = (select [PULPIT] from deleted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Удаление значения из таблицы TEACHER : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('DEL', 'TR_TEACHERS_DEL', @in);
	return;
end; 

delete TEACHER where TEACHER like 'ТНС';
select * from TR_AUDIT;

drop trigger TR_TEACHERS_DEL;
delete TR_AUDIT;

------ 3 задание -----

create trigger TR_TEACHERS_UPD on TEACHER after UPDATE
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '';
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = (select [TEACHER_NAME] from deleted);
	set @a3 = (select [GENDER] from deleted);
	set @a4 = (select [PULPIT] from deleted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Удаление значения из таблицы TEACHER : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('DEL', 'TR_TEACHERS_UPD', @in);
	set @a1 = (select [TEACHER] from inserted);
	set @a2 = (select [TEACHER_NAME] from inserted);
	set @a3 = (select [GENDER] from inserted);
	set @a4 = (select [PULPIT] from inserted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Вставка значения в таблицу TEACHER : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('INS', 'TR_TEACHERS_UPD', @in);
	return;
end; 

insert TEACHER(GENDER, PULPIT, TEACHER, TEACHER_NAME)
		values('ж', 'ЛВ', 'ТАС', 'Тараканова Александра Сергеевна');
update TEACHER set TEACHER = 'ТНН' where TEACHER = 'ТАС';
update TEACHER set TEACHER_NAME = 'Тараканова Наталья Николаевна'  where TEACHER = 'ТНН';

delete TEACHER where TEACHER = 'ТНН';

select * from TR_AUDIT;

drop trigger TR_TEACHERS_UPD;
delete TR_AUDIT;

--- 4 задание ----

create trigger TR_TEACHER on TEACHER after INSERT, DELETE, UPDATE
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '',
			@ins int = (select count(*) from inserted),
			@del int = (select count(*) from deleted);

	if @ins > 0 and @del = 0
	begin
		set @a1 = (select [TEACHER] from inserted);
		set @a2 = (select [TEACHER_NAME] from inserted);
		set @a3 = (select [GENDER] from inserted);
		set @a4 = (select [PULPIT] from inserted);
		set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
		print 'Вставка значения в таблицу TEACHER(INSERT) : ' + @in;
		insert into TR_AUDIT(STMT, TRNAME, CC)
							values('INS', 'TR_TEACHERS', @in);
	end;

	else if @del > 0 and @ins = 0
	begin
		set @a1 = (select [TEACHER] from deleted);
		set @a2 = (select [TEACHER_NAME] from deleted);
		set @a3 = (select [GENDER] from deleted);
		set @a4 = (select [PULPIT] from deleted);
		set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
		print 'Удаление значения из таблицы TEACHER(DELETE) : ' + @in;
		insert into TR_AUDIT(STMT, TRNAME, CC)
						values('DEL', 'TR_TEACHERS', @in);
	end;

	else if @del > 0 and @ins > 0
	begin
		set @a1 = (select [TEACHER] from deleted);
		set @a2 = (select [TEACHER_NAME] from deleted);
		set @a3 = (select [GENDER] from deleted);
		set @a4 = (select [PULPIT] from deleted);
		set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
		print 'Замена значения из таблицы TEACHER(UPDATE) : ' + @in;
		insert into TR_AUDIT(STMT, TRNAME, CC)
						values('DEL', 'TR_TEACHERS', @in);
		set @a1 = (select [TEACHER] from inserted);
		set @a2 = (select [TEACHER_NAME] from inserted);
		set @a3 = (select [GENDER] from inserted);
		set @a4 = (select [PULPIT] from inserted);
		set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
		print 'На значение : ' + @in;
		insert into TR_AUDIT(STMT, TRNAME, CC)
							values('INS', 'TR_TEACHERS', @in);
	end;
	return;
end;

insert into TEACHER(GENDER, PULPIT, TEACHER, TEACHER_NAME)
			values('м', 'ИСиТ', 'САВ', 'Степаненко Арина Владимировна');
insert TEACHER(GENDER, PULPIT, TEACHER, TEACHER_NAME)
		values('ж', 'ЛВ', 'САА', 'Смирнова Александра Алексеевна');

update TEACHER set TEACHER = 'НАМ' where TEACHER = 'САВ';
update TEACHER set TEACHER_NAME = 'Нурк Александра Максимовна'  where TEACHER = 'НАМ';

delete TEACHER where TEACHER = 'НАМ';
delete TEACHER where TEACHER = 'САВ';

select * from TR_AUDIT;

drop trigger TR_TEACHER;
delete TR_AUDIT;

---- 5 задание ----

insert TEACHER(GENDER, PULPIT, TEACHER, TEACHER_NAME)
		values('r', 'ЛВ', 'САА', 'Смирнова Александра Алексеевна');

select * from TR_AUDIT;

--- 5 задание ----

create trigger TR_TEACHERS_DEL1 on TEACHER after DELETE
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '';
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = (select [TEACHER_NAME] from deleted);
	set @a3 = (select [GENDER] from deleted);
	set @a4 = (select [PULPIT] from deleted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Удаление значения из таблицы TEACHER(1) : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('DEL', 'TR_TEACHERS_DEL1', @in);
	return;
end;

create trigger TR_TEACHERS_DEL2 on TEACHER after DELETE
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '';
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = (select [TEACHER_NAME] from deleted);
	set @a3 = (select [GENDER] from deleted);
	set @a4 = (select [PULPIT] from deleted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Удаление значения из таблицы TEACHER(2) : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('DEL', 'TR_TEACHERS_DEL2', @in);
	return;
end;

create trigger TR_TEACHERS_DEL3 on TEACHER after DELETE
as
begin
	declare @a1 char(10) = '',
			@a2 varchar(100) = '',
			@a3 char(1) = '',
			@a4 char(20) = '',
			@in varchar(300) = '';
	set @a1 = (select [TEACHER] from deleted);
	set @a2 = (select [TEACHER_NAME] from deleted);
	set @a3 = (select [GENDER] from deleted);
	set @a4 = (select [PULPIT] from deleted);
	set @in = concat(trim(@a1), ' ', @a2, ' ', @a3, ' ', @a4);
	print 'Удаление значения из таблицы TEACHER(3) : ' + @in;
	insert into TR_AUDIT(STMT, TRNAME, CC)
				values('DEL', 'TR_TEACHERS_DEL3', @in);
	return;
end;

-- получить список триггеров

select t.name
	from sys.triggers t
		where OBJECT_NAME(t.parent_id) = 'TEACHER'

--- получить порядок выполнения тригеров

select t.name, e.type_desc
	from sys.triggers t join sys.trigger_events e
		on t.object_id = e.object_id
		where OBJECT_NAME(t.parent_id) = 'TEACHER' and e.type_desc = 'DELETE'

--- поменять порядок выполнения триггеров

exec sp_settriggerorder @triggername = 'TR_TEACHERS_DEL3', @order = 'First', @stmttype = 'DELETE';
exec sp_settriggerorder @triggername = 'TR_TEACHERS_DEL2', @order = 'Last', @stmttype = 'DELETE';

insert into TEACHER(GENDER, PULPIT, TEACHER, TEACHER_NAME)
			values('м', 'ИСиТ', 'САВ', 'Степаненко Арина Владимировна');
delete TEACHER where TEACHER = 'САВ';

drop trigger TR_TEACHERS_DEL3, TR_TEACHERS_DEL2, TR_TEACHERS_DEL1;

---- 6 задание ----

create trigger AT_TRIG on AUDITORIUM_TYPE after insert, delete, update
as
begin
	declare @count int = (select count(*) from AUDITORIUM_TYPE);
	if(@count >= 5)
	begin
		raiserror('Общее количество записей не может быть больше 5!', 11 , 1);
		rollback;
	end;
	return;
end;

begin tran
	insert AUDITORIUM_TYPE
			values('1', '2');
commit;

select * from AUDITORIUM_TYPE;

delete AUDITORIUM_TYPE where AUDITORIUM_TYPE.AUDITORIUM_TYPE like '1'

drop trigger AT_TRIG;

--- 7 задание ---

create trigger FAC_TRIG on FACULTY instead of DELETE
as
begin
	raiserror('Удаление строк в таблице FACULTY запрещено!', 11, 1);
	return;
end;

delete FACULTY;
select * from FACULTY

drop trigger FAC_TRIG;

---- удалить все триггеры

drop trigger TR_TEACHERS_INS, TR_TEACHERS_DEL, TR_TEACHERS_UPD, TR_TEACHER, 
			 TR_TEACHERS_DEL1, TR_TEACHERS_DEL2, TR_TEACHERS_DEL3, AT_TRIG, FAC_TRIG;

---- 8 задание ----

create trigger DDL_UNIVER on database for DDL_DATABASE_LEVEL_EVENTS
as begin
	declare @t  varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]', 'varchar(50)');
	declare @t1  varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'varchar(50)');
	declare @t2 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]', 'varchar(50)');

	if(@t != 'ALTER_TABLE')
	begin
	print('Тип события : ' + @t);
	print('Имя объекта : ' + @t1);
	print('Тип объекта : ' + @t2);
	raiserror('Операции удаления и создания таблиц в базе данных UNIVER запрещена!', 18, 1);
	rollback;
	end;
	return;
end;

create table TTTe2
(
	id int
)

drop table TIMETABLE;

alter table TIMETABLE add IDD int;
alter table TIMETABLE drop column IDD;

drop trigger DDL_UNIVER on database;






