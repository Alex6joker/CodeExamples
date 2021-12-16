USE Filar;

GO
CREATE TRIGGER prov_del_upd_event
ON dbo.Event
AFTER DELETE,UPDATE
AS BEGIN
DECLARE @Event_Date DATETIME
SET @Event_Date = (SELECT [Date-Time] FROM deleted)
IF (getdate()) > @Event_Date
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Нельзя удалить/изменить данные о мероприятии, которое уже прошло',16,0) WITH NOWAIT;
	END
end