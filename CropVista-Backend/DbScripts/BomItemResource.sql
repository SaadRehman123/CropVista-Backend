using CropVista_Backend.Models;

DROP PROCEDURE IF EXISTS [dbo].[BomItemResource];
GO

CREATE PROCEDURE [dbo].[BomItemResource]
    @queryType int,
	@resourceBID nvarchar(50),
	@resourceId nvarchar(50),
	@resourceName nvarchar(50),
	@resourceType nvarchar(50),
	@resourceQuantity int,
	@resourceUOM nvarchar(50),
	@warehouseId nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    IF @queryType = 1
    BEGIN
        INSERT INTO [dbo].[itemResource] ([resourceBID], [resourceId], [resourceName], [resourceType], [resourceQuantity], [resourceUOM], [warehouseId])
        VALUES (@resourceBID, @resourceId, @resourceName, @resourceType, @resourceQuantity, @resourceUOM, @warehouseId);
    END
    ELSE IF @queryType = 2
    BEGIN
		UPDATE [dbo].[itemResource]
		SET [resourceId] = @resourceId,
			[resourceName] = @resourceName,
			[resourceType] = @resourceType,
			[resourceQuantity] = @resourceQuantity,
			[resourceUOM] = @resourceUOM,
			[warehouseId] = @warehouseId
		WHERE [resourceBID] = @resourceBID;
    END
    ELSE IF @queryType = 3
    BEGIN
        DELETE FROM [dbo].[itemResource]
        WHERE [resourceBID] = @resourceBID;
    END
    ELSE
    BEGIN
        RAISERROR('Invalid Query Type', 16, 1);
        RETURN;
    END
END;
DROP PROCEDURE IF EXISTS [dbo].[BomItemResource];
GO

CREATE PROCEDURE [dbo].[BomItemResource]
    @queryType int,
	@resourceBID nvarchar(50),
	@resourceId nvarchar(50),
	@resourceName nvarchar(50),
	@resourceType nvarchar(50),
	@resourceQuantity int,
	@resourceUOM nvarchar(50),
	@warehouseId nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    IF @queryType = 1
    BEGIN
        INSERT INTO [dbo].[itemResource] ([resourceBID], [resourceId], [resourceName], [resourceType], [resourceQuantity], [resourceUOM], [warehouseId])
        VALUES (@resourceBID, @resourceId, @resourceName, @resourceType, @resourceQuantity, @resourceUOM, @warehouseId);
    END
    ELSE IF @queryType = 2
    BEGIN
		UPDATE [dbo].[itemResource]
		SET [resourceId] = @resourceId,
			[resourceName] = @resourceName,
			[resourceType] = @resourceType,
			[resourceQuantity] = @resourceQuantity,
			[resourceUOM] = @resourceUOM,
			[warehouseId] = @warehouseId
		WHERE [resourceBID] = @resourceBID;
    END
    ELSE IF @queryType = 3
    BEGIN
        DELETE FROM [dbo].[itemResource]
        WHERE [resourceBID] = @resourceBID;
    END
    ELSE
    BEGIN
        RAISERROR('Invalid Query Type', 16, 1);
        RETURN;
    END
END;
