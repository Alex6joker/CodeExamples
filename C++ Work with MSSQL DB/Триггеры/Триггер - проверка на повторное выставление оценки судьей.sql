USE Filar;

GO
CREATE TRIGGER prov_judge_eval
ON Judge_on_Event_for_Artist
AFTER INSERT
AS BEGIN
DECLARE @ID_Judge INTEGER
DECLARE @ID_Artist INTEGER
DECLARE @ID_Event INTEGER
DECLARE @eval_COUNT INTEGER
SELECT @ID_Artist = (SELECT ID_Artist FROM inserted)
SELECT @ID_Judge = (SELECT ID_Judge FROM inserted)
SELECT @ID_Event = (SELECT ID_Event FROM inserted)
SELECT @eval_COUNT = (
		SELECT COUNT(*) 
		FROM Judge_on_Event_for_Artist 
		WHERE(@ID_ARTIST = ID_Artist) and (@ID_Judge = ID_Judge) and (@ID_Event = ID_Event))
IF (@eval_COUNT) > 1 -- Так как данные добавляются и в таблицу inserted и уже добавлены в целевую (Judge_on_Event_for_Artist)
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Невозможно поставить оценку второй раз',16,0) WITH NOWAIT;
	END
end