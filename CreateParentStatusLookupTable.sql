IF NOT EXISTS  
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'ParentStatusLookupTable'
)
BEGIN
	CREATE TABLE kappa.dbo.ParentStatusLookupTable 
	(
		Id int not null CONSTRAINT pk_ParentStatus Primary Key,
		[Status] varchar(50) not null 
	)

	INSERT INTO kappa.dbo.ParentStatusLookupTable(Id, [Status])
	VALUES
		(0, 'Archived'),
		(1, 'Active')
END