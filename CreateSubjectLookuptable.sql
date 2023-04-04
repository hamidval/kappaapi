IF NOT EXISTS  
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'SubjectLookupTable'
)
BEGIN
	CREATE TABLE kappa.dbo.SubjectLookupTable 
	(
		Id int not null CONSTRAINT pk_Subject Primary Key,
		[Subject] varchar(50) not null 
	)

	INSERT INTO kappa.dbo.SubjectLookupTable(Id, [Subject])
	VALUES
		(0, 'Maths'),
		(1, 'Science'),
		(2, 'Chemistry')
END
