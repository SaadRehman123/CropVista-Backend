DROP PROCEDURE IF EXISTS [dbo].[CreateWarehouse];
GO

CREATE PROCEDURE [dbo].[CreateWarehouse]
    @queryType int,
    @wrId nvarchar(50) OUTPUT,
    @name nvarchar(50),
    @wrType nvarchar(50),
    @active bit,
    @location nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    IF @queryType = 1
    BEGIN
        DECLARE @NextId INT;

        SELECT @NextId = ISNULL(MAX(CAST(SUBSTRING(wrId, CHARINDEX('-', wrId) + 1, LEN(wrId) - CHARINDEX('-', wrId)) AS INT)), 0) + 1
        FROM warehouse
        WHERE wrId LIKE 'WR-%';

        IF @NextId <= 9999
        BEGIN
            SET @wrId = 'WR-' + RIGHT('000' + CAST(@NextId AS NVARCHAR(10)), 4);
        END
        ELSE
        BEGIN
            SET @wrId = 'WR-' + CAST(@NextId AS NVARCHAR(10));
        END

        INSERT INTO [dbo].[warehouse] ([wrId], [name], [wrType], [active], [location])
        VALUES (@wrId, @name, @wrType, @active, @location);
    END
    ELSE IF @queryType = 2
    BEGIN
        UPDATE [dbo].[warehouse]
        SET [wrId] = @wrId,
            [name] = @name,
            [wrType] = @wrType,
            [active] = @active,
            [location] = @location
			
        WHERE [wrId] = @wrId;
    END
    ELSE IF @queryType = 3
    BEGIN
        -- Delete warehouse
        DELETE FROM [dbo].[warehouse]
        WHERE [wrId] = @wrId;
    END
    ELSE IF @queryType = 4
    BEGIN
        SELECT * FROM [dbo].[warehouse];
    END
    ELSE
    BEGIN
        RAISERROR('Invalid Query Type', 16, 1);
        RETURN;
    END
END;
