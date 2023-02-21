IF NOT EXISTS
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'LessonDayLookupTable'
)
BEGIN
	CREATE TABLE kappa.dbo.LessonDayLookupTable
	(
		Id int not null CONSTRAINT pk_LessonDay PRIMARY KEY,
		[Day] varchar(10) not null
	);

	INSERT INTO kappa.dbo.LessonDayLookupTable (Id, [Day])
	VALUES 
		(0, 'Monday'),
		(1, 'Tuesday'),
		(2, 'Wednesday'),
		(3, 'Thursday'),
		(4, 'Friday'),
		(5, 'Saturday'),
		(6, 'Sunday')
END;