CREATE PROCEDURE upd @artist_name varchar(30),
@artist_middle_name varchar(30),
@artist_family varchar(50),
@old_name varchar(30),
@old_middle_name varchar(30),
@old_family varchar(50),
@new_name varchar(30),
@new_middle_name varchar(30),
@new_family varchar(50)
as
UPDATE dbo.Artist_with_Impresario
SET ID_Impresario = ( -- Поиск ИД нового импресарио в таблице с импреарио
			Select ID_Impresario
			FROM dbo.Impresario
			WHERE (name = @new_name) and (middle_name = @new_middle_name) and (family = @new_family))
WHERE ( ID_Impresario = ( -- Поиск ИД старого импресарио для конкретного артиста
SELECT a.ID_Impresario
FROM dbo.Artist_with_Impresario as a
		INNER JOIN dbo.Impresario as b ON (a.ID_Impresario = b.ID_Impresario)
		INNER JOIN dbo.artist as c ON (a.ID_artist = c.ID_artist)
WHERE (@artist_name = c.name and @artist_middle_name = c.middle_name and @artist_family = c.family)
and (@old_name = b.name) and (@old_middle_name = b.middle_name) and (@old_family = b.family)
)
) and (ID_Artist = ( -- Поиск ИД артиста, к которому привязан импресарио
SELECT a.ID_Artist
FROM dbo.Artist_with_Impresario as a
		INNER JOIN dbo.Impresario as b ON (a.ID_Impresario = b.ID_Impresario)
		INNER JOIN dbo.artist as c ON (a.ID_artist = c.ID_artist)
WHERE (@artist_name = c.name and @artist_middle_name = c.middle_name and @artist_family = c.family)
and (@old_name = b.name) and (@old_middle_name = b.middle_name) and (@old_family = b.family)
)
)