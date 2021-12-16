Use Filar;

GO
CREATE TRIGGER Prov_place_count
ON Viewer_Categories_on_Event
AFTER INSERT, UPDATE
AS BEGIN
DECLARE @sum integer
DECLARE @event integer
DECLARE @places integer
SELECT @event = (SELECT ID_Event FROM inserted)
SELECT @sum = (SELECT SUM(Number_of_viewers) FROM Viewer_Categories_on_Event WHERE ID_Event = @event)
SELECT @places = (
SELECT Philharmonic.seat_num
FROM Philharmonic,Event
WHERE (Event.ID_Event = @event) AND (Event.ID_Philharmonic = Philharmonic.ID_Philharmonic)
)
IF (@sum > @places)
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Превышено число посадочных мест',16,0) WITH NOWAIT;
	END
END