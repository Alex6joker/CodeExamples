Declare @del int
Declare @sum int
Declare @res float
SET @del = (SELECT The_approximate_number_of_viewers
FROM dbo.Event
WHERE Name = '������� "�����"')
SET @sum = (SELECT SUM(Number_of_Viewers) as Real_number_of_viewers
FROM dbo.Viewer_Categories_on_Event
WHERE ID_event = (
	SELECT ID_event
	FROM dbo.Event
	Where Name = '������� "�����"'))
SET @res = cast(cast(@sum as float)/cast(@del as float)*100 as decimal(4,0))
SELECT Name AS [�������� �����������]
		, @sum as '����� ��������� �������', @del as '��������� ���-�� ��������', @res as '������� ���������'
FROM dbo.Event
WHERE Name = '������� "�����"'