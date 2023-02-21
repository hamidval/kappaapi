IF NOT EXISTS
(
	SELECT 1 
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'TakenLesson'
)
BEGIN
	CREATE TABLE kappa.dbo.TakenLesson
	(
		Id INT IDENTITY(1,1) CONSTRAINT pk_TakenLesson PRIMARY KEY,
		StudentId INT not null FOREIGN KEY REFERENCES Student(Id),  
		TeacherId INT not null FOREIGN KEY REFERENCES Teacher(Id),		
		[Subject] INT not null,
		YearGroup INT not null,
		Pay DECIMAL(18, 2) NOT NULL,
		Fee DECIMAL(18, 2) NOT NULL,
		PayAdjustment DECIMAL(18, 2) NOT NULL,
		FeeAdjustment DECIMAL(18, 2) NOT NULL

	)
END