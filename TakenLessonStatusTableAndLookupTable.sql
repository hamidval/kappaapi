IF NOT EXISTS  
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE table_name = 'TakenLessonPaidStatus'
)
BEGIN
	CREATE TABLE kappa.dbo.TakenLessonPaidStatus 
	(
		Id int not null CONSTRAINT pk_TakenLessonPaidStatus Primary Key,
		[Status] varchar(50) not null 
	)

	INSERT INTO kappa.dbo.TakenLessonPaidStatus(Id, [Status])
	VALUES
		(0, 'Unpaid'),
		(1, 'Paid'),
		(2, 'Refunded')
END
