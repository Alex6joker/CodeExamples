SELECT       a.Name as '�������� ����������'
			,b.Type_Name as '��� ����������'
FROM            dbo.Philharmonic AS a ,dbo.Philarmonic_Type AS b
WHERE        (b.Type_Name = '���������') and (b.ID_Philarmonic_Type = a.ID_Philarmonic_Type)