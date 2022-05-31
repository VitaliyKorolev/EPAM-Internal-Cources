use Northwind


--  1.a	Выбрать из таблицы Orders заказы, которые были доставлены после 5 мая 1998 
--года (колонка shippedDate) включительно и которые доставлены с shipVia >= 2. 
--Формат указания даты должен быть верным при любых региональных настройках. Этот 
--метод использовать далее для всех заданий. Запрос должен выбирать только колонки 
--orderID, shippedDate и shipVia.
SELECT OrderID, ShippedDate, ShipVia 
FROM Orders  
WHERE ShippedDate >= CONVERT(DATETIME, '19980506', 102) AND ShipVia >= 2

--1.b Написать запрос, который выводит только недоставленные заказы из таблицы 
--Orders. В результатах запроса высвечивать для колонки shippedDate вместо значений 
--NULL строку ‘Not Shipped’ – необходимо использовать CASЕ выражение. Запрос должен 
--выбирать только колонки orderID и shippedDate.
SELECT OrderID, "ShippedDate" =
 CASE
   WHEN ShippedDate IS NULL THEN 'Not Shipped'
 END
FROM Orders
WHERE ShippedDate IS NULL

--1.c Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года 
--(ShippedDate), не включая эту дату, или которые еще не доставлены. В запросе 
--должны высвечиваться только колонки OrderID (переименовать в Order Number) и 
--ShippedDate (переименовать в Shipped Date). В результатах запроса высвечивать
--для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, для остальных 
--значений высвечивать дату в формате по умолчанию.
SELECT OrderID AS "Order Number", "Shipped Date" =
 CASE
   WHEN ShippedDate IS NULL THEN 'Not Shipped'
   WHEN ShippedDate IS NOT NULL THEN CONVERT(VARCHAR(100), ShippedDate, 102) 
 END
FROM Orders
WHERE ShippedDate > CONVERT(DATETIME, '19980506', 102) OR ShippedDate IS NULL

--2.a Выбрать из таблицы Customers всех заказчиков, проживающих в USA или Canada. 
--Запрос сделать с только помощью оператора IN. Запрос должен выбирать колонки с 
--именем пользователя и названием страны. Упорядочить результаты запроса по имени 
--заказчиков и по месту проживания.
SELECT  ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'CANADA')
ORDER BY ContactName, Country

--2.b Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и 
--Canada. Запрос сделать с помощью оператора IN. Запрос должен выбирать колонки с 
--именем пользователя и названием страны. Упорядочить результаты запроса по имени 
--заказчиков в порядке убывания.
SELECT  ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'CANADA')
ORDER BY ContactName

--2.c Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
--Страна должна быть упомянута только один раз, Результат должен быть отсортирован 
--по убыванию. Не использовать предложение GROUP BY.
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC 

--3.a Выбрать все заказы из таблицы Order_Details (заказы не должны повторяться), 
--где встречаются продукты с количеством от 3 до 10 включительно – это колонка 
--Quantity в таблице Order_Details. Использовать оператор BETWEEN. Запрос должен 
--выбирать только колонку идентификаторы заказов.
SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10

--3.b Выбрать всех заказчиков из таблицы Customers, у которых название страны 
--начинается на буквы из диапазона B и G. Использовать оператор BETWEEN. Проверить, 
--что в результаты запроса попадает Germany. Запрос должен выбирать только колонки 
--сustomerID  и сountry. Результат должен быть отсортирован по значению столбца 
--сountry.
SELECT CustomerID, Country
FROM Customers
WHERE Country BETWEEN 'b%' AND 'h%'
ORDER BY Country 

--3.c Выбрать всех заказчиков из таблицы Customers, у которых название страны 
--начинается на буквы из диапазона B и G, не используя оператор BETWEEN. Запрос должен 
--выбирать только колонки сustomerID  и сountry. Результат должен быть отсортирован 
--по значению столбца сountry. 
SELECT CustomerID, Country
FROM Customers
WHERE Country >= 'b%' AND Country <= 'h%'
ORDER BY Country 

--4.a В таблице Products найти все продукты (колонка productName), где встречается 
--подстрока 'chocolade'. Известно, что в подстроке 'chocolade' может быть изменена 
--одна буква 'c' в середине - найти все продукты, которые удовлетворяют этому 
--условию. Подсказка: в результате должны быть найдены 2 строки.
SELECT ProductName
FROM Products
WHERE ProductName LIKE '%cho_olade%'

