select Orders.orderDetails,
	Customers.customerName,
	Products.productName,
	OrderList.productQuantity,
	Orders.orderDate
from OrderList 
inner join Orders on OrderList.orderId = Orders.OrderId
inner join Products on OrderList.productId = Products.productId
inner join Customers on Orders.customerId = Customers.customerId