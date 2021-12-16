USE Filar;

GO
CREATE TRIGGER prov_insert_event
ON dbo.Event
AFTER INSERT
AS BEGIN
DECLARE @Event_Date DATETIME
SET @Event_Date = (SELECT [Date-Time] FROM inserted)
IF (getdate()) >= @Event_Date
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Нельзя добавить мероприятие, которое произойдет сегодня или уже произошло',16,0) WITH NOWAIT;
	END
end