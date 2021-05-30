USE dolgi
GO

CREATE TABLE StudGroup(
Id bigint NOT NULL PRIMARY KEY IDENTITY, 
Номер nvarchar(8) NOT NULL UNIQUE
);
GO

CREATE TABLE Teacher(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ФИО nvarchar(64) NOT NULL UNIQUE,
Пароль nvarchar(32) NOT NULL,
Администратор bit NOT NULL
);
GO

CREATE TABLE StudSubject(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
Название nvarchar(20) NOT NULL UNIQUE
);
GO

CREATE TABLE GroupTeacher(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
Преподаватель bigint NOT NULL REFERENCES Teacher(Id),
НомерГруппы bigint NOT NULL REFERENCES StudGroup(Id),
УчебныйПредмет bigint NOT NULL REFERENCES StudSubject(Id),
CONSTRAINT УникальныйГруппаПреподаватель UNIQUE(Преподаватель, НомерГруппы, УчебныйПредмет)
);
GO

CREATE TABLE Lab(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ГруппаПреподаватель bigint NOT NULL REFERENCES GroupTeacher(Id),
ДатаЗанятия date NOT NULL,
CONSTRAINT УникальнаяРабота UNIQUE(ГруппаПреподаватель, ДатаЗанятия)
);
GO

CREATE TABLE Student(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ФИО nvarchar(64) NOT NULL,
Пароль nvarchar(32) NOT NULL,
НомерГруппы bigint NOT NULL REFERENCES StudGroup(Id),
CONSTRAINT УникальныйСтудент UNIQUE(ФИО, НомерГруппы)
);
GO

CREATE TABLE StudentLab(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ПрактическияРабота bigint NOT NULL REFERENCES Lab(Id),
Студент bigint NOT NULL REFERENCES Student(Id),
Статус nvarchar(50) NOT NULL CHECK(Статус IN ('Зач', 'Незач', 'Н')),
CONSTRAINT Работа UNIQUE(ПрактическияРабота, Студент)
);
GO

