-- ������������.
CREATE PROCEDURE viborka @name varchar(30)
as
SELECT  a.Name as [���],
		a.Middle_name as [��������],
		a.Family as [�������],
		b.Name as [��������]
FROM    dbo.Artist as a, dbo.Philharmonic as b
WHERE   (b.Name = @name) AND (b.ID_Philharmonic = a.ID_Philharmonic)