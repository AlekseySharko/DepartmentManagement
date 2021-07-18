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
WHERE  c.Name LIKE '���[^�-�]%' OR 
	   c.Name LIKE '%[^�-�]���' OR
	   c.Name LIKE '%[^�-�]���[^�-�]%'
ORDER BY c.Name