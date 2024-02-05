---- 4 задание ----

---- “–јЌ«ј ÷»я B ------

---- неподтвержденное чтение -----
set transaction isolation level READ COMMITTED 
begin transaction
update product set price = 0 where id_product = 1;
waitfor delay '00:00:02'
rollback
--------------------------------------------

---- неповтор€ющеес€ чтение ----

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
begin transaction
update product set price = 9999 where id_product = 2;
commit

update product set price = 4000 where id_product = 2;

--------------------

---- фантомное чтение ----
set transaction isolation level READ COMMITTED 
begin transaction
insert product
	values(4, 'FANTOM', 696969);
commit
delete product where id_product = 4
--------------------

---- 5 задание ----

---- “–јЌ«ј ÷»я B ------

---- -неподтвержденное чтение -----
set transaction isolation level READ COMMITTED 
begin transaction
update product set price = 0 where id_product = 1;
waitfor delay '00:00:02'
rollback
--------------------------------------------

---- неповтор€ющеес€ чтение ----
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
begin transaction
update product set price = 9999 where id_product = 2;
commit

update product set price = 4000 where id_product = 2;
--------------------

---- фантомное чтение ----
set transaction isolation level READ COMMITTED 
begin transaction
insert product
	values(4, 'FANTOM', 696969);
commit

delete product where id_product = 4;
--------------------

---- 6 задание ----

---- “–јЌ«ј ÷»я B ------

---- -неподтвержденное чтение -----
set transaction isolation level READ COMMITTED 
begin transaction
update product set price = 0 where id_product = 1;
waitfor delay '00:00:02'
rollback

--------------------------------------------

---- неповтор€ющеес€ чтение ----
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
begin transaction
update product set price = 9999 where id_product = 2;
commit

update product set price = 3432 where id_product = 2;
--------------------

---- фантомное чтение ----
set transaction isolation level READ COMMITTED 
begin transaction
insert product
	values(4, 'FANTOM', 696969);
commit

delete product where id_product = 4;
--------------------

---- 7 задание ----

---- “–јЌ«ј ÷»я B ------

---- -неподтвержденное чтение -----
set transaction isolation level READ COMMITTED 
begin transaction
update product set price = 0 where id_product = 1;
waitfor delay '00:00:02'
rollback

--------------------------------------------

---- неповтор€ющеес€ чтение ----
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
begin transaction
update product set price = 9999 where id_product = 2;
commit

update product set price = 3432 where id_product = 2;
--------------------

---- фантомное чтение ----
set transaction isolation level READ COMMITTED 
begin transaction
insert product
	values(4, 'FANTOM', 696969);
commit

delete product where id_product = 4;
--------------------
