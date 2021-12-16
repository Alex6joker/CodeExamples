SELECT b.Type_Name as [�������� ����], 
	   a.Name as [�������� �����������], a.[Date-Time] as [����� �����������]
FROM dbo.event as a, dbo.Event_type as b
WHERE (a.[Date-Time] >= '2016-12-10 14:00' and a.[Date-Time] < '2017-12-10 16:00') and (b.Type_Name = '���') and (a.ID_Event_Type = b.ID_Event_Type)
GROUP BY b.ID_Event_Type, b.Type_Name, a.ID_event, a.Name, a.[Date-Time]