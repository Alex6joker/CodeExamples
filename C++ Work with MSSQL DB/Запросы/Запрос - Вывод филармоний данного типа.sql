SELECT       a.Name as 'Название филармонии'
			,b.Type_Name as 'Тип филармонии'
FROM            dbo.Philharmonic AS a ,dbo.Philarmonic_Type AS b
WHERE        (b.Type_Name = 'Областная') and (b.ID_Philarmonic_Type = a.ID_Philarmonic_Type)