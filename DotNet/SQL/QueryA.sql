use Northwind


--  1.a	������� �� ������� Orders ������, ������� ���� ���������� ����� 5 ��� 1998 
--���� (������� shippedDate) ������������ � ������� ���������� � shipVia >= 2. 
--������ �������� ���� ������ ���� ������ ��� ����� ������������ ����������. ���� 
--����� ������������ ����� ��� ���� �������. ������ ������ �������� ������ ������� 
--orderID, shippedDate � shipVia.
SELECT OrderID, ShippedDate, ShipVia 
FROM Orders  
WHERE ShippedDate >= CONVERT(DATETIME, '19980506', 102) AND ShipVia >= 2

--1.b �������� ������, ������� ������� ������ �������������� ������ �� ������� 
--Orders. � ����������� ������� ����������� ��� ������� shippedDate ������ �������� 
--NULL ������ �Not Shipped� � ���������� ������������ CAS� ���������. ������ ������ 
--�������� ������ ������� orderID � shippedDate.
SELECT OrderID, "ShippedDate" =
 CASE
   WHEN ShippedDate IS NULL THEN 'Not Shipped'
 END
FROM Orders
WHERE ShippedDate IS NULL

--1.c ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� 
--(ShippedDate), �� ������� ��� ����, ��� ������� ��� �� ����������. � ������� 
--������ ������������� ������ ������� OrderID (������������� � Order Number) � 
--ShippedDate (������������� � Shipped Date). � ����������� ������� �����������
--��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�, ��� ��������� 
--�������� ����������� ���� � ������� �� ���������.
SELECT OrderID AS "Order Number", "Shipped Date" =
 CASE
   WHEN ShippedDate IS NULL THEN 'Not Shipped'
   WHEN ShippedDate IS NOT NULL THEN CONVERT(VARCHAR(100), ShippedDate, 102) 
 END
FROM Orders
WHERE ShippedDate > CONVERT(DATETIME, '19980506', 102) OR ShippedDate IS NULL

--2.a ������� �� ������� Customers ���� ����������, ����������� � USA ��� Canada. 
--������ ������� � ������ ������� ��������� IN. ������ ������ �������� ������� � 
--������ ������������ � ��������� ������. ����������� ���������� ������� �� ����� 
--���������� � �� ����� ����������.
SELECT  ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'CANADA')
ORDER BY ContactName, Country

--2.b ������� �� ������� Customers ���� ����������, �� ����������� � USA � 
--Canada. ������ ������� � ������� ��������� IN. ������ ������ �������� ������� � 
--������ ������������ � ��������� ������. ����������� ���������� ������� �� ����� 
--���������� � ������� ��������.
SELECT  ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'CANADA')
ORDER BY ContactName

--2.c ������� �� ������� Customers ��� ������, � ������� ��������� ���������. 
--������ ������ ���� ��������� ������ ���� ���, ��������� ������ ���� ������������ 
--�� ��������. �� ������������ ����������� GROUP BY.
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC 

--3.a ������� ��� ������ �� ������� Order_Details (������ �� ������ �����������), 
--��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� 
--Quantity � ������� Order_Details. ������������ �������� BETWEEN. ������ ������ 
--�������� ������ ������� �������������� �������.
SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10

--3.b ������� ���� ���������� �� ������� Customers, � ������� �������� ������ 
--���������� �� ����� �� ��������� B � G. ������������ �������� BETWEEN. ���������, 
--��� � ���������� ������� �������� Germany. ������ ������ �������� ������ ������� 
--�ustomerID  � �ountry. ��������� ������ ���� ������������ �� �������� ������� 
--�ountry.
SELECT CustomerID, Country
FROM Customers
WHERE Country BETWEEN 'b%' AND 'h%'
ORDER BY Country 

--3.c ������� ���� ���������� �� ������� Customers, � ������� �������� ������ 
--���������� �� ����� �� ��������� B � G, �� ��������� �������� BETWEEN. ������ ������ 
--�������� ������ ������� �ustomerID  � �ountry. ��������� ������ ���� ������������ 
--�� �������� ������� �ountry. 
SELECT CustomerID, Country
FROM Customers
WHERE Country >= 'b%' AND Country <= 'h%'
ORDER BY Country 

--4.a � ������� Products ����� ��� �������� (������� productName), ��� ����������� 
--��������� 'chocolade'. ��������, ��� � ��������� 'chocolade' ����� ���� �������� 
--���� ����� 'c' � �������� - ����� ��� ��������, ������� ������������� ����� 
--�������. ���������: � ���������� ������ ���� ������� 2 ������.
SELECT ProductName
FROM Products
WHERE ProductName LIKE '%cho_olade%'

--5.a ����� ����� ����� ���� ������� �� ������� Order_Details � ������ ���������� 
--����������� ������� � ������ �� ���. ��������� ��������� �� ����� � ��������� � 
--����� 1 ��� ���� ������ money. ������ (������� Discount) ���������� ������� �� 
--��������� ��� ������� ������. ��� ����������� �������������� ���� �� ��������� 
--������� ���� ������� ������ �� ��������� � ������� unitPrice ����. ����������� 
--������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.
SELECT ROUND(SUM(UnitPrice *(1 - Discount)), 2) AS Total
FROM [Order Details]

