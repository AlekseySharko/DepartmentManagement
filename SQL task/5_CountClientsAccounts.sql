SELECT 
	c.ClientId,
	c.Name,
	COUNT(c.ClientId) AS "Number of accounts"
FROM dbo.Clients c
INNER JOIN dbo.Accounts a 
	ON c.ClientId = a.ClientId 
GROUP BY c.ClientId, c.Name