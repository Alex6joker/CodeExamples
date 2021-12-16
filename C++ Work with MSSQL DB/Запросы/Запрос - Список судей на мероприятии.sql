SELECT DISTINCT  b.family as [Фамилия], b.name as [Имя], b.Patronymic as [Отчество], c.name as [Мероприятие]
FROM dbo.Judge_on_Event_for_Artist as a
		inner join dbo.judge as b on (a.ID_judge = b.ID_judge)
		inner join dbo.event as c on (c.ID_event = a.ID_event)
WHERE (c.name = 'Конкурс "Голос"')
GROUP BY a.ID_Judge, c.ID_event, c.name, b.family, b.name, b.Patronymic