--5.a Найти общую сумму всех заказов из таблицы Order_Details с учетом количества 
--закупленных товаров и скидок по ним. Результат округлить до сотых и высветить в 
--стиле 1 для типа данных money. Скидка (колонка Discount) составляет процент из 
--стоимости для данного товара. Для определения действительной цены на проданный 
--продукт надо вычесть скидку из указанной в колонке unitPrice цены. Результатом 
--запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
SELECT ROUND(SUM(UnitPrice *(1 - Discount)), 2) AS Total
FROM [Order Details]

--5.b По таблице Orders найти количество заказов, которые еще не были доставлены 
--(т.е. в колонке shippedDate нет значения даты доставки). Использовать при этом 
--запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.
SELECT COUNT(*)- COUNT(ShippedDate)
FROM Orders 

--5.c По таблице Orders найти количество различных покупателей (сustomerID), 
--сделавших заказы. Использовать функцию COUNT и не использовать предложения 
--WHERE и GROUP.
SELECT COUNT(DISTINCT CustomerID) AS NumberOfCustomers
FROM Orders

--6.a По таблице Orders найти количество заказов с группировкой по годам. 
--Запрос должен выбирать две колонки c названиями Year и Total. Написать проверочный 
--запрос, который вычисляет количество всех заказов.
SELECT YEAR(OrderDate) AS 'Year', COUNT(*) AS Total
FROM Orders
GROUP BY YEAR(OrderDate)

SELECT COUNT(*)
FROM Orders

--6.b По таблице Orders найти количество заказов, cделанных каждым продавцом. 
--Заказ для указанного продавца – это любая запись в таблице Orders, где в колонке 
--employeeID задано значение для данного продавца. Запрос должен выбирать колонку 
--с полным именем продавца (получается конкатенацией lastName & firstName из таблицы 
--Employees)  с названием колонки ‘Seller’ и колонку c количеством заказов с названием 
--'Amount'. Полное имя продавца должно быть получено отдельным запросом в колонке 
--основного запроса (после SELECT, до FROM). Результаты запроса должны быть 
--упорядочены по убыванию количества заказов. 
SELECT (
 SElECT LastName + ' ' + FirstName FROM Employees
 WHERE Employees.EmployeeID = Orders.EmployeeID
) AS Seller, COUNT(*) AS Amount
FROM Orders
GROUP BY EmployeeID
ORDER BY Amount DESC

--6.c 	По таблице Orders найти количество заказов, cделанных каждым продавцом и 
--для каждого покупателя. Необходимо определить это только для заказов, сделанных 
--в 1998 году. Запрос должен выбирать колонку с именем продавца (название колонки 
--‘Seller’), колонку с именем покупателя (название колонки ‘Customer’) и колонку c 
--количеством заказов высвечивать с названием 'Amount'. В запросе необходимо 
--использовать специальный оператор языка PL/SQL для работы с выражением GROUP 
--(Этот же оператор поможет выводить строку “ALL” в результатах запроса). Группировки 
--должны быть сделаны по ID продавца и покупателя. Результаты запроса должны быть 
--упорядочены по продавцу, покупателю и по убыванию количества продаж. В результатах 
--должна быть сводная информация по продажам. Т.е. в результирующем наборе должны 
--присутствовать дополнительно к информации о продажах продавца для каждого покупателя 
--следующие строчки:
--  Seller	Customer	Amount
--  ALL 	ALL         <общее число продаж>
--  <имя>	ALL         <число продаж для данного продавца>
--  ALL	    <имя>       <число продаж для данного покупателя>
--  <имя>	<имя>       <число продаж данного продавца для даннного покупателя>
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

--6.d Найти покупателей и продавцов, которые живут в одном городе. Если в городе 
--живут только продавцы или только покупатели, то информация о таких покупателях и 
--продавцах не должна попадать в результирующий набор. Не использовать конструкцию 
--JOIN или декартово произведение. В результатах запроса необходимо вывести следующие 
--заголовки для результатов запроса: ‘Person’, ‘Type’ (здесь надо выводить строку 
--‘Customer’ или  ‘Seller’ в зависимости от типа записи), ‘City’. Отсортировать 
--результаты запроса по колонкам ‘City’ и ‘Person’.
SELECT CONTACTNAME AS "Person", 'Customer' AS "Type", City AS "City"
FROM CUSTOMERS
WHERE EXISTS (SELECT * FROM EMPLOYEES WHERE EMPLOYEES.CITY = CUSTOMERS.CITY)
UNION
 SELECT LASTNAME + ' ' + FIRSTNAME AS "Person", 'Seller' AS "Type", City AS "City"
 FROM EMPLOYEES
 WHERE EXISTS (SELECT * FROM CUSTOMERS WHERE CUSTOMERS.CITY = EMPLOYEES.CITY)
