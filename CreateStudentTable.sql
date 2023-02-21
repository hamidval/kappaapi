IF NOT EXISTS 
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Student'
)
BEGIN

	CREATE TABLE kappa.dbo.Student
	(
		Id INT IDENTITY(1,1) CONSTRAINT pk_Student PRIMARY KEY,
		FirstName varchar(200) not null,  
		LastName varchar(200) not null,
		ParentId INT not null FOREIGN KEY REFERENCES Parent(Id) 
	)

END