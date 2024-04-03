CREATE PROCEDURE [dbo].[ProcedureGetProduct]
    @IncludeInactive BIT = 0,
    @SortOption VARCHAR(50) = 'Name',
    @SortDirection BIT = 0, -- Zmieniono typ parametru SortDirection na BIT
    @FilterByName NVARCHAR(100) = NULL,
    @FilterByGroupName NVARCHAR(100) = NULL,
    @FilterByGroupID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQLQuery NVARCHAR(MAX)
    DECLARE @params NVARCHAR(MAX)

    -- Inicjalizujemy zmienną @SQLQuery z podstawowym zapytaniem
    SET @SQLQuery = '
        SELECT P.ID, P.Name, P.Price, P.Image, PG.Name AS GroupName, P.GroupID, P.IsActive -- Dodano kolumnę IsActive
        FROM Products AS P
        LEFT JOIN ProductGroups AS PG ON P.GroupID = PG.ID
        WHERE (@IncludeInactive = 1 OR P.IsActive = 1)
            AND (@FilterByName IS NULL OR P.Name LIKE ''%'' + @FilterByName + ''%'')
            AND (@FilterByGroupName IS NULL OR PG.Name LIKE ''%'' + @FilterByGroupName + ''%'')
            AND (@FilterByGroupID IS NULL OR P.GroupID = @FilterByGroupID)
        ORDER BY '

    -- Dodajemy odpowiednią kolumnę do sortowania w zależności od parametru @SortOption
    IF @SortOption = 'Name'
        SET @SQLQuery = @SQLQuery + 'P.Name ' + CASE WHEN @SortDirection = 1 THEN 'ASC' ELSE 'DESC' END -- Dodajemy kierunek sortowania
    ELSE IF @SortOption = 'Price'
        SET @SQLQuery = @SQLQuery + 'P.Price ' + CASE WHEN @SortDirection = 1 THEN 'ASC' ELSE 'DESC' END -- Dodajemy kierunek sortowania
    ELSE IF @SortOption = 'GroupName'
        SET @SQLQuery = @SQLQuery + 'PG.Name ' + CASE WHEN @SortDirection = 1 THEN 'ASC' ELSE 'DESC' END -- Dodajemy kierunek sortowania

    SET @params = N'@IncludeInactive BIT, @FilterByName NVARCHAR(100), @FilterByGroupName NVARCHAR(100), @FilterByGroupID INT'

    -- Wykonujemy dynamiczne zapytanie SQL z przekazaniem parametrów
    EXEC sp_executesql @SQLQuery, @params, @IncludeInactive, @FilterByName, @FilterByGroupName, @FilterByGroupID
END;