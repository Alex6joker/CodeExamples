SELECT      c.Name as 'Имя артиста'
			, c.Middle_Name as 'Отчество артиста'
			, c.Family as 'Фамилия артиста'
			, a.Name as 'Имя импресарио'
			, a.Middle_name as 'Отчество импресарио', a.Family as 'Фамилия импресарио'
FROM            dbo.Impresario AS a, dbo.Artist_with_Impresario AS b, dbo.Artist as c
WHERE        (c.Name = 'Сергей') AND (c.Middle_Name = 'Александрович') AND (c.Family = 'Подзоров') AND (c.ID_Artist = b.ID_Artist) AND (b.ID_Impresario = a.ID_Impresario)