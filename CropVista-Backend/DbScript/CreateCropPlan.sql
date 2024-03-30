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

    IF @queryType = 1 -- Insert
    BEGIN
        DECLARE @NextId INT;

        -- Find the next available ID
        SELECT @NextId = ISNULL(MAX(CAST(SUBSTRING(id, CHARINDEX('-', id) + 1, LEN(id) - CHARINDEX('-', id)) AS INT)), 0) + 1
        FROM plannedcrops
        WHERE id LIKE 'CP-%';

        SET @id = 'CP-' + RIGHT('000' + CAST(@NextId AS NVARCHAR(10)), 3);

        -- Insert crop plan
        INSERT INTO [dbo].[plannedcrops] ([id], [crop], [season], [acre], [startdate], [enddate], [status])
        VALUES (@id, @crop, @season, @acre, @startdate, @enddate, @status);
    END
    ELSE IF @queryType = 2 -- Update
    BEGIN
        -- Check if ID exists
        IF EXISTS (SELECT 1 FROM [dbo].[plannedcrops] WHERE [id] = @id)
        BEGIN
            -- Update crop plan
            UPDATE [dbo].[plannedcrops]
            SET [crop] = @crop,
                [season] = @season,
                [acre] = @acre,
                [startdate] = @startdate,
                [enddate] = @enddate,
                [status] = @status
            WHERE [id] = @id;
        END
        ELSE
        BEGIN
            -- Handle ID not found error
            RAISERROR('ID not found for update operation.', 16, 1);
            RETURN;
        END
    END
    ELSE IF @queryType = 3 -- Delete
    BEGIN
        -- Check if ID exists
        IF EXISTS (SELECT 1 FROM [dbo].[plannedcrops] WHERE [id] = @id)
        BEGIN
            -- Delete crop plan
            DELETE FROM [dbo].[plannedcrops]
            WHERE [id] = @id;
        END
        ELSE
        BEGIN
            -- Handle ID not found error
            RAISERROR('ID not found for delete operation.', 16, 1);
            RETURN;
        END
    END
    ELSE IF @queryType = 4 -- Read
    BEGIN
        -- Read crop plans
        SELECT * FROM [dbo].[plannedcrops];
    END
    ELSE

    IF @@ERROR <> 0
    BEGIN
        ROLLBACK TRANSACTION; --Rollback changes if error occurred
        -- Handle failure due to SQL error
    END
    ELSE
    BEGIN
        COMMIT TRANSACTION; -- Commit changes if no error occurred
        -- Handle success
    END
END
