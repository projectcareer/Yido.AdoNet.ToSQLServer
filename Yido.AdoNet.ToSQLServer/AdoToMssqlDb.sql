CREATE DATABASE AdoToMssqlDb;
GO

USE AdoToMssqlDb;
GO

CREATE TABLE Student
(
	Sid INT IDENTITY(1,1) PRIMARY KEY not null,
	SName NVARCHAR(20) NOT NULL,
	SAge INT NOT NULL,
	SSex BIT NOT NULL
)
GO

--�������
INSERT INTO Student(SName,SAge,SSex)VALUES('�Ŵ��',22,1)
INSERT INTO Student(SName,SAge,SSex)VALUES('��Ӣ',18,0)
INSERT INTO Student(SName,SAge,SSex)VALUES('������',49,1)
GO

SELECT * FROM Student;