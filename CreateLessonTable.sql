
IF NOT EXISTS
(
	SELECT 1 
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Lesson'
)
BEGIN
	CREATE TABLE kappa.dbo.Lesson
	(
		Id INT IDENTITY(1,1) CONSTRAINT pk_Lesson PRIMARY KEY,
		StudentId INT not null FOREIGN KEY REFERENCES Student(Id),  
		TeacherId INT not null FOREIGN KEY REFERENCES Teacher(Id), 
		ParentId INT not null FOREIGN KEY REFERENCES Parent(Id),
		[Subject] INT not null,
		YearGroup INT not null 

	)
END