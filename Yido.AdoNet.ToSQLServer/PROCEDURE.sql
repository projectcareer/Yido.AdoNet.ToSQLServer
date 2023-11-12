
--�����洢����


USE AdoToMssqlDb
GO

--������ݴ洢����
CREATE PROCEDURE AddDataToStudent
@name nvarchar(20),
@age int,
@sex bit
AS
BEGIN
	INSERT INTO Student([SName],[SAge],[SSex]) VALUES(@name,@age,@sex)
END
GO
--�������ݴ洢����
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

--ɾ�����ݴ洢����
CREATE PROCEDURE DelDataToStudent
@id int
AS 
BEGIN
	DELETE FROM Student WHERE Sid = @id
END
GO

--��ѯ����
CREATE PROCEDURE SELECTDataToStudent
@keyword nvarchar(20)
AS
BEGIN
	SELECT * FROM Student WHERE SName LIKE '%' + @keyword +'%'
END
GO