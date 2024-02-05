DECLARE @ID int = 1001,
		@FIO varchar(50),
		@count int = 1,
		@length int,
		@space int = 0,
		@short varchar(50),
		@surname varchar(20) = '',
		@name varchar(20) = '',
		@patronymic varchar(20) = '';
		
SET @FIO = (SELECT NAME FROM STUDENT 
			 WHERE IDSTUDENT = @ID)
SET @length = LEN(@FIO)

WHILE (@count <= @length)
begin
	WHILE (SUBSTRING(@FIO, @count, 1) != ' ')
	begin
		SET @surname += SUBSTRING(@FIO, @count, 1)
		SET @count += 1
	end;

	IF (SUBSTRING(@FIO, @count, 1) = ' ' and @space < 1)
	begin
		SET @count += 1
		SET @name += SUBSTRING(@FIO, @count, 1)
		SET @space += 1
		

		WHILE (SUBSTRING(@FIO, @count, 1) != ' ')
		begin
			SET @count += 1
		end;
		SET @count += 1
		SET @patronymic = SUBSTRING(@FIO, @count, 1) 

		SET @count = @length + 1

	end;
end

SET @short = @surname + space(1) + @name + '.'  + @patronymic + '.';

SELECT @FIO 'full',
	   @short 'short';