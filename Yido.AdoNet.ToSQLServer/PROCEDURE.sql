
--创建存储过程


USE AdoToMssqlDb
GO

--添加数据存储过程
CREATE PROCEDURE AddDataToStudent
@name nvarchar(20),
@age int,
@sex bit
AS
BEGIN
	INSERT INTO Student([SName],[SAge],[SSex]) VALUES(@name,@age,@sex)
END
GO
--更新数据存储过程
CREATE PROCEDURE UpdateDataToStudent
@id int,
@name nvarchar(20),
@age int,
@sex bit
AS
BEGIN
	UPDATE Student SET SName=@name,SAge=@age,SSex=@sex WHERE SId = @id
END
GO

--删除数据存储过程
CREATE PROCEDURE DelDataToStudent
@id int
AS 
BEGIN
	DELETE FROM Student WHERE Sid = @id
END
GO

--查询数据
CREATE PROCEDURE SELECTDataToStudent
@keyword nvarchar(20)
AS
BEGIN
	SELECT * FROM Student WHERE SName LIKE '%' + @keyword +'%'
END
GO