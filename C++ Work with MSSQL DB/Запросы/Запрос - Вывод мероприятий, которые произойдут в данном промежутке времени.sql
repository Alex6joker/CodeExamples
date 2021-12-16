SELECT b.Name as [Название филармонии], 
	   a.Name as [Название мероприятия], a.[Date-Time] as [Время мероприятия]
FROM dbo.event as a, dbo.philharmonic as b
WHERE (a.[Date-Time] >= '2016-12-10 14:00' and a.[Date-Time] < '2016-12-10 16:00') and (b.Name = 'Государственная филармония') and (a.ID_Philharmonic = b.ID_Philharmonic)
GROUP BY a.ID_Philharmonic, b.Name, a.ID_event, a.Name, a.[Date-Time]