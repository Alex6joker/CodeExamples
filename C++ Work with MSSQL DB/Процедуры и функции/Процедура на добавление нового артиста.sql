create proc addition @name varchar(30), @middle_name varchar(30), @family varchar(50), @b datetime
as
Declare @id integer
Set @id = (SELECT COUNT(*) FROM dbo.Artist) + 1
INSERT INTO dbo.Artist (ID_Artist,Name,Middle_name,Family,Birthday) VALUES(@id, @name, @middle_name, @family, @b)