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
WHERE  c.Name LIKE 'Œ¿Œ[^¿-ﬂ]%' OR 
	   c.Name LIKE '%[^¿-ﬂ]Œ¿Œ' OR
	   c.Name LIKE '%[^¿-ﬂ]Œ¿Œ[^¿-ﬂ]%'
ORDER BY c.Name