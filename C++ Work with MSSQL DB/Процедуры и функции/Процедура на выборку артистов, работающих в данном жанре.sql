CREATE PROCEDURE viborka_genre @genre varchar(20)
as
SELECT  b.Name as [����],
		c.Name as [���],
		c.Middle_Name as [��������],
		c.Family as [�������]
FROM dbo.Artists_on_Genre as a
		INNER JOIN dbo.Genre as b ON (a.ID_Genre = b.ID_Genre)
		INNER JOIN dbo.artist as c ON (a.ID_artist = c.ID_artist)
WHERE (b.Name = @genre)