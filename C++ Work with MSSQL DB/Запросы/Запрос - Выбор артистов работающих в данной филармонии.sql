SELECT      a.Name AS [Имя артиста]
			, a.Middle_name AS [Отчество артиста]
			, a.Family AS [Фамилия артиста]
			, b.Name as [Название филармонии]
			, b.Address as [Адрес]
FROM            dbo.Artist as a, dbo.Philharmonic as b
WHERE        (b.Name = 'Классическая филармония') AND (b.ID_Philharmonic = a.ID_Philharmonic)