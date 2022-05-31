/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) list.ID
      ,list.OrderID
	  , ord.Date
	  ,ord.Details
      ,list.ProductID
	  , prod.Name Product
	  , prod.Price
	  , list.Amount
  FROM OrderList list
  join Orders ord on list.OrderID = ord.ID
  join Products prod on list.ProductID = prod.ID