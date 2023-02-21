IF NOT EXISTS
(
	SELECT 1 
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Teacher'
)
BEGIN
CREATE TABLE kappa.dbo.Teacher
	(
		Id INT IDENTITY(1,1) CONSTRAINT pk_Teacher PRIMARY KEY,
		FirstName varchar(200) not null,  
		LastName varchar(200) not null,
		Email varchar(400) not null
	)
END