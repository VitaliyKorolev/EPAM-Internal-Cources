USE Northwind

--1.a
GO
CREATE OR ALTER PROCEDURE GreatestOrders
    @year INT,
	@numberOfRows INT
AS
	WITH src AS (
            SELECT O.EmployeeID, OD.OrderID, ROUND(SUM(OD.UnitPrice * OD.Quantity * (1 - OD.Discount)), 2) AS Total
              FROM Orders O
              JOIN [Order Details] OD ON O.OrderID = OD.OrderID
              WHERE YEAR(O.OrderDate) = @year
              GROUP BY O.EmployeeID, OD.OrderID
    )
	SELECT top(@numberOfRows) t3.LastName + ' ' + t3.FirstName AS FullName, t1.OrderID, t1.Total
            FROM src t1
            JOIN (SELECT EmployeeID, MAX(Total) AS MaxSum
                    FROM src 
                    GROUP BY EmployeeID) t2
            ON t1.EmployeeID = t2.EmployeeID AND t1.Total = t2.MaxSum
            JOIN Employees t3
            ON t2.EmployeeID = t3.EmployeeID
            ORDER BY t1.Total DESC;
GO 

CREATE OR ALTER PROCEDURE GreatestOrdersCur
    @year INT,
	@numberOfRows INT
AS
	DECLARE cur CURSOR FOR (
            SELECT O.EmployeeID, OD.OrderID, ROUND(SUM(OD.UnitPrice * OD.Quantity * (1 - OD.Discount)), 2) AS Total
              FROM Orders O
              JOIN [Order Details] OD ON O.OrderID = OD.OrderID
              WHERE YEAR(O.OrderDate) = @year
              GROUP BY O.EmployeeID, OD.OrderID
    )
	OPEN cur
	DECLARE @EmployeeID INT
	DECLARE @OrderID INT
	DECLARE @Total MONEY

	FETCH NEXT FROM cur INTO @EmployeeID, @OrderID, @Total
	DECLARE @MaxSale TABLE ( employeeID INT, orderID INT, maxSale MONEY)
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF(@EmployeeID NOT IN (SELECT employeeID FROM @MaxSale) )
		   INSERT @MaxSale (employeeID, orderID, maxSale) VALUES (@EmployeeID, @OrderID, @Total)
	    ELSE
		BEGIN
		    IF((SELECT TOP(1) maxSale FROM @MaxSale WHERE employeeID = @EmployeeID) < @Total)
				UPDATE @MaxSale SET maxSale = @Total, orderID =@OrderID  WHERE employeeID = @EmployeeID
		END
		FETCH NEXT FROM cur INTO @EmployeeID, @OrderID, @Total			
	END

	SELECT TOP(@numberOfRows) LastName + ' ' + FirstName AS FullName, M.orderID, M.maxSale
	FROM @MaxSale M
	JOIN Employees E ON M.employeeID = E.EmployeeID
	ORDER BY M.maxSale DESC

	CLOSE cur
	DEALLOCATE cur
GO

--1.b
GO
CREATE OR ALTER PROCEDURE ShippedOrdersDiff
    @specifiedDelay INT = 35
AS
	SELECT OrderID, CONVERT(VARCHAR(100), OrderDate, 102) AS OrderDate, 
	Coalesce(CONVERT(VARCHAR(100), ShippedDate, 102), 'Not Shipped') AS ShippedDate, 
	Coalesce(CONVERT(VARCHAR(100), DAY( ShippedDate-OrderDate)), 'Not Shipped')AS ShippedDelay, 
	@specifiedDelay AS SpecifiedDelay
	FROM Orders
	WHERE DAY(ShippedDate - OrderDate) >= @specifiedDelay OR ShippedDate IS NULL
	ORDER BY ShippedDelay DESC
GO

--1.c
GO
CREATE OR ALTER PROCEDURE SubordinationInfo
    @employeeID INT
AS
	DECLARE cur CURSOR FOR
	WITH DirectReports (ReportsTo, EmployeeID, Name, Level, Sort)
	AS
	(
		SELECT e.ReportsTo, e.EmployeeID, 
		CONVERT(varchar(255), REPLICATE ('|    ' , 1) + e.FirstName + ' ' + e.LastName), 
		1 AS Level, 
		CONVERT(varchar(255), REPLICATE ('|    ' , 1) + e.FirstName + ' ' + e.LastName)
		FROM Employees AS e
		WHERE ReportsTo = @employeeID
		UNION ALL
		SELECT e.ReportsTo, e.EmployeeID, 
		CONVERT(varchar(255), REPLICATE ('|    ' , Level+1) + e.FirstName + ' ' + e.LastName),  
		Level + 1,
		CONVERT (varchar(255), RTRIM(Sort) + '|    ' + FirstName + ' ' + LastName)
		FROM Employees AS e
		INNER JOIN DirectReports AS d ON e.ReportsTo = d.EmployeeID
	)
	SELECT  E.FirstName + ' '+ E.LastName AS Name, 0 AS Level
	FROM Employees AS e
	WHERE EmployeeID = @employeeID
	UNION
	SELECT D.Name, Level
	FROM DirectReports D
	ORDER BY Level

	OPEN cur
	DECLARE @name varchar(255)
	DECLARE @lvl int

	FETCH NEXT FROM cur INTO @name, @lvl
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		PRINT(@name)
		FETCH NEXT FROM cur INTO @name, @lvl		
	END
	CLOSE cur
	DEALLOCATE cur
GO

--1.d
CREATE OR ALTER PROCEDURE IsBoss
    @employeeID INT,
	@isBoss BIT OUTPUT
AS
	IF((SELECT COUNT(*)
		FROM Employees AS E
		WHERE ReportsTo = @employeeID) = 0)
		SET @isBoss = 0
	ELSE
		SET @isBoss = 1
GO