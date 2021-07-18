SELECT 
	c.ClientId,
	c.Name,
	COUNT(a.ClosedDate) AS "Number of closed accounts",
	COUNT(c.ClientId) AS "Number of accounts"
FROM dbo.Clients c
INNER JOIN dbo.Accounts a 
	ON c.ClientId = a.ClientId 
GROUP BY c.ClientId, c.Name
HAVING 
	COUNT(c.ClientId) = COUNT(a.ClosedDate)