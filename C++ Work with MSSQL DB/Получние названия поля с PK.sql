CREATE PROCEDURE get_IDcolumn @table_name varchar(30)
as
select COLUMN_NAME from INFORMATION_SCHEMA.columns 
where TABLE_NAME= @table_name and (COLUMN_NAME LIKE 'ID%')