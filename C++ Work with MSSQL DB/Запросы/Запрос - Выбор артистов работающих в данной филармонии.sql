SELECT      a.Name AS [��� �������]
			, a.Middle_name AS [�������� �������]
			, a.Family AS [������� �������]
			, b.Name as [�������� ����������]
			, b.Address as [�����]
FROM            dbo.Artist as a, dbo.Philharmonic as b
WHERE        (b.Name = '������������ ����������') AND (b.ID_Philharmonic = a.ID_Philharmonic)