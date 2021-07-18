CREATE TABLE dbo.Clients(
	ClientId bigint PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name nvarchar(50) NOT NULL,
	Address nvarchar(130) NULL
);
GO
CREATE TABLE dbo.Accounts(
	AccountId bigint PRIMARY KEY IDENTITY(1,1) NOT NULL,
	AccountNumber nvarchar(50) NOT NULL CONSTRAINT UQ_Accounts_AccountNumber UNIQUE(AccountNumber),
	CurrencyCode nvarchar(3) NOT NULL,
	OpenedDate datetime2(2) NOT NULL,
	ClosedDate datetime2(2) NULL,
	Balance decimal(19, 4) NULL,
	ClientId bigint NOT NULL,
	FOREIGN KEY (ClientId) REFERENCES dbo.Clients (ClientId)
);
GO
CREATE TABLE dbo.T1(
	T1Id bigint PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Value int NOT NULL
);
GO
CREATE TABLE dbo.T2(
	T1Id bigint PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Value int NOT NULL
);