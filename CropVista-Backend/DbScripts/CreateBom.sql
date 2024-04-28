DROP PROCEDURE IF EXISTS [dbo].[CreateBom];
GO

CREATE PROCEDURE [dbo].[CreateBom]
    @queryType int,
    @BID nvarchar(50) OUTPUT,
	@productId nvarchar(50),
	@productDescription nvarchar(50),
	@productionStdCost float,
	@quantity int,
	@wrId nvarchar(50),
	@priceList nvarchar(50),
	@type nvarchar(50),
	@routeSequence int,
	@itemQuantity int,
	@additionalQuantity int,
	@UOM nvarchar(50),
	@totalProductionStdCost float,
	@unitPrice float,
	@total float,
	@productPrice float,
	@creationDate date
AS
BEGIN
	SET NOCOUNT ON;

    IF @queryType = 1
    BEGIN
        DECLARE @NextId INT;

        SELECT @NextId = ISNULL(MAX(CAST(SUBSTRING(BID, CHARINDEX('-', BID) + 1, LEN(BID) - CHARINDEX('-', BID)) AS INT)), 0) + 1
        FROM BOM
        WHERE BID LIKE 'BM-%';

        IF @NextId <= 9999
        BEGIN
            SET @BID = 'BM-' + RIGHT('000' + CAST(@NextId AS NVARCHAR(10)), 4);
        END
        ELSE
        BEGIN
            SET @BID = 'BM-' + CAST(@NextId AS NVARCHAR(10));
        END

        INSERT INTO [dbo].[BOM] ([BID], [productId], [productDescription], [productionStdCost], [quantity], [wrId], [priceList], [type], [routeSequence], [itemQuantity], [additionalQuantity], [UOM], [totalProductionStdCost], [unitPrice], [total], [productPrice], [creationDate])
        VALUES (@BID, @productId, @productDescription, @productionStdCost, @quantity, @wrId, @priceList, @type, @routeSequence, @itemQuantity, @additionalQuantity, @UOM, @totalProductionStdCost, @unitPrice, @total, @productPrice, @creationDate);
    END
    ELSE IF @queryType = 2
    BEGIN
		UPDATE [dbo].[BOM]
		SET [productId] = @productId,
			[productDescription] = @productDescription,
			[productionStdCost] = @productionStdCost,
			[quantity] = @quantity,
			[wrId] = @wrId,
			[priceList] = @priceList,
			[type] = @type,
			[routeSequence] = @routeSequence,
			[itemQuantity] = @itemQuantity,
			[additionalQuantity] = @additionalQuantity,
			[UOM] = @UOM,
			[totalProductionStdCost] = @totalProductionStdCost,
			[unitPrice] = @unitPrice,
			[total] = @total,
			[productPrice] = @productPrice,
			[creationDate] = @creationDate
		WHERE [BID] = @BID;
    END
    ELSE IF @queryType = 3
    BEGIN
        DELETE FROM [dbo].[BOM]
        WHERE [BID] = @BID;
    END
    ELSE IF @queryType = 4
    BEGIN
        SELECT * FROM [dbo].[BOM] bom inner join itemResource resource on bom.BID = resource.resourceBID;
    END
    ELSE
    BEGIN
        RAISERROR('Invalid Query Type', 16, 1);
        RETURN;
    END
END;
