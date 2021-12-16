SELECT	b.Name AS [Название мероприятия]
		, Count(*) as [Число задействованных артистов]
FROM dbo.Artist_on_Events as a, dbo.Event as b
Where b.Name = 'Шоу талантов' and (a.ID_event = b.ID_event)
GROUP BY b.ID_Event,b.Name