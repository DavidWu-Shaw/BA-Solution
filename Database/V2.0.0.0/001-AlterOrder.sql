
alter table dbo.tblOrder 
add 
	ShipToContactPhone nvarchar(20), 
	ShipToContactPerson nvarchar(100), 
	ShipToAddress nvarchar(100), 
	ShipToZipCode nvarchar(20),
	QtyOrderedTotal int

alter table dbo.tblOrderItem 
add 
	UnitPrice decimal(10, 2) NULL,
	ProductName nvarchar(100) NULL
