SELECT b.Name as [�������� ����������], 
	   a.Name as [�������� �����������], a.[Date-Time] as [����� �����������]
FROM dbo.event as a, dbo.philharmonic as b
WHERE (a.[Date-Time] >= '2016-12-10 14:00' and a.[Date-Time] < '2016-12-10 16:00') and (b.Name = '��������������� ����������') and (a.ID_Philharmonic = b.ID_Philharmonic)
GROUP BY a.ID_Philharmonic, b.Name, a.ID_event, a.Name, a.[Date-Time]