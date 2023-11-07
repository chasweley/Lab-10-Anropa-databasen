SELECT Categories.CategoryName, ProductName, UnitPrice FROM Products --Selects the columns I want to view
JOIN Categories ON Categories.CategoryID = Products.CategoryID --Joins two different tables on their primary key/foreign key relationship
ORDER BY CategoryName, ProductName; --Orders the rows first by CategoryName, then by ProductName

SELECT CompanyName, COUNT(Orders.OrderID) as NumberOfOrders FROM Customers --Selects the columns I want to view and also does a count of orders based on OrderID
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
GROUP BY CompanyName --Groups the info by CompanyName
ORDER BY NumberOfOrders DESC; --Orders by NumberOfOrders in descending order

SELECT LastName, FirstName, EmployeeTerritories.TerritoryID FROM Employees --Selects the columns I want to view
JOIN EmployeeTerritories ON EmployeeTerritories.EmployeeID = Employees.EmployeeID
ORDER BY LastName;
