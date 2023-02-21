IF EXISTS 
(
	SELECT 1 
	FROM INFORMATION_SCHEMA.Columns
	WHERE table_name = 'Lesson'
	and column_name = 'ParentId'
)

BEGIN
	ALTER TABLE Lesson DROP CONSTRAINT [FK__Lesson__ParentId__4D94879B]
	ALTER TABLE Lesson DROP COLUMN ParentId
END