ORDER BY "City", "Person";

--6.e Найти всех покупателей, которые живут в одном городе. В запросе использовать 
--соединение таблицы Customers c собой - самосоединение. Высветить колонки сustomerID  
--и City. Запрос не должен выбирать дублируемые записи. Отсортировать результаты 
--запроса по колонке City. Для проверки написать запрос, который выбирает города, 
--которые встречаются более одного раза в таблице Customers. Это позволит проверить 
--правильность запроса.
SELECT DISTINCT C1.CUSTOMERID, C1.CITY
FROM CUSTOMERS C1, CUSTOMERS C2
WHERE C1.CITY = C2.CITY AND C1.CUSTOMERID != C2.CUSTOMERID;

SELECT CITY, COUNT(*) AS "Count"
FROM CUSTOMERS
GROUP BY CITY
HAVING COUNT(*) > 1;

--6.f По таблице Employees найти для каждого продавца его руководителя, т.е. 
--кому он делает репорты. Высветить колонки с именами 'User Name' (lastName) и 'Boss'. 
--Имена должны выбираться из колонки lastName.
SELECT LASTNAME AS "User Name",
    (SELECT LASTNAME FROM EMPLOYEES E2  WHERE E2.EMPLOYEEID = E1.REPORTSTO) AS "Boss"
FROM EMPLOYEES E1
EXCEPT 
SELECT LASTNAME AS "User Name",
    (SELECT LASTNAME FROM EMPLOYEES E2  WHERE E2.EMPLOYEEID = E1.REPORTSTO) AS "Boss"
FROM EMPLOYEES E1
WHERE REPORTSTO IS NULL

--7.a Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
--Запрос должен выбирать: 'lastName' продавца и название обслуживаемой территории 
--(столбец territoryDescription из таблицы Territories). Запрос должен использовать 
--JOIN в предложении FROM. Для определения связей между таблицами Employees и 
--Territories надо использовать графическую схему для базы Southwind.

SELECT LastName, T.TerritoryDescription
FROM Employees E
JOIN EmployeeTerritories ET ON E.EmployeeID = ET.EmployeeID
JOIN Territories T ON ET.TerritoryID = T.TerritoryID
JOIN Region R ON  R.RegionID = T.RegionID
WHERE R.RegionDescription = 'Western'

--8.a Запрос должен выбирать имена всех заказчиков из таблицы Customers и суммарное 
--количество их заказов из таблицы Orders. Принять во внимание, что у некоторых 
--заказчиков нет заказов, но они также должны быть выведены в результатах запроса. 
--Упорядочить результаты запроса по возрастанию количества заказов.
SELECT C.ContactName, COUNT(O.ORDERID) AS Total
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.ContactName
ORDER BY Total 

--9.a Запрос должен выбирать всех поставщиков (колонка companyName в таблице 
--Suppliers), у которых нет хотя бы одного продукта на складе (unitsInStock в 
--таблице Products равно 0). Использовать вложенный SELECT для этого запроса с 
--использованием оператора IN. Можно ли использовать вместо оператора IN оператор '='?
SELECT CompanyName 
FROM Suppliers S
WHERE S.SupplierID IN (
	SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0
)

  --можно использовать оператор "=" вместо "IN", только в том случае, когда вложенный 
  --запрос должен вернуть только одну запись.

--  10.a Запрос должен выбирать имена всех продавцов, которые имеют более 150 
--заказов. Использовать вложенный коррелированный SELECT.
SELECT (
	SELECT E.FirstName + ' ' + E.LastName
	FROM Employees E
	WHERE O.EmployeeID = E.EmployeeID
) AS Employee
FROM Orders O
GROUP BY O.EmployeeID
HAVING COUNT(*) > 150;

--11.a Использование EXISTS
--Высветить всех заказчиков (таблица Customers), которые не имеют ни 
--одного заказа (подзапрос по таблице Orders). Использовать коррелированный 
--SELECT и оператор EXISTS.
SELECT C.ContactName
FROM Customers C
WHERE NOT EXISTS(
	SELECT O.CustomerID
	FROM Orders O
	WHERE O.CustomerID = C.CustomerID
)

--12.a Использование строковых функций
--Для формирования алфавитного указателя Employees высветить из таблицы 
--Employees список только тех букв алфавита, с которых начинаются фамилии 
--Employees (колонка LastName ) из этой таблицы. Алфавитный список должен 
--быть отсортирован по возрастанию.
SELECT DISTINCT LEFT(LASTNAME,1) AS "Letter"
FROM Employees E
ORDER BY Letter
