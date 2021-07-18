CREATE PROCEDURE usp_SetBalanceToZero(
	@nameLike AS NVARCHAR(50)	
)
AS
BEGIN
    UPDATE dbo.Accounts
	SET dbo.Accounts.Balance = 0
	FROM
		dbo.Accounts a INNER JOIN dbo.Clients c on a.ClientId = c.ClientId
	WHERE
		a.Balance IS NULL AND c.Name LIKE @nameLike 
END;
GO
EXECUTE dbo.usp_SetBalanceToZero @nameLike = 'Œ¿Œ "Õ‡ÙÚ‡Ì"'