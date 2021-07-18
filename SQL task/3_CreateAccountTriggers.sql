CREATE TABLE dbo.AccountsChanges(
	ChangeId bigint PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Operation nvarchar(10) NOT NULL,
	Date datetime2(2) NOT NULL,
	AccountId bigint NOT NULL,
	AccountNumber nvarchar(50) NOT NULL,
	CurrencyCode nvarchar(3) NOT NULL,
	OpenedDate datetime2(2) NOT NULL,
	ClosedDate datetime2(2) NULL,
	Balance decimal(19, 4) NULL,
	ClientId bigint NOT NULL,
);
GO
CREATE TRIGGER dbo.TR_Accounts_AfterInsert
ON dbo.Accounts
AFTER  INSERT
AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO dbo.AccountsChanges(
		Operation,
		Date,
		AccountId,
		AccountNumber,
		CurrencyCode,
		OpenedDate,
		ClosedDate,
		Balance,
		ClientId
	)
	SELECT
		'INSERT',
		SYSDATETIME(),
		i.AccountId,
		i.AccountNumber,
		i.CurrencyCode,
		i.OpenedDate,
		i.ClosedDate,
		i.Balance,
		i.ClientId
	FROM 
		inserted as i
END
GO
CREATE TRIGGER dbo.TR_Accounts_AfterUpdate
ON dbo.Accounts
AFTER  UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO dbo.AccountsChanges(
		Operation,
		Date,
		AccountId,
		AccountNumber,
		CurrencyCode,
		OpenedDate,
		ClosedDate,
		Balance,
		ClientId
	)
	SELECT
		'UPDATE',
		SYSDATETIME(),
		i.AccountId,
		i.AccountNumber,
		i.CurrencyCode,
		i.OpenedDate,
		i.ClosedDate,
		i.Balance,
		i.ClientId
	FROM 
		inserted as i
END
GO
CREATE TRIGGER dbo.TR_Accounts_AfterDelete
ON dbo.Accounts
AFTER  DELETE
AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO dbo.AccountsChanges(
		Operation,
		Date,
		AccountId,
		AccountNumber,
		CurrencyCode,
		OpenedDate,
		ClosedDate,
		Balance,
		ClientId
	)
	SELECT
		'DELETE',
		SYSDATETIME(),
		d.AccountId,
		d.AccountNumber,
		d.CurrencyCode,
		d.OpenedDate,
		d.ClosedDate,
		d.Balance,
		d.ClientId
	FROM 
		deleted as d
END
GO