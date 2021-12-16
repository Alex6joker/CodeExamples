SELECT      a.Surname as [Фамилия организатора]
			, a.Name as [Имя организатора], a.Patronymic as [Отчество организатора]
			, b.Name as [Название мероприятия]
FROM            dbo.Organizer AS a, dbo.Event AS b
WHERE        (b.Name = 'Шоу талантов') AND (b.ID_Organizer = a.ID_Organizer)