--5.b �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� 
--(�.�. � ������� shippedDate ��� �������� ���� ��������). ������������ ��� ���� 
--������� ������ �������� COUNT. �� ������������ ����������� WHERE � GROUP.
SELECT COUNT(*)- COUNT(ShippedDate)
FROM Orders 

--5.c �� ������� Orders ����� ���������� ��������� ����������� (�ustomerID), 
--��������� ������. ������������ ������� COUNT � �� ������������ ����������� 
--WHERE � GROUP.
SELECT COUNT(DISTINCT CustomerID) AS NumberOfCustomers
FROM Orders

--6.a �� ������� Orders ����� ���������� ������� � ������������ �� �����. 
--������ ������ �������� ��� ������� c ���������� Year � Total. �������� ����������� 
--������, ������� ��������� ���������� ���� �������.
SELECT YEAR(OrderDate) AS 'Year', COUNT(*) AS Total
FROM Orders
GROUP BY YEAR(OrderDate)

SELECT COUNT(*)
FROM Orders

--6.b �� ������� Orders ����� ���������� �������, c�������� ������ ���������. 
--����� ��� ���������� �������� � ��� ����� ������ � ������� Orders, ��� � ������� 
--employeeID ������ �������� ��� ������� ��������. ������ ������ �������� ������� 
--� ������ ������ �������� (���������� ������������� lastName & firstName �� ������� 
--Employees)  � ��������� ������� �Seller� � ������� c ����������� ������� � ��������� 
--'Amount'. ������ ��� �������� ������ ���� �������� ��������� �������� � ������� 
--��������� ������� (����� SELECT, �� FROM). ���������� ������� ������ ���� 
--����������� �� �������� ���������� �������. 
SELECT (
 SElECT LastName + ' ' + FirstName FROM Employees
 WHERE Employees.EmployeeID = Orders.EmployeeID
) AS Seller, COUNT(*) AS Amount
FROM Orders
GROUP BY EmployeeID
ORDER BY Amount DESC

--6.c 	�� ������� Orders ����� ���������� �������, c�������� ������ ��������� � 
--��� ������� ����������. ���������� ���������� ��� ������ ��� �������, ��������� 
--� 1998 ����. ������ ������ �������� ������� � ������ �������� (�������� ������� 
--�Seller�), ������� � ������ ���������� (�������� ������� �Customer�) � ������� c 
--����������� ������� ����������� � ��������� 'Amount'. � ������� ���������� 
--������������ ����������� �������� ����� PL/SQL ��� ������ � ���������� GROUP 
--(���� �� �������� ������� �������� ������ �ALL� � ����������� �������). ����������� 
--������ ���� ������� �� ID �������� � ����������. ���������� ������� ������ ���� 
--����������� �� ��������, ���������� � �� �������� ���������� ������. � ����������� 
--������ ���� ������� ���������� �� ��������. �.�. � �������������� ������ ������ 
--�������������� ������������� � ���������� � �������� �������� ��� ������� ���������� 
--��������� �������:
--  Seller	Customer	Amount
--  ALL 	ALL         <����� ����� ������>
--  <���>	ALL         <����� ������ ��� ������� ��������>
--  ALL	    <���>       <����� ������ ��� ������� ����������>
--  <���>	<���>       <����� ������ ������� �������� ��� �������� ����������>
SELECT ISNULL((SELECT Employees.LastName + ' ' + Employees.FIRSTNAME
            FROM EMPLOYEES
            WHERE ORDERS.EMPLOYEEID = EMPLOYEES.EMPLOYEEID
        ),'ALL') AS "Seller",
        ISNULL((SELECT CUSTOMERS.COMPANYNAME
            FROM CUSTOMERS
            WHERE ORDERS.CUSTOMERID = CUSTOMERS.CUSTOMERID
        ),'ALL') AS "Customer",
        COUNT(*) AS "Amount"
    FROM ORDERS
    WHERE YEAR(ORDERDATE) = 1998
    GROUP BY CUBE(EMPLOYEEID, CUSTOMERID)
    ORDER BY "Seller" ASC, "Customer" ASC, "Amount" DESC;

--6.d ����� ����������� � ���������, ������� ����� � ����� ������. ���� � ������ 
--����� ������ �������� ��� ������ ����������, �� ���������� � ����� ����������� � 
--��������� �� ������ �������� � �������������� �����. �� ������������ ����������� 
--JOIN ��� ��������� ������������. � ����������� ������� ���������� ������� ��������� 
--��������� ��� ����������� �������: �Person�, �Type� (����� ���� �������� ������ 
--�Customer� ���  �Seller� � ����������� �� ���� ������), �City�. ������������� 
--���������� ������� �� �������� �City� � �Person�.
SELECT CONTACTNAME AS "Person", 'Customer' AS "Type", City AS "City"
FROM CUSTOMERS
WHERE EXISTS (SELECT * FROM EMPLOYEES WHERE EMPLOYEES.CITY = CUSTOMERS.CITY)
UNION
 SELECT LASTNAME + ' ' + FIRSTNAME AS "Person", 'Seller' AS "Type", City AS "City"
 FROM EMPLOYEES
 WHERE EXISTS (SELECT * FROM CUSTOMERS WHERE CUSTOMERS.CITY = EMPLOYEES.CITY)
