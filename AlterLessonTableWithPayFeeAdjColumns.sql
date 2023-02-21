IF NOT EXISTS 
	(
		SELECT 1
		FROM INFORMATION_SCHEMA.COLUMNS
		WHERE table_name = 'Lesson'
		and column_name = 'Pay'
		and column_name = 'Fee'
		and column_name = 'FeeAdjustment'
		and column_name = 'PayAdjustment'

	)
BEGIN 
	ALTER TABLE Lesson	
	ADD 
		Fee DECIMAL,
		Pay DECIMAL,
		FeeAdjustment Decimal,
		PayAdjustment Decimal
END