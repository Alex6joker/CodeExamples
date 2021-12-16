CREATE PROCEDURE get_columns @table_name varchar(30)
as
select COLUMN_NAME from INFORMATION_SCHEMA.columns 
where TABLE_NAME= @table_name and ((COLUMN_NAME LIKE '%Name%') or (COLUMN_NAME LIKE '%name%') or (COLUMN_NAME LIKE '%Family%') or (COLUMN_NAME LIKE '%Middle_Name%'))