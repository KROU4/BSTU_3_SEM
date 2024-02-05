----- 1 задание -----------



set nocount on;

if(exists(select * 
			from SYS.objects
				where object_id = object_id(N'dbo.temp')))
begin
	drop table temp
end;
declare @c int,
		@flag char = 'r';
set implicit_transactions on
create table temp
(
	name varchar(40)
);
insert temp
		values('hahahaah'),
			  ('hehehhe'),
		      ('hohoho');
set @c = (select count(*) from temp);
print 'Количество строк в таблице temp: ' + cast(@c as varchar(3));
if @flag = 'c'
begin
	commit;
end;
else
begin
	rollback;
end;
set implicit_transactions off

if exists(select * from sys.objects
			where object_id = object_id(N'dbo.temp'))
begin
	print('Таблица temp существует');
end;
else
begin
	print('Таблица temp не существует');
end;

------ 2 задание аттомарность(либо все либо не один) ----

use UNIVER;

begin try
	begin tran;
	create table TEST_TABLE
	(
		id int primary key,
		name varchar(30),
		age int
	);
	declare @i int = 0;
	while @i < 100
	begin
		insert TEST_TABLE
				values(@i, concat('student', cast(@i as varchar(3))), rand() * 100);
		set @i = @i + 1;
	end;
	delete TEST_TABLE where age > 70 and age < 100;
	update TEST_TABLE set age = age + 10 where age > 10 and age < 20
	insert TEST_TABLE
		values(3, '2', 3);
	commit tran;
	print('Транзакция успешно выполнена');
end try
begin catch
	print 'Ошибка: ' + case
		when error_number() = 2627 and patindex('%TEST_TABLE%', error_message()) > 0
		then 'Дублирование id'
		else 'Неизвестная ошибка: ' + cast(error_number() as varchar(5)) + error_message()
		end;
	if @@TRANCOUNT > 0 
	begin
		rollback tran;
		print('Откат транзакции!');
	end;
end catch

if(exists(select * 
			from SYS.objects
				where object_id = object_id(N'dbo.TEST_TABLE')))
begin
	print('Таблица TEST_TABLE существует');
	drop table TEST_TABLE;
end;
else
begin
	print('Таблица TEST_TABLE не существует');
end;

------ 3 задание ------

declare @point varchar(32);
begin try
	begin tran;
	create table TEST_TABLE
	(
		id int primary key,
		name varchar(30),
		age int
	);
	declare @i int = 0;
	while @i < 100
	begin
		insert TEST_TABLE
				values(@i, concat('student', cast(@i as varchar(3))), rand() * 100);
		set @i = @i + 1;
	end;
	delete TEST_TABLE where id = 1;
	set @point = 'point1'; save tran @point;
	update TEST_TABLE set age = age - 1 where id = 3;
	set @point = 'point2'; save tran @point;
	insert TEST_TABLE 
			values(2, '2', 2);
			set @point = 'point3'; save tran @point;
	commit tran;
	print('Фиксация транзакции');
end try
begin catch
	print 'Ошибка: ' + case when error_number() = 2627 and patindex('%TEST_TABLE%', error_message()) > 0
							then 'Дублирование id!'
							else 'Неизвестная ошибка: ' + cast(error_number() as varchar(5)) + error_message()
							end;
	if @@TRANCOUNT > 0
	begin
		print 'Контрольная точка: ' + @point;
		rollback tran @point;
		commit tran;
	end;
end catch;
if(exists(select * 
			from SYS.objects
				where object_id = object_id(N'dbo.TEST_TABLE')))
begin
	print('Таблица TEST_TABLE существует');
	drop table TEST_TABLE;
end;
else
begin
	print('Таблица TEST_TABLE не существует');
end;

declare @note int = 4,
@idstudent int = 1016,
@pdate date = getdate(),
@subject varchar(30) = 'СУБД';
begin tran
insert PROGRESS(IDSTUDENT, NOTE, PDATE, SUBJECT)
values(@idstudent, @note, @pdate, @subject);
if @note > 5
begin
        rollback;
		print('Откат транзакции!');
end
else 
begin 
commit;
end
select * from PROGRESS p
where p.IDSTUDENT = @idstudent