SELECT      c.Name as '��� �������'
			, c.Middle_Name as '�������� �������'
			, c.Family as '������� �������'
			, a.Name as '��� ����������'
			, a.Middle_name as '�������� ����������', a.Family as '������� ����������'
FROM            dbo.Impresario AS a, dbo.Artist_with_Impresario AS b, dbo.Artist as c
WHERE        (c.Name = '������') AND (c.Middle_Name = '�������������') AND (c.Family = '��������') AND (c.ID_Artist = b.ID_Artist) AND (b.ID_Impresario = a.ID_Impresario)