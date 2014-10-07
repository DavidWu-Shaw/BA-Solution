USE [restorder]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderInfo]    Script Date: 01/12/2012 17:41:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetOrderInfo]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderItemList]    Script Date: 01/12/2012 17:41:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderItemList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetOrderItemList]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderSummaryList]    Script Date: 01/12/2012 17:41:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrderSummaryList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetOrderSummaryList]
GO

/****** Object:  StoredProcedure [dbo].[GetProductInfoList]    Script Date: 01/12/2012 17:41:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProductInfoList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProductInfoList]
GO

USE [restorder]
GO

/****** Object:  StoredProcedure [dbo].[GetOrderInfo]    Script Date: 01/12/2012 17:41:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		David Wu
-- Create date: Jan.12, 2012
-- Description:	
--
-- MODIFICATIONS
-- Modified ID		Modified Date	Modifications
-- David Wu			2012-01-12		Created SP
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderInfo] 
	@OrderId INT
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @EntityId INT
	SELECT 
		@EntityId = EntityId 
	FROM 
		dbo.tblEntity 
	WHERE 
		EntityKey = 'OrderStatus'

	DECLARE @LookupTable TABLE
	(
		Value INT,
		[Text] NVARCHAR(100)
	)
	
	INSERT INTO @LookupTable
		SELECT 
			Value, 
			[Text] 
		FROM 
			dbo.tblEntityItem 
		WHERE 
			EntityId = @EntityId

	SELECT 
		dbo.tblOrder.*,
		dbo.tblSupplier.Name AS SupplierName,
		LT.[Text] AS StatusText
	FROM 
		dbo.tblOrder
		INNER JOIN dbo.tblSupplier ON dbo.tblSupplier.SupplierId = dbo.tblOrder.SupplierId
		INNER JOIN @LookupTable LT ON LT.Value = dbo.tblOrder.StatusId
	WHERE 
		OrderId = @OrderId

END

GO

/****** Object:  StoredProcedure [dbo].[GetOrderItemList]    Script Date: 01/12/2012 17:41:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		David Wu
-- Create date: Jan.12, 2012
-- Description:	
--
-- MODIFICATIONS
-- Modified ID		Modified Date	Modifications
-- David Wu			2012-01-12		Created SP
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderItemList] 
	@OrderId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		dbo.tblOrderItem.*,
		dbo.tblProduct.Name AS ProductName
	FROM 
		dbo.tblOrderItem
		INNER JOIN dbo.tblProduct ON dbo.tblProduct.ProductId = dbo.tblOrderItem.ProductId
	WHERE 
		OrderId = @OrderId

END

GO

/****** Object:  StoredProcedure [dbo].[GetOrderSummaryList]    Script Date: 01/12/2012 17:41:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		David Wu
-- Create date: Jan.12, 2012
-- Description:	
--
-- MODIFICATIONS
-- Modified ID		Modified Date	Modifications
-- David Wu			2012-01-12		Created SP
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderSummaryList] 
	@SupplierId int = NULL,
	@CustomerId int = NULL,
	@StatusId int = NULL,
	@OrderNumber nvarchar(50) = NULL
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @EntityId INT
	SELECT 
		@EntityId = EntityId 
	FROM 
		dbo.tblEntity 
	WHERE 
		EntityKey = 'OrderStatus'

	DECLARE @LookupTable TABLE
	(
		Value INT,
		[Text] NVARCHAR(100)
	)
	
	INSERT INTO @LookupTable
		SELECT 
			Value, 
			[Text] 
		FROM 
			dbo.tblEntityItem 
		WHERE 
			EntityId = @EntityId

	SELECT 
		dbo.tblOrder.*,
		LT.[Text]
	FROM 
		dbo.tblOrder
		INNER JOIN @LookupTable LT ON LT.Value = dbo.tblOrder.StatusId
	WHERE 
		( @SupplierId IS NULL OR SupplierId = @SupplierId )
		AND ( @CustomerId IS NULL OR CustomerId = @CustomerId )
		AND ( @StatusId IS NULL OR dbo.tblOrder.StatusId = @StatusId )
		AND ( @OrderNumber IS NULL OR OrderNumber = @OrderNumber )
END

GO

/****** Object:  StoredProcedure [dbo].[GetProductInfoList]    Script Date: 01/12/2012 17:41:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		David Wu
-- Create date: Jan.12, 2012
-- Description:	
--
-- MODIFICATIONS
-- Modified ID		Modified Date	Modifications
-- David Wu			2012-01-12		Created SP
-- =============================================
CREATE PROCEDURE [dbo].[GetProductInfoList] 
	@SupplierId int = NULL,
	@CategoryId int = NULL,
	@ProductName nvarchar(100)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @EntityId INT
	SELECT 
		@EntityId = EntityId 
	FROM 
		dbo.tblEntity 
	WHERE 
		EntityKey = 'ProductCategory'

	DECLARE @LookupTable TABLE
	(
		Value INT,
		[Text] NVARCHAR(100)
	)
	
	INSERT INTO @LookupTable
		SELECT 
			Value, 
			[Text] 
		FROM 
			dbo.tblEntityItem 
		WHERE 
			EntityId = @EntityId

	SELECT 
		Product.*,
		Supplier.Name AS SupplierName,
		LT.Text AS CategoryText
	FROM 
		dbo.tblProduct Product
		INNER JOIN dbo.tblSupplier Supplier ON Supplier.SupplierId = Product.SupplierId
		INNER JOIN @LookupTable LT ON LT.Value=Product.CategoryId
	WHERE 
		( @SupplierId IS NULL OR Product.SupplierId = @SupplierId )
		AND ( @CategoryId IS NULL OR Product.CategoryId = @CategoryId )
END

GO


