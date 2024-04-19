DROP PROCEDURE IF EXISTS [dbo].[CreateResource];
GO

CREATE PROCEDURE [dbo].[CreateResource]
    @queryType int,
    @rId nvarchar(50) OUTPUT,
    @name nvarchar(50),
    @rType nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    IF @queryType = 1
    BEGIN
        DECLARE @NextId INT;

        SELECT @NextId = ISNULL(MAX(CAST(SUBSTRING(rId, CHARINDEX('-', rId) + 1, LEN(rId) - CHARINDEX('-', rId)) AS INT)), 0) + 1
        FROM resources
        WHERE rId LIKE 'RE-%';

        IF @NextId <= 9999
        BEGIN
            SET @rId = 'RE-' + RIGHT('000' + CAST(@NextId AS NVARCHAR(10)), 4);
        END
        ELSE
        BEGIN
            SET @rId = 'RE-' + CAST(@NextId AS NVARCHAR(10));
        END

        INSERT INTO [dbo].[resources] ([rId], [name], [rType])
        VALUES (@rId, @name, @rType);
    END
    ELSE IF @queryType = 2
    BEGIN
        UPDATE [dbo].[resources]
        SET [rId] = @rId,
            [name] = @name,
            [rType] = @rType

        WHERE [rId] = @rId;
    END
    ELSE IF @queryType = 3
    BEGIN
        -- Delete resource
        DELETE FROM [dbo].[resources]
        WHERE [rId] = @rId;
    END
    ELSE IF @queryType = 4
    BEGIN
        SELECT * FROM [dbo].[resources];
    END
    ELSE
    BEGIN
        RAISERROR('Invalid Query Type', 16, 1);
        RETURN;
    END
END;
