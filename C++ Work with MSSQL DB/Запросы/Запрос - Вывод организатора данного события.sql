SELECT      a.Surname as [������� ������������]
			, a.Name as [��� ������������], a.Patronymic as [�������� ������������]
			, b.Name as [�������� �����������]
FROM            dbo.Organizer AS a, dbo.Event AS b
WHERE        (b.Name = '��� ��������') AND (b.ID_Organizer = a.ID_Organizer)