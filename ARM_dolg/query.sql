USE dolgi
GO

CREATE TABLE Группы(
Id bigint NOT NULL PRIMARY KEY IDENTITY, 
Номер nvarchar(8) NOT NULL
);
GO

CREATE TABLE Преподаватели(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ФИО nvarchar(64) NOT NULL,
Пароль nvarchar(32) NOT NULL,
Администратор bit NOT NULL
);
GO

CREATE TABLE УчебныеПредметы(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
Название nvarchar(20) NOT NULL
);
GO

CREATE TABLE ГруппыПреподаватели(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
Преподаватель bigint NOT NULL REFERENCES Преподаватели(Id),
НомерГруппы bigint NOT NULL REFERENCES Группы(Id),
УчебныйПредмет bigint NOT NULL REFERENCES УчебныеПредметы(Id)
);
GO

CREATE TABLE ПрактическиеРаботы(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ГруппаПреподаватель bigint NOT NULL REFERENCES ГруппыПреподаватели(Id),
ДатаЗанятия date NOT NULL
);
GO

CREATE TABLE Студенты(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ФИО nvarchar(64) NOT NULL,
Пароль nvarchar(32) NOT NULL,
НомерГруппы bigint NOT NULL REFERENCES Группы(Id)
);
GO

CREATE TABLE СтудентыПрактическиеРаботы(
Id bigint NOT NULL PRIMARY KEY IDENTITY,
ПрактическияРабота bigint NOT NULL REFERENCES ПрактическиеРаботы(Id),
Студент bigint NOT NULL REFERENCES Студенты(Id),
Статус nvarchar(50) NOT NULL CHECK(Статус IN ('Зач', 'Незач', 'Н'))
);
GO

