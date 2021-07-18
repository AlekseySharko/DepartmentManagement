SELECT 
	c.ClientId,
	c.Name,
	a.AccountId,
	a.AccountNumber,
	a.CurrencyCode,
	a.OpenedDate,
	a.ClosedDate,
	a.Balance,
	a.ClientId
FROM dbo.Clients c
INNER JOIN dbo.Accounts a 
	ON c.ClientId = a.ClientId
WHERE a.ClosedDate IS NULL
ORDER BY c.Name
