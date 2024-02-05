USE UNIVER		 ------------ 1 задание --------------


declare @var1 char = 'a',
		@var2 varchar = 'b',
		@var3 datetime,
		@var4 time,
		@var5 int,
		@var6 smallint,
		@var7 tinyint,
		@var8 numeric(12, 5)

set @var3 = '2022-01-10'; set @var4 = '11:11:11'; set @var5 = '1234'; 

select @var6 = 987, @var7 = 10; select @var8 = 1234567.29824423;

select @var1 var1, @var2 var2, @var3 var3, @var4 var4, @var5 var5, @var6 var6, @var7 var7, @var8 var8

print(
'var1 = ' + cast(@var1 as varchar(17)) + '
var2 = ' + cast(@var2 as varchar(17)) + '
var3 = ' + cast(@var3 as varchar(17)) + '
var4 = ' + cast(@var4 as varchar(17)) + '
var5 = ' + cast(@var5 as varchar(17)) + '
var6 = ' + cast(@var6 as varchar(17)) + '
var7 = ' + cast(@var7 as varchar(17)) + '
var8 = ' + cast(@var8 as varchar(17)))

------------ 2 задание ---------------

declare @capacityAud int = (select sum(AUDITORIUM.AUDITORIUM_CAPACITY) from AUDITORIUM) 
if @capacityAud > 200
	begin
		declare @countAud int = (select count(AUDITORIUM.AUDITORIUM) from AUDITORIUM), 
				@midCap real, @countAudMid int, @procAudMid real;
		select @midCap = (select avg(AUDITORIUM.AUDITORIUM_CAPACITY) from AUDITORIUM);
		set @countAudMid = (select count(AUDITORIUM.AUDITORIUM) from AUDITORIUM
								where AUDITORIUM.AUDITORIUM_CAPACITY <= @midCap);
		set @procAudMid = @countAudMid * 100 / @countAud;
		select @countAud as 'Количество аудиторий', @midCap as 'Средняя вместимость',
				@countAudMid as 'Количество аудиторий вместимость < средней', concat(@procAudMid, '%')[Процент]
	end
else
	begin
		print 'Общая вместимость: ' + cast(@capacityAud as varchar)
	end

------------ 3 задание ---------------

print(
	'Число обработанных строк: ' + cast(@@ROWCOUNT as varchar) + '
Версия SQL Server: ' + @@VERSION + '
Cистемный идентификатор процесса, назначенный сервером текущему подключению: ' + cast(@@SPID as varchar) + '
Kод последней ошибки: ' + cast(@@ERROR as varchar) + '
Имя сервера: ' + @@SERVERNAME + '
Возвращает уровень вложенности транзакции: ' + cast(@@TRANCOUNT as varchar) + '
Проверка результата считывания строк результирующего набора: ' + cast(@@FETCH_STATUS as varchar) + '
Уровень вложенности текущей процедуры: ' + cast(@@NESTLEVEL as varchar)
);

------------ 4-1 задание ---------------

declare @t real = 4, 
		@x real = 5,
		@z real;

if(@t > @x)
	set @z = power(sin(@t), 2);
else if(@t < @x)
	set @z = 4 * (@t + @x);
else if(@t = @x)
	set @z = 1 - exp(@x - 2)
select @z [Значение переменной z]

------------ 4-2 задание ---------------

declare @nextMonth int = month(getdate()) + 1;

select STUDENT.NAME, STUDENT.BDAY, (YEAR(getdate()) - YEAR(STUDENT.BDAY)) as 'Исполняется лет'
	from STUDENT
		where MONTH(STUDENT.BDAY) = @nextMonth

------------ 4-3 задание ---------------

declare @subject varchar(4) = 'СУБД';
declare @group int = 5;

select STUDENT.IDGROUP[Группа], STUDENT.NAME[Имя], PROGRESS.SUBJECT[Дисциплина], PROGRESS.NOTE[Оценка],
		PROGRESS.PDATE[Дата]
	from STUDENT inner join PROGRESS
		on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
			where PROGRESS.SUBJECT like @subject and STUDENT.IDGROUP = @group

------------ 5 задание ---------------

declare @faculty varchar(4) = 'ИСиТ'

select GROUPS.FACULTY[Факультет], STUDENT.NAME[Студент],
	case
	when PROGRESS.NOTE between 0 and 3 then 'неудовлетворительно'
	when PROGRESS.NOTE between 4 and 6 then 'удовлетворительно'
	when PROGRESS.NOTE between 7 and 8 then 'хорошо'
	when PROGRESS.NOTE between 9 and 10 then 'отлично'
	else 'ошибка в оценке'
	end[Статус оценки]
		from dbo.PROGRESS inner join dbo.STUDENT
			on STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
		inner join dbo.GROUPS
			on GROUPS.IDGROUP = STUDENT.IDGROUP
			where GROUPS.FACULTY like @faculty
	group by case
			when PROGRESS.NOTE between 0 and 3 then 'неудовлетворительно'
			when PROGRESS.NOTE between 4 and 6 then 'удовлетворительно'
			when PROGRESS.NOTE between 7 and 8 then 'хорошо'
			when PROGRESS.NOTE between 9 and 10 then 'отлично'
			else 'ошибка в оценке'
			end, GROUPS.FACULTY, STUDENT.NAME, GROUPS.FACULTY, GROUPS.FACULTY

------------ 6 задание ---------------

create table #friends
(
	name varchar(30),
	years int,
	id INT
)

set nocount on; -- не выводит сообщение о вводе строк
declare @i int = 0;
while @i < 10
begin
	insert #friends(name, years, id)
		values('Велютич Дмитрий', cast((100 * rand()) as int), cast((1000 * rand()) as int))
	set @i = @i + 1;
end;

select * from #friends

------------ 7 задание ---------------

declare @ix int = 0;
while @ix < 10
begin
	set @ix = @ix + 1;
	print @ix;
	return;
end;

------------ 8 задание ---------------

begin try
	insert PULPIT(PULPIT, PULPIT_NAME, FACULTY)
		values('ПИ', 'Программной инженерии', 'ИТИ')
end try

begin catch
	print 'Ошибка номер: ' + cast(ERROR_NUMBER() as varchar(8))
	print 'Описание ошибки:
' + ERROR_MESSAGE()
	print 'Строка: ' + cast(ERROR_LINE() as varchar(8))
	print 'Имя процедур: ' + ERROR_PROCEDURE()
	print 'Уровень серьезности: ' + cast(ERROR_SEVERITY() as varchar(8))
	print 'Метка ошибки: ' + cast(ERROR_STATE() as varchar(8))
end catch

declare @a int = 0,
@b real;
while @a < 10
begin
    set @a = @a + 1;
	set @b = @a % 3;
    print 'Остаток от деления' + cast(@b as varchar(10))
	
end;

		