ORDER BY "City", "Person";

--6.e ����� ���� �����������, ������� ����� � ����� ������. � ������� ������������ 
--���������� ������� Customers c ����� - ��������������. ��������� ������� �ustomerID  
--� City. ������ �� ������ �������� ����������� ������. ������������� ���������� 
--������� �� ������� City. ��� �������� �������� ������, ������� �������� ������, 
--������� ����������� ����� ������ ���� � ������� Customers. ��� �������� ��������� 
--������������ �������.
SELECT DISTINCT C1.CUSTOMERID, C1.CITY
FROM CUSTOMERS C1, CUSTOMERS C2
WHERE C1.CITY = C2.CITY AND C1.CUSTOMERID != C2.CUSTOMERID;

SELECT CITY, COUNT(*) AS "Count"
FROM CUSTOMERS
GROUP BY CITY
HAVING COUNT(*) > 1;

--6.f �� ������� Employees ����� ��� ������� �������� ��� ������������, �.�. 
--���� �� ������ �������. ��������� ������� � ������� 'User Name' (lastName) � 'Boss'. 
--����� ������ ���������� �� ������� lastName.
SELECT LASTNAME AS "User Name",
    (SELECT LASTNAME FROM EMPLOYEES E2  WHERE E2.EMPLOYEEID = E1.REPORTSTO) AS "Boss"
FROM EMPLOYEES E1
EXCEPT 
SELECT LASTNAME AS "User Name",
    (SELECT LASTNAME FROM EMPLOYEES E2  WHERE E2.EMPLOYEEID = E1.REPORTSTO) AS "Boss"
FROM EMPLOYEES E1
WHERE REPORTSTO IS NULL

--7.a ���������� ���������, ������� ����������� ������ 'Western' (������� Region). 
--������ ������ ��������: 'lastName' �������� � �������� ������������� ���������� 
--(������� territoryDescription �� ������� Territories). ������ ������ ������������ 
--JOIN � ����������� FROM. ��� ����������� ������ ����� ��������� Employees � 
--Territories ���� ������������ ����������� ����� ��� ���� Southwind.

SELECT LastName, T.TerritoryDescription
FROM Employees E
JOIN EmployeeTerritories ET ON E.EmployeeID = ET.EmployeeID
JOIN Territories T ON ET.TerritoryID = T.TerritoryID
JOIN Region R ON  R.RegionID = T.RegionID
WHERE R.RegionDescription = 'Western'

--8.a ������ ������ �������� ����� ���� ���������� �� ������� Customers � ��������� 
--���������� �� ������� �� ������� Orders. ������� �� ��������, ��� � ��������� 
--���������� ��� �������, �� ��� ����� ������ ���� �������� � ����������� �������. 
--����������� ���������� ������� �� ����������� ���������� �������.
SELECT C.ContactName, COUNT(O.ORDERID) AS Total
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.ContactName
ORDER BY Total 

--9.a ������ ������ �������� ���� ����������� (������� companyName � ������� 
--Suppliers), � ������� ��� ���� �� ������ �������� �� ������ (unitsInStock � 
--������� Products ����� 0). ������������ ��������� SELECT ��� ����� ������� � 
--�������������� ��������� IN. ����� �� ������������ ������ ��������� IN �������� '='?
SELECT CompanyName 
FROM Suppliers S
WHERE S.SupplierID IN (
	SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0
)

  --����� ������������ �������� "=" ������ "IN", ������ � ��� ������, ����� ��������� 
  --������ ������ ������� ������ ���� ������.

--  10.a ������ ������ �������� ����� ���� ���������, ������� ����� ����� 150 
--�������. ������������ ��������� ��������������� SELECT.
SELECT (
	SELECT E.FirstName + ' ' + E.LastName
	FROM Employees E
	WHERE O.EmployeeID = E.EmployeeID
) AS Employee
FROM Orders O
GROUP BY O.EmployeeID
HAVING COUNT(*) > 150;

--11.a ������������� EXISTS
--��������� ���� ���������� (������� Customers), ������� �� ����� �� 
--������ ������ (��������� �� ������� Orders). ������������ ��������������� 
--SELECT � �������� EXISTS.
SELECT C.ContactName
FROM Customers C
WHERE NOT EXISTS(
	SELECT O.CustomerID
	FROM Orders O
	WHERE O.CustomerID = C.CustomerID
)

--12.a ������������� ��������� �������
--��� ������������ ����������� ��������� Employees ��������� �� ������� 
--Employees ������ ������ ��� ���� ��������, � ������� ���������� ������� 
--Employees (������� LastName ) �� ���� �������. ���������� ������ ������ 
--���� ������������ �� �����������.
SELECT DISTINCT LEFT(LASTNAME,1) AS "Letter"
FROM Employees E
ORDER BY Letter
