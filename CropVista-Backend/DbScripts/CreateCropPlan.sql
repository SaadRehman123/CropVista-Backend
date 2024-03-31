DROP PROCEDURE IF EXISTS [dbo].[CreateCropPlan];
GO

CREATE PROCEDURE [dbo].[CreateCropPlan]
    @queryType int,
    @id nvarchar(50) OUTPUT,
    @crop nvarchar(50),
    @season nvarchar(50),
    @acre int,
    @startdate date,
    @enddate date,
    @status nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

     IF @queryType = 1
    BEGIN
        DECLARE @NextId INT;

        SELECT @NextId = ISNULL(MAX(CAST(SUBSTRING(id, CHARINDEX('-', id) + 1, LEN(id) - CHARINDEX('-', id)) AS INT)), 0) + 1
        FROM plannedcrops
        WHERE id LIKE 'CP-%';

        IF @NextId <= 9999
        BEGIN
            SET @id = 'CP-' + RIGHT('000' + CAST(@NextId AS NVARCHAR(10)), 4);
        END
        ELSE
        BEGIN
            SET @id = 'CP-' + CAST(@NextId AS NVARCHAR(10));
        END

        INSERT INTO [dbo].[plannedcrops] ([id], [crop], [season], [acre], [startdate], [enddate], [status])
        VALUES (@id, @crop, @season, @acre, @startdate, @enddate, @status);
    END
    ELSE IF @queryType = 2
    BEGIN
        UPDATE [dbo].[plannedcrops]
        SET [crop] = @crop,
            [season] = @season,
            [acre] = @acre,
            [startdate] = @startdate,
            [enddate] = @enddate,
            [status] = @status
        WHERE [id] = @id;
    END
    ELSE IF @queryType = 3
    BEGIN
        -- Delete crop plan
        DELETE FROM [dbo].[plannedcrops]
        WHERE [id] = @id;
    END
    ELSE IF @queryType = 4
    BEGIN
        SELECT * FROM [dbo].[plannedcrops];
    END
    ELSE
    BEGIN
        RAISERROR('Invalid Query Type', 16, 1);
        RETURN;
    END
END
