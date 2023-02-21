IF NOT EXISTS 
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Parent'
)
BEGIN

	CREATE TABLE kappa.dbo.Parent
	(
		Id INT NOT NULL CONSTRAINT pk_Parent PRIMARY KEY,
		FirstName varchar(200) not null,  
		LastName varchar(200) not null,
		Email varchar(300) not null,
	)

END