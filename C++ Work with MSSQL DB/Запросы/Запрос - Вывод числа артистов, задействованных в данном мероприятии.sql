SELECT	b.Name AS [�������� �����������]
		, Count(*) as [����� ��������������� ��������]
FROM dbo.Artist_on_Events as a, dbo.Event as b
Where b.Name = '��� ��������' and (a.ID_event = b.ID_event)
GROUP BY b.ID_Event,b.Name