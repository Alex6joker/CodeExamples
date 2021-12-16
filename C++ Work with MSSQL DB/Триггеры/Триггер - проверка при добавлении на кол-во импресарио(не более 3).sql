USE Filar;

GO
CREATE TRIGGER provtr
ON Artist_with_Impresario
AFTER INSERT
AS BEGIN
DECLARE @ID_ARTIST INTEGER
DECLARE @NEW_COUNT INTEGER
SELECT @ID_ARTIST = (SELECT ID_Artist FROM inserted)
SELECT @NEW_COUNT = (SELECT COUNT(*) FROM Artist_with_Impresario WHERE(@ID_ARTIST = ID_Artist))
IF (@NEW_COUNT) > 3
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Импрессарио не был добавлен, их количество не может превышать 3',16,0) WITH NOWAIT;
	END
end