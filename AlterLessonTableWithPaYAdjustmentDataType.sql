IF EXISTS
(
	SELECT 1
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE table_name = 'Lesson'
	and column_name = 'PayAdjustment'
)

BEGIN

ALTER TABLE Lesson 
ALTER COLUMN PayAdjustment DECIMAL(18,2)

END 