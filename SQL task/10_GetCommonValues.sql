SELECT 
	t1.Value
FROM dbo.T1 t1
INNER JOIN  dbo.T2 t2 
	ON t1.Value = t2.Value 