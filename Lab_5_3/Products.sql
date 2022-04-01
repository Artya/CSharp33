/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) pr.ID
      ,pr.Name 
      ,pr.Description
      ,pr.Price
      --,pr.ProductType  ProductType
	  ,typ.Name ProductType
      --,pr.SupplierID
	  , sup.Name Supplier
  FROM Products pr
  join ProductTypes  typ on pr.ProductType = typ.ID
  join Suppliers sup on pr.SupplierID = sup.ID