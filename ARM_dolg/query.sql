USE dolgi
GO

CREATE TABLE StudGroup(
Id bigint NOT NULL PRIMARY KEY IDENTITY, 
����� nvarchar(8) NOT NULL UNIQUE
);
GO

CREATE TABLE Teacher(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
��� nvarchar(64) NOT NULL UNIQUE,
������ nvarchar(32) NOT NULL,
������������� bit NOT NULL
);
GO

CREATE TABLE StudSubject(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
�������� nvarchar(20) NOT NULL UNIQUE
);
GO

CREATE TABLE GroupTeacher(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
������������� bigint NOT NULL REFERENCES Teacher(Id),
����������� bigint NOT NULL REFERENCES StudGroup(Id),
�������������� bigint NOT NULL REFERENCES StudSubject(Id),
CONSTRAINT ����������������������������� UNIQUE(�������������, �����������, ��������������)
);
GO

CREATE TABLE Lab(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
������������������� bigint NOT NULL REFERENCES GroupTeacher(Id),
����������� date NOT NULL,
CONSTRAINT ���������������� UNIQUE(�������������������, �����������)
);
GO

CREATE TABLE Student(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
��� nvarchar(64) NOT NULL,
������ nvarchar(32) NOT NULL,
����������� bigint NOT NULL REFERENCES StudGroup(Id),
CONSTRAINT ����������������� UNIQUE(���, �����������)
);
GO

CREATE TABLE StudentLab(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
������������������ bigint NOT NULL REFERENCES Lab(Id),
������� bigint NOT NULL REFERENCES Student(Id),
������ nvarchar(50) NOT NULL CHECK(������ IN ('���', '�����', '�')),
CONSTRAINT ������ UNIQUE(������������������, �������)
);
GO

