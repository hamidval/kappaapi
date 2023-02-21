IF NOT EXISTS
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'InvoiceStatusLookupTable'
)
BEGIN

	CREATE TABLE kappa.dbo.InvoiceStatusLookupTable 
	(
		Id int NOT NULL CONSTRAINT pk_InvoiceStatus PRIMARY KEY,
		InvoiceStatus VARCHAR(100) not null
	)

	INSERT INTO kappa.dbo.InvoiceStatusLookupTable (Id, InvoiceStatus)
	VALUES
		(0, 'UnPaid'),
		(1, 'Paid'),
		(2, 'Pending')
	
END






IF NOT EXISTS
(
	SELECT 1
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Invoice'
)
BEGIN

	CREATE TABLE dbo.Invoice 
	(
		Id int Identity(1,1) CONSTRAINT pk_Invoice PRIMARY KEY,
		CreatedOn DateTime not null,
		StripeInvoiceId varchar(250) not null,
		InvoiceStatus int CONSTRAINT fk_InvoiceStatusLookupTable not null,
		StripeInvoiceUrl varchar(2000) not null,
		InvoiceAmount DECIMAL(18,2) not null		
	)
	
END


