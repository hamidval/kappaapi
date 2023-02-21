IF EXISTS
(
	SELECT 1 
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'TakenLesson' and COLUMN_NAME = 'FeeAdjustment' 
)
BEGIN
	EXEC sp_RENAME 'TakenLesson.FeeAdjustment', 'GroupFee', 'COLUMN'
END