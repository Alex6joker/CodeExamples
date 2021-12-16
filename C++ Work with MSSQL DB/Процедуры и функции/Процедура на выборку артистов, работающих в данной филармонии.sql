-- Планирование.
CREATE PROCEDURE viborka @name varchar(30)
as
SELECT  a.Name as [Имя],
		a.Middle_name as [Отчество],
		a.Family as [Фамилия],
		b.Name as [Название]
FROM    dbo.Artist as a, dbo.Philharmonic as b
WHERE   (b.Name = @name) AND (b.ID_Philharmonic = a.ID_Philharmonic)