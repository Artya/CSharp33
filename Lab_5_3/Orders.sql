/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) ord.ID
     --,ord.CustomerID
	  , cust.Name Customer
      --,ord.Status
	  ,ost.Name OrderStatus
      ,ord.Details
      ,ord.Date
  FROM Orders ord
  join Customers cust on ord.CustomerID = cust.ID
  join OrderStatuses ost on ord.Status = ost.ID 
  order by ord.Date