# How to work with this??

## Restore database
Create new database (for example: yetAnotherDB),
right click on yetAnotherDB pick tasks -> restore -> database,
in opened window click on device option,
then click on three dots and there pick TestDB.bak file,
go to the options, enable overwrite the existing database,
click on OK button

Congrats, database is successfully restored!

## How to use SQLQuery.sql file?
Just drag and drop it in SSMS with database that you already have restored and run it
or run this sql query:
```sql
select Orders.orderDetails,
	Customers.customerName,
	Products.productName,
	OrderList.productQuantity,
	Orders.orderDate
from OrderList 
inner join Orders on OrderList.orderId = Orders.OrderId
inner join Products on OrderList.productId = Products.productId
inner join Customers on Orders.customerId = Customers.customerId
```