---- 4 задание ----

---- ТРАНЗАКЦИИ А ------

---- неподтвержденное чтение ----
set transaction isolation level READ UNCOMMITTED 
begin transaction
select * from product where id_product = 1;
waitfor delay '00:00:04'
select * from product where id_product = 1;
commit
--------------------

---- неповторяющееся чтение ----

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
begin transaction
select * from product where id_product = 2;
waitfor delay '00:00:02'
select * from product where id_product = 2;
commit

--------------------
---- фантомное чтение ----
set transaction isolation level READ UNCOMMITTED 
begin transaction
select * from product where id_product = 4
waitfor delay '00:00:03'
select * from product where id_product = 4
commit
--------------------

---- 5 задание ----

---- ТРАНЗАКЦИИ А ------

---- -неподтвержденное чтение ----
set transaction isolation level READ COMMITTED 
begin transaction
select * from product where id_product = 1;
waitfor delay '00:00:02'
select * from product where id_product = 1;
commit
--------------------

---- неповторяющееся чтение ----
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
begin transaction
select * from product where id_product = 2;
waitfor delay '00:00:02'
select * from product where id_product = 2;
commit
--------------------
---- фантомное чтение ----
set transaction isolation level READ COMMITTED 
begin transaction
select * from product where id_product = 4
waitfor delay '00:00:02'
select * from product where id_product = 4
commit
--------------------

---- 6 задание ----

---- ТРАНЗАКЦИИ А ------

---- -неподтвержденное чтение ----
set transaction isolation level REPEATABLE READ 
begin transaction
select * from product where id_product = 1;
waitfor delay '00:00:02'
select * from product where id_product = 1;
commit
--------------------

---- неповторяющееся чтение ----
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ 
begin transaction
select * from product where id_product = 2;
waitfor delay '00:00:02'
select * from product where id_product = 2;
commit
--------------------
---- фантомное чтение ----
set transaction isolation level REPEATABLE READ 
begin transaction
select * from product where id_product = 4
waitfor delay '00:00:02'
select * from product where id_product = 4
commit
--------------------

---- 7 задание ----

---- ТРАНЗАКЦИИ А ------

---- -неподтвержденное чтение ----
set transaction isolation level SERIALIZABLE 
begin transaction
select * from product where id_product = 1;
waitfor delay '00:00:02'
select * from product where id_product = 1;
commit
--------------------

---- неповторяющееся чтение ----
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE 
begin transaction
select * from product where id_product = 2;
waitfor delay '00:00:02'
select * from product where id_product = 2;
commit
--------------------
---- фантомное чтение ----
set transaction isolation level SERIALIZABLE 
begin transaction
select * from product where id_product = 4
waitfor delay '00:00:02'
select * from product where id_product = 4
commit
--------------------

---- 8 задание ----

---- оператор COMMIT вложенной транзакции действует только 
---- на внутренние операции вложенной транзакции; 

set transaction isolation level REPEATABLE READ 
begin transaction
insert product
	values(4, 'FANTOM', 696969);
select * from product where id_product = 4;

	begin transaction
	update product set price = 0 where id_product = 4;
	commit

	select * from product where id_product = 4;
commit

delete from product where id_product = 4;

--- Уровень изоляции транзакции наследуется от родительской транзакции
 
--- оператор ROLLBACK внешней транзакции отменяет зафиксированные 
--- операции внутренней транзакции; 

set transaction isolation level REPEATABLE READ 
begin transaction

insert product
	values(4, 'FANTOM', 696969);
select * from product where id_product = 4;

	begin transaction
	update product set price = 0 where id_product = 4;
	insert product
		values(5, 'FANTOM', 696969);
	commit;

	select * from product where id_product = 4;
	select * from product where id_product = 5;

rollback;
select * from product where id_product = 4;
select * from product where id_product = 5;

--- оператор ROLLBACK вложенной транзакции 
--- действует на операции внешней и внутренней транзакции, а 
--- также завершает обе транзакции; 

set transaction isolation level REPEATABLE READ 
begin transaction
insert product
	values(4, 'FANTOM', 696969);
select * from product where id_product = 4;

	begin transaction
		update product set price = 0 where id_product = 4;
		insert product
			values(5, 'FANTOM', 696969);
		select * from product where id_product = 5;
	rollback;

	select * from product where id_product = 4;
	select * from product where id_product = 5;
commit;

select * from product where id_product = 4;
select * from product where id_product = 5;

--- уровень вложенности транзакции можно определить с помощью 
--- системной функции @@TRANCOUT. 

begin tran

	begin tran
		select @@TRANCOUNT
	commit tran;

commit tran;
