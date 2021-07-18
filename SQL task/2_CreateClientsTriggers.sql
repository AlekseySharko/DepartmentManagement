CREATE TABLE dbo.ClientsChanges(
	ChangeId bigint PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Operation nvarchar(10) NOT NULL,
	Date datetime2(2) NOT NULL,
	ClientId bigint  NOT NULL,
	Name nvarchar(50) NOT NULL,
	Address nvarchar(130) NULL
);
GO
CREATE TRIGGER dbo.TR_Clients_AfterInsert
ON dbo.Clients
AFTER  INSERT
AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO dbo.ClientsChanges(
		Operation,
		Date,
		ClientId,
		Name,
		Address
	)
	SELECT
		'INSERT',
		SYSDATETIME(),
		i.ClientId,
		i.Name,
		i.Address
	FROM 
		inserted as i
END
GO
CREATE TRIGGER dbo.TR_Clients_AfterUpdate
ON dbo.Clients
AFTER  UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO dbo.ClientsChanges(
		Operation,
		Date,
		ClientId,
		Name,
		Address
	)
	SELECT
		'UPDATE',
		SYSDATETIME(),
		i.ClientId,
		i.Name,
		i.Address
	FROM 
		inserted as i
END
GO
CREATE TRIGGER dbo.TR_Clients_AfterDelete
ON dbo.Clients
AFTER  DELETE
AS
BEGIN
    SET NOCOUNT ON;
	INSERT INTO dbo.ClientsChanges(
		Operation,
		Date,
		ClientId,
		Name,
		Address
	)
	SELECT
		'DELETE',
		SYSDATETIME(),
		d.ClientId,
		d.Name,
		d.Address
	FROM 
		deleted as d
END