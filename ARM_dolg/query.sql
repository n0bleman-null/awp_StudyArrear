USE dolgi
GO

CREATE TABLE ������(
Id bigint NOT NULL PRIMARY KEY IDENTITY, 
����� nvarchar(8) NOT NULL
);
GO

CREATE TABLE �������������(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
��� nvarchar(64) NOT NULL,
������ nvarchar(32) NOT NULL,
������������� bit NOT NULL
);
GO

CREATE TABLE ���������������(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
�������� nvarchar(20) NOT NULL
);
GO

CREATE TABLE �������������������(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
������������� bigint NOT NULL REFERENCES �������������(Id),
����������� bigint NOT NULL REFERENCES ������(Id),
�������������� bigint NOT NULL REFERENCES ���������������(Id)
);
GO

CREATE TABLE ������������������(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
������������������� bigint NOT NULL REFERENCES �������������������(Id),
����������� date NOT NULL
);
GO

CREATE TABLE ��������(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
��� nvarchar(64) NOT NULL,
������ nvarchar(32) NOT NULL,
����������� bigint NOT NULL REFERENCES ������(Id)
);
GO

CREATE TABLE ��������������������������(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
������������������ bigint NOT NULL REFERENCES ������������������(Id),
������� bigint NOT NULL REFERENCES ��������(Id),
������ nvarchar(50) NOT NULL CHECK(������ IN ('���', '�����', '�'))
);
GO

