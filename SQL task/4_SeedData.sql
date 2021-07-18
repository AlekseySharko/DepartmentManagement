INSERT INTO dbo.Clients (
	Name,
	Address
)
VALUES
('Nereida A Pittman', '708 Saddlebrook Dr, Colleyville, TX, 76034'),
('Œ¿Œ "Õ‡ÙÚ‡Ì"', '4956 Memorial Dr, Poplar, WI, 54864'),
('—Œ¿Œ " ÓÏÏÛÌ‡Í‡"', '77001 180th St, Sacred Heart, MN, 56285'),
('Ã¿« Œ¿Œ', '405 Brink St, Grayling, MI, 49738'),
('Philip R Smith', NULL)
GO
INSERT INTO dbo.Accounts(
	AccountNumber,
	CurrencyCode,
	OpenedDate,
	ClosedDate,
	Balance,
	ClientId
)
VALUES
(
	'124325143523432156',
	'USD',
	SYSDATETIME(),
	NULL,
	3141235.25,
	2
),
(
	'124345143524332156',
	'USD',
	SYSDATETIME(),
	SYSDATETIME(),
	NULL,
	3
),
(
	'124325145523462146',
	'USD',
	SYSDATETIME(),
	SYSDATETIME(),
	NULL,
	3
),
(
	'424325443523432156',
	'USD',
	SYSDATETIME(),
	NULL,
	143215.8,
	1
),
(
	'124825143823432856',
	'USD',
	SYSDATETIME(),
	SYSDATETIME(),
	NULL,
	2
),
(
	'125325543523432155',
	'USD',
	SYSDATETIME(),
	NULL,
	NULL,
	1
),
(
	'924325143523432956',
	'USD',
	SYSDATETIME(),
	NULL,
	1341325.25,
	1
),
(
	'224325123523432252',
	'USD',
	SYSDATETIME(),
	NULL,
	NULL,
	4
),
(
	'124325143111432156',
	'USD',
	SYSDATETIME(),
	NULL,
	2.25,
	2
),
(
	'024325143523432006',
	'BYN',
	SYSDATETIME(),
	NULL,
	3,
	2
)
GO
INSERT INTO  dbo.T1 (
	Value
)
VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10)

INSERT INTO  dbo.T2 (
	Value
)
VALUES (1),(3),(6),(7),(8),(11),(15)